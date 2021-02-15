import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import { DocumentService } from "./../../services/document.service";
import { DocumentPrintService } from "./../../services/documentPrint.service";
import { ParamsList, CardMode } from "./../../models/common.models";
import { DocTemplatesPresetValuesVm, DocTemplatePresetsVm, DocTemplateElementsVm, DocDataVm, DocTemplatesVm, DocumentVm } from './../../models/vm.models';
import { DocTemplatesPresetValuesPm, DocTemplatePresetsPm, DocDataPm, DocumentPm } from './../../models/pm.models';
import { IFrameService } from "./../../astum/services/frame/frame.service";
import './document.scss';


export class DocumentComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = DocumentController;
        this.template = require('./document.html');
        this.controllerAs = 'ctl';
    }
}

class DocumentController implements ng.IComponentController {
    item: any;

    static $inject = ['$element', 'DocumentService', 'DocumentPrintService', '$scope', '$window', '$stateParams', '$state', 'frameService'];
    constructor(
        private element: ng.IAugmentedJQuery,
        private service: DocumentService,
        private printService: DocumentPrintService,
        private scope: ng.IScope, private window: ng.IWindowService,
        private stateParams: StateParams,
        private state: StateService,
        private frameService: IFrameService) {
        this.isValid = true;
    }

    public origId: string;
    private templId: string;
    public templCode: string;
    private entityType: string;
    private isFrame: boolean;
    private system: string;
    private parentId: string;
    private _elements: DocDataVm[] | DocTemplateElementsVm[];

    public isValid: boolean;
    public isLoading: boolean;
    public templateObj: Array<any>;

    public usePreview: boolean = false;
    public document: DocumentVm | DocumentPm;

    public templates: DocTemplatesVm[];
    public isPreview: boolean = false;
    private onTabPress = (event) => {
        if (event.keyCode === 9) {
            event.preventDefault();
            event.stopPropagation();
            this.nextPath();
            this.scope.$broadcast('documentClick', this.currentEditPath);
        }
    };

    private nextPath() {
        if (!this.currentEditPath)
            this.currentEditPath = [];
        if (!this.currentEditPath.length) {
            this.currentEditPath.push(0);
            return;
        }
        this.currentEditPath = this.increasePath(this.templateObj, this.currentEditPath);
    }

    private increasePath(template: any, path: number[]): number[] {
        let localPath = [].concat(path);
        let elm = localPath.shift() || 0;
        if (template[elm] && template[elm].nested && template[elm].nested.length) {
            if (template[elm].nested[localPath[0] + 1] && template[elm].nested[localPath[0] + 1].panel.controlTypeCode !== "SECTOR") {
                localPath[0] = localPath[0] + 1;
                localPath.unshift(elm);
                return localPath;
            }
            localPath = this.increasePath(template[elm].nested, localPath);
            if (localPath.length) {
                localPath.unshift(elm);
                return localPath;
            }
        }
        if (!path.length && template[elm] && template[elm].panel.controlTypeCode !== "SECTOR") {
            localPath.push(elm);
            return localPath;
        }
        if (template[elm + 1] && template[elm + 1].panel && template[elm + 1].panel.controlTypeCode !== "SECTOR") {
            localPath.unshift(elm + 1);
            return localPath;
        }
        if (template[elm + 1] && template[elm + 1].panel.controlTypeCode === "SECTOR" && template[elm + 1].nested.length) {
            localPath = this.increasePath(template[elm + 1].nested, localPath);
            localPath.unshift(elm + 1);
            return localPath;
        }
        return localPath;
    }

    private onDocClick = () => {
        if (this.changedPath) {
            this.scope.$broadcast('documentClick', this.currentEditPath);
            this.changedPath = false;
            return;
        }
        this.endEditing();
    };

    private endEditing() {
        this.scope.$broadcast('documentClick', null);
        this.currentEditPath = [];
        this.changedPath = false;
        angular.element(document.getElementsByClassName('edit-wrap')).hide();
    }

    private changedPath: boolean;
    private currentEditPath: number[];



    private onElmModeChanged = (event: ng.IAngularEvent, path?: number[]) => {
        this.currentEditPath = [].concat(path);
        this.changedPath = true;
    };

    public showPreview() {
        this.save().then(() => {
            this.frameService.sendShowPreviewMessage();
            this.isPreview = !this.isPreview;
        });
    }

    public backPreview() {
        this.showPreview();
    }

    public goBack(){
        window.history.back();
    }

    public refreshPreview() {
        this.frameService.sendPreviewMessage();
    }

    //TODO: move to base
    public cardMode: CardMode;

    public loadTemplates() {
        if (!this.templates) {
            return this.service.getDocTemplatesList().then((data) => {
                this.templates = data;
                return this.templates;
            });
        }
    }

    public templateSelected() {
        this.getTemplateData(this.templId);
    }

    $onInit() {
        this.isFrame = this.stateParams['isFrame'];
        this.origId = this.stateParams['id'];
        this.templId = this.stateParams['templId'];
        this.templCode = this.stateParams['templCode'];
        this.system = this.stateParams['system'] || '';
        this.parentId = this.stateParams['parentId'];
        this.entityType = this.stateParams['entityType'];
        this.usePreview = this.stateParams['usePreview'];
        this.initialize();
        if (this.frameService.isEmbed)
            this.window.onscroll = (e) => {
                if (this.window.scrollY > 0) {
                    this.element.find('.doc-menu').addClass('fixed-header');
                    this.element.find('.document').addClass('margin');
                }
                else {
                    this.element.find('.doc-menu').removeClass('fixed-header');
                    this.element.find('.document').removeClass('margin');
                }
            };
    }

    private saveHandler: any;
    private changeElmHendler: any;
    private checkValidityHandler: any;

    $postLink() {
        document.getElementsByClassName("document")[0].addEventListener('click', this.onDocClick);
        document.addEventListener('keyup', this.onTabPress);
        this.changeElmHendler = this.scope.$on("elmModeChanged", this.onElmModeChanged);
        this.saveHandler = this.scope.$on("documentSave", () => {
            this.save();
        });
        if (this.frameService.isEmbed)
            this.checkValidityHandler = this.scope.$on("validate", ($event, isSilent) => {
                this.checkValidity($event, isSilent);
            });
    }

    $onDestroy() {
        document.removeEventListener('keyup', this.onTabPress);
        document.removeEventListener('click', this.onDocClick);
        if (this.frameService.isEmbed)
            this.checkValidityHandler();
        this.changeElmHendler();
        this.saveHandler();
    }

    private checkValidity = (event: ng.IAngularEvent, silent: boolean) => {
        if (this.entityType !== 'ExtRequest' && this.entityType !== 'Request') {
            this.validate(silent);
            this.frameService.sendValidationMessage(this.isValid);
            return;
        }
        //if (this.cardMode === CardMode.Create) {
        let savePromise = this.save();
        if (!savePromise) {
            this.validate(silent);
            this.frameService.sendValidationMessage(this.isValid);
            return;
        }
        savePromise.then((data) => {
            this.frameService.sendValidationMessage(true);
        });
        //}
        //this.frameService.sendValidationMessage(true);
    }

    private initialize() {
        if (!this.isFrame && !this.origId) {
            this.cardMode = CardMode.Create;
            return;
        }
        if (this.origId && !this.isFrame) {
            this.getDocument(this.origId).then((data) => {
                if (data) {
                    this.cardMode = CardMode.Edit;
                }
            });
            return;
        }
        if (this.origId && this.isFrame) {
            this.getDocument(this.origId, true, this.entityType).then((data) => {
                if (data) {
                    this.cardMode = CardMode.Edit;
                    return;
                }
                if (!data) {
                    this.cardMode = CardMode.Create;
                    if (this.templCode)
                        this.getTemplateData(null, this.templCode);
                    return;
                }
            }, (error) => {
            });
            return;
        }
    }

    private getDocument(id: string, isOrigin: boolean = false, entityTypeCode: string = null) {
        this.isLoading = true;
        return this.service.getDocument(id, isOrigin, entityTypeCode).then((data) => {
            if (data) {
                data.documentDataList = data.documentDataList.map(dd => {
                    if (dd.controlTypeCode === "BIT" && dd.value)
                        dd.value = dd.value === "true";
                    return dd;
                });
                this.document = data;
                this._elements = data.documentDataList;
                this.templateObj = this._prepareTemplate<DocDataVm>(<DocDataVm[]>this._elements);
                this.templId = data.templateId;
            }
            this.isLoading = false;
            return data;
        }, (error) => {
            this.isLoading = false;
        });
    }

    private getTemplateData(id?: string, code?: string) {
        let params = new ParamsList;
        params['templateId'] = id;
        params['TemplateCode'] = code;
        params["recordState"] = 2;
        this.isLoading = true;
        return this.service.getDocTemplateElementsList(params).then((data) => {
            this._elements = data;
            this.templId = data[0].templateId;
            this.templateObj = this._prepareTemplate<DocTemplateElementsVm>(<DocTemplateElementsVm[]>this._elements);
            this.isLoading = false;
        },
            (error) => {
                console.log(error);
                this.isLoading = false;
            });
    }

    //private getDocData(id: number) {
    //    let params = new ParamsList;
    //    params["originDbRecordId"] = id;
    //    return this.service.getDocDataList(params).then((data) => {
    //        this._elements = data;
    //        if (data.length) {
    //            this.templId = data[0].templateId;

    //        }
    //        debugger;
    //        this.templateObj = this._prepareTemplate<DocDataVm>(<DocDataVm[]>this._elements);
    //        return data;
    //    },
    //        (error) => {
    //        });
    //}

    private _prepareTemplate<T>(elements: T[], parent?: number) {
        let result = [];
        if (elements.length) {
            elements.forEach((curr: any, index, arr) => {
                if ((!parent && curr.parentId === null) || curr.parentId === parent) {
                    let obj = {};
                    obj['panel'] = curr;
                    if (curr.controlTypeCode.trim() === 'SECTOR')
                        obj['nested'] = this._prepareTemplate(arr, (this.cardMode && this.cardMode === CardMode.Create) ? curr.id : curr.templateElementId);
                    result.push(obj);
                };
            }, {});
            return result.sort((a: any, b: any) => {
                if (a.panel.orderNumber < b.panel.orderNumber)
                    return -1;
                if (a.panel.orderNumber > b.panel.orderNumber)
                    return 1;
                return 0;
            });
        }
        return result;
    }
    private _saving = false;
    public save = () => {
        this.endEditing();
        this.isValid = true;
        if (this._elements) {
            this._saving = true;
            this.validate();
            delete this._saving;
            let dataList = (<DocDataVm[]>this._elements).map((item, index) => {
                let postModel = new DocDataPm;
                if (this.cardMode === CardMode.Create) {
                    postModel.originDbRecordId = this.origId;
                    postModel.templateElementId = item.id;
                }
                if (this.cardMode === CardMode.Edit) {
                    postModel.id = item.id;
                    postModel.templateElementId = item.templateElementId;
                    postModel.originDbRecordId = item.originDbRecordId;
                }
                postModel.valuesTreeId = item.valuesTreeId;
                postModel.templateId = this.templId;
                postModel.ownerId = item.ownerId;
                postModel.code = item.code;
                postModel.value = item.value;
                postModel.controlTypeCode = item.controlTypeCode;
                postModel.elementTypeCode = item.elementTypeCode;
                postModel.orderNumber = item.orderNumber;
                postModel.parentId = item.parentId;
                postModel.caption = item.caption;
                postModel.description = item.description;
                return postModel;
            });

            if (!this.isValid) {
                return;
            }
            if (this.cardMode === CardMode.Create) {
                if (!this.document)
                    this.document = new DocumentPm();
                if (this.isFrame) {
                    (<DocumentPm>this.document).originDbRecordId = this.origId;
                    this.document.caption = this.entityType + this.origId;
                }
                this.document.classShortCode = 'D';
                this.document.recordState = 2;
                this.document.templateId = this.templId;
                this.document.documentDataList = dataList;
                this.document.parentId = this.parentId;
                this.document.entityTypeCode = this.entityType;
                this.isLoading = true;
                return this.service.documentInsert(<DocumentPm>this.document).then((data: DocumentVm) => {
                    this.document = data;
                    this.cardMode = CardMode.Edit;
                    this.origId = this.isFrame ? data.originDbRecordId : data.id;
                    this.initialize();
                    if (this.usePreview)
                        this.refreshPreview();
                    //this.state.go('.', { id: this.origId }, { notify: false });
                }).finally(() => {
                    this.isLoading = false;
                });
            }
            if (this.cardMode === CardMode.Edit) {
                this.isLoading = true;
                this.document.documentDataList = dataList;
                return this.service.documentUpdate(<DocumentPm>this.document)
                    .then((data) => {
                        this.cardMode = CardMode.Edit;
                        if (this.usePreview)
                            this.refreshPreview();
                    })
                    .catch((error) => {
                        this.frameService.sendError();
                        throw (error);
                    })
                    .finally(() => {
                        this.isLoading = false;
                    });
            }
        }
    };

    private validate(silent: boolean = false): void | boolean {
        let isValid = true;
        (<DocDataVm[]>this._elements).map((item, index) => {
            if (item.config && item.config["required"] && !item.value) {
                isValid = false;
                this.setElmVlidity(this.templateObj, item, { required: true, caused: item.caption });
            }
            if (item.config && item.config["requiredIf"]) {
                let elm = this.pathToElement(this.templateObj, item.config["requiredIf"]);
                if (!!elm.panel.value && !item.value) {
                    isValid = false;
                    this.setElmVlidity(this.templateObj, item, { requiredIf: true, caused: elm });
                }
            }
            if (item.controlTypeCode === "NUMBER" && item.value) {
                var match = item.value.toString().match(/^[0-9]+([\.][0-9]+)?$/g);
                if (!match) {
                    isValid = false;
                    this.setElmVlidity(this.templateObj, item, { pattern: true });
                }
            }
            return item;
        });
        if (!silent) {
            this.isValid = isValid;
            if (!this._saving)
                this.scope.$apply();
        }
        if (!this.isValid && this.isFrame) this.frameService.sendValidationError('Заповніть обов`язкові поля!');
        return isValid;
    }

    private pathToElement = (templateObj: Array<any>, path: number[]) => {
        let _path = [].concat(path);
        let index = _path.shift();
        if (templateObj[index].nested && templateObj[index].nested.length && path.length) {
            return this.pathToElement(templateObj[index].nested, _path);
        }
        return templateObj[index];
    };

    private setElmVlidity(templateObj: Array<any>, item, error: any) {
        let templateItem = this.findTemplateItem(templateObj, item);
        if (!templateItem.errors)
            templateItem.errors = [];

        if (!this.containError(templateItem.errors, error))

            templateItem.errors.unshift(error);
    }


    private containError(arr: Array<any>, error: any) {
        return arr.some((item) => {
            // debugger;
            return angular.equals(item, error);
        });
    }

    private findTemplateItem(templateObj: Array<any>, item): any {
        let arr = templateObj.map((titem) => {
            if (this.equality(titem.panel, item)) {
                return titem;
            }
            if (titem.nested && titem.nested.length) {
                return this.findTemplateItem(titem.nested, item);
            }
        });
        return arr.filter((t) => !!t)[0];
    }
    private equality(item1, item2) {
        let keys = ["code", "controlTypeCode", "caption", "orderNumber"];
        return keys.every((key) => {
            return item1[key] === item2[key];
        });
    };

    public print() {
        if (this.cardMode === CardMode.Create) {
            alert('Необхідно зберегти  документ');
            return;
        }
        this.printService.printDocDataPdf(this.isFrame ? this.document.id : this.origId, this.entityType, this.system);
    }

    public getWord() {
        this.printService.printDocx(this.isFrame ? this.document.id : this.origId);
    }

    //presets
    public presets: DocTemplatePresetsVm[];

    public isNewPreset: boolean = false;

    public preset: DocTemplatePresetsVm;

    private elementsBackUp: DocTemplateElementsVm[];

    public hasElementsBackUp: boolean;

    public loadTemplatePresets() {
        let params = new ParamsList;
        params['templateId'] = this.templId;
        return this.service.getDocTemplatePresetsList(params).then((data) => {
            this.presets = data;
            return this.presets;
        });
    }

    public changePresets(newVal, oldVal, item) {
        this.preset = item;
    }

    public addPreset() {
        this.isNewPreset = true;
    }

    public cancelInsertPreset() {
        this.isNewPreset = false;
    }

    public insertPreset(name: string) {
        debugger;
        let field = (this.cardMode === CardMode.Create) ? "id" : "templateElementId";
        let preset = new DocTemplatePresetsPm;
        preset.caption = name;
        preset.templateId = this.templId;
        preset.recordState= 2;

        preset.presetValue = (<DocDataVm[]>this._elements).map((item: DocDataVm) => {
            let pm = new DocTemplatesPresetValuesPm;
            if (item.controlTypeCode.trim() !== 'SECTOR')
                pm.value = item.value;
            pm.templateElementId = item[field];
            return pm;
        });

        return this.service.docTemplatePresetInsert(preset).then((data) => {
            this.loadTemplatePresets();
            this.isNewPreset = false;
        },
            error => {
                debugger;
            });
    }

    public updatePreset() {
        if (!this.preset) {
            return;
        }
        let preset = new DocTemplatePresetsPm;
        preset.id = this.preset.id;
        preset.caption = this.preset.caption;
        preset.templateId = this.preset.templateId;
        preset.recordState = 2;
        let field = (this.cardMode === CardMode.Create) ? "id" : "templateElementId";
        preset.presetValue = this.preset.presetValue.map((value: DocTemplatesPresetValuesVm) => {
            (<DocDataVm[]>this._elements).forEach((item: DocDataVm) => {
                if (value.templateElementId === item[field])
                    value.value = item.value;
            });
            return value;
        });
        return this.service.docTemplatePresetUpdate(preset).then((data) => {
        },
            error => {
                debugger;
            });
    }

    public updateElementsData() {
        this._updateElementsData();
    }

    private _updateElementsData() {
        if (this.preset) {
            this._applyPreset(this.preset);
            return;
        }
    }

    private _applyPreset(preset: DocTemplatePresetsVm) {
        debugger;
        let values = preset.presetValue;
        let field = (this.cardMode === CardMode.Create) ? "id" : "templateElementId";
        (<DocDataVm[]>this._elements).forEach((item: DocDataVm, index: number, arr: DocDataVm[]) => {
            values.forEach((value) => {
                if (value.templateElementId === item[field])
                    item.value = value.value;
            });
        });
        //this.scope.$broadcast("configClosed", {});
    }

    private _backUpElements() {
        this.elementsBackUp = [];
        angular.copy(this._elements, this.elementsBackUp);
        this.hasElementsBackUp = true;
    }

    private _clearBackUp() {
        delete this.elementsBackUp;
        this.hasElementsBackUp = false;
    }
}
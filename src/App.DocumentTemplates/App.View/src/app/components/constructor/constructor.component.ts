import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import { ParamsList, CardMode } from "./../../models/common.models";
import { DocTemplatesPm, DocTemplateElementsPm, DocTemplateElementValuesTreePm } from "./../../models/pm.models";
import { DocTemplatesVm, DocTemplateElementsVm, DocControlTypesVm, DocElementsVm, DocTemplateElementValuesTreeVm } from "./../../models/vm.models";
import { DocumentService } from './../../services/document.service';
import { ConstructorService } from "./../../services/constructor.service";
import { DragManagerService } from "./../../astum/services/dragManager/dragmanager.service";
import '../../scss/blocks/constructor.style.scss';
import { IModalService, IModalOptions } from "../../astum/modal/astum.modal.service";

export class ConstructorComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = ConstructorController;
        this.template = require('./constructor.html');
        this.controllerAs = 'ctl';
    }
}

export class ConstructorController implements ng.IComponentController {

    static $inject = ['$scope', '$window', '$state', '$stateParams', 'DocumentService', 'ConstructorService', 'dragManager', '$element', 'modalService', '$timeout'];
    constructor(private scope: ng.IScope,
        private _window: ng.IWindowService,
        private state: StateService,
        private stateParams: StateParams,
        private documentService: DocumentService,
        private constructorService: ConstructorService,
        private dragManager: DragManagerService,
        private element: ng.IAugmentedJQuery,
        private modal: IModalService,
        private timeout: ng.ITimeoutService) {

        this.onDropListener = scope.$on('onDrop', this.onDropHandler);


    }

    public gotodoc() {
        this.state.go('app.document', { templCode: 'Cardio' });
    }
    private onDropListener: () => void;
    private columnNumber: number;
    private rowNumber: number;
    private elementForm: ng.IFormController;
    public removeDialog: boolean = false;
    private templateShadow: boolean = false;
    // private originalData:any;
    public parentId: string;
    public templateForm: any;
    //TODO: move to base
    public cardMode: CardMode;
    // public setDragWidth:boolean = false;

    public template: DocTemplatesVm | DocTemplatesPm;


    $onInit() {
        this.rowNumber = 8;
        this.columnNumber = 4;
        this.parentId = this.stateParams['parentId'];
        if (!this.stateParams['id'] && !this.stateParams['copyId']) {
            this.cardMode = CardMode.Create;
            this.template = new DocTemplatesPm();
            this.template.recordState = 2;
            this.template.templateElements = [];
            this.template.parentId = this.parentId;
            this.template.classShortCode = 'T';
        }
        if (this.stateParams['id']) {
            this.constructorService.DocTemplatesCard(this.stateParams['id']).then(data => {
                this.template = data;
                this.parentId = this.template.parentId;
                this.cardMode = CardMode.View;
            })
        }
        if (this.stateParams['copyId']) {
            this.constructorService.DocTemplatesCard(this.stateParams['copyId']).then(data => {
                this.template = this.prepareCopy(data);
                this.cardMode = CardMode.Create;
            })
        }
        this.constructorService.getControlTypesList().then((data) => {
            this.controlTypes = data;
        });
        this.getValuesTreeList();


        //this.dragManager.init(this.element[0]);

    }
    $onDestroy() {
        this.onDropListener();
        //this.dragManager.release();
        // debugger;
    }
    // uiCanExit = (trans) => {
    //     if(this.cardMode != CardMode.View)
    //         return confirm("Елементи було змінено без збереження. Залишити сторінку?");
    // };
    private prepareCopy(templ: DocTemplatesVm) {
        delete templ.id;
        templ.caption += " Copy";
        templ.code += " Copy";
        templ.description += " Copy";
        templ.templateElements = this.removeIds(templ.templateElements);
        return templ;
    }

    private removeIds(elements: DocTemplateElementsVm[]) {
        return elements.map(elm => {
            delete elm.id;
            if (elm.templateElements.length > 0)
                elm.templateElements = this.removeIds(elm.templateElements);
            return elm;
        })
    }


    private setDocProps(prop) {

        this.currElement.config.showName = prop;

        console.log(this.currElement.config)
    }

    private formContoll: HTMLFormElement;

    private controlFormOn() {
        this.fixBody();
        this.timeout(this.setFormPosition);
        this._window.addEventListener('resize', this.setFormPosition);
    };
    private controlFormOff = () => {
        this.unFixBody();
        this.formContoll = null;
        this._window.removeEventListener('resize', this.setFormPosition);

    };
    private setFormPosition = () => {
        let windowHeight = document.documentElement.clientHeight - 40;
        if (!this.formContoll)
            this.formContoll = (<HTMLFormElement>document.querySelector('form.controls-aside'));
        (<HTMLFormElement>this.formContoll).style.maxHeight = windowHeight + 'px';
    };

    public fixBody() {
        this.templateShadow = true;
        let docBody = document.body;
        let top = (-1 * window.pageYOffset) + 'px';
        let left = docBody.getBoundingClientRect().left;
        let scrollLineWidth = window.innerWidth - docBody.clientWidth;
        docBody.classList.add('fixed');
        docBody.style.top = top;
        docBody.style.left = left + 'px';
        docBody.style.paddingRight = scrollLineWidth + 'px';
        docBody.setAttribute('data-top', top);


    }

    public unFixBody() {
        this.templateShadow = false;
        let docBody = document.body;
        docBody.classList.remove('fixed');
        window.scrollTo(0, (-1 * parseInt(docBody.getAttribute('data-top'))));
        docBody.style.top = '';
        docBody.style.left = '';
        docBody.style.paddingRight = '';
        docBody.removeAttribute('data-top');
    }

    private onDropHandler = (event: ng.IAngularEvent, data: any) => {
        let isFirst: boolean = false;
        let dropZoneItem: DocTemplateElementsPm | any;
        if (data.dropZoneData.elCtl)
            dropZoneItem = data.dropZoneData.elCtl.item;
        if (data.dropZoneData.template) {
            dropZoneItem = data.dropZoneData.template.elements[0];
            while (!dropZoneItem) {
                data.dropZoneData = data.dropZoneData.$parent;
                if (data.dropZoneData.elCtl) {
                    dropZoneItem = <DocTemplateElementsVm>data.dropZoneData.elCtl.item;
                    dropZoneItem.templateElements.push(new DocTemplateElementsPm());
                }
                //dropZoneItem = <DocTemplateElementsPm>{ parentId: data.dropZoneData.elCtl.item.id, id: 0 };
            }
            isFirst = true;
        }

        let dragItem: DocTemplateElementsPm = data.avatarData.elCtl.item;

        //if (dropZoneItem.controlTypeCode == "SECTOR")
        //    dragItem.parentId = dropZoneItem.id;
        //else
        //    dragItem.parentId = dropZoneItem.parentId;

        this.templateBackUp = new DocTemplatesPm();
        angular.copy(this.template, this.templateBackUp);
        if (!dropZoneItem || !dragItem) {
            return;
        }

        let itemPath, zonePath;
        try {
            // debugger;
            itemPath = this.findPath((<DocTemplateElementsVm[]>this.template.templateElements), dragItem);
            this.removeNode((<DocTemplateElementsVm[]>this.template.templateElements), itemPath);
            zonePath = this.findPath((<DocTemplateElementsVm[]>this.template.templateElements), dropZoneItem);
            this.insertNode(<DocTemplateElementsVm[]>this.template.templateElements, zonePath, dragItem, isFirst);
            // debugger;
        }
        catch (ex) {
            console.error(ex);
            angular.copy(this.templateBackUp, this.template);
            return;
        }
        this.cardMode = CardMode.Edit;
        this.scope.$apply();
    };
    private findPath(items: DocTemplateElementsVm[], item: DocTemplateElementsPm, path?: Array<number>) {
        path = path || new Array<number>();
        const equality = (item1, item2) => {
            let keys = ["code", "controlTypeCode", "caption", "orderNumber"];
            return keys.every((key) => {
                return item1[key] === item2[key];
            });
        };
        for (let i = 0; i < items.length; i++) {
            if (item.id && items[i].id === item.id) {
                path.push(i);
                break;
            }
            if (equality(items[i], item)) {
                path.push(i);
                break;
            }
            if (!item.id && item.parentId && items[i].id === item.parentId) {
                path.push(i);
                if (items[i].templateElements && !items[i].templateElements.length)
                    path.push(0);
            }
            if (items[i].templateElements && items[i].templateElements.length) {
                let _path = this.findPath(items[i].templateElements, item, path.concat(i))
                if (_path.length > path.length + 1)
                    path = _path;
            }
        }
        return path;
    }

    private insertNode(elements: DocTemplateElementsVm[], zonePath: Array<number>, elm: any, insertBefore: boolean) {

        while (zonePath.length > 1) {
            elements = elements[zonePath.shift()].templateElements;
        }
        let position = insertBefore ? zonePath.shift() : zonePath.shift() + 1;

        if (elements[position] && angular.equals(elements[position].templateElements[0], {})) {
            elements[position].templateElements.splice(0, 1, elm);
            this.reorder(elements[0].templateElements);
            return;
        }


        elements.splice(position, 0, elm);
        this.reorder(elements);
        //elements[position].templateElements = [].concat(elm.templateElements);
    }

    private removeNode(elements: DocTemplateElementsVm[], elmPath: Array<number>) {
        while (elmPath.length > 1) {
            elements = elements[elmPath.shift()].templateElements;
        }
        elements.splice(elmPath.shift(), 1);
        this.reorder(elements);
    }

    private reorder(arr: DocTemplateElementsVm[]) {
        arr.map((item, index) => {
            return item.orderNumber = index + 1;
        });
    }


    public onElementsChanged(event) {
        // console.info("template onElementsChanged event");
        //this.template.templateElements = event.elements;
    }

    public canTemplatePost: boolean = true;
    //TODO:move to templateCardComponent
    public templatePost() {
        this.canTemplatePost = false;
        let postPromise;
        if (this.cardMode === CardMode.Create)
            postPromise = this.constructorService.DocTemplateInsert(<DocTemplatesPm>this.template).then(data => {
                this.template = data;
                this.cardMode = CardMode.View;
                this.canTemplatePost = true;
                this.getValuesTreeList(true);
                this.state.go(".", { id: data.id }, { inherit: false, location: 'replace' });
            })
        if (this.cardMode === CardMode.Edit) {
            this._numerator = 0;
            this.reorderTemplate((<DocTemplatesPm>this.template).templateElements);
            postPromise = this.constructorService.DocTemplateUpdate(<DocTemplatesPm>this.template).then(data => {
                this.template = data;
                this.cardMode = CardMode.View;
                this.canTemplatePost = true;
                this.getValuesTreeList(true);
            });
        }
        postPromise.catch(err => {
            if(err.ValidationException)
                alert(err.Message);
            })
            .finally(() => {
                this.canTemplatePost = true;
            });
    }

    private _numerator: number;
    public reorderTemplate(elements: DocTemplateElementsPm[]){
        for(let i = 0; i < elements.length; i++)
        {
            this._numerator++;
            elements[i].orderNumber = this._numerator;

            if (elements[i].templateElements && elements[i].templateElements.length > 0)
            {
                this.reorderTemplate(elements[i].templateElements);
            }
        }
    }

    public elementToPath = (items: DocTemplateElementsVm[], item: DocTemplateElementsPm) => {
        return this.findPath(items, item);
    };

    public pathToElement = (items: DocTemplateElementsVm[], path: number[]) => {


        let _path = [].concat(path);
        let index = _path.shift()
        if (items[index].templateElements.length && path.length) {
            return this.pathToElement(items[index].templateElements, _path);
        }
        return items[index];
    };


    private templateBackUp: DocTemplatesPm;
    public editTemplate() {
        if (this.cardMode === CardMode.View) {
            this.templateBackUp = new DocTemplatesPm();
            angular.copy(this.template, this.templateBackUp);
            this.cardMode = CardMode.Edit;
        }
    }

    public cancelTemplateEdit() {
        angular.copy(this.templateBackUp, this.template);
        delete this.templateBackUp;
        this.clearElementData();
        this.cardMode = CardMode.View;
    }
    //end TODO

    public currElement: DocTemplateElementsVm | DocTemplateElementsPm;

    public controlTypes: DocControlTypesVm[];
    public getControlTypesList() {
        if (!this.controlTypes)
            return this.constructorService.getControlTypesList().then((data) => {
                this.controlTypes = data;
                return this.controlTypes;
            });
    }

    public valuesTreeList: DocTemplateElementValuesTreeVm[];

    public getValuesTreeList(force?: boolean) {
        if (!this.valuesTreeList || force)
            return this.constructorService.ValuesTreeList().then((data) => {
                this.valuesTreeList = data;
                this.valuesTreeList.push(<DocTemplateElementValuesTreeVm>{ id: '', caption: "New", code: "New" });
                return this.valuesTreeList;
            });
    }

    //public elementsList: DocElementsVm[];
    //public getElementsList(load?: boolean) {
    //    debugger;
    //    if (load)
    //        return this.constructorService.getDocElements(this.currElement.controlTypeCode).then(data => {
    //            this.elementsList = data;
    //            return this.elementsList;
    //        });
    //}

    public addDocElement() {
        this.state.go('app.customelement.add', { controlCode: this.currElement.controlTypeCode });
    }


    private updateCallback: (DocTemplateElementsPm) => void;
    private insertCallback: (DocTemplateElementsPm) => void;
    private cancelCallback: (x?: boolean, _?: boolean) => void;

    public get isInsertMode() {
        return !!this.insertCallback;
    }


    public editElement(elm: DocTemplateElementsPm, onSaveCallback: (DocTemplateElementsPm) => void, cancellCallback: (x?: boolean, _?: boolean) => void, isCreating?: boolean) {
        if (this.insertCallback && isCreating)
            return;
        if (this.currElement)
            this.cancelCallback(isCreating, cancellCallback === this.cancelCallback);

        this.clearElementData();
        this.editTemplate();
        this.currElement = elm;
        if (this.elementForm)
            this.elementForm.$setPristine();
        this.cancelCallback = cancellCallback;
        isCreating ? this.insertCallback = onSaveCallback : this.updateCallback = onSaveCallback;
        this.controlFormOn();
    }
    public saveElement() {
        if (this.elementForm.$invalid)
            return;
        if (this.insertCallback)
            this.insertCallback(this.currElement);


        if (this.updateCallback)
            this.updateCallback(this.currElement);
        this.clearElementData();
        this.controlFormOff();
    }

    public cancelElmement() {
        if (this.cancelCallback) {
            this.cancelCallback();
            this.clearElementData();
        }
        this.controlFormOff();
    }

    private clearElementData() {
        delete this.insertCallback;
        delete this.updateCallback;
        delete this.cancelCallback;
        delete this.currElement;
    }

    public insertTemplateElement(elm: DocTemplateElementsPm) {
        this.constructorService.DocTemplateElementInsert(elm).then(data => {
        });
    }

    private onValuesTreeModalClose = (data: any) => {
        this.valuesTreeList.push(data);
        this.currElement.valuesTree = data;
    };

    public setElementValuesTree() {
        if (!this.currElement.valuesTreeId && (this.currElement.controlTypeCode === 'LEXTREE' || this.currElement.controlTypeCode === 'TEXT' || this.currElement.controlTypeCode === 'CHECKLIST'))
            this.currElement.valuesTreeId = '';
    }

    public copyElementDialog(elm: DocTemplateElementsPm) {
        this.elementToCopy = elm;
        let options = <IModalOptions>{
            id: 'dialogModal',
            attributes: {
                message: "Копіювати зі значеннями?"
            },
            onCloseCb: this.onDialogClose,
            innerCb: this.onDialogAnswer
        };
        this.modal.open(options);
    }

    public copyElementToTemplate(elm: DocTemplateElementsPm) {
        this.elementToCopy = elm;
        this.documentService.getDocTemplatesList().then((data) => {
            let options = <IModalOptions>{
                id: 'copyToTemplateModal',
                attributes: {
                    templateList: data
                },
                onCloseCb: this.onDialogClose,
                innerCb: this.onCopyToTemplate
            };
            this.modal.open(options);
        });

    }
    private elementToCopy: DocTemplateElementsPm;


    public onDialogClose = () => {
        delete this.elementToCopy;
    };

    public onDialogAnswer = (answer: boolean) => {
        this.constructorService.DocTemplateElementCopy(this.elementToCopy, answer).then((data: DocTemplateElementsVm) => {
            this.getValuesTreeList();
            if (data.parentId) {
                (<DocTemplateElementsVm[]>this.template.templateElements).forEach((item: DocTemplateElementsVm) => {
                    if (item.id === data.parentId)
                        item.templateElements.push(data);
                });
                return;
            }
            (<DocTemplateElementsVm[]>this.template.templateElements).push(data);
        });
        delete this.elementToCopy;
    };

    public onCopyToTemplate = (data: any) => {
        this.elementToCopy.ownerId = null;
        this.constructorService.DocTemplateElementCopyToTemplate(this.elementToCopy, data.templateId, data.withValues).then((data) => {
            if (data.success)
                console.log("copy success");
            else
                console.error(data.text);
        });
        delete this.elementToCopy;
    };
    public copyValuesTree() {
        let orig = this.valuesTreeList.filter((tree) => {
            return tree.id === this.currElement.valuesTreeId;
        })[0];
        let model = new DocTemplateElementValuesTreePm;
        if (orig) {
            angular.copy(orig, model);
            model.caption += "_copy";
            model.code += "_copy";
            this.constructorService.ValuesTreeCopy(model).then((data) => {
                if (data) {
                    this.valuesTreeList.push(data);
                    this.currElement.valuesTreeId = data.id;
                }
            })
        }
    }



    public editValuesTree() {
        let options = <IModalOptions>{
            id: 'valuesModal',
            attributes: {
                valuesTreeId: this.currElement.valuesTreeId,
                attributeTypeCode: this.currElement.controlTypeCode
            },
            onCloseCb: this.onValuesEditClose,
            innerCb: this.onValuesEdited
        };
        this.modal.open(options);

    }

    public onValuesEditClose = () => {

    };

    public onValuesEdited = () => {

    }


}


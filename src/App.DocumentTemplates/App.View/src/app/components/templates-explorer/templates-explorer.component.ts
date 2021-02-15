import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import { ParamsList, CardMode, Result } from "./../../models/common.models";
import { DocTemplatesVm } from "./../../models/vm.models";
import { DocumentService } from "./../../services/document.service";
import "./templates.scss";
import { BaseExplorerController } from "../../common/explorer/baseExplorer.controller";
import { ConstructorService } from "../../services/constructor.service";
import { DocTemplatesPm, DocTemplateElementsPm } from "../../models/pm.models";
import { IFrameService } from "./../../astum/services/frame/frame.service";
import { IModalOptions, IModalService } from "../../astum/modal/astum.modal.service";

export class TemplatesExplorerComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = TemplatesExplorerController;
        this.template = require('./templates-explorer.html');
        this.controllerAs = 'explorer';
    }
}


export class TemplatesExplorerController extends BaseExplorerController<DocTemplatesVm> {

    public items: DocTemplatesVm[];
    public isEmbed: boolean;
    public itemNum: number;

    static $inject = ['$state', '$stateParams', 'DocumentService', 'ConstructorService', 'frameService', 'modalService'];
    constructor(protected state: StateService,
        protected stateParams: StateParams,
        private service: DocumentService,
        private constructorService: ConstructorService,
        private _frameService: IFrameService,
        private modal: IModalService) {
        super(state, stateParams, 'app.constructor');
        this.isEmbed = this._frameService.isEmbed;
    }

    $onInit() {
        super.$onInit();
    }

    $onDestroy() {

    }

    protected getDataImpl(parentId?: string, searchText?: string): ng.IPromise<DocTemplatesVm[]> {
        let params = new ParamsList;
        if (!parentId && this.stateParams["rootName"]) {
            params['RootName'] = this.stateParams["rootName"];
        } else { 
            params['parentId'] = parentId;
        }
        if (searchText) {
            params['SearchText'] = searchText;
        }
        return this.service.getDocTemplates(params);
    }

    protected getParentsHierarchy(parentId?: string): ng.IPromise<DocTemplatesVm[]> {
        var rootName = this.stateParams["rootName"] || null;
        return this.service.getDocTemplateParents(parentId,rootName);
    }

    protected submitFolderImpl(folder: DocTemplatesPm): ng.IPromise<DocTemplatesVm> {
        return this.constructorService.DocTemplateInsert(folder);
    }

    public removeItemImpl(id: string) {
        return this.constructorService.DocTemplateRemove(id);
    }

    public onItemClick(item: DocTemplatesVm, index?: number) {
        
        this.itemNum = index;

        if (this.isEmbed && item) {
            if (item.classShortCode !== "F"){
                this._frameService.sendTemplateMessage(item.code, item.caption);
            } else {
                this._frameService.sendTemplateMessage(null, null);
                super.onItemClick(item);
            }
            return;
        }
        if (this.isEmbed && !item) {
            this.state.go('.', { 'rootName': this.stateParams["rootName"],'parentId':null})
            return;
        }
        else {
            super.onItemClick(item);
        }
    }

    public copy(id: string, event) {
        this.state.go('app.constructor', { copyId: id });
    }

    private elementToMove: DocTemplatesPm;

    public moveTemplateDialog(elm: DocTemplatesPm, event: Event) {
        event.stopPropagation();
        this.elementToMove = elm;
        let options = <IModalOptions>{
            id: 'template-move-dialog',
            attributes: {
                element: this.elementToMove
            },
            onCloseCb: this.onDialogClose,
            innerCb: this.onDialogAnswer
        };
        this.modal.open(options);
    }

    public onDialogClose = () => {
        delete this.elementToMove;
    }

    public onDialogAnswer = (folder) => {
        this.elementToMove.parentId = folder ? folder.id : null;
        this.constructorService.DocTemplateMove(this.elementToMove.id, folder ? folder.id : null).then((data) => {
            if (data.success)
                this.$onInit();
            else
                console.log(data.text);
        }, (error) => {
            console.error(error);
        });
    }

}
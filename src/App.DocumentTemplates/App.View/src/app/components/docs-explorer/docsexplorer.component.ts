import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import { ParamsList, CardMode, Result, IFolderEntity } from "./../../models/common.models";
import { DocumentService } from "./../../services/document.service";
import { BaseExplorerController } from "./../../common/explorer/baseExplorer.controller";
import { DocumentVm } from "./../../models/vm.models";
import { DocTemplatesPm, DocumentPm } from "../../models/pm.models";
import { IModalOptions, IModalService } from "../../astum/modal/astum.modal.service";


export class DocsExplorerComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = DocsExplorerController;
        this.template = require('./docsexplorer.html');
        this.controllerAs = 'explorer';
    }
}

export class DocsExplorerController extends BaseExplorerController<DocumentVm> {

    static $inject = ['$state', '$stateParams', 'DocumentService', 'modalService'];


    constructor(protected _state: StateService,
        protected _stateParams: StateParams,
        private _service: DocumentService,
        private _modal: IModalService) {
        super(_state, _stateParams, 'app.document');
    }

    $onInit() {
        super.$onInit();
    }

    protected getDataImpl(parentId?:string, searchText?: string) {
        let params = new ParamsList;
        params['parentId'] = parentId;
        if (searchText) {
            params['SearchText'] = searchText;
        }
        return this._service.getDocumentList(params);
    }

    protected getParentsHierarchy(parentId?:string) {
        return this._service.getDocParents(parentId);
    }

    protected submitFolderImpl(folder: IFolderEntity) {
        return this._service.documentInsert(<DocumentPm>folder);
    }

    protected removeItemImpl(id: string): ng.IPromise<Result> {
        return this._service.removeDoc(id);
    }

    private elementToMove: DocumentPm;

    public moveDocDialog(elm: DocumentPm, event: Event) {
        event.stopPropagation();
        this.elementToMove = elm;
        let options = <IModalOptions>{
            id: 'doc-move-dialog',
            attributes: {
                element: this.elementToMove
            },
            onCloseCb: this.onDialogClose,
            innerCb: this.onDialogAnswer
        };
        this._modal.open(options);
    }

    public onDialogClose = () => {
        delete this.elementToMove;
    }

    public onDialogAnswer = (folder) => {
        this.elementToMove.parentId = folder ? folder.id : null;
        this._service.documentUpdate(this.elementToMove).then((data) => {
            this.$onInit();
        }, (error) => {
            console.error(error);
        });
    }


}
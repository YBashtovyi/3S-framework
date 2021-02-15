import * as angular from 'angular';
import { BaseExplorerMoveController } from '../../common/explorer/base.explorermove.controller';
import { DocumentVm } from '../../models/vm.models';
import { DocumentService } from '../../services/document.service';
import { ParamsList } from '../../models/common.models';


export class DocMoveComponent implements ng.IComponentOptions{
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = DocMoveController;
        this.template = require('./docmove.html');
        this.controllerAs = 'dmove';
        this.bindings = {
            config: '<',
            onAnswer: '<'
        }
        this.controllerAs = 'tmove';
    }
}

export class DocMoveController extends BaseExplorerMoveController<DocumentVm> {

    public config: any;
    public onAnswer: Function;

    static $inject = ['DocumentService'];

    constructor(private docService: DocumentService) {
        super();
    }

    protected getFoldersImpl(): ng.IPromise<DocumentVm[]> {
        return this.docService.getDocumentsTree();
    }

    $onInit() {
        super.$onInit();
    }
    $onChanges(changeObj) {
        if (changeObj.config.currentValue && !this.folders)
            this.getFolders();
    }

    public onFolderClick = (folder?) => {
        this.item = folder || null;
    }

    public onSelect() {
        if (this.onAnswer) {
            this.onAnswer(this.item);
        }
    }
}

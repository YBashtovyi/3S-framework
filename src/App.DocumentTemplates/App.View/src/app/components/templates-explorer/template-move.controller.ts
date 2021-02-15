import  * as angular from 'angular';
import { BaseExplorerMoveController } from '../../common/explorer/base.explorermove.controller';
import { DocTemplatesVm } from '../../models/vm.models';
import { DocumentService } from '../../services/document.service';
import { ParamsList } from '../../models/common.models';
import { ConstructorService } from "../../services/constructor.service";

export class TemplateMoveComponent implements ng.IComponentOptions{
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = TemplateMoveContoller;
        this.template = require('./templatemove.html');
        this.bindings = {
            config: '<',
            onAnswer: '<'
        }
        this.controllerAs = 'tmove';
    }
}

export class TemplateMoveContoller extends BaseExplorerMoveController<DocTemplatesVm>{
    
    public config: any;
    public onAnswer: Function;
    
    static $inject = ['DocumentService','ConstructorService'];

    constructor(private docService: DocumentService, private constService: ConstructorService){
        super();
    }

    protected getFoldersImpl():ng.IPromise<DocTemplatesVm[]>{
        return this.docService.getDocTemplatesTree();
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
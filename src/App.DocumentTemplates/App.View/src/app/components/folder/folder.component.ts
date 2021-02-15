import * as angular from 'angular';
import { IFolderEntity } from "../../models/common.models";

export class FolderComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = FolderController;
        this.template = require('./folder.html');
        this.bindings = {
            parentId: '<',
            onSubmit: '&',
            onCancel:'&'
        };
        this.controllerAs = 'folder';
    }
}

class FolderController implements ng.IComponentController {

    public folder: IFolderEntity;
    private parentId: string;
    private onSubmit: (any) => void;
    private onCancel: () => void;
    

    constructor() {
        
    }

    $onInit() {
        this.folder = <IFolderEntity>{
            parentId: this.parentId,
            classShortCode: 'F',
            recordState: 2
        };
    }
    $onDestroy() {

    }

    public save() {
        this.onSubmit({ folder: this.folder });
    } 

    public cancel() {
        this.onCancel();
    }
}
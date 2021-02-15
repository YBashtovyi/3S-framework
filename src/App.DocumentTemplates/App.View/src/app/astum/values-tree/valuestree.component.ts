import * as angular from 'angular';
import { DocTemplateElementValuesTreePm } from './../../models/pm.models';
export class ValuesTreeComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = ValuesTreeController;
        this.template = require('./valuestree.html');
        this.bindings = {
            config: '<',
            onSave: '&'
        }
        this.controllerAs = 'vt';
    }
}

export class ValuesTreeController implements ng.IComponentController{
    static $inject = ['$element'];

    private config:any;
    private onSave:Function;

    public valuesTreeModel:DocTemplateElementValuesTreePm;
    public valuesTreeForm:any;

    $onInit(){

    }

    $onDestroy(){
    
    }


    constructor(private element:ng.IAugmentedJQuery){
    
    }

    public save(){
    
    }

    public cancel(){
    
    }
}


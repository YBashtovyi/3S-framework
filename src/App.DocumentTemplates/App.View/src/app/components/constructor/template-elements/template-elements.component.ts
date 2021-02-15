import * as angular from 'angular';
import { DocTemplatesPm, DocTemplateElementsPm } from "./../../../models/pm.models";
import { DocTemplatesVm, DocTemplateElementsVm, DocControlTypesVm } from "./../../../models/vm.models";
import { ConstructorController } from "../constructor.component";

export class TemplateElementsComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;
    bindings: { [index: string]: string; };
    require: { [controller: string]: string };

    constructor() {
        this.controller = TemplateElementsController;
        this.template = require('./template-elements.html');
        this.bindings = {
            elements: '<',
            onChange: '&',
            parentConfig:'<?'

        };
        this.controllerAs = 'template';
        this.require = { "main": '^^templateConstructor' };
    }
}

export class TemplateElementsController implements ng.IComponentController {

    public templateGrid: Array<DocTemplateElementsPm|DocTemplateElementsVm>;
    public addingInProgress: boolean;

    private elements: Array<DocTemplateElementsPm|DocTemplateElementsVm>;
    private main: ConstructorController;
    private onChange: (any) => void;
    public parentConfig: any;
    public editIndex: number;
    private tempWidth:number;
    public get isInsertMode() {
        return this.main.isInsertMode;
    }

    static $inject = ['$element', '$scope'];
    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {
    }

    $onInit() {
        //this.generateGrid();
        this.addingInProgress = false;

    }



    //private generateGrid() {
    //    if (this.elements) {
    //        this.templateGrid = this.elements;
    //        return;
    //    }
    //    this.templateGrid =[];
    //}

    public onElementChange(event) {
        //this.elements[event.index] = event.element;
        this.callOnChange();
    }
    // public get isParentInline(){
    //     return this.isInline;
    // }

    private callOnChange() {
        if (this.onChange) {
            //let grid = new Array<DocTemplateElementsPm>();
            //angular.copy(this.templateGrid, grid);
            this.onChange({ event: { elements: this.elements } });
        }
    }

    public addElement($event) {
        $event.stopPropagation();
        if (this.main.isInsertMode)
            return;
        this.addingInProgress = true;
        let elm = new DocTemplateElementsPm();
        if (this.elements) {
            elm.orderNumber = parseInt('10' + this.elements.length);
        }
        else {
            elm.orderNumber = 10;
        }        
        elm.recordState = 2;
        elm.templateElements = [];
        elm.config = {};
        this.main.editElement(elm, this.insertDelegat, this.cancelDelegat,true);
    }

    public editElement(index: number) {
        this.editIndex = index;
        let obj = new DocTemplateElementsPm;
        angular.copy(this.elements[index], obj);
        this.main.editElement(obj, this.updateDelegat, this.cancelDelegat);


    }

    public removeElement(index: number) {
        this.main.editTemplate();
        this.elements.splice(index, 1);
    }

    public copyElementDialog(index: number, toTemplate: boolean = false) {
        let clone = new DocTemplateElementsPm;
        angular.copy(this.elements[index], clone);      
        if (toTemplate) {
            this.main.copyElementToTemplate(clone);
            return;
        }
        this.main.copyElementDialog(clone);
    }

    public copyElement(index: number) {
        let clone = new DocTemplateElementsPm;
        angular.copy(this.elements[index], clone);
        this.prepareClone(clone);
        this.elements.push(clone);        
    }

    private prepareClone(clone: DocTemplateElementsPm) {
        for (let key in clone) {
            if (key === "id")
                delete clone[key];
            if (key === "caption" || key === "code"){
                clone[key] = clone[key] + "_copy";
            }
            if (key === "templateElements" && clone[key] && clone[key].length){
                clone.templateElements =clone.templateElements.map((item) => {
                    return this.prepareClone(item);
                });
            }
        }
        return clone;
    }


    public insertDelegat = (elm: DocTemplateElementsPm | DocTemplateElementsVm) => {
        this.elements.push(elm);
        delete this.editIndex;
        this.callOnChange();
        this.addingInProgress = false;
    };

    public updateDelegat = (elm: DocTemplateElementsPm) => {
        this.elements[this.editIndex] = elm;
        delete this.editIndex;
        this.callOnChange();
    };

    public cancelDelegat = (isCreateMode?: boolean, causeBySibling?: boolean) => {
        if (isCreateMode && causeBySibling) {
            delete this.editIndex;
            return;
        }
        if (!isCreateMode && causeBySibling) {
            this.addingInProgress = false;
            return;
        }
        if (!isCreateMode && !causeBySibling) {
            delete this.editIndex;
            this.addingInProgress = false;
            return;
        }
    };


    $onChanges(changesObj: ng.IOnChangesObject) {

        //this.generateGrid();
    }

    $onDestroy() {
    }
    $postLink(){

    }
}


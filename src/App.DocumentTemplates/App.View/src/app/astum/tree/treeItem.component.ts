import * as angular from "angular";
import { TreeController } from "./tree.component";

export class TreeItemComponent implements ng.IComponentOptions{
    controller:ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };
    constructor() {
        this.controller = TreeItemController;
        this.template = require("./treeItem.html")
        this.bindings = {
            item:"<",
            parent:"<?"
        };
        this.controllerAs = "treeItem";
        this.require = { "root": "^^tree" };
    }
}

export class TreeItemController implements ng.IComponentController {

    public item: any;
    public parent: any;

    private root: TreeController;
    public nestedField:string;
    public labelField:string;
    public test: string;
    public isNestedVisible: boolean;

    static $inject = ["$element","$scope"];

    constructor(private element: ng.IAugmentedJQuery, private scope:ng.IScope) {

    }

    $onInit(){      
        this.labelField = this.root.label;
        this.nestedField = this.root.nested;
    }

    $postLink(){
       
    }

    public  onClick(){
        this.root.click(this.item);
    }

    public showNested(isVisible:boolean){
        this.isNestedVisible = isVisible;   
    }

    public isActive(elm){
        if(this.root.ngmodel && this.root.ngmodel.$viewValue)
            return this.equals(elm,this.root.ngmodel.$viewValue);
        if(this.root.model)
            return this.equals(elm,this.root.model);
    }

    public isNested(){
        return !!this.parent;
    }

    private equals(o1,o2){
        let isEqual:boolean = true;
        for(let prop in o1){
            if((<Object>o1).hasOwnProperty(prop) && (<Object>o2).hasOwnProperty(prop) && prop!=="$$hashKey")
                isEqual= o1[prop]===o2[prop];
        }
        return isEqual;
    }

}
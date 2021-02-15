import * as  angular from "angular";

export class TreeComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };
    constructor() {
        this.controller = TreeController;
        this.template = require('./tree.html');
        this.bindings = {
            items: '<',
            label: '@',
            nested:'@',
            model:'<?',
            onChange:'<?'
        }
        this.controllerAs = "tree";
        this.require = { "ngmodel": "?ngModel" };
    }
}

export class TreeController implements ng.IComponentController   {

    public items: any[];

    public label: string;
    public nested: string;
    public ngmodel: ng.INgModelController;
    public onChange: Function;
    public model:any;


    static $inject = ["$element","$scope"];
    constructor(private element: ng.IAugmentedJQuery, private scope:ng.IScope) {
        
    }

    $onInit() {
        this.element.addClass('tree');
    }

    $postLink() {
    }

    $onDestroy() {
    }

    public click=(item:any)=>{
        if(this.ngmodel){
        this.ngmodel.$setViewValue(item);
            return;    
        }
        if(this.onChange){
            this.onChange(item);
            return;
        }
    }



}
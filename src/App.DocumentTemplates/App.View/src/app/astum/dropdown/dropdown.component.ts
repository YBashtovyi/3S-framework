import * as angular from 'angular';

export class DropDownComponent implements ng.IComponentOptions{
    controller:ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };
    transclude: boolean;
    constructor() {
        this.controller = DropDownController;
        this.template = require("./dropdown.html");
        this.transclude = true;
        this.bindings = {
            label:'@',
            modelParser:'<?',
            modelFormater:'<?',
            value:'<?',
            items:'<?'
        };
        this.controllerAs = "dropdown";
        this.require = { "model": "ngModel" };

    }
}

export class DropDownController implements ng.IComponentController{
    static $inject = ["$element","$transclude",'$compile','$scope'];

    public model: ng.INgModelController;
    public innerModel: any;
    public items:any[];

    public label: string;
    private value: string;
    private input: ng.IAugmentedJQuery;
    private body: ng.IAugmentedJQuery;
    private modelFormater:Function;
    private modelParser:Function;

    constructor(private element:ng.IAugmentedJQuery, private transclude: ng.ITranscludeFunction,
        private compile:ng.ICompileService, private scope:ng.IScope){
        this.input = this.element.find("input");
        this.body = this.element.find(".drop-down-body");
    }

    public onChange = (item)=>{
        if(item.controlTypeCode === "SECTOR")
            return;

        this.innerModel = item;
        if(this.modelFormater){
            // debugger;
            this.model.$setViewValue(this.modelFormater(this.items,this.innerModel));
        }
        else{
            this.model.$setViewValue(item);
        }
        this.input.val(this.innerModel[this.label]);
        this.body.hide();
        
    };

    public render = ()=>{
        if(this.model.$modelValue && Array.isArray(this.model.$modelValue) && this.modelParser){
            this.innerModel = this.modelParser(this.items,[].concat(this.model.$modelValue));
            this.input.val(this.innerModel[this.label]);
            this.body.hide();
            return;
        }
        if(this.model.$viewValue && !this.modelParser){
            this.innerModel = this.model.$viewValue;
            this.input.val(this.model.$viewValue[this.label]);
            this.body.hide();
            return;
        }       
    };

    $onInit(){
        this.model.$render = this.render;
        this.input.click(()=>{
            this.body.show();
        });
    }

    private clickHandler = (event) => {
        var that = this;
        if(this.body.css('display')!=='none' && !this.element.find(event.target).length)
            this.body.hide();
    }

    $postLink(){
        this.body.outerWidth(this.input.outerWidth());
        document.addEventListener('click',this.clickHandler);
    }

    $onDestroy(){
        document.removeEventListener('click',this.clickHandler);
    }
 

}
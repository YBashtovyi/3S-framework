import * as angular from 'angular';
import { DocumentService } from '../../services/document.service';

export class AstumAutocompleteComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = AstumAutocompleteController;
        this.template = require('./astum.autocomplete.html');
        this.bindings = {
            sourceFn: '&?',
            source: '<?',
            label: '@',
            value: '@'
        }
        this.controllerAs = 'control';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

export class AstumAutocompleteController implements ng.IComponentController {

    public inputText: string;
    public list: any[] = [];
    public showList: boolean = false;
    public isActive: boolean = false;

    private label: string;
    private value: string;
    private source: any[];
    private prevSource: any[];
    private ngModelController: ng.INgModelController;
    private modelFiesld: string;
    private sourceFn: (string) => Promise<any[]>;
    private internalUpdate: boolean = false;
    

    static $inject = ['$element', '$scope', 'DocumentService'];

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private service: DocumentService) {
    }



    $onInit() {
        document.addEventListener('click', this.clickHandler)
    }
    private clickHandler = (event: MouseEvent) => {
        var that = this;
        if (that.element[0].contains((<any>event).target)) {
            this.isActive = true;
            this.scope.$apply();
            return;
        }
        else {
            this.blur();
        }
    }

    $postLink() {
        if (this.source)
            this.list = this.source;
        this.scope.$watch(() => this.inputText, this.find);
        this.scope.$watch(() => this.ngModelController.$modelValue, this.onModelChange)
    }

    $onDestroy() {
        document.removeEventListener('click', this.clickHandler);
    }

    $doCheck() {
        if (!this.prevSource || !angular.equals(this.source, this.prevSource)) {
            this.prevSource = angular.copy(this.source, this.prevSource)
            this.setSource(this.source);
        }
    }

    private setSource = (n) => {
        this.list = n;
        if (this.ngModelController.$modelValue) {
            this.onModelChange(this.ngModelController.$modelValue);
        }
    }

    private onModelChange = (n, o?, s?: ng.IScope) => {
        if (this.source && (n !== o || !this.inputText)) {
            this.source.forEach(item => {
                if (item[this.value] === n) {
                    this.setInput(item[this.label]);
                }
            });
        }
    }
    private setInput(text) {
        this.internalUpdate = true;
        this.inputText = text;
    } 

    private find = (n: string, o: string, s: ng.IScope) => {
        if (this.internalUpdate) {
            this.internalUpdate = false;
            return;
        }
        if (n && n.length > 2) {
            this.getData(n).then((data) => {
                s.$apply(() => {
                    this.list = data;
                    this.showList = true;
                });
            });
        }
    }

    private getData(str: string) {
        if (this.source) {
            let pr = new Promise<any[]>((resolve, reject) => {
                resolve(this.source.filter((item) => {
                    return (<string>item[this.label]).toLowerCase().indexOf(str.toLowerCase()) > -1;
                }))
            });
            return pr;
        }
        if (this.sourceFn) {
            return this.sourceFn({ $query: str, force: true });
        }
    }

    public onItemClick(item) {
        if (item) {
            this.ngModelController.$setViewValue(item[this.value]);
            this.setInput(item[this.label]);
            this.showList = false;
        }
    }
    public clear() {
        this.ngModelController.$setViewValue(null);
        this.setInput(null);
        this.showList = false;
    }

    private blur = () => {
        if (!this.isActive)
            return;
        if (this.ngModelController.$modelValue || this.ngModelController.$modelValue===0) {
            this.source.forEach(item => {
                if (item[this.value] === this.ngModelController.$modelValue) {
                    this.setInput(item[this.label]);
                }
            });
        } else {
            this.clear();
        }
        this.isActive = false;
        this.showList = false;
        this.scope.$apply();
    }

}
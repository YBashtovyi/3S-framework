import * as angular from 'angular';
import { DocTemplatesVm } from './../../models/vm.models';


export class AstumTemplateCopyComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = AstumTemplateCopyController;
        this.template = require('./copydialog.html');
        this.bindings = {
            config: '<',
            onAnswer: '<'
        }
        this.controllerAs = 'tcopy';
    }
}

export class AstumTemplateCopyController implements ng.IComponentController {

    public config: any;
    public templateId: number;
    public onAnswer: Function;

    static $inject = ['$element', '$scope']

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {
    }

    $onInit() {
    }

    $onDestroy() { }

    public onClose() {
        console.log(this);
    }

    public close = (answer: boolean, templateId?: number) => {
        if (this.onAnswer && templateId)
            this.onAnswer({ templateId: templateId, withValues: answer });
    }
}
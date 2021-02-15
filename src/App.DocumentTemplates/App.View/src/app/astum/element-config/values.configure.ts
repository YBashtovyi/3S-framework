import * as angular from 'angular';
import { DocumentService } from "./../../services/document.service";
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import { DocTemplateElementValuesPm } from "../../models/pm.models";

export class AstumValuesConfigureComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = AstumValuesConfigureController;
        this.template = require('./values.html');
        this.bindings = {
            config: '<'
        }
        this.controllerAs = 'cfg';
    }
}

export class AstumValuesConfigureController implements ng.IComponentController {

    static $inject = ['$element','$scope', '$compile'];

    public config: any;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private compile: ng.ICompileService) {

    }

    $onInit() {
    }

    $onChange(changes) {
    }
}

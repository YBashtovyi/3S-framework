import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import { ParamsList, CardMode } from "./../../models/common.models";
import { DocTemplatesPm, DocTemplateElementsPm, DocElementsPm } from "./../../models/pm.models";
import { DocTemplatesVm, DocTemplateElementsVm, DocControlTypesVm, DocElementsVm } from "./../../models/vm.models";
import { CustomElementService } from './custom-element.service';

export class CustomElementComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = CustomElementController;
        this.template = require('./custom-element.html');
        this.controllerAs = 'ce';
    }
}


export class CustomElementController implements ng.IComponentController {

    static $inject = ['$scope', '$state', '$stateParams','customElementService'];
    constructor(private scope: ng.IScope,
        private state: StateService,
        private stateParams: StateParams,
        private service: CustomElementService) {
    }

    $onInit() {
        //if (this.state.is('app.customelement.add')) {
        //} else {
        //}

    }
    $onDestroy() {

    }
}
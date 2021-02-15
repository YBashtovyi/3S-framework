import * as angular from 'angular';
import { StateParams, StateService } from '@uirouter/angularjs';
import {CustomElementService} from "../../components/custom-element/custom-element.service";

export class NotFoundComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = NotFoundController;
        this.template = require('./not-found');
        this.controllerAs = 'nf';
    }
}
export class NotFoundController implements ng.IComponentController {

    static $inject = ['$state'];
    constructor(private state: StateService,
    ) {
    }

    $onInit() {


    }
    $onDestroy() {

    }
}
import * as angular from 'angular';
import * as moment from 'moment';
import './astum.spreadsSheet.scss';

export class AstumSpreadsSheetComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = AstumSpreadsSheetController;
        this.template = require('./astum.spreadsSheet.html');
        this.bindings = {
            attributeId: '<',
            templateElementId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 's';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

export class AstumSpreadsSheetController implements ng.IComponentController {

    static $inject = ['$element', '$scope'];

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {

    }


    $onInit() {

    }

    $onDestroy() {

    }

    //$doCheck(): void {
    //    throw new Error("Method not implemented.");
    //}
    //$onChanges(onChangesObj: angular.IOnChangesObject): void {
    //    throw new Error("Method not implemented.");
    //}
    //$postLink(): void {
    //    throw new Error("Method not implemented.");
    //}
}
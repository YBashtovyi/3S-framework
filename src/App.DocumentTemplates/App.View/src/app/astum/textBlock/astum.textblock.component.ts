import * as angular from 'angular';
import { DocumentService } from "./../../services/document.service";
import './astumtextblock.scss';

export class AstumTextBlockComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = TextBlockController;
        this.template = require('./astum.textblock.html');
        this.bindings = {
            attributeId: '<',
            templateElementId: '<',
            attributeTypeCode: '<',
            model:'<'
        }
        this.controllerAs = 'control';
    }
}

class TextBlockController implements ng.IComponentController {

    static $inject = [];

    public model: any;

    constructor() {

    }

    $onInit() {
        console.log(this.model);
    }
    $onDestroy() {

    }
}
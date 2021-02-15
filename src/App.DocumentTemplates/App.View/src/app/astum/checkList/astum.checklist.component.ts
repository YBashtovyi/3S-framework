import * as angular from 'angular';
import { DocumentService } from "./../../services/document.service";
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import { DocTemplateElementValuesPm } from "../../models/pm.models";
import './checkList.scss';

export class AstumCheckListComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = CheckListController;
        this.template = require('./astum.checklist.html');
        this.bindings = {
            attributeId: '<',
            valuesTreeId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'control';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

class CheckListController implements ng.IComponentController {

    static $inject = ['$element', '$scope', 'DocumentService'];

    public items: any[];
    public editMode: boolean;

    private valuesTreeId: number;
    private attributeTypeCode: string;
    private attributeId: number;
    private ngModelController: ng.INgModelController;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private service: DocumentService) {

    }

    $onInit() {
        this.scope.$on('activateDocElement', this.onActivateDocElement);
        this.scope.$on('configClosed', this.onConfigClose);
    }

    private onActivateDocElement = (event, data) => {
        this.getData().then((data) => {
            this.items = data.length ? this.prcocessChecks(data, this.ngModelController.$viewValue) : [];
        });
    }

    private getData = () => {
        let params: any = {};
        params['valuesTreeId'] = this.valuesTreeId;
        return this.service.getDocTemplateElementValuesList(params);
    }

    private prcocessChecks(data: any[], model: string) {
        if (model) {
            let modelArr = model.split(',');
            return data.map((item) => {
                item.checked = modelArr.some((model) => {
                    return model === item.caption;
                });
                return item;
            });
        } else {
            return data.map((item) => {
                item.checked = false;
                return item;
            });
        }
    }

    private onConfigClose = (d) => {
        this.getData().then(data => {
            this.items = data.length ? this.prcocessChecks(data, this.ngModelController.$viewValue) : [];
        });
    }


    public onChange() {
        let model = this.items.filter((item) => {
            return !!item.checked;
        }).map((item) => {
            return item.caption;
        });
        this.ngModelController.$setViewValue(model.join(','));
    }

    $onDestroy(): void {
    }
}

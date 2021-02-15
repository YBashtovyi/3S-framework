import * as angular from 'angular';
import { DocumentService } from "./../../services/document.service";
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import { DocTemplateElementValuesPm } from "../../models/pm.models";
import { BaseConfigureController } from "./base.configure.controller";

export class AstumListConfigureComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = ListConfigureController;
        this.template = require('./list-configure.html');
        this.bindings = {
            attributeId: '<',
            valuesTreeId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'config';
    }
}

export class ListConfigureController extends BaseConfigureController {

    static $inject = ['$element', '$scope', 'DocumentService'];

    public items: any[];
    public editMode: boolean;
    public checkValue: string;

    private _value: DocTemplateElementValuesPm;
    private asContent: boolean;
    private ngModelController: ng.INgModelController;
    public showField;

    constructor(private element: ng.IAugmentedJQuery, scope: ng.IScope, service: DocumentService) {
        super(scope, service);
    }


    $onInit() {
        super.$onInit();
        this.getData().then((data) => {
            this.items = data.length ? data : [];
        });
    }

    private getData = () => {
        let params: any = {};
        params['valuesTreeId'] = this.valuesTreeId;
        return this.service.getDocTemplateElementValuesList(params);
    }

    public setEditMode(val: boolean) {
        this.editMode = val;

    }

    public postValue(value: string) {
        if (!this.editMode && value) {
            let valueModel = new DocTemplateElementValuesPm;
            valueModel.valuesTreeId = this.valuesTreeId;
            valueModel.caption = value;
            valueModel.contentValue = value;
            valueModel.recordState = 2;
            valueModel.valueTypeCode = 'L';
            valueModel.originDbRecordId = '';
            valueModel.originDbId = '';
            return this.addTemplateElementValue(valueModel);
        }
        if (this.editMode) {
            this._value.caption = value;
            this._value.contentValue = value;
            return this.updateTemplateElementValue(this._value).then(data => {
                delete this.checkValue;
                this.editMode = false;
            }, err => {
                delete this.checkValue;
                this.editMode = false;
            })
        }

    }

    public editValue(value: DocTemplateElementValuesPm) {
        this.editMode = true;
        this._value = value;
        this.checkValue = value.contentValue;
        //this.updateTemplateElementValue(valueModel);
    }

    public deleteValue(elementValue: DocTemplateElementValuesVm) {
        this.removeTemplateElementValue(elementValue);
    }


    protected reloadImpl(data) {
        if (this.valuesTreeId === data.valuesTreeId) {
            delete this.checkValue;
            if (this.editMode) {
                this.items = [].concat(this.items.map(item => {
                    if (item.id === data.id)
                        return data;
                    else
                        return item;
                }))
                return;
            }
            this.items.push(data.elementValue);
        }
    }

    protected reduceImpl(data) {
        if (this.valuesTreeId === data.valuesTreeId) {
            this.items.splice(this.items.indexOf(data.elementValue), 1);
        }
    }


}

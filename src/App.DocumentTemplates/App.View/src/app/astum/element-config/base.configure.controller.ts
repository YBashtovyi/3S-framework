import * as angular from 'angular';
import { DocumentService } from "./../../services/document.service";


export abstract class BaseConfigureController implements ng.IComponentController {

    constructor(protected scope: ng.IScope, protected service: DocumentService) {
        this.scope.$on('elementValueInserted', this.reload);
        this.scope.$on('elementValueRemoved', this.reduce);
    }

    protected valuesTreeId: string;
    protected attributeTypeCode: string;
    protected attributeId: number;

    protected abstract reloadImpl(any): void;
    protected abstract reduceImpl(any):void;

    public reload (data) {
        this.reloadImpl(data);
    }

    public reduce (data) {
        this.reduceImpl(data);
    }

    $onInit() {
    }

    protected addTemplateElementValue (model: any) {
        this.service.docTemplateElementValuesInsert(model).then((data) => {
            this.reload({ valuesTreeId: model.valuesTreeId, elementId: model.id, parentId: model.parentId, elementValue: model });
        });
    }

    protected updateTemplateElementValue = (model: any) => {
        return this.service.docTemplateElementValuesUpdate(model).then((data) => {
            this.reload({ valuesTreeId: model.valuesTreeId, elementId: model.id, parentId: model.parentId, elementValue: model });
        });
    }

    protected removeTemplateElementValue = (model: any) => {
        this.service.docTemplateElementValueDelete(model.id).then((data) => {
            if (data.success) 
                this.reduce({ valuesTreeId: model.valuesTreeId, elementId: model.id, parentId: model.parentId, elementValue: model });
        })
    }


}
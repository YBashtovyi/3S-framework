import { DocTemplatePresetsPm, DocTemplateElementValuesPm, DocDataPm, DocTemplatesPm, DocumentPm } from './../models/pm.models';
import { ParamsList, Result } from "../models/common.models";
import { DocTemplatePresetsVm, DocTemplateElementsVm, DocTemplateElementValuesVm, DocDataVm, DocTemplatesVm, DocumentVm } from '../models/vm.models';
import { UrlString } from './utils';
import { GlobalConfig } from './global.config';
import { IDataService } from '../astum/services/data.service';
import BaseService from "./base.service";


export class DocumentService extends BaseService {

    static $inject = ['dataService']
    constructor(private service: IDataService) {
        super();
    }


    public getDocTemplatesList(): ng.IPromise<DocTemplatesVm[]> {
        return this.service.getQuery<DocTemplatesVm[]>('dtm/GetDocTemplatesList', {});
    }

    public getDocTemplates(params?: ParamsList): angular.IPromise<DocTemplatesVm[]> {
        return this.service.postQuery<DocTemplatesVm[]>('dtm/GetDocTemplates', params, {});
    }

    public getDocTemplatesTree(): angular.IPromise<DocTemplatesVm[]> {
        return this.service.getQuery<DocTemplatesVm[]>('dtm/GetDocTemplatesTree', {});
    }



    public getDocTemplateParents(id: string, rootName: string): ng.IPromise<DocTemplatesVm[]> {
        let query = new UrlString();
        query.addParameter('id', id);
        if (rootName)
            query.addParameter('RootName', rootName);
        return this.service.getQuery<DocTemplatesVm[]>('dtm/GetDocTemplateParents?' + query.value, {});
    }
    public getDocParents(id: string): ng.IPromise<DocumentVm[]> {
        let query = new UrlString();
        query.addParameter('id', id);
        return this.service.getQuery<DocumentVm[]>('dtm/GetDocsParents?' + query.value, {});
    }

    public docTemplateElementValuesInsert(model: DocTemplateElementValuesPm): angular.IPromise<DocTemplateElementValuesVm> {
        return this.service.postQuery<DocTemplateElementValuesVm>('dtm/DocTemplateElementValuesInsert', model, {});
    }

    public docTemplateElementValuesUpdate(model: DocTemplateElementValuesPm): angular.IPromise<DocTemplateElementValuesVm> {
        return this.service.postQuery<DocTemplateElementValuesVm>('dtm/DocTemplateElementValuesUpdate', model, {});
    }

    public docTemplateElementValueDelete(id: string): angular.IPromise<Result> {
        let query = new UrlString();
        query.addParameter('id', id);
        return this.service.getQuery<Result>('dtm/DocTemplateElementValueDelete?' + query.value, {});
    }

    public getDocTemplateElementsList(params: ParamsList): angular.IPromise<DocTemplateElementsVm[]> {
        let config = <ng.IRequestShortcutConfig>{
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.parseConfigs(resp, 'templateElements');
                else
                    return resp;
            }
        }
        return this.service.postQuery('dtm/GetDocTemplateElementsList', params, config);
    }

    public getDocTemplateElementValuesList(params: ParamsList): angular.IPromise<DocTemplateElementValuesVm[]> {
        return this.service.postQuery('dtm/GetDocTemplateElementValuesList', params, {});
    }

    public getDocDataList(params: ParamsList): angular.IPromise<DocDataVm[]> {
        return this.service.postQuery<DocDataVm[]>('dtm/GetDocDataList', params, {}).then((result) => {
            return !!result ? this.convertData(<DocDataVm[]>result) : result;
        });
    }

    public getDocument(id: string, isOrigin: boolean = false, entityTypeCode: string = null): angular.IPromise<DocumentVm> {
        let query = new UrlString();
        query.addParameter('id', id);
        query.addParameter('isOrigin', isOrigin);
        query.addParameter('entityTypeCode', entityTypeCode);
        let config = <ng.IRequestShortcutConfig>{
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'documentDataList', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.getQuery('dtm/GetDocument?' + query.value, config);
    }

    public getDocumentList(params: ParamsList): angular.IPromise<DocumentVm[]> {
        return this.service.postQuery('dtm/GetDocumentList', params, {});
    }

    public getDocumentsTree(): angular.IPromise<DocumentVm[]> {
        return this.service.getQuery('dtm/GetDocumentsTree', {})
    }

    private convertData(data: DocDataVm[]): DocDataVm[] {
        return data.map((item: DocDataVm) => {
            if (item.controlTypeCode === 'NUMBER')
                item.value = Number(item.value);
            return item;
        });
    };

    public DocDataInsertBatch(dataList: DocDataPm[]): ng.IPromise<Result> {
        return this.service.postQuery('dtm/DocDataInsertBatch', dataList, {});
    }

    public DocDataUpdateBatch(dataList: DocDataPm[]): ng.IPromise<Result> {
        return this.service.postQuery('dtm/DocDataUpdateBatch', dataList, {});
    }

    public getDocTemplatePresetsList(params: ParamsList): ng.IPromise<DocTemplatePresetsVm[]> {
        return this.service.postQuery('dtm/GetDocTemplatePresetsList', params, {});
    }


    public docTemplatePresetInsert(model: DocTemplatePresetsPm): angular.
        IPromise<DocTemplateElementValuesVm> {
        return this.service.postQuery<DocTemplateElementValuesVm>('dtm/DocTemplatePresetInsert', model, {});
    }

    public docTemplatePresetUpdate(model: DocTemplatePresetsPm): angular.IPromise<DocTemplateElementValuesVm> {
        return this.service.postQuery<DocTemplateElementValuesVm>('dtm/DocTemplatePresetUpdate', model, {});
    }


    //TODO:change to docs service
    public documentInsert(model: DocumentPm): ng.IPromise<DocumentVm> {
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.transformConfigurable(req, 'documentDataList', this.stringifyConfigs);
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'documentDataList', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.postQuery<DocumentVm>('dtm/DocumentInsert', model, config);
    }
    public documentUpdate(model: DocumentPm): ng.IPromise<DocumentVm> {
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.transformConfigurable(req, 'documentDataList', this.stringifyConfigs)
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'documentDataList', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.postQuery<DocumentVm>('dtm/DocumentUpdate', model, config);
    }

    public DocumentPreviewInsert(model: DocumentPm): ng.IPromise<DocumentVm> {
        return this.service.postQuery<DocumentVm>('dtm/DocumentPreviewInsert', model, {});
    }

    //TODO:change to docs service
    public removeDoc(id: string): ng.IPromise<Result> {
        let query = new UrlString();
        query.addParameter('id', id);
        return this.service.getQuery('dtm/DocumentRemove?' + query.value, {});
    }
}





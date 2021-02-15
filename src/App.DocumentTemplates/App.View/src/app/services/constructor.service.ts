import * as angular from 'angular';
import { DocTemplatesVm, DocControlTypesVm, DocTemplateElementsVm, DocElementsVm, DocTemplateElementValuesTreeVm } from './../models/vm.models';
import { DocTemplatesPm, DocTemplateElementsPm, DocTemplateElementValuesTreePm } from './../models/pm.models';
import { UrlString } from './utils';
import { GlobalConfig } from './global.config';
import { IDataService } from '../astum/services/data.service';
import { Result, IConfigurable } from "../models/common.models";
import BaseService from "./base.service";

export class ConstructorService extends BaseService {

    static $inject = ["dataService"];
    constructor(private service: IDataService) {
        super();
    }


    public getDocElements(controlCode: string): ng.IPromise<DocElementsVm[]> {
        let query = new UrlString();
        query.addParameter('controlCode', controlCode);
        return this.service.getQuery<DocElementsVm[]>('DtmConstructor/GetDocElementsList?' + query.value, null);
    }

    public getControlTypesList(): ng.IPromise<DocControlTypesVm[]> {
        return this.service.getQuery('DtmConstructor/GetDocControlTypesList', {});
    }

    public DocTemplatesCard(id: string): ng.IPromise<DocTemplatesVm> {
        let query = new UrlString();
        query.addParameter('id', id);
        let config = <ng.IRequestShortcutConfig>{
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'templateElements', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.getQuery("Dtm/GetDocTemplatesCard?" + query.value, config);
    }

    public ValuesTreeList(): ng.IPromise<DocTemplateElementValuesTreeVm[]> {
        return this.service.getQuery("Dtm/ValuesTreeList", {});
    }

    public ValuesTreeInsert(model: DocTemplateElementValuesTreePm): ng.IPromise<DocTemplateElementValuesTreeVm> {
        return this.service.postQuery<DocTemplateElementValuesTreeVm>('Dtm/ValuesTreeInsert', model, {});
    }

    public ValuesTreeCopy(model: DocTemplateElementValuesTreePm): ng.IPromise<DocTemplateElementValuesTreeVm> {
        return this.service.postQuery<DocTemplateElementValuesTreeVm>('Dtm/ValuesTreeCopy', model, {});
    }

    public DocTemplateElementCopy(model: DocTemplateElementsPm, withValues: boolean): ng.IPromise<DocTemplateElementsVm> {
        let query = new UrlString();
        query.addParameter('withValues', withValues);
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.stringifyConfig(req, 'templateElements')
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200) {
                    return this.parseConfig(resp, 'templateElements');
                }
                else
                    return resp;
            }
        }
        return this.service.postQuery<DocTemplateElementsVm>('DtmConstructor/DocTemplateElementCopy?' + query.value, model, config);
    }

    public DocTemplateElementCopyToTemplate(model: DocTemplateElementsPm, templateId: string, withValues: boolean): ng.IPromise<Result> {
        let query = new UrlString();
        query.addParameter('withValues', withValues);
        query.addParameter('templateId', templateId);
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.stringifyConfig(req, 'templateElements')
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200) {
                    return this.parseConfig(resp, 'templateElements');
                }
                else
                    return resp;
            }
        }
        return this.service.postQuery<Result>('DtmConstructor/DocTemplateElementCopyToTemplate?' + query.value, model, config);
    }

    public DocTemplateInsert(model: DocTemplatesPm): ng.IPromise<DocTemplatesVm> {
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.transformConfigurable(req, 'templateElements', this.stringifyConfigs)
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'templateElements', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.postQuery<DocTemplatesVm>('DtmConstructor/DocTemplateInsert', model, config);
    }

    public DocTemplateUpdate(model: DocTemplatesPm): ng.IPromise<DocTemplatesVm> {
        let config = <ng.IRequestShortcutConfig>{
            transformRequest: (req) => {
                return this.transformConfigurable(req, 'templateElements', this.stringifyConfigs)
            },
            transformResponse: (resp, headers, status) => {
                if (status === 200)
                    return this.transformConfigurable(resp, 'templateElements', this.parseConfigs);
                else
                    return resp;
            }
        }
        return this.service.postQuery<DocTemplatesVm>('DtmConstructor/DocTemplateUpdate', model, config);
    }

    public DocTemplateMove(templateId: string, parentId: string): ng.IPromise<Result> {
        let query = new UrlString();
        query.addParameter('templateId', templateId);
        query.addParameter('parentId', parentId);
        return this.service.getQuery("DtmConstructor/DocTemplateMove?" + query.value, {});
    }


    public DocTemplateElementInsert(model: DocTemplateElementsPm): ng.IPromise<DocTemplateElementsVm> {

        return this.service.postQuery<DocTemplateElementsVm>('DtmConstructor/DocTemplateElementInsert', model, {});
    }

    public DocTemplateElementUpdate(model: DocTemplateElementsPm): ng.IPromise<DocTemplateElementsVm> {
        return this.service.postQuery<DocTemplateElementsVm>('DtmConstructor/DocTemplateElementUpdate', model, {});
    }

    public DocTemplateRemove(id: string): ng.IPromise<Result> {
        let query = new UrlString();
        query.addParameter('id', id);
        return this.service.getQuery('DtmConstructor/DocTemplateRemove?' + query.value, {});
    }
}
import * as angular from 'angular';
import { DocTemplatesVm } from "./../../models/vm.models";
import { StateParams, StateService } from '@uirouter/angularjs';
import { UrlString } from './../../services/utils';
import { DocumentService } from './../../services/document.service';
import { ConstructorService } from "../../services/constructor.service";
import { Result } from "../../models/common.models";


export class TemplatesExplorerService {

    static $inject = ['$state', '$stateParams', 'DocumentService','ConstructorService'];

    constructor(private state: StateService,
        private stateParams: StateParams,
        private documentService: DocumentService,
        private constructorService: ConstructorService) {
        this.parents = [];
        if (this.stateParams["parentId"]) {
            //this.documentService.getDocTemplateParents(this.stateParams["parentId"]).then((data) => {
            //    this.parents = data;
            //})
        }
    }

    public parents: DocTemplatesVm[];

    public go(item: DocTemplatesVm) {
        if (!item) {
            this.parents = [];
            this.state.go('app.templates', { parentId: null });
        }
        if (item.classShortCode === 'F') {
            
            this.reduceParents(item);
            this.state.go('app.templates', { parentId: item.id });
        }
        if (item.classShortCode === 'T') {
            this.state.go('app.constructor', { id: item.id })
        }
    }

    private reduceParents(item) {
        let index;
        this.parents.forEach((parent, i) => {
            if (parent === item)
                index = i;
        })
        if (angular.isDefined(index)) { 
            this.parents.splice(index+1, this.parents.length - 1);
            return;
        }
        this.parents.push(item);
    }

    public stateParam(param) {
        return this.stateParams[param];
    }

    public addItem() {
        this.state.go('app.constructor', { parentId: this.stateParams['parentId'] });
    }

    //public remove(id: number): ng.IPromise<Result> {
    //    return this.constructorService.DocTemplateRemove(id).then((result: Result) => {
    //        if (result.success) {
    //            let parent = !!this.stateParam('parentId') ? this..stateParam('parentId') : null;
    //            this.getData(parent);
    //        }
    //    });
    //}

}
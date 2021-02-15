import * as angular from 'angular';
import { DocumentService } from './../../services/document.service';
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import { BaseConfigureController } from "./base.configure.controller";
import { DocTemplateElementValuesPm } from "../../models/pm.models";
import * as $ from 'jquery';

export class AstumTreeConfigureComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };


    constructor() {
        this.controller = AstumTreeConfigureController;
        this.template = require('./tree.html');
        this.bindings = {
            attributeId: '<',
            valuesTreeId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'tree';
    }
}

export class AstumTreeConfigureController extends BaseConfigureController {
    static $inject = ['$element', '$scope', 'DocumentService'];

    public tree: any[];

    //protected valuesTreeId: number;
    //protected attributeTypeCode: string;
    //protected attributeId: number;

    public initialising: boolean = true;

    public updatingItem: boolean = false;
    public itemName: string;
    public itemContent: string;





    constructor(private element: ng.IAugmentedJQuery, scope: ng.IScope, service: DocumentService) {
        super(scope, service);
    }

    $onInit() {

        this.initialising = false;
        if (!this.tree) {
            this.initialising = true;
            this.getData().then((data) => {
                this.tree = data;
                this.initialising = false;
            });
        }

        this.element.on('click', '.bar-left', this.clearCurrentNode);
    }



    $onDestroy(): void {
        this.element.off('click', '.bar-left', this.clearCurrentNode);
    }

    public updateItem() {
        this.updatingItem = true;
        this.itemName = this.currentNode.caption;
        this.itemContent = this.currentNode.contentValue;
        this.focusInput();
    }

    public addNewItem() {
        delete this._currentNode;
        this.updatingItem = false;
        this.itemName = '';
        this.itemContent = '';
        this.focusInput();
    }

    public addNestedItem(node) {
        this._currentNode = node;
        this.updatingItem = false;
        this.itemName = '';
        this.itemContent = '';
        this.focusInput();
    }

    private focusInput() {
        this.element.find('input').focus();
    }

    public clearCurrentNode = (e: JQueryEventObject) => {
        let node = $(e.target);
        if (!node.hasClass('node-value') && !node.hasClass('tree__edit-button') && !node.hasClass('node-action') && !node.parent().hasClass('node-action'))
            this.scope.$apply(() => {
                delete this._currentNode;
            });

    }


    protected reloadImpl(data) {
        this._currentNode && (this._currentNode.ChildsCount = data.elementValue.orderNumber);
        this.getData(data.parentId).then((nodes) => {
            if (!data.parentId) {
                this.tree = nodes;
                return;
            }
            this.scope.$broadcast('expandNode', { id: data.parentId, nodes: nodes });
        },
            (error) => {
            });
    }


    protected reduceImpl(data) {
        return this.reloadImpl(data);
    }

    private onTreeEvent = (event, data) => {
        this.reloadImpl(data);
    }

    private _currentNode: any;

    public get currentNodeContent(): string {
        if (this._currentNode)
            return this._currentNode.contentValue;
    }

    public set currentNode(value: any) {
        this._currentNode = value;
    }

    public get currentNode() {
        return this._currentNode;
    }

    public nodeExpanded = (node) => {
        this.scope.$broadcast('nodeExpanded', { id: node.id, parent: node.parentId });
    }

    public getNodes = (node): Promise<DocTemplateElementValuesVm[]> => {
        return this.getData(node.id);
    }
    private getData(parentId?: number) {
        let params: any = {};
        params['valuesTreeId'] = this.valuesTreeId;
        params['parentId'] = parentId ? parentId : null;
        return this.service.getDocTemplateElementValuesList(params);
    }

    public postItem(name, value) {
        if (!this.updatingItem) {
            let model = new DocTemplateElementValuesPm;
            model.valuesTreeId = this.valuesTreeId;
            model.caption = name;
            model.parentId = this._currentNode ? this._currentNode.id : null;
            model.contentValue = value;
            model.recordState = 2;
            model.valueTypeCode = 'L';
            model.originDbRecordId = '';
            model.originDbId = '';
            let orderNum = this._currentNode ? this._currentNode.ChildsCount+1: this.tree.length+1;
            model.orderNumber = orderNum;
            this.addTemplateElementValue(model);
            return;
        }
        if (this.updatingItem) {
            let model = this.currentNode;
            model.caption = name;
            model.contentValue = value;
            this.updateTemplateElementValue(model);
            return;
        }
    }
    public removeValue(elementValue: DocTemplateElementValuesVm) {
        this.removeTemplateElementValue(elementValue);
    }
}

import * as angular from 'angular';
import { AstumTreeConfigureController } from './tree.configure.component';
import { DocTemplateElementValuesVm } from './../../models/vm.models';



export class AstumTreeItemConfigureComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };


    constructor() {
        this.controller = AstumTreeItemConfigureController;
        this.template = require('./treeitem.html');
        this.bindings = {
            item: '<'
        }
        this.controllerAs = 'treeItem';
        this.require = { "treeConfig": '^^astumTreeConfigure', "parentItem": '?^^astumTreeItemConfigure' };
    }
}

class AstumTreeItemConfigureController implements ng.IComponentController {

    static $inject = ['$element', '$scope'];

    public isExpanded: boolean;
    public isLoading: boolean;
    public nodeContent: string;
    public item: DocTemplateElementValuesVm;
    public itemName: string;
    public itemValue: string;

    private treeConfig: AstumTreeConfigureController;
    private parentItem: AstumTreeItemConfigureController;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {
    }

    $onInit() {
        this.scope.$on('nodeExpanded', this.onNodeExpanded);
        this.scope.$on('expandNode', this.onExpand);
    }

    $postLink(): void {

    }

    $onDestroy(): void {
    }



    private onExpand = (event, data) => {
        if (this.item.id === data.id) {
            this.item.ChildsCount = data.nodes.length;
            (<any>this.item).tree = data.nodes;
            this.expand(this.item, true);
        }
    }

    private onNodeExpanded = (event, data) => {
        if (this.item.parentId === data.parent && this.item.id !== data.id)
            this.isExpanded = false;
    }

    public nodeExpanded = (node) => {
        this.scope.$broadcast('nodeExpanded', node.id);
    }



    public setNodeContent(node): void {
        this.treeConfig.currentNode = node;
    }

    public addToNode(): void {
        this.treeConfig.addNestedItem(this.item);
    }

    public updateNode(): void {
        this.treeConfig.currentNode = this.item;
        this.treeConfig.updateItem();
    }

    public removeItem(): void {
        this.treeConfig.removeValue(this.item);
    }

    public get currentNode() {
        return this.treeConfig.currentNode;
    }



    public expand = (node, onEvent?: boolean) => {
        if (!onEvent)
            this.isExpanded = !this.isExpanded;
        else
            this.isExpanded = true;
        this.treeConfig.nodeExpanded(node);
        if (!angular.isDefined(node.tree)) {
            this.isLoading = true;
            this.treeConfig.getNodes(node)
                .then((data) => {
                    node.tree = data;
                    this.isLoading = false;
                },
                (e) => {
                    this.isLoading = false;
                });
        }
    };

}
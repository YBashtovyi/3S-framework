import * as angular from 'angular';
import { AstumLexicalTreeController } from './astum.lexicaltree.component';
import { DocTemplateElementValuesVm } from './../../models/vm.models';



export class AstumLexicalTreeItemComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };


    constructor() {
        this.controller = AstumLexicalTreeItemController;
        this.template = require('./astum.lexicaltreeitem.html');
        this.bindings = {
            item: '<'
        }
        this.controllerAs = 'treeItem';
        this.require = { "lexicalTree": '^^astumLexicalTree' , "parentItem": '?^^astumLexicalTreeItem' };
    }
}

class AstumLexicalTreeItemController implements ng.IComponentController {

    static $inject = ['$element', '$scope'];

    public isExpanded: boolean;
    public isLoading: boolean;
    public nodeContent: string;
    public item: DocTemplateElementValuesVm;
    public itemName: string;
    public itemValue: string;

    private lexicalTree: AstumLexicalTreeController;
    private parentItem: AstumLexicalTreeItemController;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {
    }

    $onInit() {
        this.scope.$on('nodeExpanded', this.onNodeExpanded);
        this.scope.$on('expandNode', this.onExpand);
    }

    $onDestroy(): void {
    }

    private onExpand = (event, data) => {
        if (this.item.id === data.id) { 
            this.item.ChildsCount = data.nodes.length;
            (<any>this.item).tree = data.nodes;
            this.expand(this.item,true);
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
        this.lexicalTree.currentNode = node;
        this.setNodeChain();
    }

    //public addToNode(): void {
    //    this.lexicalTree.currentNode = this.item;
    //    this.lexicalTree.addNewItem();
    //}

    //public updateNode(): void {
    //    this.lexicalTree.currentNode = this.item;
    //    this.lexicalTree.updateItem();
    //}

    //public removeItem(): void {
    //    this.lexicalTree.removeValue(this.item);
    //}

    public get currentNode() {
        return this.lexicalTree.currentNode;
    }

    public insertNodeValue($event:MouseEvent) {
        this.lexicalTree.insertValue($event,this.item);
    }

    public setNodeChain = (values?: string[]) => {
        if (!values)
            values = [];
        values.push(this.item.caption);
        if (this.parentItem) {
            this.parentItem.setNodeChain(values);
            return;
        }
        this.lexicalTree.setCurrentNodeChain(values);
    }

    public expand = (node, onEvent?: boolean) => {
        if (!onEvent)
            this.isExpanded = !this.isExpanded;
        else
            this.isExpanded = true;
        this.lexicalTree.nodeExpanded(node);
        if (!angular.isDefined(node.tree)) {
            this.isLoading = true;
            this.lexicalTree.getNodes(node)
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
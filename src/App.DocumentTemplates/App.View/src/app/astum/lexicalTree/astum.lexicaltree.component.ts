import * as angular from 'angular';
import { DocumentService } from './../../services/document.service';
import { DocTemplateElementValuesPm } from "../../models/pm.models";
import { DocTemplateElementValuesVm } from '../../models/vm.models'
import * as Quill from 'quill';
import * as Delta from 'quill-delta';
import * as $ from 'jquery';

export class AstumLexicalTreeComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };


    constructor() {
        this.controller = AstumLexicalTreeController;
        this.template = require('./astum.lexicaltree.html');
        this.bindings = {
            attributeId: '<',
            valuesTreeId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'lex';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

export class AstumLexicalTreeController implements ng.IComponentController {
    static $inject = ['$element', '$scope', 'DocumentService', '$timeout'];

    public tree: any[];
    public attributeValue: string;
    public textArea: ng.IAugmentedJQuery;

    public initialising: boolean = true;
    public showAddForm: boolean = false;
    public updatingItem: boolean = false;
    public itemName: string;
    public itemContent: string;

    public textModel: string;

    private valuesTreeId: number;
    private attributeTypeCode: string;
    private attributeId: number;
    private ngModelController: ng.INgModelController;

    private container: HTMLElement;
    private editor: Quill.Quill;
    private quill: any = Quill;
    private delta: any = Delta;
    private timer: ng.IPromise<any>;


    //TODO:remove when tree component would be ready
    private configMode: boolean;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private service: DocumentService, private timeout: ng.ITimeoutService) {
    }

    $onInit() {
        this.textArea = this.element.find('textarea');
        this.scope.$on('activateDocElement', this.onActivateDocElement);
        this.scope.$on('closeDocElement', this.onDestroy);
        this.scope.$on('configOpen', this.onDestroy);
        this.scope.$on('configClosed', this.onConfigClose);
        this.ngModelController.$parsers.push(value => {
            return JSON.stringify(value);
        });
        this.ngModelController.$formatters.push(value => {
            return !!value ? JSON.parse(value) : "";
        });
        this.container = $(this.element).find('.textditor').get(0);
        this.ngModelController.$render = this.render;
        this.initialising = false;
    }

    private render = () => {
        this.createEditor();
        this.editor.setContents(this.ngModelController.$viewValue);
    }

    private createEditor = () => {
        if (!this.editor)
            this.editor = new this.quill(this.container,
                {
                    modules: {
                        toolbar: [
                            ['bold', 'italic', 'underline']
                        ]
                    },
                    placeholder: 'Edit text...',
                    theme: 'snow'
                });
    }

   


    private editHendler = (eventName, ...args) => {
        if (eventName === 'text-change') {
            this.ngModelController.$setViewValue(this.editor.getContents());
            // args[0] will be delta
        } else if (eventName === 'selection-change') {
            if (!args[0] && this.range) {
                this.editor.setSelection(this.range);
                return;
            }
            this.range = args[0];
            // args[0] will be old range
        }
    }
    private range: Quill.RangeStatic;

    public clearCurrentNode = (e: JQueryEventObject) => {
        if (!$(e.target).hasClass('node-value') && !$(e.target).hasClass('tree__edit-button') && !this.configMode)
            this.resetNode();
    }

    private onActivateDocElement = (event, data) => {
        this.timer = this.timeout(() => {
            this.editor.focus();
        })

        this.element.on('click', '.bar-left', this.clearCurrentNode);
        this.editor.on('editor-change', this.editHendler);
        if (!this.tree) {
            this.initialising = true;
            this.getData().then((data) => {
                this.tree = data;
                this.initialising = false;
            });
        }
    }

    private onConfigClose = (d) => {
        this.timer = this.timeout(() => {
            this.editor.focus();
        })
        this.element.on('click', '.bar-left', this.clearCurrentNode);
        this.editor.on('editor-change', this.editHendler);
        this.initialising = true;
        this.getData().then(data => {
            this.tree = data;
            this.initialising = false;
        })
    }

    private onDestroy = (e, id) => {
        this.timeout.cancel(this.timer);
        this.element.off('click', '.bar-left', this.clearCurrentNode);
        this.editor.off('editor-change', this.editHendler);
    }

    $onDestroy(): void {
        this.timeout.cancel(this.timer);
        this.element.off('click', '.bar-left', this.clearCurrentNode);
        this.editor.off('editor-change', this.editHendler);
    }



    private onTreeEvent = (event, data) => {
        this.reloadTree(event, data);
    }

    private reloadTree = (event, data) => {
        if (this.valuesTreeId === data.valuesTreeId) {
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
    }


    private _currentNode: DocTemplateElementValuesVm;
    public currentNodeChain: string;


    public get currentNodeContent(): string {
        if (this._currentNode)
            return this._currentNode.contentValue;
    }

    public resetNode() {
        delete this._currentNode;
        delete this.currentNodeChain;
    }

    public set currentNode(value: DocTemplateElementValuesVm) {
        this._currentNode = value;
    }

    public get currentNode() {
        return this._currentNode;
    }

    public nodeExpanded = (node) => {
        this.scope.$broadcast('nodeExpanded', { id: node.id, parent: node.parentId });
    }

    //textarea manipulation


    public insertValue = ($event: MouseEvent, node?: DocTemplateElementValuesVm, useContent?: boolean) => {
        if (!node && !this._currentNode)
            return;
        if (!node && this._currentNode)
            node = this._currentNode;
        let value = node.caption;
        if (useContent) {
            value = node.contentValue;
        }
        if (!value)
            return;
        let startChar: string
        if ($event)
            startChar = this.getStartChar($event);
        if (this.range && this.range.length)
            this.editor.deleteText(this.range.index, this.range.length);
        this.editor.insertText(!!(this.range && this.range.index) ? this.range.index : 0, this._prepareText(value, this.range, startChar));
    }

    public insertValueChain = ($event: MouseEvent) => {
        if (!this.currentNodeChain)
            return;
        let startChar: string
        if ($event)
            startChar = this.getStartChar($event);
        if (this.range && this.range.length)
            this.editor.deleteText(this.range.index, this.range.length);
        this.editor.insertText(!!(this.range && this.range.index) ? this.range.index : 0, this._prepareText(this.currentNodeChain, this.range, startChar));
    }

    private getStartChar($event: MouseEvent) {
        let startChar: string = ' ';
        if ($event && $event.ctrlKey) {
            startChar = '. '
        }
        if ($event && $event.shiftKey) {
            startChar = ', '
        }
        return startChar;
    }

    private _prepareText(newStr: string, range: Quill.RangeStatic, startChar?: string) {
        let prevChar: string;
        if (range && range.index > 0)
            prevChar = this.editor.getText(range.index - 1, 1);
        if (prevChar && prevChar === "." || startChar === ". ")
            newStr = this.capitalizeFirstLetter(newStr);
        startChar = !!(range && range.index) ? startChar : "";
        return startChar + newStr;
    }

    private capitalizeFirstLetter(str: string) {
        return str.charAt(0).toUpperCase() + str.slice(1);
    }

    public setCurrentNodeChain = (values: string[]) => {
        let text = values.reverse().join(' ');
        this.currentNodeChain = text;
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
}

import * as angular from 'angular';
import './astumtextarea.scss';
import { DocTemplateElementValuesPm } from './../../models/pm.models';
import { DocTemplateElementsVm } from './../../models/vm.models';
import { DocumentService } from "./../../services/document.service";
import { ParamsList } from "../../models/common.models";
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import * as Quill from 'quill';
import * as Delta from 'quill-delta';


export class AstumTextareaComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = TextAreaController;
        this.template = require('./astum.textarea.html');
        this.bindings = {
            attributeId: '<',
            valuesTreeId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'control';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

class TextAreaController implements ng.IComponentController {

    static $inject = ['$element', 'DocumentService', '$scope', '$timeout'];

    private addButton: ng.IAugmentedJQuery;
    private textarea: ng.IAugmentedJQuery;
    private listEl: ng.IAugmentedJQuery;
    private currentPosition: number;
    private watchModel: boolean;
    private valuesTreeId: string;
    private attributeTypeCode: string;
    private attributeId: number;
    public textModel: string;
    public searchStr: string;
    public searchPos: number;

    private ngModelController: ng.INgModelController;


    public showTips: boolean;

    private container: HTMLElement;
    private editor: Quill.Quill;
    private quill: any = Quill;
    private delta: any = Delta;
    private initTimer: ng.IPromise<any>;



    constructor(private element: ng.IAugmentedJQuery, private service: DocumentService,
        private scope: ng.IScope,
        private timeout: ng.ITimeoutService) {
        this.element.addClass('dh-text');
        this.addButton = this.element.find('button');
        angular.element(document).bind('click', this.blur);
    }

    $onInit() {
        this.ngModelController.$parsers.push(value => {
            return JSON.stringify(value);
        });
        this.ngModelController.$formatters.push(value => {
            return !!value ? JSON.parse(value) : "";
        });
        this.container = this.element.find(".editor").get(0);
        this.ngModelController.$render = this.onChange;
        this.scope.$on('activateDocElement', this.onActivateDocElement);
        this.scope.$on('closeDocElement', this.onDestroy);
        //this.scope.$on('configOpen', this.onDestroy);
        //this.scope.$on('configClosed', this.onActivateDocElement);
    }

    private createEditor = () => {
        if (!this.editor) {
            let bindings = {
                enter: {
                    key: 13,
                    handler: this.onKey
                },
                backspace: {
                    key: 8,
                    handler: this.onKey
                },
                uparrow: {
                    key: 38,
                    handler: this.onKey
                },
                downarrow: {
                    key: 40,
                    handler: this.onKey
                },
                escape: {
                    key: 27,
                    handler: this.onKey
                }

            }
            this.editor = new this.quill(this.container, {
                modules: {
                    toolbar: [
                        ['bold', 'italic', 'underline']
                    ],
                    keyboard: {
                        bindings: bindings
                    }
                },
                placeholder: 'Edit text...',
                theme: 'snow'
            });
        }
    }

    private onKey = (range, context) => {
        if (!this.showTips)
            return true;
    }

    private onChange = () => {
        this.createEditor();
        this.editor.setContents(this.ngModelController.$viewValue);
    }

    private onActivateDocElement = () => {
        this.initTimer = this.timeout(() => {
            this.editor.focus();
        });
        this.editor.on('editor-change', this.editHendler);
        this.element.bind('keydown', this.keyListener);
        this.element.bind('click', this.clickListener);
    }

    private onDestroy = () => {
        this.timeout.cancel(this.initTimer);
        this.timeout.cancel(this.timer);
        this.editor.off('editor-change', this.editHendler);
        this.element.unbind('keydown', this.keyListener);
        this.element.unbind('click', this.clickListener);
    }

    //private onConfigOpen = () => {
    //    this.editor.off('editor-change', this.editHendler);
    //    this.element.unbind('keydown', this.keyListener);
    //}

    //private onConfigCloserd = () => {
    //    this.editor.on('editor-change', this.editHendler);
    //    this.element.bind('keydown', this.keyListener);
    //}


    $onDestroy() {
        this.timeout.cancel(this.initTimer);
        this.timeout.cancel(this.timer);
        this.editor.off('editor-change', this.editHendler);
        this.element.unbind('keydown', this.keyListener);
        this.element.unbind('click', this.clickListener);
    }


    private editHendler = (eventName, ...args) => {
        if (eventName === 'text-change') {

            this.textChangeHendler(args[0], args[1], args[2]);
            this.ngModelController.$setViewValue(this.editor.getContents());
            // args[0] will be delta
        } else if (eventName === 'selection-change') {
            if (args[0])
                this.processPopUp(args[0]);
            // args[0] will be old range
        }
    }
    //change text for search tips
    private deltas: Array<Delta> = [];
    private timer: ng.IPromise<void>;

    private textChangeHendler = (delta: Delta, oldContents: Delta, source: String) => {
        if (source === 'user') {
            if (!this.timer) {
                let deltaInsert = delta.filter((op) => {
                    return op.hasOwnProperty('insert');
                });
                let isNotWhiteSpace = deltaInsert.length && deltaInsert[0].insert.trim() !== "";
                if (oldContents.ops[0].insert && /(\d |\w)\n?$/.test(oldContents.ops[0].insert) && isNotWhiteSpace) {
                    this.usePrevText(oldContents.ops[0].insert);
                }
                this.timer = this.timeout(this.finish, 1000);
            } else {
                this.timeout.cancel(this.timer);
                this.timer = this.timeout(this.finish, 1000);
            }
            this.deltas.push(delta);
        }
    }

    private usePrevText(insert: string) {
        if (/\n$/.test(insert)) {
            insert = insert.slice(0, -1);
        }
        let dividerReg = /(\s|\.|\,)/g;
        let ind = 0;
        while (dividerReg.test(insert)) {
            ind = dividerReg.lastIndex;
        }

        ind = ind > 0 ? ind : 0;
        let str = !!ind ? insert.slice(ind) : insert;
        this.deltas.push(new Delta().retain(ind).insert(str));
    }

    private finish = () => {
        let query: string;
        let delta = this.deltas.reduce((prev, curr) => {
            return prev.compose(curr);
        });
        this.deltas = [];
        if (!delta.ops || !delta.ops.length || !delta.ops.some((item) => { return item.hasOwnProperty('insert') })) {
            this.timeout.cancel(this.timer);
            delete this.timer;
            return;
        }
        query = delta.filter((op) => {
            return op.hasOwnProperty('insert');
        })[0].insert;
        let start = delta.filter((op) => {
            return op.hasOwnProperty('retain');
        });
        this.searchPos = (start.length && start[0].retain) ? start[0].retain : 0;
        if (/^\s/.test(query)) {
            query = query.trim();
            this.searchPos += 1;
        }
        this.manageTipList(query);
        this.timeout.cancel(this.timer);
        delete this.timer;
    }

    private spaceString(x: number) {
        let str = "";
        for (var i = 1; i < x + 1; i++) {
            str = str + " ";
        }
        return str;
    }


    private manageTipList(str: string) {
        if (str && str.length >= 3) {
            this.searchStr = str;
            this.getTipsList(this.searchStr).then(() => {
                this.showTipList();
            });
        } else {
            this.hideTipList();
        }
    }

    //process adding tips

    private newTipValue: string;
    private processPopUp = (range: Quill.RangeStatic) => {
        this.newTipValue = this.editor.getText(range.index, range.length);
        if (range.length > 3) {
            this.showAddButton(this.editor.getBounds(range.index, range.length));
            return;
        }
        this.hideAddButton();
    }

    private showAddButton(coordinates: Quill.BoundsStatic) {
        this.addButton.css('left', coordinates.left + 46 + 'px');
        this.addButton.css('top', coordinates.top + coordinates.height + 45 + 'px');
        this.addButton.css('display', 'block');
    }




    public list = [];

    public storeTip() {
        if (this.newTipValue)
            this._tipValueInsert(this.newTipValue);
        this.hideAddButton();
    }

    public insertTip(tip) {
        this.editor.deleteText(this.searchPos, this.searchStr.length);
        this.editor.insertText(this.searchPos, tip.caption);
        this.hideTipList();
    }


    private _tipValueInsert(value: string) {
        let tipModel = new DocTemplateElementValuesPm;
        tipModel.valuesTreeId = this.valuesTreeId;
        tipModel.caption = value;
        tipModel.contentValue = value;
        tipModel.recordState = 2;
        tipModel.valueTypeCode = 'L';
        tipModel.originDbRecordId = '';
        tipModel.originDbId = '';
        this.service.docTemplateElementValuesInsert(tipModel).then((data) => {
            debugger;
        });
    }

    private getTipsList(query: string) {
        let params = new ParamsList;
        params['valuesTreeId'] = this.valuesTreeId;
        params['contentValue'] = query;
        return this.service.getDocTemplateElementValuesList(params).then((data) => {
            this.list = data;
        }, error => console.log(error));
    }


    public activeTip: DocTemplateElementValuesVm;
    private showTipList() {
        if (this.list.length) {
            this.activeTip = this.list[0];
            this.showTips = true;
        }
    }

    private hideTipList() {
        this.searchStr = '';
        this.searchPos = 0;
        this.showTips = false;
        this.activeTip = null;
    }

    private clickListener = (e) => {
        this.hideTipList();
    }

    private keyListener = (e) => {
        let key = e.key;
        if (key === 'ArrowDown') {
            e.preventDefault();
            this.scope.$apply(() => {
                this.activeTip = this.getNextActiveTip(1);
            });
        }
        if (key === 'ArrowUp') {
            e.preventDefault();
            this.scope.$apply(() => {
                this.activeTip = this.getNextActiveTip(-1);
            });
        }
        if (key === 'Enter' && this.activeTip) {
            e.preventDefault();
            this.scope.$apply(() => {
                this.insertTip(this.activeTip);
            });
        }
        if (key === 'Backspace') {

            this.scope.$apply(() => {
                this.hideTipList();
            });
        }
        if (key === 'Escape') {
            e.preventDefault();
            this.scope.$apply(() => {
                this.hideTipList();
            });
        }
    }

    private getNextActiveTip = (direction: number) => {
        let length = this.list.length - 1;
        let index = this.list.indexOf(this.activeTip);
        if (index === length && direction > 0) {
            index = -1;
        }
        if (index === 0 && direction < 0) {
            index = length + 1;
        }
        return this.list[index + direction];
    }


    private blur = (event) => {
        if (event.target.closest('.dh-text')) {
            return;
        }
        this.hideAddButton();
        this.searchStr = '';
        this.hideTipList();
    }

    private hideAddButton = () => {
        this.addButton.css('display', 'none');
    }


    //textarea carret position
    //private getCaretCoordinates = (element, position) => {
    //    let _isFirefox = !((<any>window).mozInnerScreenX == null);
    //    let _mirrorDiv: HTMLDivElement;
    //    let _computed: CSSStyleDeclaration;
    //    let _style: CSSStyleDeclaration;
    //    let _properties = [
    //        'boxSizing',
    //        'width',
    //        'height',
    //        'overflowX',
    //        'overflowY',
    //        'borderTopWidth',
    //        'borderRightWidth',
    //        'borderBottomWidth',
    //        'borderLeftWidth',
    //        'paddingTop',
    //        'paddingRight',
    //        'paddingBottom',
    //        'paddingLeft',
    //        'fontStyle',
    //        'fontVariant',
    //        'fontWeight',
    //        'fontStretch',
    //        'fontSize',
    //        'lineHeight',
    //        'fontFamily',
    //        'textAlign',
    //        'textTransform',
    //        'textIndent',
    //        'textDecoration',
    //        'letterSpacing',
    //        'wordSpacing'
    //    ];
    //    _mirrorDiv = this.element.find("div").hasClass('mirror-div') ? <HTMLDivElement>this.element.find("div")[0] : null;
    //    if (!_mirrorDiv) {
    //        let div = angular.element('<div></div>');
    //        _mirrorDiv = <any>div[0];
    //        _mirrorDiv.className = 'mirror-div';
    //        this.element.append(_mirrorDiv);
    //    }

    //    _style = _mirrorDiv.style;
    //    _computed = getComputedStyle(element);

    //    _style.whiteSpace = 'pre-wrap';
    //    if (element.nodeName !== 'INPUT')
    //        _style.wordWrap = 'break-word';

    //    _style.position = 'absolute';
    //    _style.top = element.offsetTop + parseInt(_computed.borderTopWidth) + 'px';
    //    _style.visibility = 'hidden';

    //    _properties.forEach((prop) => {
    //        _style[prop] = _computed[prop];
    //    });

    //    if (_isFirefox) {
    //        _style.width = parseInt(_computed.width) - 2 + 'px';
    //        if (element.scrollHeight > parseInt(_computed.height))
    //            _style.overflowY = 'scroll';
    //    } else {
    //        _style.overflow = 'hidden';
    //    }

    //    _mirrorDiv.textContent = element.value.substring(0, position);
    //    if (element.nodeName === 'INPUT')
    //        _mirrorDiv.textContent = _mirrorDiv.textContent.replace(/\s/g, "\u00a0");

    //    var span = document.createElement('span');
    //    span.textContent = element.value.substring(position) || '.';
    //    _mirrorDiv.appendChild(span);



    //    let coordinates = {
    //        top: span.offsetTop + parseInt(_computed['borderTopWidth']) + parseInt(_computed['paddingTop']) + parseInt(_computed['font-size']) - element.scrollTop,
    //        left: span.offsetLeft + parseInt(_computed['borderLeftWidth'])
    //    };

    //    return coordinates;
    //}


}


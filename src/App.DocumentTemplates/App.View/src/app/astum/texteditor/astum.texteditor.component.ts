import * as angular from 'angular';
import * as Quill from 'quill';
import * as Delta from 'quill-delta';


export class AstumTextEditorComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = TextEditorController;
        this.template = require('./astum.texteditor.html');
        this.bindings = {
            attributeId: '<',
            templateElementId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'control';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

class TextEditorController implements ng.IComponentController {

    private editor: Quill.Quill;
    private quill: any = Quill;
    private delta: any = Delta;


    private ngModelController: ng.INgModelController;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private timeout: ng.ITimeoutService) {

    }

    static $inject = ['$element', '$scope','$timeout'];

    $onInit() {
        this.ngModelController.$parsers.push(value => {
            return JSON.stringify(value);
        });
        this.ngModelController.$formatters.push(value => {
            return !!value? JSON.parse(value): "";
        });
        let container = this.element.find('.textditor').get(0);
        this.editor = new this.quill(container,
            {
                modules: {
                    toolbar: [
                        ['bold', 'italic', 'underline']
                    ]
                },
                placeholder: 'Edit text...',
                theme: 'snow'
            });
        this.scope.$on('activateDocElement', this.onActivateDocElement);
        this.scope.$on('closeDocElement', this.onDestroy);
        //this.editor.on('editor-change', this.editHendler);
        //this.editor.setSelection(0, 0, "api");
        //this.editor.on('text-change', this.textChangeHendler);
        let watcher = this.scope.$watch(() => { return this.ngModelController.$viewValue }, (newVal, oldVal) => {
            if (angular.isDefined(newVal)) {
                this.editor.setContents(newVal);
                watcher();
            }
        });
    }

    private onActivateDocElement = () => {
        this.editor.setSelection(0, 0, "api");
        this.editor.on('editor-change', this.editHendler);
    }

    private onDestroy = () => {
        this.editor.off('editor-change', this.editHendler);
    }

    $onDestroy() {
        this.editor.off('editor-change', this.editHendler);
    }
    private editHendler = (eventName, ...args) => {
        if (eventName === 'text-change') {
            this.ngModelController.$setViewValue(this.editor.getContents());
            // args[0] will be delta
        } else if (eventName === 'selection-change') {
            // args[0] will be old range
        }
    }




}


//this.editor.container.getElementsByClassName("ql-editor")[0].innerHTML
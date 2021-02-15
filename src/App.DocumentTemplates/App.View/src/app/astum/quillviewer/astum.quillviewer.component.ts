import * as angular from 'angular';
import * as Quill from 'quill';



export class AstumQuillViewerComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = QuillViewerController;
        this.template = require('./astum.quillviewer.html');
        this.bindings = {
            value: '<',
        }
        this.controllerAs = 'viewer';
    }
}

class QuillViewerController implements ng.IComponentController {

    static $inject = ['$scope','$timeout'];

    private editor: Quill.Quill;
    private quill: any = Quill;
    private value: string;

    public viewValue: string;

    constructor(private scope: ng.IScope) {

    }
    $onInit() {
        this.render();
        this.scope.$watch(() => this.value, this.changeValue);
    }
    $onChnges(changes) {
    }

    $onDestroy() {

    }

    private render() {
        let element = document.createElement('div');
        let viewer = new this.quill(element);
        if (this.value)
        viewer.setContents(JSON.parse(this.value));
        let value = viewer.container.getElementsByClassName("ql-editor")[0].innerHTML;
        
        this.viewValue = value === "<p><br></p>" ? "" : value;
        element.remove();
    }

    private changeValue = (old, val) => {
        if (old !== val)
            this.render();
    }

}
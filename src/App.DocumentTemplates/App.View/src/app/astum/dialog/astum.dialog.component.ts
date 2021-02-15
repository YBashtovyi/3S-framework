import * as angular from 'angular';


export class AstumDialogComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = AstumDialogController;
        this.template = require('./dialog.html');
        this.bindings = {
            config: '<',
            onAnswer:'<'
        }
        this.controllerAs = 'dialog';
    }
}

export class AstumDialogController implements ng.IComponentController {

    public config: any;
    private onAnswer: Function;


    constructor() {
    }

    $onInit() {

    }

    $onDestroy() { }

    public close(answer: boolean) {
        if(this.onAnswer)
        this.onAnswer(answer);  
    }
}
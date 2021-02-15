import * as angular from 'angular';


export class AstumBreadCrumbsComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;
    bindings: { [index: string]: string; };

    constructor() {
        this.controller = AstumBreadCrumbsController;
        this.template = require('./breadcrumbs.html');
        this.controllerAs = 'bread';
        this.bindings = {
            crumbs: '<',
            onClick:'&'
        }
    }
}

export class AstumBreadCrumbsController implements ng.IComponentController {

    public crumbs: any;
    private onClick: (any) => void;
    constructor() {

    }

    $onInit() {
    }

    public goTo(item: any) {
        this.onClick({ crumb:item });
    }

}
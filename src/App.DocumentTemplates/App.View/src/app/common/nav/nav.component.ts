import { Ng1StateDeclaration } from '@uirouter/angularjs';
import { NavService, NavItem } from './nav.service';

export class NavComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    transclude: boolean;
    

    constructor() {
        this.controller = NavController;
        this.template = require('./nav.html');
        this.transclude = true;
    }
}

class NavController implements ng.IComponentController {
    pages: Array<NavItem>;
    currentPage: string;

    constructor(private NavService: NavService) {
        "ngInject";
    }

    $onInit() {
        this.pages = this.NavService.pages;
    }
}
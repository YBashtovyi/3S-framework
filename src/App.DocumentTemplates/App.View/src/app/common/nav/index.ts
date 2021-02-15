import { StateProvider } from '@uirouter/angularjs';
import * as angular from 'angular';
import { NavComponent } from './nav.component';
import { NavService } from './nav.service';


const Nav: ng.IModule = angular
    .module('common.nav', [])
    .config(($stateProvider: StateProvider) => {
        "ngInject";

    })
    .service('NavService', NavService)
    .component('nav', new NavComponent);

export default <string>Nav.name;
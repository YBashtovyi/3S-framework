import { StateProvider } from '@uirouter/angularjs';
import * as angular from 'angular';
import { NotFoundComponent } from './not-found.component';


const NotFound: ng.IModule = angular
    .module('common.notFound', [])
    .config(($stateProvider: StateProvider) => {
        "ngInject";

    })
    .component('notFound', new NotFoundComponent);

export default <string>NotFound.name;
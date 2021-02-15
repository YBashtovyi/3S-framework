import * as angular from 'angular';
import { StateProvider } from '@uirouter/angularjs';
import { AccountComponent } from "./account.controller";
import { ServicesComponent } from "./services.component";

function routeConfig($stateProvider: StateProvider) {
    "ngInject";
    $stateProvider
        .state('app.account', {
            abstract: true
        })
        .state('app.account.login', {
            url: '/login',
            component: 'account',
        })
        .state('app.account.services', {
            url: '/services',
            component:'services'
        })
}


const Account: ng.IModule = (angular
    .module('components.account', [])
    .component('account', new AccountComponent)
    .component('services',new ServicesComponent)
    .config(routeConfig)) as any;
export default <string>Account.name;
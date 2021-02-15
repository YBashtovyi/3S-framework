import * as angular from 'angular';

import { StateService, StateProvider } from '@uirouter/angularjs';
import { ConstructorComponent } from './constructor.component';
import { NavService, NavItem } from './../../common/nav/nav.service';
import { ConstructorService } from './../../services/constructor.service';
import { TemplateElementComponent } from './template-element/template-element.component';
import { TemplateElementsComponent } from './template-elements/template-elements.component';



function routeConfig($stateProvider: StateProvider) {
    "ngInject";


    $stateProvider
        .state('app.constructor', {
            url: '/constructor/:id?/parentId:?/copyId:?',
            component: 'templateConstructor',
            params: {
                id: { squash: true, value: null, dynamic:true },
                parentId: { squash: true, value: null },
                copyId: { squash: true, value: null }
            }
        });

}
function runConfig(NavService: NavService) {
    const page: NavItem = {
        state: 'app.constructor',
        url: '/',
        label: 'Constructor'
    };
    NavService.addNavItem(page);

}


const Constructor: ng.IModule = (angular
    .module('components.constructor', [])
    .service('ConstructorService', ConstructorService)
    .component('templateConstructor', new ConstructorComponent)
    .component('templateElement', new TemplateElementComponent)
    .component('templateElements', new TemplateElementsComponent)
    .config(routeConfig)) as any;
export default <string>Constructor.name;
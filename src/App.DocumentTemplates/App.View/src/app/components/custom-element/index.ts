import * as angular from 'angular';

import { StateService, StateProvider, Ng1StateDeclaration } from '@uirouter/angularjs';
import { CustomElementComponent } from './custom-element.component';
import { NavService, NavItem } from './../../common/nav/nav.service';
import { CustomElementService } from './custom-element.service';




function routeConfig($stateProvider: StateProvider) {
    "ngInject";


    $stateProvider
        .state('app.customelement', {
            url: '/customelement/:id',
            component: 'customElement',
            params: {
                id: { squash: true, value: null, dynamic: true }
            }
        })
        .state('app.customelement.add', {
            url: '/customelement/add',
            component: 'customElement',
            params: {
                controlCode: { squash: true, value: null },
                parentId: { squash: true, value: null }
            }
        });

}
function runConfig(NavService: NavService) {
    const page: NavItem = {
        state: 'app.customelement',
        url: '/',
        label: 'Customelement'
    };
    NavService.addNavItem(page);

}

const CustomElement: ng.IModule = (angular
    .module('components.customelements', [])
    .component('customElement', new CustomElementComponent)
    .service('customElementService', CustomElementService)
    .config(routeConfig)
    //.run(runConfig)
) as any;

export default <string>CustomElement.name;
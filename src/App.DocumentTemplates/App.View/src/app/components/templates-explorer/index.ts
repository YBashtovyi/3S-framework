import * as angular from 'angular';

import { StateProvider, Ng1StateDeclaration } from '@uirouter/angularjs';
import { NavService, NavItem } from './../../common/nav/nav.service';
import { TemplatesExplorerComponent } from './templates-explorer.component';
//import { TemplatesExplorerService } from './templates-explorer.service';
import { TemplateMoveComponent } from './template-move.controller';




function routeConfig($stateProvider: StateProvider) {
    "ngInject";


    $stateProvider
        .state('app.templates', {
            url: '/templates/:parentId?/:rootName?',
            component: 'templatesExplorer',
            params: {
                parentId: { squash: true, value: null },
                rootName: { squash: true, value:null},
                searchText: {squash: true, value:null}
            }
        });

}
function runConfig(NavService: NavService) {
    const page: NavItem = {
        state: 'app.templates',
        url: '/templates',
        label: 'Шаблони'
    };
    NavService.addNavItem(page);

}

const Templates: ng.IModule = (angular
    .module('components.templates-explorer', [])
    .component('templatesExplorer', new TemplatesExplorerComponent)
    .component('templateMove',new TemplateMoveComponent)
    
    //.service('templatesExplorerService', TemplatesExplorerService)
    .config(routeConfig)
    .run(runConfig)) as any;

export default <string>Templates.name;
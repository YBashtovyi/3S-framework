import * as angular from 'angular';
import { StateService, StateProvider, Ng1StateDeclaration } from '@uirouter/angularjs';
import { DocsExplorerComponent } from './docsexplorer.component';
import { NavService, NavItem } from './../../common/nav/nav.service';
import { DocMoveComponent } from "./doc-move.component";

function routeConfig($stateProvider: StateProvider) {
    "ngInject";


    $stateProvider
        .state('app.docsExplorer', {
            url: '/docsExplorer/:parentId?',
            component: 'docsExplorer',
            params: {
                parentId: { squash: true, value: null }
            }
        });

}
function runConfig(NavService: NavService) {
    const page: NavItem = {
        state: 'app.docsExplorer',
        url: '/docsExplorer',
        label: 'Документи'
    };
    NavService.addNavItem(page);

}

const DocsExplorer: ng.IModule = angular
    .module('components.docsexplorer', [

    ])
    .component('docsExplorer', new DocsExplorerComponent)
    .component('docMove', new DocMoveComponent)
    .config(routeConfig)
    .run(runConfig);

export default <string>DocsExplorer.name;
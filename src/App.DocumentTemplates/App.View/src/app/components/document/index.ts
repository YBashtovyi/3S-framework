import * as angular from 'angular';

import { StateService, StateProvider, Ng1StateDeclaration } from '@uirouter/angularjs';
import { DocumentComponent } from './document.component';
import { DocumentService } from './../../services/document.service';
import { NavService, NavItem } from './../../common/nav/nav.service';
import { DocumentPrintService } from "../../services/documentPrint.service";


function routeConfig($stateProvider: StateProvider) {
    "ngInject";


    $stateProvider
        .state('app.document', {
            url: '/document/:id/:templId?/:templCode?/:parentId?',
            component: 'document',
            params: {
                id: { squash: true, value: null },
                templId: { squash: true, value: null },
                templCode: { squash: true, value: null },
                isFrame: { squash: true, value: null },
                system: { squash: true, value: null },
                parentId: { squash: true, value: null },
                entityType: {squash: true, value: null},
                usePreview: {squash: true, value: null}
            }
        });

}
//function runConfig(NavService: NavService) {
//    const page: NavItem = {
//        state: 'app.document',
//        url: '/',
//        label: 'Документи'
//    };
//    NavService.addNavItem(page);

//}

const Document: ng.IModule = (angular
    .module('components.document', [])
    .service('DocumentService', DocumentService)
    .service('DocumentPrintService', DocumentPrintService)
    .component('document', new DocumentComponent)
    .config(routeConfig))as any;
    //.run(runConfig)) 

export default <string>Document.name;
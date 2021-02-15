
import * as angular from 'angular';

import { AstumBreadCrumbsComponent } from './astum.breadcrumbs.component';

const BreadCrumbs: ng.IModule = angular
    .module('astum.breadcrumbs', [])
    .component('breadCrumbs', new AstumBreadCrumbsComponent);


export default <string>BreadCrumbs.name;
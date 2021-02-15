import * as angular from 'angular';

import { AstumCheckListComponent } from './astum.checklist.component';

const CheckList: ng.IModule = angular
    .module('astum.checklist', [])
    .component('astumCheckList', new AstumCheckListComponent);


export default <string>CheckList.name;
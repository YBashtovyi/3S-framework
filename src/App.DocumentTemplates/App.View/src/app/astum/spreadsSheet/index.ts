import * as angular from 'angular';

import { AstumSpreadsSheetComponent } from './astum.spreadsSheet.component';

const SpreadsSheet: ng.IModule = angular
    .module('astum.spreadSheet', [])
    .component('astumSpreadsSheet', new AstumSpreadsSheetComponent);


export default <string>SpreadsSheet.name;
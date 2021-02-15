import * as angular from 'angular';

import { AstumTemplateCopyComponent } from './astum.templatecopy.component';
const TemplateCopy: ng.IModule = angular
    .module('astum.templatecopy', [])
    .component('astumTemplateCopy', new AstumTemplateCopyComponent);
export default <string>TemplateCopy.name;
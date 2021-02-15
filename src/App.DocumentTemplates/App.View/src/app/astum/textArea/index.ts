import * as angular from 'angular';

import { AstumTextareaComponent } from './astum.textarea.component';

const TeaxtArea: ng.IModule = angular
    .module('astum.textarea', [])
    .component('astumTextarea', new AstumTextareaComponent);


export default <string>TeaxtArea.name;
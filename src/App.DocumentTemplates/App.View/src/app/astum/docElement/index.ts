import * as angular from 'angular';
import AstumTextarea from './../textArea';

import { AstumDocElementComponent } from './astum.docelement.component';

const docElement: ng.IModule = angular
    .module('astum.docelement', [])
    .component('astumDocElement', new AstumDocElementComponent);


export default <string>docElement.name;
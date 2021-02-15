import * as angular from 'angular';

import { AstumTextBlockComponent } from './astum.textblock.component';

const TeaxtBlock: ng.IModule = angular
    .module('astum.textblock', [])
    .component('astumTextBlock', new AstumTextBlockComponent);


export default <string>TeaxtBlock.name;
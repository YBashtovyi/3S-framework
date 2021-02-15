import * as angular from 'angular';

import { AstumTextEditorComponent } from './astum.texteditor.component';

const TeaxtEditor: ng.IModule = angular
    .module('astum.texteditor', [])
    .component('astumTextEditor', new AstumTextEditorComponent);


export default <string>TeaxtEditor.name;
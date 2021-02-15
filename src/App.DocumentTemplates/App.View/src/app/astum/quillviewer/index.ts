import * as angular from 'angular';

import { AstumQuillViewerComponent } from './astum.quillviewer.component';

const QuillViewer: ng.IModule = angular
    .module('astum.quillviewer', [])
    .component('astumQuillViewer', new AstumQuillViewerComponent);


export default <string>QuillViewer.name;
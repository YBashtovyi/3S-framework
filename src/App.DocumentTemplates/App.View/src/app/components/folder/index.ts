import * as angular from 'angular';

import { FolderComponent } from './folder.component';

const Calendar: ng.IModule = angular
    .module('components.folder', [])
    .component('folder', new FolderComponent);


export default <string>Calendar.name;
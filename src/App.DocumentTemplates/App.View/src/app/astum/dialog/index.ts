import * as angular from 'angular';

import { AstumDialogComponent } from './astum.dialog.component';
const Dialog: ng.IModule = angular
    .module('astum.dialog', [])
    .component('astumDialog', new AstumDialogComponent);
export default <string>Dialog.name;
import * as angular from 'angular';

import { DropDownComponent } from './dropdown.component';


const AstumDropDown: ng.IModule = angular
    .module('astum.dropDown', [])
    .component('dropDown', new DropDownComponent);

   

export default <string>AstumDropDown.name;
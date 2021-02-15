import * as angular from 'angular';
import { AstumListConfigureComponent } from './list.configure.component';
import { AstumValuesConfigureComponent } from './values.configure';
import { AstumTreeConfigureComponent } from './tree.configure.component';
import { AstumTreeItemConfigureComponent } from './tree.item.configure.component';


const ElemenTConfig: ng.IModule = angular
    .module('astum.elementconfig', [])
    .component('astumListConfigure', new AstumListConfigureComponent)
    .component('astumValuesConfigure', new AstumValuesConfigureComponent)
    .component('astumTreeConfigure', new AstumTreeConfigureComponent).
    component('astumTreeItemConfigure', new AstumTreeItemConfigureComponent);


export default <string>ElemenTConfig.name;
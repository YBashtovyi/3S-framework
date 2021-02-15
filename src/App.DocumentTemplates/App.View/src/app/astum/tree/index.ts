import * as angular from 'angular';

import { TreeComponent } from './tree.component';
import { TreeItemComponent } from './treeItem.component';


const AstumTree: ng.IModule = angular
    .module('astum.tree', [])
    .component('tree', new TreeComponent)
    .component('treeItem', new TreeItemComponent);
   

export default <string>AstumTree.name;
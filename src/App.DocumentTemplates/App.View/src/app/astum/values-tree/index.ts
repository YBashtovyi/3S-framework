import * as angular from 'angular';
import { ValuesTreeComponent } from './valuestree.component';

const valuesTree: ng.IModule = angular
    .module('astum.valuesTree', [])
    .component('astumValuesTree', new ValuesTreeComponent);

export default <string>valuesTree.name;
import * as angular from 'angular';
import './astum.lexicaltree.scss';

import { AstumLexicalTreeComponent } from './astum.lexicaltree.component';
import { AstumLexicalTreeItemComponent } from './astum.lexicaltreeItem.component';


const AstumLexical: ng.IModule = angular
    .module('astum.lexicalTree', [])
    .component('astumLexicalTree', new AstumLexicalTreeComponent)
    .component('astumLexicalTreeItem', new AstumLexicalTreeItemComponent);
   

export default <string>AstumLexical.name;
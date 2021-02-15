import * as angular from 'angular';

//import Home from './home';
//import About from './about';
import Document from './document';
import Constructor from './constructor';
import Templates from './templates-explorer';
import CustomElement from './custom-element';
//import DocsExplorer from './docs-explorer';
import Folder from './folder';
import Account from './account';


const Components: ng.IModule = angular
.module('app.components', [
    //Home,
    //About,
    Document,
    Constructor,
    Templates,
    CustomElement,
    //DocsExplorer,
    Folder,
    Account
]);

export default <string>Components.name;
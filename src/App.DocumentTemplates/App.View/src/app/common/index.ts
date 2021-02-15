import * as angular from 'angular';
import Nav from './nav';
// import NotFound from './not-found'

const Common: ng.IModule = angular
.module('app.common', [
    Nav
]);

export default <string>Common.name;
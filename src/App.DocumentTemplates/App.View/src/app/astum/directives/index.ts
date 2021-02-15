import * as angular from 'angular';

import { AstumTransclude } from './astum.transclude.directive';


const AstumDirectives: ng.IModule = angular
    .module('astum.Directives', [])
    .directive('astumTransclude',AstumTransclude);
   

export default <string>AstumDirectives.name;
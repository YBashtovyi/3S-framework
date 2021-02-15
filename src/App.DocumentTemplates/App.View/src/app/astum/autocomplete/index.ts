import * as angular from 'angular';
import './autocomplete.scss';

import { AstumAutocompleteComponent } from './astum.autocomplete.component';

const Autocomplete: ng.IModule = angular
    .module('astum.autocomplete', [])
    .component('astumAutocomplete', new AstumAutocompleteComponent);


export default <string>Autocomplete.name;
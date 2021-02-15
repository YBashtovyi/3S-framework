import * as angular from 'angular';

import { AstumModalComponent } from './astum.modal.component';
import { AstumModalService } from './astum.modal.service';


const Modal: ng.IModule = angular
    .module('astum.modal', [])
    .component('astumModal', new AstumModalComponent)
    .service('modalService',  AstumModalService);


export default <string>Modal.name;
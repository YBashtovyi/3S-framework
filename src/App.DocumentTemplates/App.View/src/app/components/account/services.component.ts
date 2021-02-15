import * as angular from 'angular';
import { IAuthService, IOwner } from './../../astum/services/auth/auth.service';
import './services.scss';


import { StateService } from '@uirouter/angularjs';




export class ServicesComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = ServicesController;
        //this.template = require('./services.html');
        this.controllerAs = 'services';
    }
}

export class ServicesController implements ng.IComponentController {

    static $inject = ['$window','authService','$state'];

    public owners: IOwner[];

    constructor(private window: ng.IWindowService,
                private authService:IAuthService,
                protected state: StateService) {

    }

    $onInit() {
        //this.owners = this.authService.getOwners();
    }
    $onDestroy() {

    }

    public setOwner(owner: IOwner) {
        //this.authService.setCurrentOwner(owner);
        this.go();
    }
    public isSelected(owner: IOwner) {
        return owner.ownerId === this.authService.getCurrentOwner();
    }
    public go(){
        this.state.go('app.home');
    }
}
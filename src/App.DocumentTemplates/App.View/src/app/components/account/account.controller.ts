import * as angular from 'angular';
import { IAuthService } from './../../astum/services/auth/auth.service';
import { StateService } from '@uirouter/angularjs';

export class AccountComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;

    constructor() {
        this.controller = AccountController;
        this.template = require('./account.html');
        this.controllerAs = 'account';
    }
}


export class AccountController implements ng.IComponentController {
    static $inject = ['authService','$state']

    public username: string;
    public password:string;

    constructor(private auth: IAuthService, private state: StateService) {

    }

    $onInit() {

    }

    $onDestroy() {

    }

    public login() {
        this.auth.login(this.username, this.password).then((organizations) => {
            if (organizations)
                this.state.go('app.account.services');
        });
    }
} 
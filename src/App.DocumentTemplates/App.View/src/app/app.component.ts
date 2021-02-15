
import { IAuthService } from './astum/services/auth/auth.service';
import { IFrameService } from './astum/services/frame/frame.service';
import { StateService } from '@uirouter/angularjs';


/**
 * App Component
 *
 * @export
 * @class AppComponent
 * @implements {ng.IComponentOptions}
 */
export class AppComponent implements ng.IComponentOptions {
    template: string;
    controller: ng.IControllerConstructor;
    controllerAs:string;

    constructor() {
        this.template = require("./app.html");
        this.controller = AppController;
        this.controllerAs = 'app';
    }
};

/**
 * App Controller
 *
 * @class AppController
 * @implements {ng.IComponentController}
 */
export class AppController implements ng.IComponentController {

    static $inject = ['$window', 'frameService', '$scope', 'authService','$state'];

    constructor(private $window: ng.IWindowService,
        private frameService: IFrameService,
        private $scope: ng.IScope,
        private authService: IAuthService,
        private state: StateService) {
        if (!this.authService.isAuth && !frameService.isEmbed)
            this.login();
        this.$scope.$on("logOut", () => {
            this.login();
        })
    }
    public get isEmbed(): boolean {
        return this.frameService.isEmbed;
    };

    $onInit() {

    }

    $onChanges(changes: ng.IOnChangesObject) {

    }
    public get isAuth() {
        return this.authService.isAuth;
    }

    public login() {
        this.state.go('app.account.login');
    }

    public logout() {
        this.authService.logOut();
    }
}
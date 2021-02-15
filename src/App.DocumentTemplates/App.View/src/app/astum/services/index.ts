import * as angular from 'angular';
import { DataServiceProvider } from './data.service';
import { AuthServiceProvider, AuthService } from './auth/auth.service';
import { AuthInterceptor } from './auth/auth.interseptor';
import { FrameService } from './frame/frame.service';
import { DragManagerService } from './dragManager/dragmanager.service';
import { StateParams, StateProvider, StateService, Ng1StateDeclaration } from '@uirouter/angularjs';
import * as moment from 'moment';
import 'moment-timezone';
(<any>moment).tz.setDefault((<any>moment).tz.guess() || 'Europe/Kiev');

//const authCallbackController = (auth: AuthService, state: StateService, stateParams: StateParams) => {
//    console.debug('oidc-angular: handling login-callback');
//    auth.signinRedirectCallback(stateParams['data']).then(() => {
//        (<any>state).go('app.home');
//    }, error => {
//        debugger;
//    })
//}

//authCallbackController.$inject = ['authService', '$state', '$stateParams'];

//const routeConfig = ($stateProvider: StateProvider) => {


//    $stateProvider
//        .state('authcallback', {
//            url: '/authcallback/:data?',
//            controller: authCallbackController,
//            template: "<div>authLoginCallback</div>",
//            params: {
//                data: { squash: true, value: null }
//            }
//        });

//}
//routeConfig.$inject = ['$stateProvider'];


const Services: ng.IModule = angular
    .module('astum.services', [])
    .provider('dataService', DataServiceProvider)
    .provider('authService', AuthServiceProvider)
    .service('frameService', FrameService)
    .service("authInterceptor", AuthInterceptor)
    .service('dragManager', DragManagerService);
    //.config(httpConfig);
    //.config(routeConfig);



export default <string>Services.name;
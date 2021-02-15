import * as angular from 'angular';
import { UrlRouterProvider, StateProvider } from '@uirouter/angularjs';
import { AppComponent } from './app.component';
import Common from './common';
import Components from './components';
import Astum from './astum';
import { GlobalConfig } from './services/global.config';
import { IDataServiceProvider } from './astum/services/data.service';
import "font-awesome/scss/font-awesome.scss";
import * as moment from 'moment';
import './app.scss';
import { IIdentitySettings } from "./astum/services/auth/auth.service";


function routeConfig(
    $locationProvider: ng.ILocationProvider,
    $urlRouterProvider: UrlRouterProvider,
    $stateProvider: StateProvider
) {
    "ngInject";
    $stateProvider
        .state('app', {
            redirectTo: 'app.home',
            abstract: true,
            component: 'app'
        });

    //$locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/');
}

function themeConfig($mdThemingProvider: ng.material.IThemingProvider) {
   // "ngInject";
    $mdThemingProvider.theme('default')
        .primaryPalette('teal')
        .accentPalette('blue-grey');
}
themeConfig.$inject = ['$mdThemingProvider'];


function dataConfig(dataService: IDataServiceProvider) {
    dataService.setBaseUrl(GlobalConfig.instance.baseUrl);
}
dataConfig.$inject = ['dataServiceProvider'];

const settings: Oidc.UserManagerSettings = {
    authority: GlobalConfig.instance.identityUrl,
    client_id: "Dicom",
    redirect_uri: GlobalConfig.instance.appRoot+"/#/authcallback/",
    response_type: "id_token token",
    scope: "DtmApiScope IdentityApiScope openid offline_access",
    post_logout_redirect_uri: GlobalConfig.instance.appRoot
};

const identitySettings: IIdentitySettings = {
    authority: GlobalConfig.instance.identityUrl,
    clientId: "DtmApi",
    scopes: "DtmApiScope IdentityApiScope openid offline_access"
}

const datesToMomentsResp = (response): any => {

    if (angular.isArray(response))
        return response.map(function (item) {
            return datesToMomentsResp(item);
        });
    if (angular.isObject(response)) {
        var pattern = /\d{4}-\d{2}-\d{2}\T\d{2}:\d{2}:\d{2}(.\d*)?Z/;
        var patternWithTz = /\d{4}-\d{2}-\d{2}\T\d{2}:\d{2}:\d{2}\+\d{2}:\d{2}/;
        for (var key in response) {
            if (angular.isArray(response[key])) {
                response[key] = datesToMomentsResp(response[key]);
            }
            if (pattern.test(response[key])) {
                response[key] = moment(response[key]);
            }
            if (patternWithTz.test(response[key])) {
                response[key] = moment(response[key]);
                //response[key] = mom.subtract(mom.utcOffset(), 'm');
            }
        }
        return response;
    }
    return response;
}

const IsJsonString = (str) => {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

const momentToStringRequest = (request): any => {
    if (angular.isArray(request))
        return request.map(function (item) {
            return momentToStringRequest(item);
        });
    if (angular.isObject(request)) {

        for (var key in request) {
            if (angular.isArray(request[key])) {
                request[key] = momentToStringRequest(request[key]);
            }
            if (moment.isMoment(request[key])) {
                request[key] = request[key].format();
            }
        }
        return request;
    }
    return request;
}

const interseptorConfigFn = (httpProvider: ng.IHttpProvider) => {
    httpProvider.defaults.withCredentials = true;
	(<Array<ng.IHttpRequestTransformer>>httpProvider.defaults.transformRequest).unshift((request) => {
        return momentToStringRequest(request);
    });
    httpProvider.interceptors.push('authInterceptor');
    (<Array<ng.IHttpResponseTransformer>>httpProvider.defaults.transformResponse).push((response) => {
        return datesToMomentsResp(response);
    });
};

interseptorConfigFn.$inject = ['$httpProvider'];

const authProviderConfigFn = (AuthServiceProvider) => {
    AuthServiceProvider.configure(identitySettings);
};
authProviderConfigFn.$inject = ['authServiceProvider'];

const changeState = (rootScope: ng.IRootScopeService) => {
    rootScope.$on("$stateChangeStart", () => {
        console.log('changeState');
    })
};
changeState.$inject = ['$rootScope'];

const App: ng.IModule = angular
    .module('app', [
        'ui.router',
        'ngMessages',
        'ngMaterial',
        'ngAria',
        'ngAnimate',
        // 'ngCookies',
        // 'ngSanitize',
        Common,
        Components,
        Astum
    ])

    .config(routeConfig)
    .config(themeConfig)
    .config(dataConfig)
    .config(authProviderConfigFn)
    .config(interseptorConfigFn)
    .component('app', new AppComponent)
    .run(changeState);


export default <string>App.name;
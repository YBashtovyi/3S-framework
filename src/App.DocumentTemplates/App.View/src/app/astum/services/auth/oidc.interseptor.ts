import { IOidcService } from './oidc.service';
import { IFrameService } from '../frame/frame.service';
import { IDataService } from '../data.service';
import * as Oidc from 'oidc-client';


export class AuthInterceptor implements ng.IHttpInterceptor {

    static $inject = ['authService', 'frameService', '$injector'];
    constructor(private auth: IOidcService, private frame: IFrameService,
        private injector: ng.auto.IInjectorService) {

    }

    private frameTokenPromise: any = null;

    public request = (config: ng.IRequestConfig): ng.IRequestConfig | ng.IPromise<ng.IRequestConfig> => {
        config.headers = config.headers || {};
        if (this.frame.isEmbed) {
            (<any>config.headers).Authorization = 'Bearer ' + this.frame.token;
            return config;
        }
        return (<ng.IPromise<Oidc.User>>this.auth.getUser()).then((user) => {
            if (user && user.access_token) {
                (<any>config.headers).Authorization = 'Bearer ' + user.access_token;
                //console.info('access_token:' + user.access_token);
                return config;
            }

            console.info('AuthInterceptor! Oidc.User:null,return config without header.');
            return config;
        },
            (error) => {
                console.log('ON Oidc.getUser() promise reject.');
                console.log('ERROR:');
                console.info(error);
                console.log('RequestConfig:');
                console.info(config);

                return error;
            },
        )

    }
    public responseError = (rejection: ng.IHttpPromiseCallbackArg<any>): Promise<any> => {
        let p = new Promise((resolve, reject) => {
            switch (rejection.status) {
                case 401:
                    if (this.frame.isEmbed) {
                        if (!this.frameTokenPromise) {
                            this.frameTokenPromise = this.frame.sendTokenRequest();
                        }
                        this.frameTokenPromise.then(() => {
                            this.frameTokenPromise = null;
                            this.injector.get<ng.IHttpService>("$http")(rejection.config).then((data) => {
                                resolve(data);
                            }, (error) => {
                                reject(error);
                            });
                        })
                        break;
                    }
                    this.auth.login();
                    reject(rejection);
                    break;
                case 403:
                    alert('dont enaught rights');
                    reject(rejection);
                    break;
                default:
                    console.log(rejection);
                    reject(rejection);
                    break;
            }
        });
        return p;

    }
}
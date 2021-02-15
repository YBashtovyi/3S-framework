import { IAuthService } from './auth.service';
import { IFrameService } from '../frame/frame.service';
import { IDataService } from '../data.service';
import * as Oidc from 'oidc-client';


export class AuthInterceptor implements ng.IHttpInterceptor {

    static $inject = ['$q', '$injector'];
    constructor(private q: ng.IQService, private injector: ng.auto.IInjectorService) {
        
        
    }
    private frameTokenPromise: any = null;
    private refreshRequestInProgress: any = null; 

    public request = (config: ng.IRequestConfig): ng.IRequestConfig | ng.IPromise<ng.IRequestConfig> => {
        let auth: IAuthService = this.injector.get<IAuthService>('authService');
        let frame: IFrameService = this.injector.get<IFrameService>('frameService');
        config.headers = config.headers || {};
        if (frame.isEmbed) {
            (<any>config.headers).Authorization = 'Bearer ' + frame.token;
            (<any>config.headers).Owner = frame.ownerId;
            return config;
        }

        let token = auth.getToken();
        if (token && !(<any>config).loginRequest) {
            (<any>config.headers).Authorization = 'Bearer ' + token;
        }
        let ownerId = auth.getCurrentOwner();
        if (ownerId && !(<any>config).loginRequest)
            (<any>config.headers).Owner = ownerId;

        return config;
    }


    public responseError = (rejection: ng.IHttpPromiseCallbackArg<any>): Promise<any> => {
        let auth: IAuthService = this.injector.get<IAuthService>('authService');
        let frame: IFrameService = this.injector.get<IFrameService>('frameService');
        let p = new Promise((resolve, reject) => {
            switch (rejection.status) {
                case 401:
                    if (frame.isEmbed) {
                        if (!this.frameTokenPromise) {
                            this.frameTokenPromise = frame.sendTokenRequest(true);
                        }
                        this.frameTokenPromise.then(() => {
                            this.frameTokenPromise = null;
                            this.injector.get<ng.IHttpService>("$http")(rejection.config).then((data) => {
                                    resolve(data);
                                },
                                (error) => {
                                    reject(error);
                                });
                        });
                        break;
                    }
                    if (!frame.isEmbed) {
                        if (!this.refreshRequestInProgress)
                            this.refreshRequestInProgress = auth.refreshToken().then((resp) => {
                                this.refreshRequestInProgress = null;
                                this.injector.get<ng.IHttpService>("$http")(rejection.config)
                                    .then((resp) => {
                                        resolve(resp);
                                    },
                                    (resp) => {
                                        reject(resp);
                                    });
                            }, (error) => {
                                this.refreshRequestInProgress = null;
                                console.error("Interceptor error:")
                                console.error(error);
                                reject(error)
                                })
                    }
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
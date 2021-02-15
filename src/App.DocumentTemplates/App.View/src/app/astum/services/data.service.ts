import * as angular from 'angular';


export interface IDataService {
    getQuery<T>(urn: string, config: ng.IRequestShortcutConfig, needSession?: boolean): ng.IPromise<T>;
    postQuery<T>(urn: string, data: any, config: ng.IRequestShortcutConfig): ng.IPromise<T>;
}

export interface IDataServiceProvider extends ng.IServiceProvider {
    setBaseUrl(baseurl: string): void;
    $get($http: ng.IHttpService, $q: ng.IQService, baseUrl: string): IDataService;
}


export class DataServiceProvider implements ng.IServiceProvider {

    private _baseUrl: string;

    constructor() {
    }

    setBaseUrl(baseurl: string): void {
        this._baseUrl = baseurl;
    }

    $get = ["$http", "$q", ($http: ng.IHttpService, $q: ng.IQService) => {
        return new DataService($http, $q, this._baseUrl);
    }];

}

export class DataService implements IDataService {
    constructor(private _http: ng.IHttpService, private _q: ng.IQService, private _baseUrl: string) {
    }

    public getQuery<T>(urn: string, config: ng.IRequestShortcutConfig): ng.IPromise<T> {
        this.appendTransform(config);
        let uri = this._baseUrl + urn;
        return this._http.get<T>(uri, config)
            .then(result => {
                return result.data;
            }, (reject) => {
                console.log(reject);
                return this._q.reject(reject.data);
            }).finally(() => {
            });
    }

    postQuery<T>(urn: string, data: any, config: ng.IRequestShortcutConfig): ng.IPromise<T> {
        let uri = this._baseUrl + urn;
        this.appendTransform(config);
        return this._http.post<T>(uri, data, config).then(result => {
            return result.data;
        }, (reject) => {
            console.log(reject);
            return this._q.reject(reject.data);
        }).finally(() => {
        });
    }
    private appendTransform(config: ng.IRequestShortcutConfig) {
        if (config.transformRequest) {
            let defaults = angular.isArray(this._http.defaults.transformRequest) ? this._http.defaults.transformRequest : [this._http.defaults.transformRequest];
            config.transformRequest = [].concat(config.transformRequest).concat(defaults);
        }
        if (config.transformResponse) {
            let defaults = angular.isArray(this._http.defaults.transformResponse) ? this._http.defaults.transformResponse : [this._http.defaults.transformResponse];
            config.transformResponse = defaults.concat(config.transformResponse);
        }    
    }
}
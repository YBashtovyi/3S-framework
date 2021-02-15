import * as angular from 'angular';


export interface IAuthService {
    login(userName: string, password: string): ng.IPromise<any>;
    logOut(): void;
    isAuth: boolean;
    getToken(): string;
    getOwner(): string;
    refreshToken(): ng.IPromise<any>;
    setCurrentOwner(owner: IOwner): void;
    getCurrentOwner(): number;
    getOwners(): IOwner[];
}


export interface IAuthServiceProvider extends ng.IServiceProvider {
    configure(config: any): void;
    $get(injector: ng.auto.IInjectorService, q: ng.IQService, window: ng.IWindowService, rootScope: ng.IRootScopeService, settings: any): IAuthService;
}



export class AuthServiceProvider implements ng.IServiceProvider {

    private settings: any;

    constructor() {
    }

    configure(config: any): void {
        this.settings = config;
    }
    $get = ["$injector", "$q", "$window", "$rootScope", (injector: ng.auto.IInjectorService, q: ng.IQService, window: ng.IWindowService, rootScope: ng.IRootScopeService) => {
        return new AuthService(injector, q, window, rootScope, this.settings);
    }];

}

export class AuthService implements IAuthService {

    private http: ng.IHttpService;
    private currentOwner: number;
    constructor(private injector: ng.auto.IInjectorService, private q: ng.IQService, private window: ng.IWindowService, private rootScope: ng.IRootScopeService, private settings: IIdentitySettings) {
        this.http = injector.get("$http");
    }

    public login(userName: string, password: string): ng.IPromise<any> {
        let data = "username=" + userName + "&password=" + password + "&client_id=" + this.settings.clientId + "&scope=" + this.settings.scopes + "&grant_type=password";
        let _headers = { 'Content-Type': 'application/x-www-form-urlencoded' }

        let deferred = this.q.defer();
        let requestConfig = {
            headers: _headers,
            loginRequest:
                true
        };

        this.http.post(this.settings.authority + 'connect/token', data, requestConfig).then((resp: any) => {
            let token = resp.data.access_token;
            let refreshToken = resp.data.refresh_token;
            let owners = {};
            let requestConfig = {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`,
                }
            }
            this.http.get(this.settings.authority + 'MisUsers/GetUserOwnersForDtm', requestConfig).then(resp => {
                owners = resp.data;
                return owners;
            }).then((data) => {
                this.storeToken(token, refreshToken, owners);
                deferred.resolve(data);
            })
        }, (error) => {
            console.log(error);
            this.logOut();

            deferred.reject(error);
        });

        return deferred.promise;
    }

    private storeToken(accessToken: any, refreshToken: any, owners: any): void {
        let key = this.settings.clientId;
        this.window.localStorage.setItem(key + "_access_token", accessToken);
        this.window.localStorage.setItem(key + "_refresh_token", refreshToken);
        this.window.localStorage.setItem(key + "_owners", JSON.stringify(owners));
    }

    public logOut() {
        this.clearStoredData();
        this.rootScope.$broadcast("logOut");
    }

    public get isAuth(): boolean {
        let key = this.settings.clientId;
        let token = this.window.localStorage.getItem(key + "_access_token");
        return !!token;
    }

    public getToken(): string {
        let key = this.settings.clientId;
        return this.window.localStorage.getItem(key + "_access_token");
    }


    private clearStoredData() {
        let key = this.settings.clientId;
        this.window.localStorage.removeItem(key + "_access_token");
        this.window.localStorage.removeItem(key + "_refresh_token");
        this.window.localStorage.removeItem(key + "_owners");
        this.window.localStorage.removeItem(key + "_ownerId");
    }

    public getOwner(): string {
        let key = this.settings.clientId;
        return this.window.localStorage.getItem(key + "_ownerId");
    }

    public refreshToken() {
        var deferred = this.q.defer();
        let key = this.settings.clientId;
        var refreshToken = this.window.localStorage.getItem(key + "_refresh_token");

        if (refreshToken) {
            var data = "grant_type=refresh_token&refresh_token=" + refreshToken + "&client_id=" + this.settings.clientId +
                "&scope=" + this.settings.scopes;

            var req = {
                method: 'POST',
                url: this.settings.authority + 'connect/token',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                data: data
            }

            this.http<any>(req)
                .then(
                    (response) => {
                        this.window.localStorage.setItem(key + "_access_token", response.data.access_token);
                        this.window.localStorage.setItem(key + "_refresh_token", response.data.refresh_token);
                        deferred.resolve(response.data);
                    },
                    (err) => {
                        this.logOut();
                        deferred.reject(err);
                    });
        } else {
            deferred.reject();
        }

        return deferred.promise;
    }

    public getOwners() {
        //debugger;
        let key = this.settings.clientId;
        let owners = this.window.localStorage.getItem(key + "_owners");
        if (owners) {
            try {
                return JSON.parse(owners);
            }
            catch (e) {
                console.error(e);
                return [];
            }
        }
        return [];
    }

    public setCurrentOwner(owner: IOwner): void {
        let key = this.settings.clientId;
        this.window.localStorage.setItem(key + "_ownerId", owner.ownerId.toString());
    }

    public getCurrentOwner() {
        if (this.currentOwner)
            return this.currentOwner;
        let key = this.settings.clientId;
        let owner = this.window.localStorage.getItem(key + "_ownerId");
        if (owner)
            return parseInt(owner);
        return;
    }

}
export interface IIdentitySettings {
    clientId: string;
    scopes: string;
    authority: string;
}
interface ILoginData {
    userId: string;
    login: number;
    token: ITokenData;
    owners: IOwner;
}
export interface IOwner {
    ownerId: number;
    ownerName: string;
}
interface ITokenData {
    access_token: string;
    expires_in: number;
    token_type: string;
    refresh_token: string;
}
interface IRefreshTokenData {
    id_token: string;
    access_token: string;
    expires_in: string;
    token_type: string;
    refresh_token: string;
}

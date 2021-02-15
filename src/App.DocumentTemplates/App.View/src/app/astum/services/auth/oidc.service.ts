import * as Oidc from 'oidc-client';
import * as angular from 'angular';



export interface IOidcService extends IOidcStorage {
    login(): void;
    logout(): void;
    signinRedirectCallback(url?: string): Promise<Oidc.User>;
    isAuth(): boolean;
}

export interface IOidcStorage {
    getUser(): Promise<Oidc.User>;
}

export interface IOidcServiceProvider extends ng.IServiceProvider {
    configure(config: Oidc.UserManagerSettings): void;
    $get($scope: ng.IScope, config: Oidc.UserManagerSettings): IOidcService;
}



export class OidcServiceProvider implements ng.IServiceProvider {

    private settings: Oidc.UserManagerSettings;

    constructor() {
    }

    configure(config: Oidc.UserManagerSettings): void {
        this.settings = config;
    }
    $get = () => {
        return new OidcService(this.settings);
    };

}

export class OidcService implements IOidcService {
    private userManager: Oidc.UserManager;
    private isAuthenticated: boolean;
    constructor(private settings: Oidc.UserManagerSettings) {
        this.userManager = new Oidc.UserManager(settings);
        this.userManager.getUser().then((user) => {
            this.isAuthenticated = !!user;
        }, (error) => {
            this.isAuthenticated = false;
            })
        this.userManager.events.addUserLoaded((u)=>{
            this.isAuthenticated = true;
            console.info("user loaded");
        });
        this.userManager.events.addUserUnloaded(()=>{
            this.isAuthenticated = false;
            console.info("user unloaded");
        });
        this.userManager.events.addAccessTokenExpiring(()=>{
            console.info("token expiring");
        });
        this.userManager.events.addAccessTokenExpired(()=> {
            this.isAuthenticated = false;
            console.info("token expired");
        });
        this.userManager.events.addSilentRenewError((e)=> {
            this.isAuthenticated = false;
            console.info("silent renew error", e.message);
        });
    }

    public login() {
        this.userManager.signinRedirect();
    }

    public isAuth(): boolean {
        return this.isAuthenticated
    }

    public logout() {
        //this.userManager.removeUser();
        this.userManager.signoutRedirect();
    }

    public getUser() {
        return this.userManager.getUser();
    }

    public signinRedirectCallback(url?:string) {
        return this.userManager.signinRedirectCallback(url);
    }



} 
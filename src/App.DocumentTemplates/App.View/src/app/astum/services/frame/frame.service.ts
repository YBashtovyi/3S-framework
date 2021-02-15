import * as angular from 'angular';
import { StateService } from '@uirouter/angularjs';
import { IAuthService } from '../auth/auth.service';




//export interface IFrameServiceProvider extends ng.IServiceProvider {
//    configure(use:boolean): void;
//    $get($scope: ng.IScope, use: boolean): IFrameService;
//}

export interface IFrameService {
    isEmbed: boolean;
    token: string;
    ownerId: string;
    sendTokenRequest(refresh?:boolean): Promise<string>;
    //setPreviewCb(cb: () => ng.IPromise<any>): void;
    sendTemplateMessage(code: string, name: string): void;
    sendPreviewMessage(): void;
    sendShowPreviewMessage(): void;
    sendValidationMessage(isValid: boolean): void;
    sendValidationError(value: string);
    sendError(): void;
}


export class FrameService implements IFrameService {

    static $inject = ['$window', '$state', '$rootScope', 'authService'];
    private _isEmbed: boolean;
    private _token: string;
    private _ownerId: string;
    
    //private _previewCb: () => ng.IPromise<any>;

    constructor(private $window: ng.IWindowService,
        private $state: StateService,
        private $rootScope: ng.IRootScopeService,
        private auth: IAuthService) {
        this._isEmbed = this.$window.self !== this.$window.top;
        if (this._isEmbed) {
            this.$window.addEventListener("message", this.onExternalEvent);
            this.sendLoadMessage();
        }
    }

    public get isEmbed() {
        return this._isEmbed;
    }
    public get token() {
        return this._token;
    }
    public get ownerId() {
        return this._ownerId;
    }

    public sendTemplateMessage(code: string, name: string) {
        let config = {};
        (<any>config).message = "setTemplate";
        (<any>config).code = code;
        (<any>config).name = name;
        this.sendMessage(config);
    }

    public sendValidationMessage(isValid: boolean): void {
        let config = {};
        (<any>config).message = "validationResult";
        (<any>config).isValid = isValid;
        this.sendMessage(config);
    }

    private onExternalEvent = (event) => {
        let data;
        if (typeof event.data === "string" && event.data !== "unchanged")
            data = JSON.parse(event.data);
        else
            data = event.data;


        if (data.action === "afterDocumentFrameLoad") {
            if (data.params['token']) {
                this._token = data.params['token'];
                this._ownerId = data.params['owner'];
            }
            this.$state.go('app.document', {
                'id': data.params['id'],
                'templCode': data.params['templCode'],
                'isFrame': true,
                'entityType': data.params['entityType'],
                'system': data.params['system'] || '',
                'usePreview': data.params['usePreview']
            }, { location: 'replace' }).then((data) => {
                this.observer("content");
            });
            return;
        }
        if (data.action === "afterDocExplorerFrameLoad") {
            let params = {};
            if (data.params['token']) {
                this._token = data.params['token'];
                this._ownerId = data.params['owner'];
                if (data.params['rootName'])
                    params['rootName'] = data.params['rootName'];
                if (data.params['searchText'])
                    params['searchText'] = data.params['searchText'];
            }
            this.$state.go('app.templates', params).then((data) => {
                this.observer("container");
            });
            return;
        }
        if (data.action === "onSave") {
            this.$rootScope.$broadcast("documentSave");
            return;
        }
        if (data.action === "validate") {
            this.$rootScope.$broadcast("validate",data.params.isSilent);
            return;
        }
        //if (data.action === "preview") {  
        //    this._previewCb().then((data) => {
        //        parent.postMessage({ 'message': 'reloadPreview' }, '*');
        //    }, (error) => {
        //        console.error(error);
        //    });
        //    return;
        //}
    }


    private sendLoadMessage() {
        this.sendMessage({ 'message': 'load' });
    }

    private sendResizeMessage(selector: string) {
        let elm: HTMLDivElement = <HTMLDivElement>document.getElementsByClassName(selector)[0];
        let height = elm.offsetHeight;
        this.sendMessage({ 'message': 'size', 'value': height });
    }

    public sendPreviewMessage() {
        this.sendMessage({ 'message': 'reloadPreview' });
    }

    public sendShowPreviewMessage() {
        this.sendMessage({ 'message': 'showPreview' });
    }

    public sendValidationError(value: string) {
        this.sendMessage({ 'message': 'validationError', 'value': value});
    }

    public sendError(): void {
        this.sendMessage({ 'message': 'error' });
    }

    public sendTokenRequest(refresh: boolean = false): Promise<any> {
        if (refresh) {
            this.sendMessage({ 'message': 'refreshToken' });
        } else {
            this.sendMessage({ 'message': 'needToken' });
        }
        
        let onTokenMessage = (event) => {
            let data;
            if (typeof event.data === "string")
                data = JSON.parse(event.data);
            else
                data = event.data;
            if (data.action === "token" && data.params['token'] && (data.params['token'].Access_token)) {
                this.$window.removeEventListener("message", onTokenMessage);
                this._token = data.params['token'].Access_token;
                this._ownerId = data.params['owner'];
                _resolve(data.params['token'].Access_token);
            }
        }
        let _resolve: (value?: {}) => void;
        let _reject: (error?: any) => void;
        let tokenPromise = new Promise((resolve, reject) => {
            _resolve = resolve;
            _reject = reject;
            this.$window.addEventListener("message", onTokenMessage);
        });

        return tokenPromise;
    }


    private sendMessage(messageConfig: any) {
        if (parent !== window && parent.postMessage) {
            parent.postMessage(messageConfig, "*");
        }
    }

    private observer(selector: string) {
        let height;
        let doc = document.getElementsByClassName(selector)[0];
        let obs = new MutationObserver((mutations: MutationRecord[], observer: MutationObserver) => {
            if (doc.scrollHeight !== height) {
                height = doc.scrollHeight;
                this.sendResizeMessage(selector);
            }
        });
        obs.observe(doc, { childList: false, subtree: true, attributes: true });
    }
} 

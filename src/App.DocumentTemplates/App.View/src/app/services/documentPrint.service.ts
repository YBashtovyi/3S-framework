    import { ParamsList } from "../models/common.models";
import { GlobalConfig } from './global.config';
import { IAuthService } from '../astum/services/auth/auth.service';
import { IFrameService } from '../astum/services/frame/frame.service';


export class DocumentPrintService {


    constructor(private $http: ng.IHttpService, private authService: IAuthService, private frameService: IFrameService) {
        "ngInject";
    }


    private baseUrl = GlobalConfig.instance.baseUrl;
    private printUrl = GlobalConfig.instance.printServiceUrl;
    private mvcUrl = GlobalConfig.instance.mvcUrl;
    public printDocDataList(params: ParamsList): void {
        this.$http.post(this.baseUrl + 'DtmPrint/PrintDocument', params).then((result) => {
            this.downloadLink(result.data, 'DocData.pdf');
        },
            (error) => {
                debugger;
            });
    }

    public printDocDataPdf(docId: string, entityType: string, sysPrefix: string): void {
        var a: any = document.createElement('a');
        a.href = this.printUrl + 'Print/' + sysPrefix + 'Pdf/' + docId +'/'+entityType;
        //a.href = this.printUrl + 'Home/Index/' + docId;
        a.target = 'blank';
        if (this.frameService.isEmbed) {

            this.clickLink(this.frameService.token, this.frameService.ownerId, a);
            return;
        }
        if (!this.frameService.isEmbed) {
            let token = this.authService.getToken();
            let owner = this.authService.getOwner();
                if (token && owner)
                    //a.href = a.href +'?access_token='+ user.access_token;
                    this.clickLink(token, owner, a);
        }
    }

    public printDocx(docId: string): void {
        var a: any = document.createElement('a');
        a.href = this.mvcUrl + 'DtmDocument/GetDocx/' + docId;
        a.target = 'blank';
        if (this.frameService.isEmbed) {

            this.clickLink(this.frameService.token, this.frameService.ownerId, a);
            return;
        }
        if (!this.frameService.isEmbed) {
            let token = this.authService.getToken();
            let owner = this.authService.getOwner();
            if (token && owner)
                //a.href = a.href +'?access_token='+ user.access_token;
                this.clickLink(token, owner, a);
        }
    }


    private clickLink(token: string, owner:string, a:HTMLLinkElement) {
        this.setCookie('token', token, 10, '/', GlobalConfig.instance.domain, null);
        this.setCookie('owner', owner, 10, '/', GlobalConfig.instance.domain, null);
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }

    private setCookie(name, value, expires, path, domain, secure) {
        let c = name + "=" + value +
            ((expires) ? "; expires=" + expires : "") +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            ((secure) ? "; secure" : "");
        document.cookie = c;
    }

    private downloadLink(data, fileName) {

        if (navigator.msSaveBlob) { // IE 10+ 
            navigator.msSaveBlob(this.base64ToBlob(data, 'text/pdf;'), fileName);
            return;
        }

        var a: any = document.createElement('a');
        a.download = decodeURI(fileName);

        a.href = 'data:' + 'application/pdf;base64, ' + data;
        //var blob = new Blob([data], { type: 'application/pdf' });
        //var url = window.URL.createObjectURL(blob);
        //a.setAttribute('href', url);
        //a.setAttribute("download", fileName);

        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }

    private base64ToBlob(b64Data, contentType, sliceSize?): Blob {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(b64Data);
        var byteArrays = [];

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);

            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            var byteArray = new Uint8Array(byteNumbers);

            byteArrays.push(byteArray);
        }

        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }

    //night khackaton
    public printDocumentTemplate(docId: string) {
        var a: HTMLAnchorElement = document.createElement('a');
        a.setAttribute("target", "blank");
        a.href = this.mvcUrl + 'Document/Print/' + docId;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
    }

}
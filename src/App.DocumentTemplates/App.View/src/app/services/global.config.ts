declare var process;

export class GlobalConfig {

    private _baseUrl: string;
    private _mvcUrl: string;
    private _appRoot: string;
    private _identityUrl: string;
    private _printServiceUrl: string;
    private _domain: string;

    private static _instance: GlobalConfig;
    constructor() {
        if (process.env.ENV === "build_Okhmatdyt_production") {
            this._domain = "axel.com.ua";
            this._appRoot = "https://moddi.axel.com.ua/dtm";
            this._baseUrl = "https://moddi.axel.com.ua/api/api/";
            this._mvcUrl = "https://moddi.axel.com.ua/api/";
            this._identityUrl = "https://signin.axel.com.ua/";
            this._printServiceUrl = "https://print.axel.com.ua/";
        }
        if (process.env.ENV === "build_Dkl7_production") {
            this._domain = "dkl7.axel.com.ua";
            this._appRoot = "https://moddi.dkl7.axel.com.ua/dtm";
            this._baseUrl = "https://moddi.dkl7.axel.com.ua/api/api/";
            this._mvcUrl = "https://moddi.dkl7.axel.com.ua/api/";
            this._identityUrl = "https://signin.dkl7.axel.com.ua/";
            this._printServiceUrl = "https://print.dkl7.axel.com.ua/";
        }
            if (process.env.ENV === "build_Chuguevcrb_production") {
            this._domain = "chuguev-crb.com.ua";
            this._appRoot = "https://moddi.chuguev-crb.com.ua/dtm";
            this._baseUrl = "https://moddi.chuguev-crb.com.ua/api/api/";
            this._mvcUrl = "https://moddi.chuguev-crb.com.ua/api/";
            this._identityUrl = "https://signin.chuguev-crb.com.ua/";
            this._printServiceUrl = "https://print.chuguev-crb.com.ua/";

        }

        if (process.env.ENV === "build-dev") {
            this._domain = "axel.com.ua";
            this._appRoot = "http://doctemplates-dev.axel.com.ua";
            this._baseUrl = "http://doctemplates-dev.axel.com.ua/api/api/";
            this._mvcUrl = "http://doctemplates-dev.axel.com.ua/api/";
            this._identityUrl = "http://identity-dev.axel.com.ua";
            this._printServiceUrl = "http://print-dev.axel.com.ua/";
        }
        if (process.env.ENV === "dev-server") {
            this._domain = "localhost";
            this._printServiceUrl = "http://localhost:8088/";
            this._baseUrl = "http://localhost:5051/api/";
            this._mvcUrl = "http://localhost:5051/api/";
            this._appRoot = "http://localhost:8082";
            this._identityUrl = "https://id-dev.it4medicine.com.ua/";
            //this._identityUrl = "http://identity-dev.axel.com.ua/";
        }

        if (process.env.ENV === "dev-soc-server") {
            this._domain = "localhost";
            this._printServiceUrl = "http://localhost:8088/";
            this._baseUrl = "http://localhost:8084/api/";
            this._mvcUrl = "http://localhost:8084/api/";
            this._appRoot = "http://localhost:8082";
            this._identityUrl = "http://ids.soc.axel.com.ua/";
        }

        if (process.env.ENV === "build_Zinaida_production") {
            this._domain = "zinaida.dicom-hub.com";
            this._appRoot = "https://moddi.zinaida.dicom-hub.com/dtm";
            this._baseUrl = "https://moddi.zinaida.dicom-hub.com/api/api/";
            this._mvcUrl = "https://moddi.zinaida.dicom-hub.com/api/";
            this._identityUrl = "https://signin.zinaida.dicom-hub.com/";
            this._printServiceUrl = "https://moddi.zinaida.dicom-hub.com/print/";
        }


        if (process.env.ENV === "build_Mis_temptest") {
            this._domain = "dicom-hub.com";
            this._appRoot = "https://mis-dev.dicom-hub.com/dtm";
            this._baseUrl = "https://mis-dev.dicom-hub.com/api/api/";
            this._mvcUrl = "https://mis-dev.dicom-hub.com/api/";
            this._identityUrl = "https://id-dev.it4medicine.com.ua/";
            this._printServiceUrl = "https://mis-dev.dicom-hub.com/print/";
        }

        if (process.env.ENV === "build_Mis_test") {
            this._domain = "dicom-hub.com";
            this._appRoot = "https://mis-test.dicom-hub.com:50001/dtm";
            this._baseUrl = "https://mis-test.dicom-hub.com:50001/api/api/";
            this._mvcUrl = "https://mis-test.dicom-hub.com:50001/api/";
            this._identityUrl = "https://id-test.it4medicine.com.ua/";
            this._printServiceUrl = "https://mis-test.dicom-hub.com:50001/print/";
        }

        if (process.env.ENV === "build_Mis_production") {
            this._domain = "dicom-hub.com";
            this._appRoot = "https://mis.dicom-hub.com/dtm";
            this._baseUrl = "https://mis.dicom-hub.com/api/api/";
            this._mvcUrl = "https://mis.dicom-hub.com/api/";
            this._identityUrl = "https://id.dicom-hub.com/";
            this._printServiceUrl = "https://mis.dicom-hub.com/print/";
        }

        if (process.env.ENV === "build_Demo_production") {
            this._domain = "demo.it4medicine.com.ua";
            this._appRoot = "https://telemed.demo.it4medicine.com.ua/dtm";
            this._baseUrl = "https://telemed.demo.it4medicine.com.ua/api/api/";
            this._mvcUrl = "https://telemed.demo.it4medicine.com.ua/api/";
            this._identityUrl = "https://id.demo.it4medicine.com.ua/";
            this._printServiceUrl = "https://telemed.demo.it4medicine.com.ua/print/";
        }

        if (process.env.ENV === "build_Kharkiv_production") {
            this._domain = "kh.dicom-hub.com";
            this._appRoot = "https://mis.kh.dicom-hub.com/dtm";
            this._baseUrl = "https://mis.kh.dicom-hub.com/api/api/";
            this._mvcUrl = "https://mis.kh.dicom-hub.com/api/";
            this._identityUrl = "https://id.kh.dicom-hub.com/";
            this._printServiceUrl = "https://mis.kh.dicom-hub.com/print/";
        }

        if (process.env.ENV === "build_Kharkiv_test") {
            this._domain = "kh.dicom-hub.com";
            this._appRoot = "https://mis-test.kh.dicom-hub.com:50001/dtm";
            this._baseUrl = "https://mis-test.kh.dicom-hub.com:50001/api/api/";
            this._mvcUrl = "https://mis-test.kh.dicom-hub.com:50001/api/";
            this._identityUrl = "https://id-test.kh.dicom-hub.com:50001/";
            this._printServiceUrl = "https://mis-test.kh.dicom-hub.com:50001/print/";
        }
    }

    public static get instance(): GlobalConfig {
        if (!this._instance)
            this._instance = new GlobalConfig();

        return this._instance;
    }
    public get appRoot(): string {
        return this._appRoot;
    }

    public get baseUrl(): string {
        return this._baseUrl;
    }

    public get mvcUrl(): string {
        return this._mvcUrl;
    }

    public get identityUrl() {
        return this._identityUrl;
    }

    public get printServiceUrl() {
        return this._printServiceUrl;
    }

    public get domain() {
        return this._domain;
    }

}
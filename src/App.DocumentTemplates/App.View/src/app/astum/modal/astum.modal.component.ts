import * as angular from 'angular';
import { IModalService } from './astum.modal.service';
import './modal.scss';

export class AstumModalComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    transclude: boolean;
    bindings: { [index: string]: string; };
    controllerAs: string;

    constructor() {
        this.controller = AstumModalController;
        this.template = require('./astum.modal.html');
        this.transclude = true;
        this.bindings = {
            modalId: '@',
        }
        this.controllerAs = 'modal';
    }
}

export class AstumModalController implements ng.IComponentController {

    static $inject = ['$element','modalService','$transclude','$compile','$scope'];

    constructor(private element: ng.IAugmentedJQuery,
        private modalService: IModalService,
        private transclude: ng.ITranscludeFunction,
        private compile: ng.ICompileService,
        private scope: ng.IScope
    ) {
    }

    private modalId: string;
    public config: any;
    private onCloseCb: Function;
    private innerCb: Function;
    public isOpen: boolean;

    public callbackWrapper = (data?: any) => {
        this.close(data,true);
    }

    $onInit() {
        let modal:any = {};
        modal.id = this.modalId;
        modal.open = this.open;
        modal.close = this.close;
        this.modalService.add(this);
    }

    $onDestroy() {
        this.modalService.remove(this);
    }

    //$postLink() {        
    //    this.transclude((clone, scope) => {
    //        let compiled = this.compile(clone)(scope.$parent);
    //        this.element.find('.modal-body').append(compiled);
    //    });
    //}

    public open = (attributes: any, onCloseCb?: Function,innerCb?:Function) => {
        angular.element('body').css('overflow', 'hidden');      
        this.config = attributes;
        this.onCloseCb = onCloseCb;
        this.innerCb = innerCb;
        this.element.show();
        this.isOpen = true;
    }

    public close = (data?:any,isWrapped:boolean = false) => {
        angular.element('body').removeAttr('style');
        this.element.hide();
        this.isOpen = false;
        if (!isWrapped && this.onCloseCb)
            this.onCloseCb();
        if (isWrapped && this.innerCb)
            this.innerCb(data);
        delete this.config;
        delete this.onCloseCb;
        delete this.innerCb;
    }

}
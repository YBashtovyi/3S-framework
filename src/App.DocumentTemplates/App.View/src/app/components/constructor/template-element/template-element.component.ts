import * as angular from 'angular';
import {DocTemplatesPm, DocTemplateElementsPm} from "./../../../models/pm.models";
import {DocTemplatesVm, DocTemplateElementsVm, DocControlTypesVm} from "./../../../models/vm.models";
import {TemplateElementsController} from "./../template-elements/template-elements.component";
import {CardMode} from "./../../../models/common.models";
import {ConstructorController} from "../constructor.component";

export class TemplateElementComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    controllerAs: string;
    bindings: { [index: string]: string; };
    require: { [controller: string]: string };

    constructor() {
        this.controller = TemplateElementController;
        this.template = require('./template-element.html');
        this.bindings = {
            item: '<',
            index: '<',
            onChange: '&',
            parentConfig: '<?'
        };
        this.controllerAs = 'elCtl';
        this.require = {
            'elementList': '^^templateElements',
            'main': '^^templateConstructor',
            'parentElement': '?^^templateElement'
        };
    }
}

class TemplateElementController implements ng.IComponentController {
    static $inject = ['$element', '$scope', '$timeout'];
    public item: DocTemplateElementsVm | DocTemplateElementsPm;
    public parentConfig: (any);
    private index: number;
    private elementList: TemplateElementsController;
    private main: ConstructorController;
    private parentElement: (any);
    private onChange: (any) => void;
    private dialog: boolean = false;
    private elementForm: ng.IFormController;
    private showInlineBlock: boolean = false;
    private maxWidth: boolean = false;
    private minWidth: boolean = false;
    private setTimeout: (any);
    private narrowBlock: boolean = false;

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope, private timeout: ng.ITimeoutService) {
    }


    private checkChanges() {
        if (this.checkInlineChild(this.parentElement.item)) {
            this.addErrorTemplates(this.parentElement);
            this.alertMaxWidth();
        }
        this.updateItem()

    }

    private updateItem() {

        let copy = <any>{};
        angular.copy(this.item.config, copy);
        this.item.config = copy;
    }

    private updateParent(item) {
        let copy = <any>{};
        angular.copy(item.config, copy);
        item.config = copy;
    }


    private switchShowInlineBlock($event, bul) {
        $event.stopPropagation();
        this.showInlineBlock = bul;
        if (this.showInlineBlock && this.narrowBlock)
            this.setAddingBlockPosition()
    }

    private setAddingBlockPosition() {
        let mainTemplateElementsCoordsLeft = document.getElementById('main-template-elements').getBoundingClientRect().left,
            mainTemplateElementsCoordsWidth = document.getElementById('main-template-elements').getBoundingClientRect().width,
            elLeft = this.element.offset().left;
        if (mainTemplateElementsCoordsWidth + mainTemplateElementsCoordsLeft - elLeft >= 450) {
            this.element.find('.template-element-inline-copy').css('left', '')
        } else {
            this.element.find('.template-element-inline-copy').css('left', mainTemplateElementsCoordsWidth + mainTemplateElementsCoordsLeft - elLeft - 450 + 'px')
        }
    }

    private alertMaxWidth() {
        this.maxWidth = true;
        if (this.setTimeout)
            this.timeout.cancel(this.setTimeout);
        this.setTimeout = this.timeout(this.clearAlert, 3000)
    };

    private alertMinWidth = () => {
        this.minWidth = true;
        if (this.setTimeout)
            this.timeout.cancel(this.setTimeout);
        this.setTimeout = this.timeout(this.clearAlert, 3000)
    };

    private clearAlert = () => {
        if (!this.maxWidth && !this.minWidth)
            return;
        this.maxWidth = false;
        this.minWidth = false;
    };

    public edit($event) {
        $event.stopPropagation();
        this.elementList.editElement(this.index);
    }

    private clearError($event) {
        alert($event)
    }

    public removeElement($event) {
        $event.stopPropagation();
        this.elementList.removeElement(this.index);
        this.main.unFixBody();
        this.dialog = false;
        this.setCardModeEdit();
    }

    public dialogRemove($event) {
        $event.stopPropagation();
        this.main.fixBody();
        this.dialog = true;
    }

    public cancelRemove($event) {
        $event.stopPropagation();
        this.main.unFixBody();
        this.dialog = false;
    }

    public setInlineSector($event) {
        if ($event)
            $event.stopPropagation();
        this.setCardModeEdit();
        let copy = <any>{};
        angular.copy(this.item.config, copy);
        copy.inline = !copy.inline;
        this.item.config = copy;
        //this.checkSector();
    }

    private checkSector() {
        if (!this.item.config.inline) {
            this.removeErrorTemplates(this);
            return true;
        }
        if (this.checkInlineChild(this.item)) {
            this.addErrorTemplates(this);
            return false;
        }

        this.removeErrorTemplates(this);
        return true;
    }

    public checkInlineChild(el) {
        let childWidth = 0;
        for (let i = 0; i < el.templateElements.length; i++) {
            childWidth += el.templateElements[i].config.width
        }
        if (childWidth > el.config.width) {
            return true;
        } else {
            return false;
        }
    }

    private addErrorTemplates(item) {
        item.element.find('.nested')[0].classList.add('med-error');
    }

    private removeErrorTemplates(item) {
        item.element.find('.nested')[0].classList.remove('med-error');
    }

    private changeWidth($event, up) {
        this.checkSector();
        if ($event) {
            this.setCardModeEdit();
            this.clearAlert();
            if (up) {
                this.item.config.width += 1;
                if (this.checkInlineChild(this.parentElement.item)) {
                    this.addErrorTemplates(this.parentElement);
                    this.alertMaxWidth();
                    this.item.config.width -= 1;
                    return;
                }
            } else {
                if (this.item.config.width <= 1) {
                    this.alertMinWidth();
                    return;
                }
                this.item.config.width -= 1;
                if (!this.checkInlineChild(this.parentElement.item)) {
                    this.removeErrorTemplates(this.parentElement)
                }
            }
        }
        this.setElWidth();

        if (this.item.controlTypeCode === 'SECTOR') {
            this.checkSector();
            this.updateItem();
        }

        if (this.showInlineBlock && this.narrowBlock)
            this.setAddingBlockPosition()
    }

    private setElWidth() {
        if (this.parentConfig.inline) {
            this.element.outerWidth(this.parentElement.element.find('template-elements').width() * this.item.config.width / this.parentElement.item.config.width);
            this.element.addClass('inline-template');
        } else {
            // this.parentElement.element.find('template-elements').width('')
            this.element.outerWidth('');
            this.element.removeClass('inline-template')
        }

        this.element.outerWidth() < 450 ? this.narrowBlock = true : this.narrowBlock = false;
    }

    private setCardModeEdit() {
        this.main.cardMode = CardMode.Edit;
    }

    public copy($event) {
        $event.stopPropagation();
        //this.elementList.copyElement(this.index);
        this.elementList.copyElementDialog(this.index);
    }

    public copyToTemplate($event) {
        $event.stopPropagation();
        this.elementList.copyElementDialog(this.index, true);
    }

    public onElementsChange(event) {
        //this.item.templateElements = event.elements;
        if (this.onChange) {
            this.onChange({event: {element: this.item, index: this.index}});
        }
    }

    public get isActive(): boolean {
        return this.index === this.elementList.editIndex;
    }

    // private  defaultConfig = {};
    // //todo добавить дефолтный конфиг и промерку на существование методов
    //
    //     // this.item.config.showHeader =  this.item.config.showHeader || true;
    //     // this.item.config.showValue =  this.item.config.showValue || true;
    //     // this.item.config.vertical =  this.item.config.vertical || false;
    //////////////
//     if(this.item.config.parentWidth)
//     delete this.item.config.parentWidth;
//     if(this.item.config.parentInline)
//     delete this.item.config.parentInline;
//     if(this.item.config.showValue)
//     delete this.item.config.showValue;
//     if(this.item.config.showHeader)
//     delete this.item.config.showHeader;
//     if(this.item.config.vertical)
//     delete this.item.config.vertical;
// //////////todo delete this

    private setConfig() {
        if (!this.item.config) {
            this.item.config = {};
        }
        this.item.config.showName = this.item.config.showName || 'block';

        if (this.item.controlTypeCode === 'SECTOR') {
            this.item.config.width = this.item.config.width || 12;
        } else {
            this.item.config.width = this.item.config.width || 1;
        }
        this.item.config.inline = this.item.config.inline || false;
        if (!this.parentConfig) {
            this.item.config.width = 12;
            return
        }
        this.setElWidth()
    }
    private findParent() {
        let e = this.parentElement;
        if (e.parentElement) {
            this.findParent();
        } else {
            return e.item;
        }
    }
    $onInit() {
        this.setConfig();
    }
    $onDestroy() {
        this.timeout.cancel(this.setTimeout);
    }
    $onChanges(changeObj: ng.IOnChangesObject) {
        if (this.parentConfig) {
            this.setElWidth();
            if (this.parentConfig.inline) {
                this.checkChanges();
            }
        }
    }
}

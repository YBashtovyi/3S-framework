import * as angular from "angular";
import "./astumDocElement.scss";
import { DocDataVm, DocTemplateElementsVm } from "./../../models/vm.models";
import { DocumentService } from "./../../services/document.service";
import { DocTemplateElementValuesVm } from "../../models/vm.models";
import { DocTemplateElementValuesPm } from "../../models/pm.models";
import { DragManagerService } from "../services/dragManager/dragmanager.service";
import { IModalService, IModalOptions } from "./../../astum/modal/astum.modal.service";

import { ElementMode } from "../models/common.models";

import "../templates";

export class AstumDocElementComponent implements ng.IComponentOptions {
	controller: ng.IControllerConstructor;
	template: string;
	bindings: { [index: string]: string };
	controllerAs: string;

	constructor() {
		this.controller = DocElementController;
		this.template = require("./astum.docelement.html");
		this.bindings = {
			item: "<",
			tIndex: "<",
			parentIndex: "<",
			isDocValid: "<",
			isFrame: "<",
			parentConfig: "<?",
		};
		this.controllerAs = "docEl";
	}
}

class DocElementController implements ng.IComponentController {
	static $inject = ["$element", "$scope", "DocumentService", "$templateCache", "$compile", "modalService"];

	private mode: ElementMode;
	private docClickListener: () => void;
	private addEventListener: () => void;
	private updateEventListener: () => void;
	private removeEventListener: () => void;
	private tIndex: number;
	private parentIndex: number[];
	private pathIndex: number[];
	public isFrame: boolean;
	public parentConfig: any;

	public isDocValid: boolean;
	public item: {
		panel: DocTemplateElementsVm | DocDataVm;
		nodes: Array<DocTemplateElementsVm | DocDataVm>;
		errors: any[];
	};
	public isConfigurable: boolean;
	public isEditable: boolean;

	public get config() {
		return this.item.panel.config;
	}

	private elementTemplate: string;
	private _rootElm: ng.IAugmentedJQuery;

	constructor(
		private element: ng.IAugmentedJQuery,
		private scope: ng.IScope,
		private service: DocumentService,
		private templateCache: ng.ITemplateCacheService,
		private compile: ng.ICompileService,
		private modalService: IModalService,
		private document: Document,
	) {
		this.mode = ElementMode.View;
		this._rootElm = this.element.find(".doc-element");
	}

	public edit($event: Event) {
        if (this.isClearing){
            return;
        }

		if (
			this.item.panel.controlTypeCode !== "SECTOR" &&
			this.item.panel.controlTypeCode !== "TEXTBLOCK" &&
			this.mode === ElementMode.View
		) {
			this.setMode(ElementMode.Edit);
		}
		if (this.item.panel.controlTypeCode == "SECTOR") {
			angular.element(document.getElementsByClassName("edit-wrap")).show();
		}
		if (this.item.errors) {
			this.item.errors = [];
		}
	}

	public setMode(mode?: ElementMode) {
		if (mode) {
			this.focus();
			this.mode = mode;
			this.scope.$broadcast("activateDocElement");
			this.element.addClass("nested-item__edit");
			return;
		}

		this.mode = ElementMode.View;
		this.setValidation();
		this.element.removeClass("nested-item__edit");
	}

	private setValidation() {
		if (this.item.errors && this.item.errors.length) {
			this.item.errors = this.item.errors.filter((err, i) => {
				if (err.required && (<DocDataVm>this.item.panel).value) return false;
				if (err.requiredIf && (!err.caused.panel.value || !!(<DocDataVm>this.item.panel).value)) return false;
				return true;
			});
		}
	}

	public configure($event) {
		let options = <IModalOptions>{
			id: "docModal",
			attributes: {
				valuesTreeId: this.item.panel.valuesTreeId,
				attributeTypeCode: this.item.panel.controlTypeCode,
			},
			onCloseCb: this.onConfigClose,
			innerCb: null,
		};
		this.scope.$broadcast("configOpen");
		this.modalService.open(options);
	}

	private onConfigClose = (data: any) => {
		this.scope.$broadcast("configClosed", data);
	};

	private focus() {
		setTimeout(() => {
			angular.element(this._rootElm.find("textarea,input")[0]).focus();
		});
	}

	private onDocClick = (event: ng.IAngularEvent, path?: number[]) => {
        if (this.isClearing){
            this.isClearing = false;
            return;
        }

		if (this.item.panel.controlTypeCode !== "SECTOR") {
			if (path && JSON.stringify(path) === JSON.stringify(this.pathIndex)) {
				if (this.mode === ElementMode.View) {
					this.setMode(ElementMode.Edit);
					//his.scope.$apply();
				}
				return;
			}
			this.setMode();
			//this.scope.$apply();
			this.scope.$broadcast("closeDocElement");
		}
	};

	private componetName: string;

	$onInit() {
		this.renderElement(this.item.panel.controlTypeCode);
		this.pathIndex = [];
		if (this.parentIndex) this.pathIndex = this.pathIndex.concat(this.parentIndex);
		this.pathIndex.push(this.tIndex);
	}

	$postLink() {
		this.element.on("click", this.onElementClick);
		this.docClickListener = this.scope.$on("documentClick", this.onDocClick);
		if (this.item.panel.config && this.item.panel.config.inline)
			this.element.find(".doc-element").addClass("inline-item");
		if (
			this.item.panel.parentId &&
			this.item.panel.config &&
			this.item.panel.config.width &&
			this.parentConfig.inline
		) {
			// this.element.css("flex-grow",this.item.panel.config.width);
			// this.element.css("flex-basis", (100 / this.parentConfig.width) * this.item.panel.config.width + '%');
			this.element.css(
				"width",
				(document.querySelector(".document-body").clientWidth * this.item.panel.config.width) / 12 + "px",
			);
			//todo: resize function - set this.element.width
		}
	}

	private onElementClick = event => {
		if (this.item.panel.controlTypeCode !== "SECTOR") this.scope.$emit("elmModeChanged", this.pathIndex);
	};

	private renderElement(type) {
		//TODO:find better approach to change edit and configure mode
		this.isConfigurable = false;
		this.isEditable = false;

		if (this.item.panel.controlTypeCode === "SECTOR") {
			let template = this.fixTemplate("astum/templates/sector.tmpl.html");
			let compiled = this.compile(template)(this.scope);
			this._rootElm.append(compiled);

			return;
		}
		if (this.item.panel.controlTypeCode === "TEXTBLOCK") {
			let template = this.fixTemplate("astum/templates/text-block.tmpl.html");
			let compiled = this.compile(template)(this.scope);
			this._rootElm.append(compiled);

			return;
		}
		//if (this.item.panel.controlTypeCode === 'DATE') {
		//    debugger;
		//    let template = this.fixTemplate("astum/templates/calendar.tmpl.html");
		//    let compiled = this.compile(template)(this.scope);
		//    this._rootElm.append(compiled);
		//}

		let template = this.fixTemplate("astum/templates/edit-element.tmpl.html");
		template = this.setEditor(template, this.item.panel.controlTypeCode);
		//TODO:change to  use  roles
		if (!this.isFrame) this.setConfigurator(template, this.item.panel.controlTypeCode);
		let compiled = this.compile(template)(this.scope);
		this._rootElm.append(compiled);
	}

	private setEditor(editTmpl, type): ng.IAugmentedJQuery {
		let template;
		this.isEditable = true;
		switch (type) {
			case "LEXTREE":
				template = this.fixTemplate("astum/templates/text-tree.tmpl.html");
				break;
			case "TEXT":
				template = this.fixTemplate("astum/templates/textarea.tmpl.html");
				break;
			case "TEXTEDIT":
				template = this.fixTemplate("astum/templates/textedit.tmpl.html");
				break;
			case "CHECKLIST":
				template = this.fixTemplate("astum/templates/check-list.tmpl.html");
				break;
			case "NUMBER":
				template = this.fixTemplate("astum/templates/number.tmpl.html");
				break;
			case "BIT":
				template = this.fixTemplate("astum/templates/bit.tmpl.html");
				break;
			case "DATE":
				template = this.fixTemplate("astum/templates/calendar.tmpl.html");
				break;
			case "SPREADSSHEET":
				template = this.fixTemplate("astum/templates/spreadssheet.tmpl.html");
				break;
		}
		// let editor = this.compile(template)(this.scope);

		angular.element(editTmpl[2]).append(template);

		return editTmpl;
	}

	private setConfigurator(editTmpl, type): ng.IAugmentedJQuery {
		let template;
		switch (type) {
			case "LEXTREE":
				this.isConfigurable = true;
				break;
			case "CHECKLIST":
				this.isConfigurable = true;
				break;
			case "TEXT":
				this.isConfigurable = true;
				break;
			default:
				//this._rootElm.addClass('inline');
				break;
		}
		return;
	}

	//TODO:remove this hardcoded fix, change template loader
	private fixTemplate(uri: any) {
		let template = this.templateCache.get(uri).toString();
		template = template.replace('module.exports = "module.exports = \\"', "");
		template = template.replace('\\""', "");
		template = template.replace(/\\"/g, '"');
		template = template.replace(/\\r/g, "");
		template = template.replace(/\\n/g, "");
		template = template.replace(/\\/g, "");
		return angular.element(template);
	}

    private isClearing: boolean;
    private clearElement(){
        (<any>this.item.panel).value = null;
        this.isClearing = true;
    }

	$onDestroy(): void {
		this.docClickListener();
		this.element.off("click", this.onElementClick);
	}
}

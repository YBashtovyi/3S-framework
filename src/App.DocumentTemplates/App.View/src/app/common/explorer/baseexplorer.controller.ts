import * as angular from 'angular';
import { StateParams, StateService, TransitionPromise } from '@uirouter/angularjs';
import { IFolderEntity, Result } from "../../models/common.models";

export abstract class BaseExplorerController<TItem extends IClassCodeEntity> implements ng.IComponentController {

    public items: TItem[];
    public path: TItem[];
    public parentId: string;
    public searchText: string;

    public showFolderForm: boolean;
    //searchTextExclude field needs for excluding auto adding CODE for searching field while creating new doc template
    public searchTextExclude: string[] = [
        "Other", "Nefrology", "Minimal Invasive Surgery", "Transplantation", "Pulmonology", "Endocrynology", "Urolog",
        "Nevropatolog", "Ginekolog", "Oftalmolog", "Alergolog", "Otolaryng", "Thoracic surgery", "Gastroentherology",
        "Cardiovascular surgeon", "Cardiology", "Rentgenology", "US", "Revmatolog", "Neurosorgeon", "Travmatolog",
        "Functional diagnostic", "Endoscopy", "Cardiology", "Oncology", "Pulmonology"
    ]
    constructor(protected state: StateService,
        protected stateParams: StateParams,
        private entityRouteState: string) {
        this.path = [];
    }

    $onInit() {
        this.parentId = this.stateParams['parentId'];
        if (this.stateParams && this.searchTextExclude.filter(c => this.stateParams['searchText'] === c).length > 0) {
            this.stateParams['searchText'] = '';
        }
        this.searchText = this.stateParams['searchText'];
        this.getData(this.parentId, this.searchText);
        if (this.parentId)
            this.getParentsHierarchy(this.parentId).then((data) => {
                this.path = data;
            },
                (error) => {
                    console.error(error);
                });
    }
    $onDestroy() {

    }

    protected getData(parentId?: string, searchText: string = null) {
        return this.getDataImpl(parentId, searchText).then((data) => {
            this.items = data;
        },
            (error) => {
                console.error(error);
            });
    }

    public search() {
        this.getData(this.parentId, this.searchText);
    }

    public clear() {
        delete this.searchText;
        this.getData(this.parentId);
    }

    protected abstract getDataImpl(parentId?: string, searchText?: string): ng.IPromise<TItem[]>;

    protected abstract getParentsHierarchy(parentId?: string): ng.IPromise<TItem[]>;

    public addItem(): TransitionPromise {
        return this.state.go(this.entityRouteState, { parentId: this.stateParams["parentId"] });
    }

    public addFolder(): void {
        this.showFolderForm = true;
    }

    public cancelAddingFolder(): void {
        this.showFolderForm = false;
    }

    public submitFolder(folder: IFolderEntity) {
        this.submitFolderImpl(folder).then((data) => {
            this.items.push(data);
            this.showFolderForm = false;
        },
            (error) => {
                console.error(error);
            })
            .finally(() => {
                this.showFolderForm = false;
            });
    }

    protected abstract submitFolderImpl(folder: IFolderEntity): ng.IPromise<TItem>;


    public onItemClick(item: TItem = null) {
        if (!item) {
            this.path = [];
            this.state.go('.', { parentId: null });
            return;
        }
        if (item.classShortCode === 'F') {
            this.changePath(item);
            this.state.go('.', { parentId: item.id });
            return;
        }
        if (item.classShortCode !== 'F') {
            this.state.go(this.entityRouteState, { id: item.id });
            return;
        }
    }

    private changePath(item): void {
        let index;
        this.path.forEach((parent, i) => {
            if (parent === item)
                index = i;
        });
        if (angular.isDefined(index)) {
            this.path.splice(index + 1, this.path.length - index);
            return;
        }
        this.path.push(item);
    }

    public removeItem(item: TItem, $event: Event): void {
        $event.stopPropagation();
        if (confirm("Ви дійсно бажаєте видалити запис?")) {
            this.removeItemImpl(item.id).then((result: Result) => {
                if (result.success) {
                    this.getData(this.stateParams['parentId']);
                } else {
                    alert(result.text);
                }
            });
        }
    }

    protected abstract removeItemImpl(id: string): ng.IPromise<Result>;
}

interface IClassCodeEntity {
    id: string;
    classShortCode: string;
}
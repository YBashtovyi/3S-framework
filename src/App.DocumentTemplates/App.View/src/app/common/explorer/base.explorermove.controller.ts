import * as angular from 'angular';
import { IFolderEntity } from "../../models/common.models";

export abstract class BaseExplorerMoveController<TItem extends IFolderEntity> implements ng.IComponentController{

    public item:TItem;
    private _folders: TItem[];

    constructor(){
    
    }

    $onInit() {

    }



    public getFolders(){
        this.getFoldersImpl().then((data) => {
            this._folders = data;
        });
    }

    public get folders() {
        return this._folders;
    }

    protected abstract getFoldersImpl(): ng.IPromise<TItem[]>; 


    


}
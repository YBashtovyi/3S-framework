export class ParamsList {
    [key: string]: any;
}

export class Result {
    public success: boolean;
    public text:string;
}


export enum CardMode {
    None = <any>"None"
    , Edit = <any>"Edit"
    , View = <any>"View"
    , Create = <any>"Create"
}

export interface IFolderEntity {
    id: string;
    parentId: string;
    classShortCode: string;
    caption: string;
    recordState: number;
}

export class BaseEntity {
    public originDbId: string;
    public originDbRecordId: string;
    public recordState: number;
}

export interface IConfigurable{
    config: any;
}
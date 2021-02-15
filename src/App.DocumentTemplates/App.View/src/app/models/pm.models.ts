import { IFolderEntity, IConfigurable } from './common.models';

export class DocumentPm implements IFolderEntity {
    public id: string;
    public caption: string;
    public templateId: string;
    public recordState: number;
    public classShortCode: string;
    public parentId: string;
    public documentDataList: DocDataPm[];
    public originDbId: string;
    public originDbRecordId: string;
    public entityTypeCode: string;
}

export class DocTemplateElementValuesPm {
    public id: string;
    public templateElementId: string;
    public caption: string;
    public parentId: string;
    public contentValue: any;
    public recordState: number;
    public orderNumber: number;
    public valueTypeCode: string;
    public valuesTreeId: string;
    public valuesTree:any;
    public ownerId: string;
    public originDbId: string;
    public originDbRecordId:string;
}

export class DocTemplateElementValuesTreePm {
    public id: string;
    public ownerId: string;
    public caption: string;
    public code: string;
}


export class DocTemplatePresetsPm {
    public id: string;
    public templateId: string;
    public caption: string;
    public recordState: number;
    public orderNumber: number;
    public presetValue: DocTemplatesPresetValuesPm[];
}

export class DocTemplatesPresetValuesPm {
    public id: string;
    public presetId: string;
    public value: string;
    public templateElementId: string;
}

export class DocTemplatesPm implements IFolderEntity {
    public id: string;
    public parentId: string;
    public orderNumber: number;
    public classShortCode: string;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public templateElements: DocTemplateElementsPm[];
}

export class DocTemplateElementsPm implements IConfigurable {

    public id: string;
    public parentId: string;
    public docElementId: string;
    public templateElements: DocTemplateElementsPm[];//
    public valuesTreeId: string;
    public valuesTree: DocTemplateElementValuesTreePm;
    public templateId: string;
    public globalElementId: string;
    public orderNumber: number;
    public elementTypeCode: string;
    public controlTypeCode: string;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public ownerId: string;
    public originDbId: string;
    public originDbRecordId: string;
    public config: any;
}

export class DocElementsPm {
    public id: string;
    public parentId: string;
    public parent: DocElementsPm;
    public docElements: DocElementsPm[];
    public orderNumber: number;
    public controlTypeCode: string;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public docElementValues: DocElementValuesPm[];
    public serviceOwnerId: string;
    public originDbId: string;
    public originDbRecordId: string;
}

export class DocElementValuesPm {
    public id: string;
    public parentId: string;
    public docElementId: string;
    public docElement: DocElementsPm;
    public caption: string;
    public contentValue: string;
    public contentValueTypeCode: string;
    public recordState: number;
    public orderNumber: number;
    public serviceOwnerId: string;
    public originDbId: string;
    public originDbRecordId: string;
}

export class DocDataPm extends DocTemplateElementsPm {
    public id: string;
    public templateElementId: string;
    public value: string;
}





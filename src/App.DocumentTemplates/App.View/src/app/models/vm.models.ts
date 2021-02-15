import { IFolderEntity, BaseEntity, IConfigurable } from './common.models';

export class DocumentVm extends BaseEntity implements IFolderEntity  {
    public id: string;
    public caption: string;
    public templateId: string;
    public classShortCode: string;
    public parentId: string;
    public documentDataList: DocDataVm[];
    public entityTypeCode: string;
}


export class DocTemplatesVm implements IFolderEntity {
    public id: string;
    public parentId: string;
    public orderNumber: number;
    public classShortCode: string;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public templateElements: DocTemplateElementsVm[];

}

export class DocTemplateElementsVm implements IConfigurable {
    public id: string;
    public parentId: string;
    public templateId: string;
    public globalElementId: string;
    public orderNumber: number;
    public elementTypeCode: string;
    public controlTypeCode: string;
    public valuesTreeId: string;
    public valuesTree: DocTemplateElementValuesTreeVm;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public templateElements: DocTemplateElementsVm[];
    public ownerId: string;
    public originDbId: string;
    public originDbRecordId: string;
    public config: any;
}


export class DocElementsVm {
    public id: string;
    public parentId: string;
    public parent: DocElementsVm;
    public docElements: DocElementsVm[];
    public orderNumber: number;
    public controlTypeCode: string;
    public code: string;
    public caption: string;
    public description: string;
    public recordState: number;
    public docElementValues: DocElementValuesVm[];
    public serviceOwnerId: string;
    public originDbId: string;
    public originDbRecordId: string;
}

export class DocElementValuesVm {
    public id: string;
    public parentId: string;
    public docElementId: string;
    public docElement: DocElementsVm;
    public caption: string;
    public contentValue: string;
    public contentValueTypeCode: string;
    public recordState: number;
    public orderNumber: number;
    public serviceOwnerId: string;
    public originDbId: string;
    public originDbRecordId: string;
}


export class DocTemplateElementValuesVm {
    public id: string;
    public templateElementId: string;
    public parentId: string;
    public caption: string;
    public contentValue: string;
    public recordState: number;
    public orderNumber: number;
    public valueTypeCode: string;
    public ownerId: string;
    public originDbId: string;
    public originDbRecordId: string;
    public ChildsCount: number;
}

export class DocTemplateElementValuesTreeVm {
    public id: string;
    public ownerId: string;
    public caption: string;
    public code: string;
}

export class DocDataVm extends DocTemplateElementsVm {
    public value: any;
    public nested: DocDataVm[];
    public templateElementId: string;
}

export class DocTemplatePresetsVm {
    public id: string;
    public templateId: string;
    public caption: string;
    public recordState: number;
    public orderNumber: number;
    public presetValue: DocTemplatesPresetValuesVm[];
    public serviceOwnerId: string;
    public originDbId: string;
    public originDbRecordId: string;
}

export class DocTemplatesPresetValuesVm {
    public id: string;
    public presetId: string;
    public preset: DocTemplatePresetsVm;
    public value: string;
    public templateElementId: string;
}

export class DocControlTypesVm {
    public id: string;
    public code: string;
    public caption: string;
    public description: string;
}


import { IConfigurable } from "../models/common.models";

export default class BaseService {

    protected transformConfigurable(entity: any, collectionField: string, transformer: Function): any {
        if (entity[collectionField])
            entity.templateElements = transformer(entity[collectionField], collectionField);
        return entity;
    }

    protected stringifyConfigs = (elements: IConfigurable[], collectionField: string): IConfigurable[] => {
        return elements.map((item: IConfigurable) => {
            if (typeof item.config === 'object')
                item.config = JSON.stringify(item.config);
            if ((<any>item)[collectionField] && (<any>item)[collectionField].length)
                (<any>item)[collectionField] = this.stringifyConfigs((<any>item)[collectionField], collectionField);
            return item;
        });
    }
    protected stringifyConfig = (element: IConfigurable, collectionField: string): IConfigurable => {
        if (typeof element.config === 'object')
            element.config = JSON.stringify(element.config);
        if ((<any>element)[collectionField] && (<any>element)[collectionField] && (<any>element)[collectionField].length)
            (<any>element)[collectionField] = this.stringifyConfigs((<any>element)[collectionField], collectionField);
        return element;
    }

    protected parseConfigs = (elements: IConfigurable[], collectionField: string): IConfigurable[] => {
        return elements.map((item: IConfigurable) => {
            if (typeof item.config === 'string')
                item.config = JSON.parse(item.config);
            if ((<any>item)[collectionField] && (<any>item)[collectionField].length)
                (<any>item)[collectionField] = this.parseConfigs((<any>item)[collectionField], collectionField);
            return item;
        });
    }

    protected parseConfig = (element: IConfigurable, collectionField: string): IConfigurable => {
        if (typeof element.config === 'string')
            element.config = JSON.parse(element.config);
        if ((<any>element)[collectionField].length)
            (<any>element)[collectionField] = this.parseConfigs((<any>element)[collectionField], collectionField);
        return element;
    }
}
import * as angular from 'angular';

export interface IModalService {
    add(modal): void;
    remove(modal): void;
    open(options: IModalOptions): void;
    close(id: string): void;
}

export class AstumModalService {

    constructor() {
        this.modals = [];
    }

    private modals: any[];

    public add(modal) {
        this.modals.push(modal);
    }

    public remove(modal) {
        this.modals.splice(this.modals.indexOf(modal), 1);
    }

    public open(options: IModalOptions) {
        this.modals.forEach((modal) => {
            if (modal.modalId === options.id)
                modal.open(options.attributes, options.onCloseCb, options.innerCb);
        });
    }

    public close(id: string) {
        this.modals[id].close();
    }
}

export interface IModalOptions {
    id: string;
    attributes: any;
    onCloseCb: Function;
    innerCb: Function;
}
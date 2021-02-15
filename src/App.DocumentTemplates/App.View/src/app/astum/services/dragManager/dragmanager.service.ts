import * as angular from 'angular';

export class DragManagerService {
    private dragZone: TreeDragZone;
    private avatar: TreeDragAvatar;
    private dropTarget: TreeDropTarget;
    private downX: number;
    private downY: number;
    private mouseDowm: boolean = false;


    static $inject = ['$rootScope'];

    constructor(private rootScope: ng.IRootScopeService) {

        this.dragZone = new TreeDragZone(<any>document);
        this.dropTarget = new TreeDropTarget(document, rootScope);
        document.ondragstart = () => {
            return false;
        };
        document.addEventListener("touchstart", this.onMouseDown);
        document.addEventListener("touchmove", this.onMouseMove, false);
        document.addEventListener("touchend", this.onMouseUp);

        document.addEventListener("mousedown", this.onMouseDown);
        document.addEventListener("mousemove", this.onMouseMove);
        document.addEventListener("mouseup", this.onMouseUp);
    }

    //public init(element: HTMLElement): void {
    //    this.dragZone = new TreeDragZone(<any>element);
    //    this.dropTarget = new TreeDropTarget(element, this.rootScope)

    //    element.addEventListener("mousemove", this.onMouseMove);
    //    element.addEventListener("mouseup", this.onMouseUp);
    //    element.addEventListener("mousedown", this.onMouseDown);

    //    element.addEventListener("touchstart", this.onMouseDown);
    //    element.addEventListener("touchmove", this.onMouseMove);
    //    element.addEventListener("touchend", this.onMouseUp);


    //}

    //public release() {
    //    let element = this.dragZone.elem;
    //    (<HTMLElement>element).removeEventListener("touchstart", this.onMouseDown);
    //    (<HTMLElement>element).removeEventListener("touchmove", this.onMouseMove);
    //    (<HTMLElement>element).removeEventListener("touchend", this.onMouseUp);
    //    (<HTMLElement>element).removeEventListener("mousemove", this.onMouseMove);
    //    (<HTMLElement>element).removeEventListener("mouseup", this.onMouseUp);
    //    (<HTMLElement>element).removeEventListener("mousedown", this.onMouseDown);
    //    this.cleanUp();
    //}

    public onMouseDown = (e: MouseEvent | TouchEvent) => {
        if (e.type === "mousedown" && (<MouseEvent>e).which != 1) { // не левой кнопкой
            return false;
        }
        this.dragZone = this.findDragZone(e);
        if (e.type === "touchstart" && this.isDraggable(e.target)) {
            this.dragZone.elem.children[0].style['touch-action'] = 'none';
        }

        if (this.dragZone.elem !== document && !this.dragZone) {
            return;
        }
        this.downX = getEventCoords(e).pageX;
        this.downY = getEventCoords(e).pageY;
        this.mouseDowm = true;

        return false;
    };

    private isDraggable(elem): boolean {
        while (elem !== document && !elem.attributes.draggable) {
            elem = elem.parentNode;
        }
        return (elem.attributes && elem.attributes.draggable) ? true : false;
    }

    public onMouseMove = (e: MouseEvent | TouchEvent) => {
        if (!this.dragZone || !this.mouseDowm) return; // элемент не зажат

        if (e.type === "touchmove") {
            this.dragZone.elem.getElementsByTagName('body')[0].style["touch-action"] = 'none';
        }

        if (!this.avatar) { // элемент нажат, но пока не начали его двигать
            let ev = getEventCoords(e);
            if (Math.abs(ev.pageX - this.downX) < 5 && Math.abs(ev.pageY - this.downY) < 5) {
                return;
            }
            // попробовать захватить элемент
            this.avatar = this.dragZone.onDragStart(this.downX, this.downY, e);
            if (!this.avatar) { // не получилось, значит перенос продолжать нельзя
                this.cleanUp(); // очистить приватные переменные, связанные с переносом
                return;
            }
        }

        // отобразить перенос объекта, перевычислить текущий элемент под курсором
        this.avatar.onDragMove(e);
        // найти новый dropTarget под курсором: newDropTarget
        // текущий dropTarget остался от прошлого mousemove
        // *оба значения: и newDropTarget и dropTarget могут быть null
        let newDropTarget = this.findDropTarget(e);

        if (newDropTarget != this.dropTarget) {
            // уведомить старую и новую зоны-цели о том, что с них ушли/на них зашли
            this.dropTarget && this.dropTarget.onDragLeave(newDropTarget, this.avatar, e);
            newDropTarget && newDropTarget.onDragEnter(this.dropTarget, this.avatar, e);
        }

        this.dropTarget = newDropTarget;

        this.dropTarget && this.dropTarget.onDragMove(this.avatar, e);

        return false;
    };

    public onMouseUp = (e) => {

        if (e.type === "mouseup" && (<MouseEvent>e).which != 1) { // не левой кнопкой
            return false;
        }

        if (this.avatar) { // если уже начали передвигать
            if (this.dropTarget) {
                // завершить перенос и избавиться от аватара, если это нужно
                // эта функция обязана вызвать avatar.onDragEnd/onDragCancel
                this.dropTarget.onDragEnd(this.avatar, e);
            } else {
                this.avatar.onDragCancel();
            }
        }
        this.cleanUp();
        this.mouseDowm = false;
    };
    public cleanUp() {
        // очистить все промежуточные объекты
        // this.dragZone && this.dragZone.elem.getElementsByTagName('body')[0].removeAttribute("style");
        // this.dragZone && this.dragZone.elem.children[0].removeAttribute("style");

        this.dragZone = this.avatar = this.dropTarget = null;
    }

    public findDragZone(event) {
        let elem = event.target;
        while (elem != document && !elem.dragZone) {
            elem = elem.parentNode;
        }
        return elem.dragZone;
    }

    public findDropTarget(event) {
        // получить элемент под аватаром    
        let elem = this.avatar.getTargetElem();

        while (elem != document && !elem.dropTarget) {
            elem = elem.parentNode;
        }

        if (!elem.dropTarget) {
            return null;
        }
        return elem.dropTarget;
    }
}

export class TreeDragZone {
    public elem: any;

    constructor(elem: any) {
        elem.dragZone = this;
        this.elem = elem;
    }

    public makeAvatar() {
        return new TreeDragAvatar(this, this.elem);
    };

    public onDragStart = (downX, downY, event) => {
        let avatar = this.makeAvatar();
        if (!avatar.initFromEvent(downX, downY, event)) {
            return;
        }
        return avatar;
    }
}

const getElementUnderClientXY = (elem, clientX, clientY) => {
    let display = elem.style.display || '';
    elem.style.display = 'none';

    var target = document.elementFromPoint(clientX, clientY);

    elem.style.display = display;

    if (!target || <any>target == <any>document) { // это бывает при выносе за границы окна
        target = document.body; // поправить значение, чтобы был именно элемент
    }

    return target;
}

const getCoords = (elem) => {
    let box = elem.getBoundingClientRect();
    let body = document.body;
    let docElem = document.documentElement;
    let scrollTop = window.pageYOffset || docElem.scrollTop || body.scrollTop;
    let scrollLeft = window.pageXOffset || docElem.scrollLeft || body.scrollLeft;
    let clientTop = docElem.clientTop || body.clientTop || 0;
    let clientLeft = docElem.clientLeft || body.clientLeft || 0;
    let top = box.top + scrollTop - clientTop;
    let left = box.left + scrollLeft - clientLeft;
    return {
        top: Math.round(top),
        left: Math.round(left)
    };
}

const getEventCoords = (e: MouseEvent | TouchEvent): any => {

    if (e.type.indexOf('touch') !== -1 && (<TouchEvent>e).touches.length === 1) {
        return (<TouchEvent>e).touches[0]
    }
    if (e.type.indexOf('mouse') !== -1) {
        return (<MouseEvent>e);
    }

}

export class TreeDragAvatar {

    private _dragZone: TreeDragZone;
    protected _dragZoneElem: HTMLElement;
    protected _elem;
    private _currentTargetElem;
    private _dragElement;
    protected _shiftX: number;
    protected _shiftY: number;
    public _data: any;
    private intervalId: number = 0;


    constructor(dragZone, dragElem) {
        this._dragZone = dragZone;
        this._dragZoneElem = dragElem;
        this._elem = dragElem;
    }

    public getDragInfo(event) {
        return {
            elem: this._elem,
            dragZoneElem: this._dragZoneElem,
            dragZone: this._dragZone,
            data: this._data
        };
    };

    public getTargetElem() {
        return this._currentTargetElem;
    };

    public onDragMove(e: MouseEvent | TouchEvent): void {
        this._elem.style.left = getEventCoords(e).pageX + 'px';
        this._elem.style.top = getEventCoords(e).pageY + 'px';
        this._currentTargetElem = getElementUnderClientXY(this._elem, getEventCoords(e).clientX, getEventCoords(e).clientY);

        if (this.intervalId)
            clearInterval(this.intervalId);
        if (e.type.indexOf("touch") !== -1) {
            this.scroll(<TouchEvent>e);
        }
        if (e.type.indexOf("mouse") !== -1) {
            this.mouseScroll(<MouseEvent>e);
        }
    };

    private mouseScroll(e: MouseEvent) {
        if (document.documentElement.clientHeight < document.body.clientHeight) {
            if (document.documentElement.clientHeight - 20 < e.clientY) {
                this.scrollDown();
            }
            if (e.clientY < 20) {
                this.scrollUp();
            }
        }
    }

    private scroll(e: TouchEvent) {
        let touch: any = e.touches[0];
        if (document.documentElement.clientHeight < document.body.clientHeight) {

            if (document.documentElement.clientHeight - touch.radiusY < touch.clientY) {
                this.scrollDown();
        }
            if (touch.radiusY - touch.clientY > 0) {
                this.scrollUp();
            }
        }
    }

    private scrollDown() {
        let i = Math.round(document.documentElement.scrollTop);
        this.intervalId = setInterval(() => {
            window.scroll(0, i);
            i++;
            this._elem.style.top = parseInt(this._elem.style.top) + 1 + "px";
            if (document.documentElement.scrollTop === document.body.clientHeight - document.documentElement.clientHeight) {
                clearInterval(this.intervalId);
                this.intervalId = 0;
            }
        }, 5);
    }

    private scrollUp() {
        let i = Math.round(document.documentElement.scrollTop);
        this.intervalId = setInterval(() => {
            window.scroll(0, i);
            i--;
            this._elem.style.top = parseInt(this._elem.style.top) - 1 + "px";
            if (document.documentElement.scrollTop === 0) {
                clearInterval(this.intervalId);
                this.intervalId = 0;
            }
        }, 5);
    }

  

    private findDraggable(event) {
        // if(event.target.nodeName == 'template-elements')
        //     return false;
        let e;
        if (event.target.classList.contains('move-template'))
        { e = event.target.closest('template-element') }
        else {
            e = event.target;
        }
        if (!e.attributes.draggable && !e.parentNode.attributes.draggable)
            return true;
        return false;
    }

    private findClosest(el) {
        while (el) {
            if (el.classList.contains('template-element-icons-holder'))
                return el;
            else el = el.parentElement;
        }
        return null;
    }


    public initFromEvent(downX, downY, event) {
        let e;
        if (event.target.classList.contains('move-template') || event.target.parentNode.classList.contains('move-template'))
        { e = this.findClosest(event.target) }
        else {
            e = event.target;
        }
        if (!e.attributes.draggable && !e.parentNode.attributes.draggable)
            return false;

        this._dragZoneElem = e;
        while (!this._dragZoneElem.attributes["draggable"]) {
            this._dragZoneElem = <HTMLElement>this._dragZoneElem.parentNode;
        }
        this._data = angular.element(this._dragZoneElem.parentNode).scope();
        this._dragZoneElem.classList.add("hasAvatar");
        let elem = this._elem = this._dragZoneElem.cloneNode(true);

        (<HTMLElement>elem).style.backgroundColor = "red";
        (<HTMLElement>elem).className = 'avatar';

        // создать вспомогательные свойства shiftX/shiftY
        //var coords = getCoords(this._dragZoneElem);
        //debugger;
        //this._shiftX = downX - coords.left;
        //this._shiftY = downY - coords.top;

        // инициировать начало переноса
        document.body.appendChild(elem);
        (<HTMLElement>elem).style.zIndex = '9999';
        (<HTMLElement>elem).style.position = 'absolute';

        return true;
    };

    private _destroy() {
        clearInterval(this.intervalId);
        this.intervalId = 0;
        this._dragZoneElem.classList.remove('hasAvatar');
        this._elem.parentNode.removeChild(this._elem);
    }

    public onDragCancel() {
        this._destroy();
    };

    public onDragEnd() {
        this._destroy();
    };
}


export class TreeDropTarget {

    protected targetElem;
    protected elem;

    constructor(elem: any, private rootScope: ng.IRootScopeService) {
        elem.dropTarget = this;
        this.elem = elem;
        this.targetElem = null;
    }

    public showHoverIndication(avatar: TreeDragAvatar) {
        this.targetElem && this.targetElem.classList.add('drag-hover');
    }

    public hideHoverIndication(avatar: TreeDragAvatar) {
        this.targetElem && this.targetElem.classList.remove('drag-hover');
    };

    public getTargetElem(avatar, event) {
        let target = avatar.getTargetElem();
        let elemToMove = avatar.getDragInfo(event).dragZoneElem.parentNode;
        while (target) {
            if (target.attributes && target.attributes.droppable) {
                if (!this.checkIsSelf(elemToMove, target))
                    return target;
            }
            target = target.parentNode;
        }
        return target;
    }
    private checkIsSelf(elemToMove, elem: HTMLElement): boolean {
        // проверить, может быть перенос узла внутрь самого себя или в себя?       
        do {
            if (elem == elemToMove.parentNode) return true;
            elem = <HTMLElement>elem.parentNode;
        } while (elem)

        return false;
    }

    public onDragMove(avatar, event) {
        let newTargetElem = this.getTargetElem(avatar, event);
        if (this.targetElem != newTargetElem) {
            this.hideHoverIndication(avatar);
            this.targetElem = newTargetElem;
            this.showHoverIndication(avatar);
        }
    }


    public onDragEnd(avatar: TreeDragAvatar, event: MouseEvent) {
        if (!this.targetElem) {
            // перенос закончился вне подходящей точки приземления
            avatar.onDragCancel();
            return;
        }

        this.hideHoverIndication(avatar);

        // получить информацию об объекте переноса
        var avatarInfo = avatar.getDragInfo(event);

        avatar.onDragEnd(); // аватар больше не нужен, перенос успешен

        this.rootScope.$broadcast('onDrop', { avatarData: avatarInfo.data, dropZoneData: angular.element(this.targetElem).scope() })

        this.targetElem = null;
    }

    public onDragEnter(toDropTarget, avatar, event) {
    };

    public onDragLeave(toDropTarget, avatar, event) {
        this.hideHoverIndication(avatar);
        this.targetElem = null;
    };
}
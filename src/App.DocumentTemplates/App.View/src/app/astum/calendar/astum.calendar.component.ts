import * as angular from 'angular';
import * as moment from 'moment';
import './astum.calendar.scss';

export class AstumCalendarComponent implements ng.IComponentOptions {
    controller: ng.IControllerConstructor;
    template: string;
    bindings: { [index: string]: string; };
    controllerAs: string;
    require: { [controller: string]: string };

    constructor() {
        this.controller = AstumCalendarController;
        this.template = require('./astum.calendar.html');
        this.bindings = {
            attributeId: '<',
            templateElementId: '<',
            attributeTypeCode: '<'
        }
        this.controllerAs = 'control';
        this.require = { 'ngModelController': 'ngModel' };
    }
}

export class AstumCalendarController implements ng.IComponentController {


    public weeks: Week[];
    public month: moment.Moment;
    public selected: moment.Moment;

    private ngModelController: ng.INgModelController;

    static $inject = ['$element', '$scope'];

    constructor(private element: ng.IAugmentedJQuery, private scope: ng.IScope) {
        moment.locale('uk');
    }   

    $onInit() {
        this.element.addClass('calendar clearfix');
        let start:moment.Moment = this.removeTime(this.ngModelController.$viewValue || moment()).clone();
        this.selected = this.removeTime(this.ngModelController.$viewValue || moment()).clone();
        this.month = start.clone();
        start.date(1);
        this.buildMonth(start, this.month);
        let watcher = this.scope.$watch(() => this.ngModelController.$viewValue, (newVal) => {
            this.selected = newVal;
            watcher();
        })
    }

    $onChanges(){
    
    }

    $onDestroy() {

    }

    public select(day: Day) {
        this.selected = day.date;
        this.ngModelController.$setViewValue(day.date);
    }

    public next() {
        var next = this.month.clone();
        this.removeTime(next.month(next.month() + 1)).date(1);
        this.month.month(this.month.month() + 1);
        this.buildMonth(next, this.month);
    };

    public previous() {
        var previous = this.month.clone();
        this.removeTime(previous.month(previous.month() - 1).date(1));
        this.month.month(this.month.month() - 1);
        this.buildMonth(previous, this.month);
    };

    private buildMonth(start, month) {
        this.weeks = [];
        let done = false;
        let date = start.day(0).clone();
        let monthIndex = date.month();
        let count = 0;
        while (!done) {
            this.weeks.push({ days: this.buildWeek(date.clone(), month) });
            date.add(1, "w");
            done = count++ > 2 && monthIndex !== date.month();
            monthIndex = date.month();
        }
    }

    private buildWeek(date:moment.Moment, month) {
        var days = [];
        for (var i = 0; i < 7; i++) {
            days.push({
                name: date.format("dd").substring(0, 1),
                number: date.date(),
                isCurrentMonth: date.month() === month.month(),
                isToday: date.isSame(moment(), "day"),
                date: date
            });
            date = date.clone();
            date.add(1, "d");
        }
        return days;
    }

    private removeTime(date) {
        return date.hour(0).minute(0).second(0).millisecond(0);
    }

}

class Week {
    days: Day[];
}
class Day {
    name: string;
    number: number;
    isCurrentMonth: boolean;
    isToday: boolean;
    date: moment.Moment;
}
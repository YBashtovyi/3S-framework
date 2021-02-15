import * as angular from 'angular';

import { AstumCalendarComponent } from './astum.calendar.component';

const Calendar: ng.IModule = angular
    .module('astum.calendar', [])
    .component('astumCalendar', new AstumCalendarComponent);


export default <string>Calendar.name;
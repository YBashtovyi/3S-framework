import * as angular from 'angular';
import AstumDocElement from './docElement'
import AstumTextarea from './textArea'
import AstumLexical from './lexicalTree';
import AstumCheckList from './checkList';
import AstumTextBlock from './textBlock';
import AstumAutocomplete from './autocomplete';
import AstumCalendar from './calendar';
import SpreadsSheet from './spreadsSheet';
import AstumBreadCrumbs from './bread-crumbs';
import AstumTextEditor from './texteditor';
import AstumQuillViewer from './quillviewer';
import DataService from './services';
import { DataServiceProvider } from './services/data.service';
import AstumModal from './modal';
import AstumElemenTConfig from './element-config';
import AstumValuesTree from './values-tree';
import AstumDialog from './dialog';
import AstumTemplateCopy from './copy-template';
import AstumTree from './tree';
import AstumDropDown from './dropdown';
import AstumDirectives from './directives';

import * as moment from 'moment';




const Astum: ng.IModule = angular
    .module('astum', [
        AstumDocElement,
        AstumTextarea,
        AstumLexical,
        AstumCheckList,
        AstumTextBlock,
        AstumAutocomplete,
        DataService,
        AstumCalendar,
        AstumBreadCrumbs,
        SpreadsSheet,
        AstumTextEditor,
        AstumQuillViewer,
        AstumModal,
        AstumElemenTConfig,
        AstumValuesTree,
        AstumDialog,
        AstumTemplateCopy,
        AstumTree,
        AstumDropDown,
        AstumDirectives,
        'ngSanitize'
    ]).filter('calendarToDate', () => {
        return (value: moment.Moment, format: string): string => {
            if (value) {

                return value.format(format ? format : "DD.MM.YYYY");

            }
            return '';
        };
    });

export default <string>Astum.name;
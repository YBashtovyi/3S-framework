
import UrlBuilder from '../../../../utils/url-builder'
import { env } from '../../../../services/api'
import get from 'lodash.get'
import { getOperationAccessLevel } from '../../../../services/securityService'
import * as operationAccessLevels from '../../../../constants/rigths/operationAccessLevels' 
import { mapGetters, mapActions } from 'vuex'
import {
    createNotificationInOneSignal
} from '../../../../services/apiServices/notification-api'
import { stringEmpty } from '../../../../utils/function-helpers'
import isEqual from 'lodash.isequal'
import { NOT_TRANSFERED, ERRORED, FAILED } from '../../../../constants/notifications/notificationStates'
import { CreateNotification, UpdateNotification, SendNotification } from '../constants/operationRights'
import {NOTIFICATION_TYPE, NOTIFICATION_STATE } from '../../../../constants/general/enumTypes'

export default {

    data() {

        return {

            dateTimeFormat: "DD.MM.YYYY HH:mm",

            // selected row in table
            row: {},

            columns: [
                {
                    name: 'title',
                    label: 'Заголовок',
                    field: 'title',
                    align: 'left',
                    sortable: true,
                    required: true
                },
                {
                    name: 'authorCaption',
                    label: 'Автор',
                    field: 'authorCaption',
                    align: 'left',
                    sortable: true,
                    required: true
                },
                {
                    name: 'createdAt',
                    align: 'left',
                    label: 'Дата та час створення',
                    sortable: true,
                    required: true,
                    field: 'createdAt',
                    format: val => this.$options.filters.asDate(val, this.dateTimeFormat)
                },
                {
                    name: 'url',
                    label: 'Посилання',
                    field: 'url',
                    align: 'left',
                    sortable: true,
                    required: true
                },
                {
                    name: 'typeCaption',
                    label: 'Тип',
                    field: 'typeCaption',
                    align: 'left',
                    sortable: true,
                    required: true
                },
                {
                    name: 'stateCaption',
                    label: 'Статус передачі',
                    field: 'stateCaption',
                    align: 'left',
                    sortable: true,
                    required: true
                },
                {
                    name: 'createInOneSignalDate',
                    align: 'left',
                    label: 'Дата та час відправлення',
                    sortable: true,
                    required: true,
                    field: 'createInOneSignalDate',
                    format: val => this.$options.filters.asDate(val, this.dateTimeFormat)
                },
                {
                    name: 'particularTime',
                    align: 'left',
                    label: 'Запланована дата та час відправлення',
                    sortable: true,
                    required: true,
                    field: 'particularTime',
                    format: val => this.$options.filters.asDate(val, this.dateTimeFormat)
                },
                {
                    name: 'organizationCaption',
                    label: 'Медичний заклад в якому створенно сповіщення',
                    field: 'organizationCaption',
                    align: 'left',
                    sortable: true,
                    required: true
                }
            ],

            pagination: {
                descending: true,
                page: 1,
                rowsPerPage: 10,
                rowsNumber: 10,
                sortBy: 'createdAt'
            }
        }
    },
    
    computed: {

        ...mapGetters('baseElements', { 
            rights: 'getUserRights', 
            user: 'getUserInfo'
        }),
        
        createNotificationVisible() {
            
            const operationRightName = CreateNotification
            const operationAccessLevel = getOperationAccessLevel(this.rights, operationRightName)
            return isEqual(operationAccessLevel, operationAccessLevels.WRITE)
        },

        actions() {

            return [
                {
                    type: 'rowFn',
                    visible: false,
                    fn: props => this.openDetailPage(props.row)
                },
                {
                    label: 'Переглянути',
                    icon: 'fas fa-eye',
                    visible: true,
                    fn: props => this.openDetailPage(props)
                },
                {
                    label: 'Редагувати',
                    icon: 'far fa-edit',
                    visible: this.editActionVisible(),
                    fn: props => this.openEditPage(props)
                },
                {
                    label: 'Відправити',
                    icon: 'fas fa-paper-plane',
                    visible: this.sendNotificationVisible(),
                    fn: props => this.createNotificationInOneSignal(props)
                }
            ]
        }, 
        
        requestUrl() {            
            
            return new UrlBuilder({ host: env.NOTIFICATION.BY_AUTHOR })
                .build()
        },

        searchConfig() {

            return {

                searchUrl: this.requestUrl,

                mainInput: {
                    type: 'text',
                    label: "Заголовок",
                    key: 'title',
                    value: ''
                },
                // filters form 
                filters: [
                    {
                        type: "text",
                        label: "Автор повідомлення",
                        key: "authorCaption",
                        value: null
                    },
                    {
                        type: 'multi-select',
                        label: 'Тип повідомлення',
                        url: this.getNotificationTypeUrl(),
                        key: 'typeId',
                        optionLabel: 'caption',
                        value: null
                    },                    
                    {
                        type: 'multi-select',
                        label: 'Статус передачі',
                        url: this.getNotificationStateUrl(),
                        key: 'stateId',
                        optionLabel: 'caption',
                        value: null
                    },                    
                    {
                        type: "date-range",
                        label: "Дата створення",
                        key: "createdAt",
                        start: {
                            key: "createdAt_from",
                            value: ""
                        },
                        end: {
                            key: "createdAt_to",
                            value: ""
                        }
                    },
                    {
                        type: "date-range",
                        label: "Дата відправлення",
                        key: "createInOneSignalDate",
                        start: {
                            key: "createInOneSignalDate_from",
                            value: ""
                        },
                        end: {
                            key: "createInOneSignalDate_to",
                            value: ""
                        }
                    },
                    {
                        type: "text",
                        label: "Медичний заклад в якому створенно сповіщення",
                        key: "organizationCaption",
                        value: null
                    }
                ]
            }
        }
    },

    methods: {

        ...mapActions('tableList', ['init']),

        editActionVisible() {

            const operationRightName = UpdateNotification
            const operationAccessLevel = getOperationAccessLevel(this.rights, operationRightName)
            const notificationStateCode = get(this.row, 'stateCode', stringEmpty())
            const employeeId = get(this.user, 'employeeId', null)
            const authorId = get(this.row, 'authorId', null)

            return isEqual(operationAccessLevel, operationAccessLevels.WRITE) && 
                (isEqual(notificationStateCode, NOT_TRANSFERED) || isEqual(notificationStateCode, ERRORED) || isEqual(notificationStateCode, FAILED)) && 
                isEqual(employeeId, authorId)
                
        },

        sendNotificationVisible() {

            const operationRightName = SendNotification
            const operationAccessLevel = getOperationAccessLevel(this.rights, operationRightName)
            const notificationStateCode = get(this.row, 'stateCode', stringEmpty())
            const employeeId = get(this.user, 'employeeId', null)
            const authorId = get(this.row, 'authorId', null)

            return isEqual(operationAccessLevel, operationAccessLevels.WRITE) && 
                isEqual(notificationStateCode, NOT_TRANSFERED) && 
                isEqual(employeeId, authorId)
        },

        getNotificationTypeUrl() {

            return new UrlBuilder({ host: env.ENUMRECORD.PATH })
                .param("enumType", NOTIFICATION_TYPE)
                .build()
        },

        getNotificationStateUrl() {

            return new UrlBuilder({ host: env.ENUMRECORD.PATH })
                .param("enumType", NOTIFICATION_STATE)
                .build()
        },

        /**
         * Reload data in table list
         */
        reloadGridData() {
            const data = {
                pagination: this.pagination,
                requestUrl: this.requestUrl
            }
            this.init(data)
        },

        onNewNotification() {

            const routeName = "notificationCreate"
            this.routeToAction(routeName)
        },

        openDetailPage(row) {

            const routeName = 'notificationDetails'
            const routeParams = {
                id: get(row, 'id', null)
            }

            this.routeToAction(routeName, routeParams)
        },

        openEditPage(row) {

            const routeName = 'notificationEdit'
            const routeParams = {
                id: get(row, 'id', null)
            }

            this.routeToAction(routeName, routeParams)
        },

        createNotificationInOneSignal(row) {

            const id = get(row, 'id', null)
            createNotificationInOneSignal(id)
                .finally(this.reloadGridData)

        },

        /**
         * route to action
         */
        routeToAction(name, params = {}) {
            this.$router.push({
                name,
                params
            })
        }
    }
}
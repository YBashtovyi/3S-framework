import {
    fetchNotificationById
,
    createNotificationInOneSignal
} from '../../../../services/apiServices/notification-api'

import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import { mapGetters } from 'vuex'
import { getOperationAccessLevel } from '../../../../services/securityService'
import * as operationAccessLevels from '../../../../constants/rigths/operationAccessLevels'
import isEqual from 'lodash.isequal'
import { stringEmpty } from '../../../../utils/function-helpers'
import { NOT_TRANSFERED, ERRORED, FAILED } from '../../../../constants/notifications/notificationStates'
import { UpdateNotification, SendNotification } from '../constants/operationRights'

export default {

    
    data() {

        return {

            dateTimeFormat: "DD.MM.YYYY HH:mm",
            cardId: this.$route.params.id,
            loaded: false,
            hasErrors: false,
            notification: {

            },
            receivers: []
        }
    },

    computed: {

        ...mapGetters('baseElements', { 
            rights: 'getUserRights', 
            user: 'getUserInfo'
        }),

        title() {

            return {
                label: `Сповіщення від ${this.$options.filters.asDate(this.notification.createdAt, this.dateTimeFormat)}`,
                icon: 'fas fa-bell',
                content: `Заголовок: ${this.notification.title}`,
                contentExt: `Стан передачі: ${this.notification.stateCaption}`,
                actions: [
                    {
                        action: this.showEditPage,
                        icon: "far fa-edit",
                        iconSize: "18px",
                        label: "Редагувати",
                        disable: this.isEditActionDisable
                    }
                ],
                buttons: [
                    {
                        visible: this.sendNotificationVisible,
                        icon: 'fas fa-paper-plane',
                        label: 'Відправити',
                        action: this.createNotificationInOneSignal
                    }
                ]
            }            
        },

        sendNotificationVisible() {

            const operationRightName = SendNotification
            const operationAccessLevel = getOperationAccessLevel(this.rights, operationRightName)
            const notificationStateCode = get(this.notification, 'stateCode', stringEmpty())
            const employeeId = get(this.user, 'employeeId', null)
            const authorId = get(this.notification, 'authorId', null)

            return isEqual(operationAccessLevel, operationAccessLevels.WRITE) && 
                isEqual(notificationStateCode, NOT_TRANSFERED) && 
                isEqual(employeeId, authorId)
        },

        isEditActionDisable() {

            const operationRightName = UpdateNotification
            const operationAccessLevel = getOperationAccessLevel(this.rights, operationRightName)            
            const employeeId = get(this.user, 'employeeId', null)
            const authorId = get(this.notification, 'authorId', null)
            const notificationStateCode = get(this.notification, 'stateCode', stringEmpty())
            
            const result = isEqual(operationAccessLevel, operationAccessLevels.WRITE) && 
                (isEqual(notificationStateCode, NOT_TRANSFERED) || isEqual(notificationStateCode, ERRORED) || isEqual(notificationStateCode, FAILED)) &&
                isEqual(employeeId, authorId)

            return !result
        },

        receiversVisible() {

            return !isEmpty(this.receivers)
        },

        errorVisible() {

            const error = get(this.notification, 'error', stringEmpty())
            return !isEmpty(error)
        }
    },

    created() {

        this.getPageData()
    },

    methods: {

        createNotificationInOneSignal() {

            const id = get(this.notification, 'id', null) 

            createNotificationInOneSignal(id)
                .finally(this.getPageData)
        },

        showEditPage() {

            const routeName = 'notificationEdit'
            const routeParams = {
                id: this.cardId
            }

            this.routeToAction(routeName, routeParams)
        },

        /**
         * route to action
         */
        routeToAction(name, params = {}) {
            this.$router.push({
                name,
                params
            })
        },

        setPageReady() {

            this.loaded = true
        },

        setPageIsNotReady() {
            
            this.loaded = false
        },

        setHasErrors() {

            this.hasErrors = true
        },

        getPageData() {

            fetchNotificationById(this.cardId)
                .then(this.setPageData)
                .then(this.setPageReady)
        },

        setPageData(data) {

            this.notification = data
            this.receivers = get(data, 'receivers', [])
        }
    }
}
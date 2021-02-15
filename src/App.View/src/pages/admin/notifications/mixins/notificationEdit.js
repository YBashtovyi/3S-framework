import isEmpty from 'lodash.isempty'
import moment from 'moment'
import {
    fetchNotificationEditData,
    createNotification,
    updateNotification
} from '../../../../services/apiServices/notification-api'
import get from 'lodash.get'
import set from 'lodash.set'
import { stringEmpty } from '../../../../utils/function-helpers'
import { USUAL, SYSTEM } from '../../../../constants/notifications/notificationTypes'
import isEqual from 'lodash.isequal'
import validator from '../../../../services/validationService'

export default {

    data() {

        return {

            dateTimeFormat: "DD.MM.YYYY HH:mm",
            dateTimeFormatMask: 'xx.xx.xxxx xx:xx',
            emptyGuid: '00000000-0000-0000-0000-000000000000',

            notificationType: null,
            notificationTypes: [],
            cardId: this.$route.params.id,
            loaded: false,
            hasErrors: false,
            notification: {},
            receivers: [],
            particularTime: null,
            savingNotification: false
        }
    },

    computed: {

        // return page header title
        headerTitle() {

            return isEmpty(this.cardId) ? 'Створення сповіщення' : 'Редагування сповіщення'
        },


        receiversVisible() {

            const notificationTypeCode = get(this.notificationType, 'code', stringEmpty())
            return isEqual(notificationTypeCode, USUAL)
        },

        // #region rules

        requiredTitle() {

            const rules = []
            rules.push(x => !!x || "Обов'язкове поле")
            return rules
        },
 
        requiredMessage() {

            const rules = []
            rules.push(x => !!x || "Обов'язкове поле")
            return rules
        },
        // #endregion

        saveNotificationDisable () {

            return this.savingNotification
        }
    },

    created() {

        this.getPageData()
    },

    methods: {

        onRemoveReceiver(receiver) {

            this.receivers = this.receivers.filter(x => x !== receiver)
        },

        onSuccessAddReceivers(employees) {            

            this.receivers = employees
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
            
            const id = isEmpty(this.cardId) ? this.emptyGuid : this.cardId
            fetchNotificationEditData(id)
                .then(this.setPageData)
                .then(this.setPageReady)
                .catch(this.setHasErrors)

        },

        setPageData(data) {

            this.notificationTypes = get(data, 'notificationTypes', [])
            
            if (!isEmpty(this.cardId)) {

                this.notification = get(data, 'notification', {})
                this.receivers = get(data, 'receivers', [])
                const notificationTypeId = get(this.notification, 'typeId', null)

                if (!isEmpty(notificationTypeId)) {

                    this.notificationType = this.notificationTypes.find(x => x.id === notificationTypeId)
                }

                const particularTime = get(this.notification, 'particularTime', null)
                this.particularTime =  this.$options.filters.asDate(particularTime, this.dateTimeFormat)
            }
        
        },

        /**
         * validates time field
         */
        validateTime(time) {

            return !time || moment(time, this.timeMask, true).isValid()
        },

        onNotificationTypeChanged(value) {

            this.notificationType = value
        },        

        // #region card action

        /**
         * cancel card button clicked
         */
        onCancel() {
            
            this.$router.go(-1)
        },


        /**
         * check is form valid
         */
        getIsValidForm() {

            const validatedFields = {

                title: {value: !isEmpty(get(this.notification, 'title', '')), errorMessage: "Поле <b>Заголовок</b> не вказано"},
                message: {value: !isEmpty(get(this.notification, 'message', '')), errorMessage: "Поле <b>Текст сповіщення</b> не вказано"},
                notificationType: {value: !isEmpty(this.notificationType), errorMessage: "Поле <b>Тип сповіщення</b> не вказано"}
            }

            const notificationTypeCode = get(this.notificationType, 'code', stringEmpty())
            
            if (!isEmpty(notificationTypeCode) && !isEqual(notificationTypeCode, SYSTEM)) {
                
                validatedFields.receivers = {value: this.receivers && this.receivers.length > 0, errorMessage: "Секція <b>Отримувачі сповіщення</b> повинен містити хоча б один запис" }
            }

            const validationResult = isEqual(validator.validateFields(validatedFields, this.$q.notify).length, 0)

            return validationResult
        },

        /**
         * save card button clicked
         */
        onSaveCard() {

            if (!this.getIsValidForm()) {
                
                return
            }

            this.setSavingNotificationState()
            if (isEmpty(this.cardId)) {
                
                this.createNotification()
            }
            else {

                this.updateNotification()
            }
        },

        setSavingNotificationState() {

			this.savingNotification = true
		},

		setNotSavingNotificationState() {

			this.savingNotification = false
		},

        /**
         * prepare data for submit
         */
        getNotificationDataBeforeSubmit() {

            const newNotification = {
                title: get(this.notification, 'title', ''),
                message: get(this.notification, 'message', ''),
                url: get(this.notification, 'url', ''),
                typeId: get(this.notificationType, 'id', null),
                receivers: this.receivers
            }

            const notififcationTypeCode = get(this.notificationType, 'code', null)
            
            if (isEqual(notififcationTypeCode, SYSTEM)) {

                newNotification.receivers = []
            }

            let particularTime = this.particularTime

            if (!isEmpty(particularTime)) {

                particularTime = moment(particularTime, [this.dateTimeFormat], 'uk').format()
            }

            set(newNotification, ['particularTime'], particularTime)
            
            return newNotification
        },

        createNotification() {

            const data = this.getNotificationDataBeforeSubmit()
            createNotification(data)
                .then(this.leavePageAfterCreate)
                .finally(this.setNotSavingNotificationState)
        },

        leavePageAfterCreate(data) {

            const routeName = "notificationDetails"
            const params = {
                id: get(data, 'id', null)
            }
            this.routeToAction(routeName, params)
        },
        
        updateNotification() {

            const id = this.cardId
            const data = {...this.notification, ...this.getNotificationDataBeforeSubmit() }

            const leavePageAfterUpdate = () => this.leavePageAfterUpdate(id)
            updateNotification({id, data})
                .then(leavePageAfterUpdate)
                .finally(this.setNotSavingNotificationState)
        },

        leavePageAfterUpdate(id) {

            const routeName = "notificationDetails"
            const params = {
                id: id
            }
            this.routeToAction(routeName, params)
        },
        
        // #endregion

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
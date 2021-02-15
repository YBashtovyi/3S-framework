
import isEmpty from 'lodash.isempty'
import isEqual from 'lodash.isequal'
import get from 'lodash.get'
import { stringEmpty } from '../utils/function-helpers'

export default {

    methods: {

        initOneSignal(userInfo) {
            
            // Check is we're on a supported browser
            const oneSignal = get(window, 'OneSignal', [])
            const userId = get(userInfo, 'userId', null)
            const subscribedToNotifications = get(userInfo, 'subscribedToNotifications', false)
            
            if (isEmpty(oneSignal) || !oneSignal.isPushNotificationsSupported()) {
                
                return
            }

            // initialize OneSignal
            oneSignal.push(function() {

                oneSignal.init({
                    appId: process.env.ONE_SIGNAL_APPLICATION_ID
                })
            })
                
            // if the user is already subscribed to our messages, 
            // immediately send our user Id to OneSignal
            oneSignal.isPushNotificationsEnabled(isEnabled => {
                
                this.updateSubscriptionState(isEnabled, userId)
            })

            // listener that will monitor the changes in the notification subscription
            // if the user has subscribed now - send our user Id to OneSignal
            oneSignal.on('subscriptionChange', (isSubscribed) => {

                this.updateSubscriptionState(isSubscribed, userId)
                this.updateUserInfo(userInfo, isSubscribed)
            })
            
            // subscribe or unsubScribe for OneSignal notification
            if (isEqual(subscribedToNotifications, true)) {

                this.subscribeForOneSignalNotifications()
            }
            else {
                
                this.unsubscribeForOneSignalNotifications()
            }

        },

        updateUserInfo(userInfo, isSubscribed) {            
            
            const subscribedToNotifications = get(userInfo, 'subscribedToNotifications', false)

            if (!isEqual(subscribedToNotifications, isSubscribed)) {

            }
        },

        // Send our user Id to OneSignal (there it will be saved as ExternalUserId)
        updateSubscriptionState(isSubscribed, userId) {
        
            if (isSubscribed) {

                this.setExternalUserIdForPlayerId(userId)
            }
        },

        // Associate our user id with oneSignal player id
        setExternalUserIdForPlayerId(userId) {
            
            const oneSignal = get(window, 'OneSignal', [])
           
            if (!isEmpty(oneSignal)) {
            
                oneSignal.setExternalUserId(userId)
            }
        },

        // Unsubcribe for notification
        unsubscribeForOneSignalNotifications() {
            
            const oneSignal = get(window, 'OneSignal', [])
            
            if (!isEmpty(oneSignal)) {

                oneSignal.setSubscription(false)
            }
        },

        removeExternalUserIdForPlayerId() {

            const oneSignal = get(window, 'OneSignal', [])

            if (!isEmpty(oneSignal)) {

                oneSignal.removeExternalUserId()
            }
        },

        // subcribe for OneSignal notification
        subscribeForOneSignalNotifications() {
        
            this.getSubscriptionAndNotificationState()
                .then(this.subscribeOnNotification)
        },

        subscribeOnNotification(state) {
        
            const permissionState = get(state, 'permissionState', stringEmpty())
            
            if (!isEqual(permissionState, 'denied')) {
                
                const isOptedOut = get(state, 'isOptedOut', false)        
                const oneSignal = get(window, 'OneSignal', [])

                if (isEqual(isOptedOut, true)) {

                    oneSignal.setSubscription(true)
                }
            }
        },

        getSubscriptionAndNotificationState() {

            const oneSignal = get(window, 'OneSignal', [])
            const actions = [oneSignal.isPushNotificationsEnabled(), oneSignal.isOptedOut(), oneSignal.getNotificationPermission()]

            return Promise.all(actions)
                .then(this.processSubscriptionAndNotificationState)
        },

        processSubscriptionAndNotificationState(data) {

            const [isPushEnabled, isOptedOut, permissionState] = data

            return { 
                isPushEnabled, isOptedOut, permissionState
            }
        }
    }
}
// api
import {
    getData,
    env,
    postData,
    putData
} from '../api'
import UrlBuilder from '../../utils/url-builder'

/**
 * get notification edit data
 */
export const fetchNotificationEditData = id => {
    
    const url = new UrlBuilder({ host: env.NOTIFICATION.EDIT_PAGE_DATA})
        .path(id)
        .build()

    return getData(url).then(reponse => reponse.data)
}

/**
 * get notification by id
 */
export const fetchNotificationById = id => {

    const url = new UrlBuilder({ host: env.NOTIFICATION.EXT })
        .path(id)

    return getData(url).then(reponse => reponse.data)
}

/**
 * create new notification in application
 */
export const createNotification = data => {

    const url = new UrlBuilder({ host: env.NOTIFICATION.PATH })
        .build()

    return postData(url, data).then(reponse => reponse.data)
}

/**
 * update notification in application
 */
export const updateNotification = ({id, data}) => {

    const url = new UrlBuilder({ host: env.NOTIFICATION.PATH })
        .path(id)
        .build()

    return putData(url, data).then(reponse => reponse.data)
}

/**
 * create notification in oneSignal  
 */
export const createNotificationInOneSignal = id => {

    const url = new UrlBuilder({ host: env.NOTIFICATION.ONE_SIGNAL.CREATE })
        .path(id)
        .build()

    return postData(url).then(response => response.data)
}
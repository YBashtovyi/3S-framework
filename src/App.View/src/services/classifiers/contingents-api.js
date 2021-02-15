// api
import {
    getData,
    postData,
    putData,
    env
} from '../api'

import UrlBuilder from '@/utils/url-builder'

/**
 * get medical service program details data by id
 * @param {*} id 
 */
export const fetchContingentById = id => {

    const url = new UrlBuilder({ host: env.CONTINGENT.EXT})
        .path(id)
        .build()        
    return getData(url).then(reponse => reponse.data)
} 

/**
 * create contingent
 * @param {*} contingent 
 */
export const createContingent = (contingent) => {

    const url = new UrlBuilder({ host: env.CONTINGENT.PATH})
        .build()        
    return postData(url, contingent).then(reponse => reponse.data)
} 

/**
 * update contingent 
 * @param {*} contingent 
 */
export const updateContingent = ({id, contingent}) => {

    const url = new UrlBuilder({ host: env.CONTINGENT.PATH})
        .path(id)
        .build()        
    return putData(url, contingent).then(reponse => reponse.data)
} 

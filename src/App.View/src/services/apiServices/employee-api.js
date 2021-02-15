// api
import {
    getData,
    env
} from '../api'

import UrlBuilder from '../../utils/url-builder'
import get from 'lodash.get'
import { stringEmpty } from '../../utils/function-helpers'
import isEmpty from 'lodash.isempty'

/**
 * get all employees
 * @param {*} id 
 */
export const fetchAllEmployees = () => {
    
    const url = new UrlBuilder({ host: env.EMPLOYEES.PATH})
        .build()
        
    return getData(url).then(reponse => reponse.data)
} 


/**
 * get all employees with user ids
 */
export const fetchEmployeesWithUserId = queryParams => {

    const urlBuilder = new UrlBuilder({ host: env.EMPLOYEES.LIST_WITH_USER_ID })
        
    for (var index in queryParams) {

        const param = queryParams[index]
        const paramName = get(param, 'name', stringEmpty())
        const paramValue = get(param, 'value', stringEmpty())

        if (!isEmpty(paramName) && !isEmpty(paramValue)) {

            urlBuilder.param(paramName, paramValue)
        }
    }
    
    const url = urlBuilder.build()

    return getData(url).then(response => response.data)
} 
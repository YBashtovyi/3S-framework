import set from 'lodash.set'
import { ENUMRECORD } from '../utils/axios-env-config'
import { getData, postData } from './api'
import UrlBuilder from '../utils/url-builder'

export const fetchListEnumByGroup = groups => {

    const groupByName = itemsToGroup => groups.reduce((groped, group) => {
        const groupFilter = itemsToGroup.filter(itemToGroup => itemToGroup.group === group)        
        return set(groped, group, groupFilter)
    }, {})

    const url = new UrlBuilder({ host: ENUMRECORD.PATH })
        .param('group', `[${groups.toString()}]`)
        .build()
    
    return getData(url)
        .then(response => response.data)
        .then(groupByName)
}

export const fetchEnumById = id => {

    const url = new UrlBuilder({ host: ENUMRECORD.PATH })
            .path(id)
            .build()

    return getData(url).then(response => response.data)
}

export const createEnum = enumRecord => {

    const url = new UrlBuilder({ host: ENUMRECORD.PATH })
            .path('Create')
            .build()

    return postData(url, enumRecord).then(response => response.data)
}


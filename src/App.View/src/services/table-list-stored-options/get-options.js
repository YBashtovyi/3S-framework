// CONSTANTS
import { TABLE_LIST_OPTIONS } from './constants'

import find from 'lodash.find'
import get from 'lodash.get'
import { 
    validateComponentKey,
    validateOptionsKey } from './validate'
/**
 * Refers to local storage with key TABLE_LIST_OPTIONS to get stored pagination or filters for options
 * @param {String} componentKey - uses to get exact component stored data by key
 */
export default function(componentKey) {
    
    validateComponentKey(componentKey)

    const globalOptions = JSON.parse(localStorage.getItem(TABLE_LIST_OPTIONS))
    const concreteOptions = find(globalOptions, go => go.componentKey === componentKey)

    /**
     * Gets pagination of filters from stored in local storage table list options
     * @param {String} optionKey 
     * PAGINATION_OPTIONS - to get pagination, 
     * FILTER_OPTIONS - to get filters
     */
    const concreteOptionsByOptionKey = optionKey => { 
        validateOptionsKey(optionKey)
        const optionsByOptionKey = get(concreteOptions, optionKey, null)
        return optionsByOptionKey && typeof optionsByOptionKey === 'string' 
            ? JSON.parse(optionsByOptionKey)
            : null
    }
    return concreteOptionsByOptionKey
}
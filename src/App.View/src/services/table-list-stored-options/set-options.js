// CONSTANTS
import { TABLE_LIST_OPTIONS } from './constants'

import getNewGlobalOptions from './get-new-global-options'
import { 
    validateComponentKey, 
    validateOptionsKey } from './validate'

/**
 * Stores new options into local storage by component key
 * @param {String} componentKey uses to get exact component stored data by key
 * @param {Array} options new values which need to be stored
 * @param {String} optionsKey key for which new values should be stored
 */
export const setTableOptions = (componentKey, options, optionsKey) => {
    
    validateComponentKey(componentKey)
    validateOptionsKey(optionsKey)

    const newOptions = getNewGlobalOptions(componentKey)(options, optionsKey)
    localStorage.setItem(TABLE_LIST_OPTIONS, JSON.stringify(newOptions))
}
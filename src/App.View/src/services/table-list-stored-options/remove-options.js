// CONSTANTS
import { TABLE_LIST_OPTIONS } from './constants'

import { validateComponentKey } from './validate'
import isEmpty from 'lodash.isempty'
import filter from 'lodash.filter'
/**
 * Removed items from local storage by component name. 
 * If the TABLE_LIST_OPTIONS store become empty - remove it from global local storage
 * @param {String} componentKey uses to get exact component stored data by key
 */
export const removeTableOptions = (componentKey) => {
    validateComponentKey(componentKey)
    const globalOptions = JSON.parse(localStorage.getItem(TABLE_LIST_OPTIONS))

    if (!isEmpty(globalOptions)) {
        const newGlobalOptions = [...filter(globalOptions, go => go.componentKey !== componentKey)]
        if (!isEmpty(newGlobalOptions)) {
            localStorage.setItem(TABLE_LIST_OPTIONS, JSON.stringify(newGlobalOptions))
        } else {
            localStorage.removeItem(TABLE_LIST_OPTIONS)
        }
    }
}
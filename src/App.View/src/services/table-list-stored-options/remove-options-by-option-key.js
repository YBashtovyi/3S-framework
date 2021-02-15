// CONSTANTS
import { TABLE_LIST_OPTIONS } from './constants'

import { validateComponentKey, validateOptionsKey } from './validate'
import isEmpty from 'lodash.isempty'
// import filter from 'lodash.filter'
import find from 'lodash.find'
import filter from 'lodash.filter'
/**
 * Removes items from local storage by component name and option key. 
 * If the TABLE_LIST_OPTIONS store become empty - remove it from global local storage
 * @param {String} componentKey uses to get exact component stored data by key
 * @param {String} optionKey uses to get exact component options
 */
export default function (componentKey, optionsKey) {
    
    validateComponentKey(componentKey)
    validateOptionsKey(optionsKey)

    const globalOptions = JSON.parse(localStorage.getItem(TABLE_LIST_OPTIONS))
    if (!isEmpty(globalOptions)) {
        const concreteOptions = find(globalOptions, go => go.componentKey === componentKey)
        if (!isEmpty(concreteOptions)) {
            if (!isEmpty(concreteOptions[optionsKey])) {
                concreteOptions[optionsKey] = JSON.stringify([])
                const newOptions = filter(globalOptions, go => go.componentKey !== componentKey)
                newOptions.push(concreteOptions)
                localStorage.setItem(TABLE_LIST_OPTIONS, JSON.stringify(newOptions))
            }
        }
    }
}
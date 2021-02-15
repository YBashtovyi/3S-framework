// CONSTANTS
import { TABLE_LIST_OPTIONS } from './constants'
    
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'
/**
 * Returns new stored data which need to be rewrote 
 * @param {*} componentKey uses to get exact stored data unique for all grid components
 */
export default function(componentKey) {
    const globalOptions = JSON.parse(localStorage.getItem(TABLE_LIST_OPTIONS)) ||
            [{ componentKey }] 

    const concreteOptions = find(globalOptions, go => go.componentKey === componentKey)

    /**
     * Returns new global options for table list data 
     * @param newOptions - new options for stored table list options
     * @param {String} optionsKey key for which new values should be stored
     */
    const getNewOptions = (newOptions, optionsKey) => {
        
        if (isEmpty(concreteOptions)) {
            const newGlobalOptions = [...globalOptions]
            newGlobalOptions.push({ componentKey, [optionsKey]: JSON.stringify(newOptions)})
            return newGlobalOptions
        }

        return globalOptions.map(g => g.componentKey === componentKey
            ? {...g, [optionsKey]: JSON.stringify(newOptions) }
            :  g)
    }

    return getNewOptions
}
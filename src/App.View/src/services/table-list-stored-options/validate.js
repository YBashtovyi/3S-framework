// CONSTANTS
import {
    PAGINATION_OPTIONS,
    FILTER_OPTIONS,
    ACTIVE_FILTERS_OPTIONS
} from './constants'
import isEmpty from 'lodash.isempty'

/**
 * Throws exception in case if component key is invalid
 * @param {String} componentKey uses to get exact component stored data by key
 */
export const validateComponentKey = (componentKey) => {
    if (isEmpty(componentKey)) {
        throw new Error(`Component key parameter is missing`)
    }

    if (typeof componentKey !== 'string') {
        throw new Error(`Component key parameter has wrong type (${typeof componentKey}). Should be string`)
    }
}

/**
 * Throws exception in case if optionsKey key is invalid
 * @param {String} optionsKey uses to get exact component stored data by key
 */
export const validateOptionsKey = (optionsKey) => {
    if (isEmpty(optionsKey)) {
        throw new Error(`Component key parameter is missing`)
    }
    const isValidFormat =   optionsKey === PAGINATION_OPTIONS ||
                            optionsKey === FILTER_OPTIONS ||
                            optionsKey === ACTIVE_FILTERS_OPTIONS
    if (!isValidFormat) {
        throw new Error(`Component key parameter is in wrong format`)               
    }
}
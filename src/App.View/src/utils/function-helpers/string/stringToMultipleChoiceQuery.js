import isArray from 'lodash.isarray'
import isEmpty from 'lodash.isempty'
/**
 * Converts string array to query string for multiple choice
 * @param {Array} multipleValues values for 
 */
export default function stringToMultipleChoiceQuery(multipleValues) {
    if (!isEmpty(multipleValues) && isArray(multipleValues)) {
        const arrayStringSeparatedByComma = multipleValues.join(',')
        return `[${arrayStringSeparatedByComma}]`
    } else {
        return multipleValues
    }
}
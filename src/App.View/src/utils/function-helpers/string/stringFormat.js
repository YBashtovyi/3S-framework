// Utils
import isEmpty from '../common/isEmpty'


const throwIfInvalidParams = (placholderedText, valuesForPlaceholders) => {

    if (isEmpty(placholderedText)) {
        throw new Error('valuesForPlaceholders must be non-null, non-undefined or non-empty value')
    }

    if (isEmpty(valuesForPlaceholders)) {
        throw new Error('valuesForPlaceholders must be non-null or non-undefined value')
    }

    if (typeof placholderedText !== 'string') {
        throw new Error('placholderedText must be a string')
    }

    if (!Array.isArray(valuesForPlaceholders)) {
        throw new Error('valuesForPlaceholders must be array')
    }
}


/**
 * A simple method of replacing a placeholder with specific values ​​from the second parameter
 * 
 * Usage:      
 *            stringFormat('test {0} test', ['foo']) === test foo test
 *  
 * 
 * @param {String} placholderedText string whith placholders for the replacing
 * @param {Array} valuesForPlaceholders set of values for the replacing
 */
export default function (placholderedText, valuesForPlaceholders) {

    throwIfInvalidParams(placholderedText, valuesForPlaceholders)

    return placholderedText.replace(new RegExp("{[0-9]+}", "g"), placeHolder => {

        const intVal = parseInt(placeHolder.substring(1, placeHolder.length - 1))
        let replace

        if (intVal >= 0) replace = valuesForPlaceholders[intVal]
        else if (intVal === -1) replace = '{'
        else if (intVal === -2) replace = '}'
        else replace = ''

        return replace

    })
}

import isEmpty from 'lodash.isempty'
import stringFormat from './stringFormat'

export default function (text) {

    if (isEmpty(text)) {
        return null 
    }

    const capitalizedFirstChar = text.charAt(0).toUpperCase()
    const lastChars = text.slice(1)
    const capitalized = stringFormat('{0}{1}', [capitalizedFirstChar, lastChars])

    return capitalized
}
import isEmpty from 'lodash.isempty'
import { stringEmpty } from '../function-helpers'
import getMapping from './mapping'


export default function (text, transliterationOptions) {

    if (isEmpty(text)) {
        throw new Error('text param is null or undefined')
    }
    
    if (isEmpty(transliterationOptions)) {
        transliterationOptions = { mappingName: 'ukr2en' }
    }

    const mapping = getMapping(transliterationOptions.mappingName)

    if (isEmpty(mapping)) {
        return text
    }

    const transliterated = Array.from(text)
        .map(char => {
            const mappedChar = mapping[char.toUpperCase()]
            
            return isEmpty(mappedChar) 
                ? char 
                : mappedChar
        })
        .reduce((acc, char) => acc+=char, stringEmpty())

    return transliterated
}
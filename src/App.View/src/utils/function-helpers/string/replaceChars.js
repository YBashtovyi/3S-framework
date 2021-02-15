import isEmpty from 'lodash.isempty'


const throwIfInvalidParams = (str, replacebleChars, charsAsReplacements) => {

    if (isEmpty(str)) {
        throw new Error('str must be non-null, non-undefined or non-empty value')
    }

    if (isEmpty(replacebleChars)) {
        throw new Error('replacebleChars must be non-null or non-undefined value')
    }

    if (isEmpty(charsAsReplacements)) {
        throw new Error('charsAsReplacements must be non-null or non-undefined value')
    }

    if (typeof str !== 'string') {
        throw new Error('str must be a string')
    }

    if (typeof replacebleChars !== 'string') {
        throw new Error('replacebleChars must be a string')
    }

    if (typeof charsAsReplacements !== 'string') {
        throw new Error('charsAsReplacements must be a string')
    }

    if (replacebleChars.length !== charsAsReplacements.length) {
        throw new Error('The lehtgths of replacebleChars and charsAsReplacements must be equal')
    }
}

export default function(str, replacebleChars, charsAsReplacements) {

    throwIfInvalidParams(str, replacebleChars, charsAsReplacements)

    const strWithCharsReplaced = Array.from(str)
        .map(char => replacebleChars.includes(char) 
            ? charsAsReplacements.charAt(replacebleChars.indexOf(char))
            : char)
        .join('')

    return strWithCharsReplaced
}
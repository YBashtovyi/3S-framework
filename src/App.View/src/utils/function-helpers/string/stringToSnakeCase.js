/**
* Converts camel case text to snake case format
 * @param {String} camelCaseText text in camel case format
 * @param {*} upperCase in upper variant
 */
export default function stringToSnakeCase(camelCaseText, upperCase = false) {
    const snakeCaseText =  camelCaseText
        .replace(/(.)([A-Z][a-z]+)/, '$1_$2')
        .replace(/([a-z0-9])([A-Z])/, '$1_$2')
    
    return upperCase 
        ? snakeCaseText.toUpperCase() 
        : snakeCaseText
        
}
import isEmpty from 'lodash.isempty'
import { stringEmpty } from '../function-helpers'
import get from 'lodash.get'

export const generateHtmlElement = objectElement => {
    const { 
        type, 
        value       = stringEmpty(),
        style       = stringEmpty(),
        children    = [],
        attributes  = []
    } = objectElement
    
    const element = document.createElement(type)
    
    if (!isEmpty(value)) { 
        Array.isArray(value) && value.length > 1
            ? value.forEach(val => element.append(document.createTextNode(val), document.createElement('br')))
            : element.append(document.createTextNode(value))
    }
    
    if (!isEmpty(style)) {
        element.setAttribute('style', style)
    }

    if (Array.isArray(attributes) && !!attributes.length) {
        const addAttributesToElement = attribute => {

            const attributeName = get(attribute, 'name', null)
            if (isEmpty(attributeName)) return

            const attributeValue = get(attribute, 'value', stringEmpty())

            element.setAttribute(attributeName, attributeValue)
        }
        attributes.map(addAttributesToElement)
    }

    children.forEach(child => element.appendChild(generateHtmlElement(child)));
   
    return element
}
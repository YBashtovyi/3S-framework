import isEmpty from 'lodash.isempty'
import { stringEmpty } from '../function-helpers'

import { generateHtmlElement } from './helpers'


export default function (objectTreeDocument, title) {

    if (isEmpty(objectTreeDocument)) {
        return stringEmpty()
    }    

    const treeRoot = document.implementation.createHTMLDocument(isEmpty(title) ? stringEmpty() : title)
    treeRoot.body.append(generateHtmlElement(objectTreeDocument))

    const xmlSerializer = new XMLSerializer()
    const serialized  = xmlSerializer.serializeToString(treeRoot)

    return serialized
}
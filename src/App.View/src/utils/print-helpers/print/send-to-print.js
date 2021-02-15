import first from 'lodash.first'

import { cleanUp } from '../helpers'


export default function(options, printableFrame) {
    const body = first(document.getElementsByTagName('body'))
    body.appendChild(printableFrame)

    // TODO: need to search this issue and solve
    // The "load" event not triggered into chrome browser (without browser version relations)
    // const printableFrameElevent = document.getElementById(options.printableFrameId)

    // const loadHandler = () => {
    //     let printDocument = printableFrame.contentWindow || printableFrame.contentDocument
        
    //     if (printDocument.document) {
    //         printDocument = printDocument.document
    //     }

    //     printDocument.body.appendChild(options.printableElement)

    //     if (options.style) {
    //         const style = document.createElement('style')
    //         style.innerHTML = options.style
    //         printDocument.head.appendChild(style)
    //     }

    //     printableFrame.focus()

    //     try {
    //         printableFrame.contentWindow.print()
    //     } catch (error) {
    //         options.onError(error)            
    //     } finally {
    //         cleanUp(options)
    //     }
    // }
    // printableFrameElevent.addEventListener('load', loadHandler)


    let printDocument = printableFrame.contentWindow || printableFrame.contentDocument
    
    if (printDocument.document) {
        printDocument = printDocument.document
    }

    printDocument.body.appendChild(options.printableElement)

    if (options.style) {
        const style = document.createElement('style')
        style.innerHTML = options.style
        printDocument.head.appendChild(style)
    }

    printableFrame.focus()

    try {
        printableFrame.contentWindow.print()
    } catch (error) {
        options.onError(error)            
    } finally {
        cleanUp(options)
    }
}
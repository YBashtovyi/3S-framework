import isempty from 'lodash.isempty'
import printableDocumentTypes from './printable-document-types'
import { createPrintableFrame, removePrintableFrame } from './helpers'
import { printHtml, printRawHtml } from './print'


export default function(params) {
    if (isempty(params)) {
        throw new Error(`'print' function expects at least 1 attribute.`)
    }

    if (isempty(params.printable)) {
        throw new Error('Missing printable item.')
    }

    if (params.type && Object.keys(printableDocumentTypes).indexOf(params.type.toLowerCase()) === -1) {
        throw new Error(`Invalid printable document type. Available types are: ${printableDocumentTypes.join(', ')}.`)  
    }

    const options = {
        /** Common */
        type:                   'rawhtml',
        printableFrameId:       'EVOMIS_PRINTABLE_FRAME_ID',
        printable:              null,
        printableElement:       null,

        /** Style */
        // NOT NOW
        // font:                   'TimesNewRoman',
        // font_size:              '12pt',
        style:                  null,
        // css:                     null, 

        /** Event handlers */
        onError:                err => { throw err },
        onPrintDialogClose:     null,

        ...params
    }

    removePrintableFrame(options.printableFrameId)
    
    const printableFrame = createPrintableFrame(options.printableFrameId)

    switch (options.type.toLowerCase()) {
        case printableDocumentTypes.rawhtml:    return printRawHtml(options, printableFrame)
        case printableDocumentTypes.html:       return printHtml(options, printableFrame)
    }
}
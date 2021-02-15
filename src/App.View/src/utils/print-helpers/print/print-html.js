import { cloneElement } from '../helpers'
import sendToPrint from './send-to-print'


export default function (options, printableFrame) {

    const printableElement = document.getElementById(options.printable)

    if (!printableElement) {
        return console.error(`Invalid HTML element id: ${options.printable}`)
    }

    const clonePrintableElement = cloneElement(options, )

    const withPrintableElement = { ...options,  printableElement: clonePrintableElement }
    
    sendToPrint(withPrintableElement, printableFrame)
}
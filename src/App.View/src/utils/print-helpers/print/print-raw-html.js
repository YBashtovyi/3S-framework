import sendToPrint from './send-to-print'


export default function(options, printableFrame) {
    const printableElement = document.createElement('div')

    printableElement.innerHTML = options.printable
    printableElement.setAttribute('style', 'width:100%')

    const withPrintableElement = { ...options,  printableElement }

    sendToPrint(withPrintableElement, printableFrame)
}
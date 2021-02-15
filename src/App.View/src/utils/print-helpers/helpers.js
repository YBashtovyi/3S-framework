export function createPrintableFrame (printableFrameId) {
    const printableFrame = document.createElement('iframe')

    printableFrame.setAttribute('id', printableFrameId)
    printableFrame.setAttribute('style', 'visibility: hidden; height: 0; width: 0; position: absolute;')

    return printableFrame
}


export function removePrintableFrame (printableFrameId) {
    const usedPrintableFrame = document.getElementById(printableFrameId)

    if (usedPrintableFrame) {
        usedPrintableFrame.parentNode.removeChild(usedPrintableFrame)
        return true
    }

    return false
}


export function cleanUp (options) {
    const event = 'afterprint'

    const handler = () => {
        options.onPrintDialogClose && options.onPrintDialogClose()
        window.removeEventListener(event, handler)
        removePrintableFrame(options.printableFrameId)
    }

    window.addEventListener(event, handler)
}


export function cloneElement (options, element) {
    const clone = element.clone()

    const childNodesArray = Array.prototype.slice.call(element.childNodes)
    
    for (let i = 0; i < childNodesArray.length; i++) {
        clone.appendChild(cloneElement(options, childNodesArray[i]))
    }

    return clone
}
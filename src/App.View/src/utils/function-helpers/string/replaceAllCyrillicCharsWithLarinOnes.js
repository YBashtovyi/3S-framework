import replaceChars from './replaceChars'

export default function(str) {

    const replacebleChars       = 'авсенкmортх'
    const charsAsReplacements   = 'abcehkmortx'
    
    const strWithReplacedCyrillicChars = replaceChars(str, replacebleChars, charsAsReplacements)

    return strWithReplacedCyrillicChars
}
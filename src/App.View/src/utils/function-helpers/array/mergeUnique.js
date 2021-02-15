import indexOf from './indexOf'

export default function (array1, array2, filter) {
    return array1.concat(array2.filter(el2 => indexOf(array1, el1 => filter(el1, el2)) === -1))
}

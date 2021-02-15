/** 
 * Sorts objects in array by field
 * @param  {string} field field for sorting
 * @param  {Function} primer parse function
 * @param  {boolean} reverse if we need descending - set true
 **/
export default function(field, primer = null, reverse = false) {

    const key = primer
        ? x => primer(x[field])
        : x => x[field]

    reverse = !reverse ? 1 : -1

    return (a, b) => {
        return (a = key(a), b = key(b), reverse * ((a > b) - (b > a)))
    }
}
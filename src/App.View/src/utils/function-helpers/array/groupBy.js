/**
 * Groups elements by key
 * 
 * @param  {Array} items array of input items
 * @param  {string} key name of property
 * @returns array of grouped items as key value pair
 **/
export default function(items, key) {

    return items.reduce((rv, x) => {
        (rv[x[key]] = rv[x[key]] || []).push(x)
        return rv
    }, {})
    
}
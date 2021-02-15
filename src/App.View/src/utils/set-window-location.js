import isEmpty from 'lodash.isempty'
import get from 'lodash.get'
import set from 'lodash.set'

export default function(href) {

    if (isEmpty(href)) {
        throw new Error('href parametr is null or undefined')
    }

    if(isEmpty(get(window, ['location', 'href'], null))){
        throw new Error('window not available')
    }

    set(window, ['location', 'href'], href)
}
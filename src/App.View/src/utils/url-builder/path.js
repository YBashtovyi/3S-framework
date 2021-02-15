import { stringEmpty } from '../function-helpers'

export const throwIfPathIvalid = path => {

    if (typeof path !== 'string' && !Array.isArray(path)) {
        throw new Error('Path must be a string or an array of strings.')
    }

}


export const pathResolve = path => {

    if (typeof path === 'string') {
        return [path]
    }

    if (Array.isArray(path)) {
        return path
    }

    return []
}


export const parsePath = pathes => {

    return pathes.length
        ? pathes.join('/').replace(/\/+$/, '')
        : stringEmpty()

}



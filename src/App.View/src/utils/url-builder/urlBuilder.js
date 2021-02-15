// Utils
import { mergeUnique } from '@/utils/function-helpers'

// Parts
import { throwIfHostInvalid, parseHost } from './host'
import { pathResolve, throwIfPathIvalid, parsePath } from './path'
import { queryParametrResoleve, parseQuery } from './query'


export class UrlBuilder {

    constructor({ host, withoutHost }) {

        this.withoutHost = !!withoutHost

        this.parts = {
            host: '',
            pathes: [],
            params: []
        }

        if (host) {
            this.host(host)
        }
    }


    host (host) {

        if (!this.withoutHost) {
            throwIfHostInvalid(host)
        }

        this.parts = {
            ...this.parts,
            host
        }

        return this
    }


    path (path) {

        throwIfPathIvalid(path)

        const resolved = pathResolve(path)
        const pathes = mergeUnique(this.parts.pathes, resolved, (first, second) => first === second)

        this.parts = {
            ...this.parts,
            pathes
        }

        return this
    }


    query (query) {
        const resolved = queryParametrResoleve(query)
        const params = mergeUnique(this.parts.params, resolved, (first, second) => first.name === second.name)

        this.parts = {
            ...this.parts,
            params
        }

        return this
    }


    param (name, value) {
        return this.query({ [name]: value })
    }


    build () {

        const host = parseHost(this.parts.host)
        const pathes = parsePath(this.parts.pathes)
        const query = parseQuery(this.parts.params)

        const url = `${pathes.trim().length > 0 ? host + '/' : host}${pathes}${query.trim().length > 0 ? '?' : ''}${query}`

        return url
    }


    toString () {
        return this.build()
    }

}
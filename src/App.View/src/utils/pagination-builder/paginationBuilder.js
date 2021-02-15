// Utils
import { isEmpty, isNotEmpty } from '@/utils/function-helpers'
import UrlBuilder from '@/utils/url-builder'

// descending: true,
// page: 1,
// rowsPerPage: 10,
// rowsNumber: 10,
// sortBy: "caption"

export class PaginationBuilder {

    constructor(pagination) {
        this.innerPagination = {}
        this.isFirstCall = true

        if (isNotEmpty(pagination)) {
            this.setOriginPagination(pagination)
            this.setPage(pagination.page)
            this.setPageSize(pagination.rowsPerPage)
        }
    }


    setOriginPagination (origin) {
        this.originPagination = Object.freeze({ ...origin })
        return this
    }


    setPage (page) {
        if (isEmpty(page)) {
            throw new Error('Page must be non-null or non-undefined.')
        }
        this.innerPagination.page = page
        return this
    }


    setPageSize (size) {
        if (isEmpty(size)) {
            throw new Error('Size must be non-null or non-undefined.')
        }
        this.innerPagination.pageSize = size
        return this
    }


    next () {
        if (this.isFirstCall) {
            this.isFirstCall = false
            return this.pairsQuery
        } else {
            const page = this.innerPagination.page + 1
            this.innerPagination = { ...this.innerPagination, page }
            return this.pairsQuery
        }
    }


    previous () {
        const page = this.innerPagination.page - 1

        if (page === 0) {
            this.innerPagination = { ...this.innerPagination, page: 1 }
            return this.pairsQuery
        } else {
            this.innerPagination = { ...this.innerPagination, page }
            return this.pairsQuery
        }
    }


    toString () {
        return this.query
    }


    valueOf () {
        return this.asObject
    }


    get query () {
        const query = new UrlBuilder({ withoutHost: true })
            .query(this.pairsQuery)
            .toString()

        return query
    }


    get asObject () {

        const pagination = this.pairsQuery
            .reduce((acc, { name, value }) => ({ ...acc, [name]: value }), {})

        return pagination
    }


    get pairsQuery () {

        const excludeFromPagination = keyToExclude =>
            !['rowsPerPage', 'page'].includes(keyToExclude)

        const propsFromOriginPagination = Object.keys(this.originPagination)
            .filter(excludeFromPagination)
            .map(key => ({ name: key, value: this.originPagination[key] }))

        const pairs = [
            ...propsFromOriginPagination,
            { name: 'pageNumber', value: this.innerPagination.page },
            { name: 'pageSize', value: this.innerPagination.pageSize },
        ]

        return pairs
    }

}

import Vue from 'vue'
import moment from 'moment'

Vue.filter('moment', (...args) => {

    args = Array.prototype.slice.call(args)
    const input = args.shift()
    let date

    if (Array.isArray(input) && typeof input[0] === 'string') {
        date = moment(input[0], input[1], true)
    } else if (typeof input === 'number') {
        if (input.toString().length < 12) {
            date = moment.unix(input)
        } else {
            date = moment(input)
        }
    } else {
        date = moment(input)
    }

    if (!input || !date.isValid()) {
        console.warn('Could not build a valid `moment` object from input.')
        return input
    }

    const parse = (...args) => {
        args = Array.prototype.slice.call(args)
        const method = args.shift()

        switch (method) {
            case 'add': {

                const addends = args.shift()
                    .split(',')
                    .map(Function.prototype.call, String.prototype.trim)

                const obj = {}

                for (let n = 0;n < addends.length;n++) {
                    const addend = addends[n].split(' ')
                    obj[addend[1]] = addend[0]
                }
                date.add(obj)
                break
            }

            case 'subtract': {

                const subtrahends = args.shift()
                    .split(',')
                    .map(Function.prototype.call, String.prototype.trim)

                const obj = {}

                for (let n = 0;n < subtrahends.length;n++) {
                    const subtrahend = subtrahends[n].split(' ')
                    obj[subtrahend[1]] = subtrahend[0]
                }
                date.subtract(obj)
                break
            }

            case 'from': {

                let from = 'now'
                let removeSuffix = false

                if (args[0] === 'now') args.shift()
                if (moment(args[0]).isValid()) from = moment(args.shift())

                if (args[0] === true) {
                    args.shift()
                    removeSuffix = true
                }

                if (from !== 'now') {
                    date = date.from(from, removeSuffix)
                } else {
                    date = date.fromNow(removeSuffix)
                }
                break
            }

            case 'diff': {

                let referenceTime = moment()
                let units = ''
                let float = false

                if (moment(args[0]).isValid()) {
                    referenceTime = moment(args.shift())
                } else if (args[0] === null || args[0] === 'now') {
                    args.shift()
                }

                if (args[0]) units = args.shift()

                if (args[0] === true) float = args.shift()

                date = date.diff(referenceTime, units, float)
                break
            }

            case 'calendar': {

                let referenceTime = moment()
                let formats = {}

                if (moment(args[0]).isValid()) {
                    referenceTime = moment(args.shift())
                } else if (args[0] === null || args[0] === 'now') {
                    args.shift()
                }

                if (typeof args[0] === 'object') formats = args.shift()

                date = date.calendar(referenceTime, formats)
                break
            }

            case 'utc': {
                date.utc()
                break
            }

            case 'timezone': {
                date.tz(args.shift())
                break
            }

            default: {
                const format = method
                date = date.format(format)
            }
        }

        if (args.length) parse.apply(parse, args)
    }

    parse.apply(parse, args)

    return date
})
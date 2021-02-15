export default function (promise) {
    return async args => {
        await promise
        return args
    }
}
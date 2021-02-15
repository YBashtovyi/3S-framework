export default function (promise) {
    return async () => { 
        const resolvedValue = await promise
        return resolvedValue
    }
}
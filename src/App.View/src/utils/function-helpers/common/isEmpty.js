export default function (e) {

    if (e === "" || e === 0 || e === false || e === null || e === undefined) {
        return true
    } else {
        return false
    }

}
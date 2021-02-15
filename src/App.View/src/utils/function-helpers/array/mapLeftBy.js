export default function (leftArray, rightArray, predicateObj, transformFn) {

    const transformArray = (array, rightItem) =>
        array.reduce((acc, leftItem) => {
            if (predicateObj.leftSelectorFn(leftItem) === predicateObj.rightSelectorFn(rightItem)) {
                acc.push(transformFn(leftItem, rightItem))
            } else {
                acc.push(leftItem)
            }
            return acc
        }, [])

    rightArray.forEach(rightItem => {
        leftArray = transformArray(leftArray, rightItem)
    })

    return leftArray || []
}
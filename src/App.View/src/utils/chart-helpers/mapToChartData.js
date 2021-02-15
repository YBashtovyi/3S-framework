import isEmpty from 'lodash.isempty'
import isArray from 'lodash.isarray'
import mapDataCollectionToDatasets from './mapDataCollectionToDatasets'
/**
 * Maps data to chart data
 * @param {Array} dataCollection data which must be represented in chart
 * @param {Object} dataOptions - contains of 
 * * `labels` - options which will be represented as labels
 * * `valueOptions` - options which will be represented as dataset
 */
export default function (dataCollection, dataOptions) {
    if (isEmpty(dataOptions)) return

    const { labels, valueOptions } = dataOptions

    if (isEmpty(labels)) {
        throw new Error(`Labels are missing in the specified data set`)
    }

    if (isEmpty(valueOptions)) {
        throw new Error(`Value options is missing in the specified data set`)
    }

    if (!isArray(dataCollection)) {
        throw new Error(`Wrong data was represented. Need "array", getted ${typeof dataCollection}`)
    }

    if (!isArray(valueOptions)) {
        throw new Error(`Wrong valueOptions were represented. Need "array", getted ${typeof valueOptions}`)
    }

    if (!isArray(labels)) {
        throw new Error(`Wrong labels were represented. Need "array", getted ${typeof labels}`)
    }

    const datasets = mapDataCollectionToDatasets(valueOptions, dataCollection)
    const chartData = {
        labels,
        datasets
    }
    return chartData

}
/**
 * Mapping function which is used for creating datasets for charts from value options
**/
export default function (valueOptions, dataCollection) {
    return valueOptions.map((option) => {
        return {
            label: option.label,
            data: dataCollection.map(item => item[option.key]),
            backgroundColor: option.backgroundColor,
        }
    });
}

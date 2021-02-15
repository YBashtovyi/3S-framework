
/**
 * Get rows which will be displayed in the table component
 * 
 * @param {Object} state current state of the table module
 * @returns {Object} state current state of the table module
 */
const tableData = state => {
    return state.data
}


/**
 * Get rows which will be displayed in the table component
 *
 * @param {Object} state current state of the table module
 * @returns {Object} the pagination object whith set of props which need to get a piece of data from the API
 */
const tablePagination = state => {
    return state.pagination
}


/**
 * Get current loading state of the table
 * 
 * @param {Object} state current state of the table module
 * @returns {Boolean}
 */
const tableLoading = state => {
    return state.loading
}


export default {
    data: tableData,
    pagination: tablePagination,
    loading: tableLoading
}
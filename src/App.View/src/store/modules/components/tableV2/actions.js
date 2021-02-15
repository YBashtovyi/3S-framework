// Constants
import {
    SET_TABLE_DATA,
    FETCH_TEBLE_DATA,
    TABLE_LOADING_STARTED,
    TABLE_LOADING_COMPLETED,
    UPDATE_TABLE_PAGINATION
} from './constansts'


export default {

    /**
     * The action that will set rows received from the API to the store
     * Used when you need to set or update a portion of rows to display in a table component
     *
     * @param {Object} store context of the store
     * @param {Array} data rows to be displayed in the table component
     */
    [SET_TABLE_DATA]: ({ commit }, data) => {
        commit({ type: SET_TABLE_DATA, data })
    },


    /**
     * Receive data from an API without being bound to a specific API method
     * 
     * @param {Object} store context of the store
     * @param {Object} data this is an context of pagination 
     */
    [FETCH_TEBLE_DATA]: ({ dispatch, state }, { pagination, fetchTableDataFunction }) => {

        const dispatchSetTableData = actionType => data =>
            dispatch(actionType, data)

        const dispatchTableLoadingCompleted = () =>
            dispatch(TABLE_LOADING_COMPLETED)

        const dispatchUpdateTablePaginationAfterResponse = data => {
            dispatch(UPDATE_TABLE_PAGINATION, { ...pagination, rowsNumber: data[0].totalRecordCount })
            return data
        }

        dispatch(TABLE_LOADING_STARTED)
        return fetchTableDataFunction(pagination)
            .then(response => response.data)
            .then(dispatchUpdateTablePaginationAfterResponse)
            .then(dispatchSetTableData(SET_TABLE_DATA))
            .then(dispatchTableLoadingCompleted)
            .catch(dispatchTableLoadingCompleted)
    },


    /**
     * Show loading state
     * 
     * @param {Object}
     */
    [TABLE_LOADING_STARTED]: ({ commit }) => {
        commit(TABLE_LOADING_STARTED)
    },


    /**
     * Hide loading state
     * 
     * @param {Object}
     */
    [TABLE_LOADING_COMPLETED]: ({ commit }) => {
        commit(TABLE_LOADING_COMPLETED)
    },


    /**
     * Update pagination object, if you dispatch this action and transmit object only with 
     * 
     * 
     */
    [UPDATE_TABLE_PAGINATION]: ({ commit, state }, pagination) => {
        commit(UPDATE_TABLE_PAGINATION, { ...state.pagination, ...pagination })
    }

}
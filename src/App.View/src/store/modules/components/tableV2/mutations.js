import {
    SET_TABLE_DATA,
    UPDATE_TABLE_PAGINATION,
    TABLE_LOADING_STARTED,
    TABLE_LOADING_COMPLETED
} from './constansts'


export default {

    /**
     * Set data elements in the store, these elements will be used in the table component
     * 
     * @param {Object} state current state of the table module
     * @param {Array} data rows to be displayed in the table component
     */
    [SET_TABLE_DATA]: (state, { data }) => {
        state.data = [...data]
    },


    /**
     * Set the pagination object in the store, the pagination used when you need to get a portion of data from the api
     * 
     * @param {Object} state current state of the table module
     * @param {Object} pagination used when you need to get a piece of data from the API and display it in a table
     */
    [UPDATE_TABLE_PAGINATION]: (state, pagination) => {
        state.pagination = pagination
    },


    /**
     * Set the loading state as 'true' when need to display loader
     */
    [TABLE_LOADING_STARTED]: state => {
        state.loading = true
    },


    /**
     * Set the loading state as 'false' when need to hide loader
     */
    [TABLE_LOADING_COMPLETED]: state => {
        state.loading = false
    }

}
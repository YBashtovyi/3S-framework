export default {
    /**
     * Rows which will be displayed in the table component
     */
    data: [],


    /**
     * The set of next properties
     * 
     *    {
     *        sortBy: 'desc',
     *        descending: false,
     *        page: 1,
     *        rowsPerPage: 3,
     *        rowsNumber: 10
     *    }
     * 
     * Read more about it here https://quasar.dev/vue-components/table#Example--Synchronizing-with-server
     */

    pagination: {},


    /**
     * If the property is true, the component of the table displays the loader
     * 
     * More about loading read here https://quasar.dev/vue-components/table#Loading-state
     */
    loading: false
}

// Constants
import {
  FETCH_TEBLE_DATA,
  UPDATE_TABLE_PAGINATION,
} from '@/store/modules/components/tableV2/constansts'

// Vuex
import { mapGetters, mapActions } from 'vuex'

// Utils
import { isEmpty } from '@/utils/function-helpers'

export default function(component, options) {
  //
  if (isEmpty(options.fetchTableDataFunction)) {
    throw new Error('fetchFunctionForTableData must be non-null value')
  }

  if (isEmpty(options.pagination)) {
    options.pagination = {
      descending: true,
      page: 1,
      rowsPerPage: 10,
      rowsNumber: 10,
      sortBy: 'name',
    }
  }

  return {
    computed: {
      ...mapGetters('tableV2', ['data']),
    },

    methods: {
      ...mapActions('tableV2', {
        fetchTableData: FETCH_TEBLE_DATA,
        updateTablePagination: UPDATE_TABLE_PAGINATION,
      }),

      /**
       *
       *
       */
      createRequestHandler: (fetchTableDataAction, fetchTableDataFunction) => {
        return ({ pagination }) => fetchTableDataAction({ pagination, fetchTableDataFunction })
      },

      /**
       *
       *
       * @param {VueComponent} componentForApplyContext
       */
      applyComponentContext(componentForApplyContext) {
        const methods = {
          onRequest: this.createRequestHandler(this.fetchTableData, options.fetchTableDataFunction),
        }

        const computed = {
          data: function() {
            return this.$store.state.tableV2.data
          },
          pagination: function() {
            return { ...this.$store.state.tableV2.pagination }
          },
          loading: function() {
            return this.$store.state.tableV2.loading
          },
        }

        return { ...componentForApplyContext, methods, computed }
      },
    },

    created: function() {
      this.updateTablePagination({ ...options.pagination })
    },

    /**
     *
     * @param {*} createElement
     */
    render: function(createElement) {
      this.fetchTableData({
        pagination: options.pagination,
        fetchTableDataFunction: options.fetchTableDataFunction,
      })

      return createElement(this.applyComponentContext(component))
    },
  }
}

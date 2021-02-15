import {
  PAGINATION_OPTIONS,
  ACTIVE_FILTERS_OPTIONS,
} from '../../../services/table-list-stored-options/constants'
import { getTableOptions, setTableOptions } from '../../../services/table-list-stored-options'
import { mapState, mapGetters, mapActions } from 'vuex'
import isEmpty from 'lodash.isempty'
import { stringToSnakeCase } from '../../../utils/function-helpers/string'

export default {
  data() {
    return {
      innerUrl: '',
      innerDeleteUrl: '',
      innerPagination: {},
      visibleColumns: [],
      isColumnSelectDialogVisible: false,
      selectedFileFormat: null,
      isVisibleMenu: false,
      selected: [],
      selectedItem: {},
    }
  },
  props: {
    columns: Array,
    columnsSelect: Boolean,
    columnsSelectValues: Array,
    pagination: Object,
    requestUrl: String,
    deleteUrl: {
      type: String,
      default: function() {
        return this.requestUrl
      },
    },
    selection: {
      type: String,
      default: 'none',
    },
    rowKey: {
      type: String,
      default: 'none',
    },
    loading: Boolean,
    localFilter: String,
    isFileFormatSelect: Boolean,
    fileFormats: Array,
    actions: {
      type: [Array, Boolean],
      default: function() {
        return [
          {
            type: 'detail',
            label: 'Переглянути',
            icon: 'fas fa-eye',
            visible: true,
          },
          {
            type: 'edit',
            label: 'Редагувати',
            icon: 'edit',
            visible: true,
          },
        ]
      },
    },
    headerButtons: {
      type: Array,
      default: function() {
        return []
      },
    },
    selectedRow: Object,
    flat: {
      type: Boolean,
      default: true,
    },
    expanded: {
      type: [Boolean, Array],
      default: false,
    },
    customColors: Object,
    customRowColor: Function, // this makes it possible to set the color using conditions.
    headerCardLabel: String,
    getIsRowCanBeSelected: [Boolean, Function],
    withSave: {
      type: Boolean,
      default: false,
    },
    columnsSelectListSkip: {
      type: Array,
      default: () => [],
    },
  },

  computed: {
    ...mapState('tableList', [
      'tableData',
      'status',
      'paginationTable',
      'search',
      'searchDictionary',
      'sort',
    ]),
    ...mapState('baseElements', ['user']),
    ...mapGetters('baseElements', { rights: 'getUserRights' }),

    // Added cache false, Vue cached value, and work only on initialization
    searchParams: {
      get: function() {
        const savedSearchParams = !isEmpty(this.componentKey)
          ? getTableOptions(this.componentKey)(ACTIVE_FILTERS_OPTIONS)
          : []

        return isEmpty(savedSearchParams) ? this.searchDictionary : savedSearchParams
      },
      cache: false,
    },

    isLoading() {
      return this.status === 'loading'
    },

    componentKey() {
      return !isEmpty(this.$route.name) ? stringToSnakeCase(this.$route.name, true) : ''
    },

    modalColumns() {
      var modalColumns = []
      for (var i = 0; i < this.columns.length; i++) {
        if (!this.columns[i].exportIgnored) {
          modalColumns.push(this.columns[i])
        }
      }
      return modalColumns
    },

    /*
     * Defines when 'SelectAll' button needs to be disabled
     * 1. When page doesn't contains rows which can be selected
     */
    isSelectAllButtonDisabled: function() {
      if (this.tableData) {
        switch (typeof this.getIsRowCanBeSelected) {
          case 'boolean': {
            return !this.getIsRowCanBeSelected
          }
          case 'function': {
          }
        }
      }
      return true
    },

    /*
     * Defines when some rows are selected
     */
    isRowsSelected() {
      return this.selected && this.selected.length > 0
    },

    // defines visibility of actions menu
    // if at least one action is visible then menu is visible
    isVisibleActionsBtn() {
      if (this.actions.length > 0) {
        for (let i in this.actions) {
          // if visibility is defined by function, then show menu button
          // visibility of concrete action will be defined this function on selected row
          if (typeof this.actions[i].visible === 'function') {
            return true
          } else if (this.actions[i].visible) {
            return true
          }
        }
      }
      return false
    },
  },

  methods: {
    ...mapActions('tableMenu', ['fetch']),
    ...mapActions('tableList', ['init']),

    onMounted() {
      this.innerUrl = this.requestUrl
      this.innerDeleteUrl = this.deleteUrl

      if (this.columnsSelect) {
        this.visibleColumns = this.columnsSelectValues
      }

      if (this.pagination) {
        const savedPagination = !isEmpty(this.componentKey)
          ? getTableOptions(this.componentKey)(PAGINATION_OPTIONS)
          : null

        this.innerPagination =
          this.withSave && !isEmpty(savedPagination) ? savedPagination : this.pagination
      }

      this.onUrlChanged()
    },

    showMenu(row) {
      this.isVisibleMenu = !this.isVisibleMenu
      this.selectedItem = row
    },

    onUrlChanged() {
      if (this.pagination) {
        this.onRequest({
          pagination: this.innerPagination,
          requestUrl: this.innerUrl,
        })
      } else {
        this.onRequest({
          requestUrl: this.innerUrl,
        })
      }
    },

    /*
     * Handler which used to write logic afted check box was clicked
     */
    onCheckBoxClick($event) {
      /*
       * This logic throws an event which shares selected items
       */
      this.$emit('getSelected', this.selected)
    },

    onRowClick(props) {
      if (this.actions && this.actions.length > 0) {
        for (let i in this.actions) {
          if (this.actions[i].type === 'rowFn') {
            this.actions[i].fn(props)
            return
          }
        }
        return this.goToDetails(props.row)
      }
    },

    goToDetails(row) {
      this.$router.push(this.$route.path + '/details/' + row.id)
    },

    onRequest(props) {
      this.innerPagination = props.pagination

      if (this.withSave) {
        setTableOptions(this.componentKey, this.innerPagination, PAGINATION_OPTIONS)
      }

      this.init({
        pagination: props.pagination,
        params: this.search,
        requestUrl: this.innerUrl,
      }).then(() => {
        this.pageLoaded = true
      })
    },

    changePage(num) {
      this.$route.query.pageNumber = num
    },

    selectColumns() {
      this.isColumnSelectDialogVisible = true
    },

    dialogCloseHandler() {
      this.isColumnSelectDialogVisible = false
      this.selectedFileFormat = null
    },

    onFormatButtonClick(index) {
      this.selectedFileFormat = this.fileFormats[index]
    },

    setCustomColor(props) {
      for (let i in this.customColors) {
        let coloredValue = ''
        const parseResult = parseInt(i)
        if (parseResult) {
          coloredValue = parseResult
        } else {
          coloredValue = i
        }
        if (props.row[this.customColors.key] === coloredValue) {
          return this.customColors[i]
        }
      }
      return 'canceled'
    },

    /*
     *  Method is used to set the color for the row
     */
    setCustomRowColor(props) {
      if (typeof this.customRowColor === 'function') {
        return this.customRowColor(props.row)
      }
    },

    /*
     * Method is used to decide if row can be selected
     */
    canSelectRow(props) {
      return typeof this.getIsRowCanBeSelected === 'boolean'
        ? this.getIsRowCanBeSelected
        : this.getIsRowCanBeSelected(props.row)
    },

    /*
     * Handler which binded to 'Select All' Button
     */
    onSelectAllButtonClick(props) {
      if (this.isRowsSelected) {
        this.selected = []
      } else {
        this.selectAllRows()
      }
      this.$emit('getSelected', this.selected)
    },

    /*
     * Method which defines select all logic
     */
    selectAllRows() {
      if (typeof this.getIsRowCanBeSelected === 'boolean') {
        this.selected = this.getIsRowCanBeSelected ? this.tableData : []
      } else if (typeof this.getIsRowCanBeSelected === 'function') {
        this.selected = this.tableData.filter(this.getIsRowCanBeSelected)
      }
    },

    getTooltip(tooltip, col) {
      const tooltipType = typeof tooltip

      if (tooltipType === 'function') {
        return tooltip(col)
      }

      if (tooltipType === 'string') {
        return tooltip
      }

      console.error(`Tooltip has incorrect type. Expected string or function, got ${tooltipType}`)
    },
  },
  watch: {
    paginationTable: function() {
      this.innerPagination = this.paginationTable
    },

    isLoading: function() {
      this.$emit('loading', this.isLoading)
    },

    selectedFileFormat: function() {
      if (this.selectedFileFormat) {
        this.isColumnSelectDialogVisible = true
      }
    },

    requestUrl() {
      this.innerUrl = this.requestUrl
      this.onUrlChanged()
    },

    deleteUrl() {
      this.innerDeleteUrl = this.deleteUrl
    },
  },
}

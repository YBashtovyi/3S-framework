import {
  fetchOrgUnitPosition,
  deleteOrgUnitPosition,
} from '../../../services/org-api/org-unit-position-api'

export default {
  props: {
    orgUnitId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      orgUnitPositionList: [],
      columns: [
        {
          name: 'positionName',
          required: true,
          label: 'Посада',
          align: 'left',
          field: 'positionName',
        },
        {
          name: 'staffUnitCount',
          required: true,
          label: 'Кількість штатних одиниць посади',
          align: 'left',
          field: 'staffUnitCount',
        },
        {
          name: 'description',
          required: true,
          label: 'Опис',
          align: 'left',
          field: 'description',
        },
        {
          name: 'actions',
          label: '',
          align: 'left',
          field: '-',
        },
      ],

      pagination: {
        descending: true,
        page: 1,
        rowsPerPage: 50,
        rowsNumber: 50,
        sortBy: 'createdOn',
      },

      isVisibleEditOrgUnitPositionDialog: false,

      selectedOrgUnitPositionId: null,
      selectedItem: {},
      isVisibleMenu: false,
    }
  },

  methods: {
    fetchOrgUnitPosition() {
      fetchOrgUnitPosition(this.orgUnitId, this.pagination).then(this.setOrgUnitPosition)
    },

    setOrgUnitPosition(data) {
      this.orgUnitPositionList = data
    },

    onAddOrgUnitPosition(row) {
      this.selectedOrgUnitPositionId = row.id
      this.isVisibleEditOrgUnitPositionDialog = true
    },

    onDeleteOrgUnitPosition(row) {
      deleteOrgUnitPosition(row.id)
        .then(this.notifyDeleteSuccess)
        .then(this.fetchOrgUnitPosition)
    },

    onCloseOrgUnitPositionDialog() {
      this.isVisibleEditOrgUnitPositionDialog = false
      this.fetchOrgUnitPosition()
    },

    /**
     * Display row menu
     */
    onShowMenu(row) {
      this.isVisibleMenu = !this.isVisibleMenu
      this.selectedItem = row
    },

    // when pagination changed from UI
    onPaginationChanged({ pagination }) {
      this.pagination.page = pagination.page
      this.pagination.rowsPerPage = pagination.rowsPerPage

      this.getOrgUnitPositionList()
    },

    notifyDeleteSuccess() {
      this.$q.notify({ type: 'positive', message: 'Посада видалена успішно' })
      return Promise.resolve()
    },
  },

  computed: {
    actions() {
      return [
        {
          type: 'edit',
          label: 'Редагувати',
          icon: 'edit',
          fn: row => this.onAddOrgUnitPosition(row),
          visible: true,
        },
        {
          type: 'delete',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.onDeleteOrgUnitPosition(row),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.fetchOrgUnitPosition()
  },
}

import {
  getExtendedPropertyList,
  deleteExtendedProperty,
} from '../../../services/org-api/organization-api'

export default {
  props: {
    orgUnitId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      list: [],
      columns: [
        {
          name: 'orgExtendedPropertyName',
          required: true,
          label: 'Тип додаткових даних',
          align: 'left',
          field: 'orgExtendedPropertyName',
        },
        {
          name: 'value',
          required: true,
          label: 'Значення',
          align: 'left',
          field: 'value',
        },
        {
          name: 'actions',
          label: '',
          align: 'left',
          field: '-',
        },
      ],

      isVisibleEditOrgUnitExtendedPropertyDialog: false,

      selectedOrgUnitExtendedPropertyId: null,
      selectedItem: {},
      isVisibleMenu: false,
    }
  },

  methods: {
    fetchOrgUnitExtendedProperty() {
      getExtendedPropertyList(this.orgUnitId).then(this.setOrgUnitExtendedProperty)
    },

    setOrgUnitExtendedProperty(data) {
      this.list = data
    },

    onAddOrEditOrgUnitExtendedProperty(row) {
      this.selectedOrgUnitExtendedPropertyId = row.id
      this.isVisibleEditOrgUnitExtendedPropertyDialog = true
    },

    onDeleteOrgUnitExtendedProperty(row) {
      deleteExtendedProperty(row.id)
        .then(this.notifyDeleteSuccess)
        .then(this.fetchOrgUnitExtendedProperty)
    },

    onCloseOrgUnitExtendedPropertyDialog() {
      this.isVisibleEditOrgUnitExtendedPropertyDialog = false
      this.fetchOrgUnitExtendedProperty()
    },

    /**
     * Display row menu
     */
    onShowMenu(row) {
      this.isVisibleMenu = !this.isVisibleMenu
      this.selectedItem = row
    },

    notifyDeleteSuccess() {
      this.$q.notify({ type: 'positive', message: 'Додаткові дані видалено успішно' })
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
          fn: row => this.onAddOrEditOrgUnitExtendedProperty(row),
          visible: true,
        },
        {
          type: 'delete',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.onDeleteOrgUnitExtendedProperty(row),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.fetchOrgUnitExtendedProperty()
  },
}

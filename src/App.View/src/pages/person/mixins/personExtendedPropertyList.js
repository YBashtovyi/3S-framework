import {
  getExtendedPropertyList,
  deleteExtendedProperty,
} from '../../../services/common-api/person-api'

export default {
  props: {
    personId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      list: [],
      columns: [
        {
          name: 'personExtendedPropertyName',
          required: true,
          label: 'Тип додаткових даних',
          align: 'left',
          field: 'personExtendedPropertyName',
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

      isVisibleEditPersonExtendedPropertyDialog: false,

      selectedPersonExtendedPropertyId: null,
      selectedItem: {},
      isVisibleMenu: false,
    }
  },

  methods: {
    fetchPersonExtendedProperty() {
      getExtendedPropertyList(this.personId).then(this.setPersonExtendedProperty)
    },

    setPersonExtendedProperty(data) {
      this.list = data
    },

    onAddOrEditPersonExtendedProperty(row) {
      this.selectedPersonExtendedPropertyId = row.id
      this.isVisibleEditPersonExtendedPropertyDialog = true
    },

    onDeletePersonExtendedProperty(row) {
      deleteExtendedProperty(row.id)
        .then(this.notifyDeleteSuccess)
        .then(this.fetchPersonExtendedProperty)
    },

    onClosePersonExtendedPropertyDialog() {
      this.isVisibleEditPersonExtendedPropertyDialog = false
      this.fetchPersonExtendedProperty()
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
          fn: row => this.onAddOrEditPersonExtendedProperty(row),
          visible: true,
        },
        {
          type: 'delete',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.onDeletePersonExtendedProperty(row),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.fetchPersonExtendedProperty()
  },
}

import get from 'lodash.get'
import { stringEmpty } from '../../../utils/function-helpers'
import {
  addExtendedProperty,
  getExtendedProperty,
  deleteExtendedProperty,
} from '../../../services/common-api/construction-object-api'

export default {
  props: {
    typeOfObjectId: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      list: [],
      columns: [
        {
          name: 'constructionObjectExPropertyName',
          required: true,
          label: 'Назва',
          align: 'left',
          field: 'constructionObjectExPropertyName',
        },
        {
          name: 'constructionObjectSubExPropertyName',
          required: true,
          label: 'Значення',
          align: 'left',
          field: 'constructionObjectSubExPropertyName',
        },
        {
          name: 'description',
          required: true,
          label: 'Коментар',
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

      isVisibleConstObjPropertyDialog: false,
      isVisibleMenu: false,
      selectedItem: null,
    }
  },

  methods: {
    fetchExtendedProperties() {
      getExtendedProperty(this.objectId).then(this.setExtendedProperties)
    },

    setExtendedProperties(data) {
      this.list = data
    },

    onAddExtendedPropertyDialog() {
      this.isVisibleConstObjPropertyDialog = true
    },

    onAddExtendedProperty(property) {
      property.ConstructionObjectId = this.objectId
      addExtendedProperty(this.objectId, property).then(this.fetchExtendedProperties)
    },

    onDeleteExtendedProperty(id) {
      deleteExtendedProperty(id)
        .then(this.notifyDeleteSuccess)
        .then(this.fetchExtendedProperties)
    },

    /**
     * Display row menu
     */
    onShowMenu(row) {
      this.isVisibleMenu = !this.isVisibleMenu
      this.selectedItem = row
    },

    notifyDeleteSuccess() {
      this.$q.notify({ type: 'positive', message: 'Додаткова характеристика видалена успішно' })
      return Promise.resolve()
    },
  },

  computed: {
    objectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    actions() {
      return [
        {
          type: 'deleteFn',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.onDeleteExtendedProperty(row.id),
          visible: true,
        },
      ]
    },
  },

  created() {
    this.fetchExtendedProperties()
  },
}

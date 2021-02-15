import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Посада',

      columns: [
        {
          name: 'code',
          required: true,
          label: 'Код',
          align: 'left',
          field: 'code',
        },
        {
          name: 'name',
          required: true,
          label: 'Назва',
          align: 'left',
          field: 'name',
        },
        {
          name: 'dataFormatName',
          required: true,
          label: 'Формат даних',
          align: 'left',
          field: 'dataFormatName',
        },
        {
          name: 'parentName',
          required: true,
          label: 'Підпорядкування',
          align: 'left',
          field: 'parentName',
        },
      ],

      dateFormat: 'DD.MM.YYYY',

      pagination: {
        descending: true,
        page: 1,
        rowsPerPage: 50,
        rowsNumber: 50,
        sortBy: 'createdOn',
      },

      fileFormats: [
        {
          label: '.xlsx',
          value: env.CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.EXPORT.XLSX,
          icon: 'far fa-file-excel',
        },
      ],
    }
  },

  methods: {
    onCreateExProperty() {
      const routeParams = {
        path: '/constObjExPropDict/create',
      }

      this.$router.push(routeParams)
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),
    requestUrl() {
      return env.CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'text',
            label: 'Код',
            key: 'code',
            value: '',
          },
          {
            type: 'text',
            label: 'Назва',
            key: 'name',
            value: '',
          },
          {
            type: 'text',
            label: 'Підпорядкування',
            key: 'parentName',
            value: '',
          },
        ],
      }
    },

    actions() {
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
        {
          type: 'delete',
          label: 'Видалити',
          icon: 'delete',
          visible: true,
        },
      ]
    },

    tableHeaderButtons() {
      return [
        {
          name: 'createPosition',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateExProperty,
        },
      ]
    },
  },
}

import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Види робіт',

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
          name: 'measurementUnitValue',
          required: true,
          label: 'Одиниця виміру',
          align: 'left',
          field: 'measurementUnitValue',
        },
        {
          name: 'parentCode',
          required: true,
          label: 'Підпорядкування',
          align: 'left',
          field: 'parentCode',
        },
        {
          name: 'isActive',
          required: true,
          label: 'Дія',
          align: 'left',
          field: 'isActive',
          format: v => (v ? '+' : '-'),
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
          value: env.WORK_SUB_TYPE.EXPORT.XLSX,
          icon: 'far fa-file-excel',
        },
      ],
    }
  },

  methods: {
    onCreateWorkSubType() {
      const routeParams = {
        path: '/workSubType/create',
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
      return env.WORK_SUB_TYPE.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.WORK_SUB_TYPE.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'multi-select',
            label: 'Тип класифікатора',
            url: this.getEnumByGroupUrl('ClassifierType'),
            key: 'classifierType',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'text',
            label: 'Підпорядкування',
            key: 'parentName',
            value: '',
          },
          {
            type: 'text',
            label: 'Код',
            key: 'code',
            value: '',
          },
          {
            type: 'check-box',
            label: 'Активний',
            key: 'isActive',
            value: true,
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
          action: this.onCreateWorkSubType,
        },
      ]
    },
  },
}

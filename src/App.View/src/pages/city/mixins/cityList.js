import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Населені пункти',

      columns: [
        {
          name: 'name',
          required: true,
          label: 'Назва',
          align: 'left',
          field: 'name',
        }
      ],

      dateFormat: 'DD.MM.YYYY',

      pagination: {
        descending: true,
        page: 1,
        rowsPerPage: 50,
        rowsNumber: 50,
        sortBy: 'createdOn',
      },
    }
  },

  methods: {
    onCreateCity() {
      const routeParams = {
        path: '/city/create',
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
      return env.CITY.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.CITY.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва організації',
          key: 'name',
          value: '',
        },

        filters: [
          // {
          //   type: 'edrpou',
          //   label: 'ЄДРПОУ',
          //   key: 'name',
          //   value: '',
          // },
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
          name: 'createCity',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateCity,
        },
      ]
    },
  },
}

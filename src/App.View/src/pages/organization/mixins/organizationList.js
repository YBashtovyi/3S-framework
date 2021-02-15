import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Організації',

      columns: [
        {
          name: 'name',
          required: true,
          label: 'Назва організації',
          align: 'left',
          field: 'name',
        },
        {
          name: 'organizationCategoryName',
          required: true,
          label: 'Категорія організації',
          align: 'left',
          field: 'organizationCategoryName',
        },
        {
          name: 'edrpou',
          required: true,
          label: 'ЄДРПОУ',
          align: 'left',
          field: 'edrpou',
        },
        {
          name: 'orgStateName',
          required: true,
          label: 'Стан',
          align: 'left',
          field: 'orgStateName',
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
          // value: env.MEDICAL_DOCUMENTS.XLSX,
          icon: 'far fa-file-excel',
        },
      ],
    }
  },

  methods: {
    onCreateOrganization() {
      const routeParams = {
        path: '/organization/create',
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
      return env.ORGANIZATION.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.ORGANIZATION.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва організації',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'text',
            label: 'Назва організації',
            key: 'name',
            value: '',
          },
          {
            type: 'multi-select',
            label: 'Стан',
            url: this.getEnumByGroupUrl('OrgState'),
            key: 'orgState',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'multi-select',
            label: 'Категорія організації',
            url: this.getEnumByGroupUrl('OrganizationCategory'),
            key: 'organizationCategory',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'text',
            label: 'ЄДРПОУ',
            key: 'edrpou',
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
          name: 'createOrganization',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateOrganization,
        },
      ]
    },
  },
}

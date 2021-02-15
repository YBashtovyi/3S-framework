import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Підрозділи',

      columns: [
        {
          name: 'name',
          required: true,
          label: 'Назва підрозділу',
          align: 'left',
          field: 'name',
        },
        {
          name: 'code',
          required: true,
          label: 'Код',
          align: 'left',
          field: 'code',
        },
        {
          name: 'departmentType',
          required: true,
          label: 'Тип підрозділу',
          align: 'left',
          field: 'departmentTypeName',
        },
        {
          name: 'parentName',
          required: true,
          label: 'Підпорядкування',
          align: 'left',
          field: 'parentName',
        },
        {
          name: 'departmentStateName',
          required: true,
          label: 'Стан',
          align: 'left',
          field: 'departmentStateName',
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
    onCreateDepartment() {
      const routeParams = {
        path: '/department/create',
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
      return env.DEPARTMENT.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.DEPARTMENT.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва підрозділу',
          key: 'name',
          value: '',
        },

        filters: [
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
          {
            type: 'text',
            label: 'Код',
            key: 'code',
            value: '',
          },
          {
            type: 'multi-select',
            label: 'Стан',
            url: this.getEnumByGroupUrl('DepartmentState'),
            key: 'departmentState',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
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
          name: 'createDepartment',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateDepartment,
        },
      ]
    },
  },
}

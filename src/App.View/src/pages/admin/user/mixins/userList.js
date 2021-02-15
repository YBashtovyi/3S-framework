import isEmpty from 'lodash.isempty'
import { mapState } from 'vuex'
import { env } from '../../../../services/api'

export default {
  data() {
    return {
      columns: [
        {
          name: 'createdOn',
          required: true,
          label: 'Дата створення',
          align: 'left',
          field: 'createdOn',
          format: val => this.$options.filters.asDate(val, this.dateTimeFormat),
        },
        {
          name: 'employeeDetail',
          required: true,
          label: 'Співробітник',
          align: 'left',
          field: val => this.getEmployeeDetail(val),
        },
        {
          name: 'accountId',
          required: true,
          label: 'Аккаунт',
          align: 'left',
          field: 'accountId',
        },
      ],

      dateTimeFormat: 'DD.MM.YYYY HH:mm',

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
      ]
    }
  },

  methods: {
    onCreateUser() {},

    getEmployeeDetail(user) {
      const fullName = `${user.personLastName} ${user.personFirstName} ${user.personMiddleName}`

      const orgUnitDetails = !isEmpty(user.orgUnitName)
        ? `(${user.orgUnitName} - ${user.orgUnitPositionName})`
        : ''
      return `${fullName} ${orgUnitDetails}`
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),

    requestUrl() {
      return env.USER.PATH
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Співробітник',
          key: 'name',
          value: '',
        },
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
          name: 'createUser',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateUser,
        },
      ]
    },
  },
}

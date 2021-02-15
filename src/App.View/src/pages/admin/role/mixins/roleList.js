import { mapState } from 'vuex'
import { env } from '../../../../services/api'

export default {
  data() {
    return {
      columns: [
        {
          name: 'name',
          required: true,
          label: 'Назва',
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
          name: 'isActive',
          required: true,
          label: 'Діє',
          align: 'left',
          field: val => (val.isActive ? '+' : '-'),
        },
      ],

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
    onCreateRole() {
      const routeParams = {
        path: '/roles/create',
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),

    requestUrl() {
      return env.ROLE.PATH
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Назва',
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
          name: 'createRole',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateRole,
        },
      ]
    },
  },
}

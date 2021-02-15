import { mapState } from 'vuex'
import { env } from '../../../services/api'

export default {
  data() {
    return {
      title: 'Переліки',

      columns: [
        {
          name: 'group',
          required: true,
          label: 'Група (англ.)',
          align: 'left',
          field: 'group',
        },
        {
          name: 'groupName',
          required: true,
          label: 'Група (укр.)',
          align: 'left',
          field: 'groupName',
        },
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
          name: 'value',
          required: true,
          label: 'Значення',
          align: 'left',
          field: 'value',
        },
        {
          name: 'itemNumber',
          required: true,
          label: 'Порядок',
          align: 'left',
          field: 'itemNumber',
        },
      ],

      visibleColumns: ['group', 'name', 'code'],

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
    onCreateEnum() {
      const routeParams = {
        path: '/enum/create',
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),
    requestUrl() {
      return env.ENUMRECORD.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.ENUMRECORD.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'text',
            label: 'Група (англ.)',
            key: 'group',
            value: '',
          },
          {
            type: 'text',
            label: 'Група (укр.)',
            key: 'groupName',
            value: '',
          },
          {
            type: 'text',
            label: 'Код',
            key: 'code',
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
          name: 'createEnum',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateEnum,
        },
      ]
    },
  },
}

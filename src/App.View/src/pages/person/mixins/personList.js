import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: 'Персона',

      columns: [
        {
          name: 'lastName',
          required: true,
          label: 'Прізвище',
          align: 'left',
          field: 'lastName',
        },
        {
          name: 'firstName',
          required: true,
          label: "Ім'я",
          align: 'left',
          field: 'firstName',
        },
        {
          name: 'middleName',
          required: true,
          label: 'По батькові',
          align: 'left',
          field: 'middleName',
        },
        {
          name: 'birthday',
          required: true,
          label: 'Дата народження',
          align: 'left',
          field: 'birthday',
          format: (val) => this.$options.filters.asDate(val, this.dateFormat)
        },
        {
          name: 'genderName',
          required: true,
          label: 'Стать',
          align: 'left',
          field: 'genderName',
        },
        {
          name: 'taxNumber',
          required: true,
          label: 'ІПН',
          align: 'left',
          field: 'taxNumber',
        },
        {
          name: 'identityDocumentName',
          required: true,
          label: 'Документ, що посвідчує особу',
          align: 'left',
          field: 'identityDocumentName',
        },
      ],

      dateFormat: "DD.MM.YYYY",

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
    onCreatePerson() {
      const routeParams = {
        path: '/person/create',
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
      return env.PERSON.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.PERSON.PATH,

        mainInput: {
          type: 'text',
          label: 'ПІБ',
          key: 'caption',
          value: '',
        },

        filters: [
          {
            type: 'date-range',
            label: 'Дата народження',
            key: 'personBirthday',
            start: {
              key: 'birthdayFrom',
            },
            end: {
              key: 'birthdayTo',
            },
          },
          {
            type: 'multi-select',
            label: 'Стать',
            url: this.getEnumByGroupUrl('Gender'),
            key: 'gender',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'taxNumber',
            label: 'Індивідуальний податковий номер (ІПН)',
            key: 'taxNumber',
            value: '',
          },
          {
            type: 'multi-select',
            label: 'Документ, що посвідчує особу',
            url: this.getEnumByGroupUrl('IdentityDocument'),
            key: 'identityDocument',
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
          name: 'createPerson',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreatePerson,
        },
      ]
    },
  },
}

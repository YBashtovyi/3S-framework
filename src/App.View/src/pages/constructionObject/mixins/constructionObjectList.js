import isEmpty from 'lodash.isempty'
import { mapState } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'

export default {
  data() {
    return {
      title: `Об'єкт`,

      columns: [
        {
          name: 'objectStatus',
          required: true,
          label: 'Статус',
          align: 'left',
          field: 'objectStatusName',
        },
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
          name: 'typeOfConstructionObject',
          required: true,
          label: `Тип об'єкту`,
          align: 'left',
          field: 'typeOfConstructionObjectName',
        },
        {
          name: 'classOfConsequence',
          required: true,
          label: 'Клас наслідків',
          align: 'left',
          field: 'classOfConsequenceName',
        },
        {
          name: 'atuCoordinates',
          required: true,
          label: 'АТУ-координати',
          align: 'left',
          field: val => this.parseCoordinate(val.atuCoordinates),
        },
        {
          name: 'extendedProperty',
          required: true,
          label: 'Додаткові характеристики',
          align: 'left',
          field: 'extendedProperty',
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
    onCreateObject() {
      const routeParams = {
        path: '/objects/create',
      }

      this.$router.push(routeParams)
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },

    parseCoordinate(coordinates) {
      if (isEmpty(coordinates)) return
      const parseCoordinates = JSON.parse(coordinates)
      let content = []
      if (parseCoordinates.lines && parseCoordinates.lines.length > 0) {
        content.push(`Ліній: ${parseCoordinates.lines.length}`)
      }
      if (parseCoordinates.polygons && parseCoordinates.polygons.length > 0) {
        content.push(`Полігонів: ${parseCoordinates.polygons.length}`)
      }
      if (parseCoordinates.markers && parseCoordinates.markers.length > 0) {
        content.push(`Маркерів: ${parseCoordinates.markers.length}`)
      }
      return content.join(',')
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),

    requestUrl() {
      return env.CONSTRUCTION_OBJECT.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.CONSTRUCTION_OBJECT.PATH,

        mainInput: {
          type: 'text',
          label: `Назва об'єкту`,
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'multi-select',
            label: `Тип об'єкту`,
            url: this.getEnumByGroupUrl('TypeOfConstructionObject'),
            key: 'typeOfConstructionObject',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'multi-select',
            label: `Клас наслідків`,
            url: this.getEnumByGroupUrl('ClassOfConsequence'),
            key: 'classOfConsequence',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'multi-select',
            label: `Статус об'єкту`,
            url: this.getEnumByGroupUrl('ObjectStatus'),
            key: 'objectStatus',
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
          name: 'createObject',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateObject,
        },
      ]
    },
  },
}

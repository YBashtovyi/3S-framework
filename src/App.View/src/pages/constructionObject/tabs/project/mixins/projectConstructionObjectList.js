import { mapState } from 'vuex'
import { env } from '../../../../../services/api'
import { isEmpty, stringEmpty } from '../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../utils/url-builder'
import { PROJECT_IMPLEMENTATION_STATE } from '../../../../../constants/general/projectImplementationState'
import get from 'lodash.get'

export default {
  data() {
    return {
      title: 'Проєкт',

      columns: [
        {
          name: 'name',
          required: true,
          label: 'Назва проекту',
          align: 'left',
          field: 'name',
        },
        {
          name: 'regionName',
          required: true,
          label: 'Область',
          align: 'left',
          field: 'regionName',
        },
        {
          name: 'date',
          required: true,
          label: 'Термін виконання',
          align: 'left',
          field: p =>
            this.getDateFromTo({
              from: this.$options.filters.asDate(p.dateBegin, this.dateFormat),
              to: this.$options.filters.asDate(p.dateEnd, this.dateFormat),
            }),
        },
        {
          name: 'projectStatusName',
          required: true,
          label: 'Статус проекту',
          align: 'left',
          field: 'projectStatusName',
        },
        {
          name: 'projectImplementationStateName',
          required: true,
          label: 'Статус виконання',
          align: 'left',
          field: 'projectImplementationStateName',
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
    }
  },

  methods: {
    getDateFromTo(date) {
      if (isEmpty(date.from) && isEmpty(date.to)) {
        return '-'
      } else if (!isEmpty(date.from) && isEmpty(date.to)) {
        return `З ${date.from}`
      } else if (isEmpty(date.from) && !isEmpty(date.to)) {
        return `По ${date.to}`
      } else {
        return `${date.from}-${date.to}`
      }
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },

    // Row red if the prescription has expired
    setRowColor(row) {
      switch (row.projectImplementationState) {
        case PROJECT_IMPLEMENTATION_STATE.BY_PLAN.code:
          return PROJECT_IMPLEMENTATION_STATE.BY_PLAN.color
        case PROJECT_IMPLEMENTATION_STATE.SIGNIFICANT_LAG.code:
          return PROJECT_IMPLEMENTATION_STATE.SIGNIFICANT_LAG.color
        case PROJECT_IMPLEMENTATION_STATE.NOT_A_SIGNIFICANT_LAG.code:
          return PROJECT_IMPLEMENTATION_STATE.NOT_A_SIGNIFICANT_LAG.color
      }
    },

    goToDetails(row) {
      const routeData = this.$router.resolve({ path: `/projects/details/${row.id}` })
      window.open(routeData.href, '_blank')
    },
  },

  computed: {
    ...mapState('tableList', ['tableData']),

    constructionObjectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    requestUrl() {
      const url = new UrlBuilder({ host: env.CONSTRUCTION_OBJECT.PROJECT_OBJECT_PATH })
        .path(this.constructionObjectId)
        .build()
      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Назва проекту',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'text',
            label: 'Область',
            key: 'regionName',
            value: '',
          },
          {
            type: 'multi-select',
            label: 'Статус виконання проекту',
            url: this.getEnumByGroupUrl('ProjectImplementationState'),
            key: 'projectImplementationState',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'text',
            label: 'Вартість',
            key: 'cost',
            value: '',
          },
          {
            type: 'date-range',
            label: 'Дата реалізації',
            key: 'projectDate',
            start: {
              key: 'dateBegin',
            },
            end: {
              key: 'dateEnd',
            },
          },
          {
            type: 'multi-select',
            label: 'Статус проекту',
            url: this.getEnumByGroupUrl('ProjectStatus'),
            key: 'code',
            optionLabel: 'name',
            value: [],
          },
        ],
      }
    },

    actions() {
      return [
        {
          type: 'detailFn',
          label: 'Переглянути',
          icon: 'fas fa-eye',
          fn: row => this.goToDetails(row),
          visible: true,
        },
        {
          type: 'rowFn',
          label: 'Переглянути',
          icon: 'fas fa-eye',
          fn: data => this.goToDetails(data.row),
          visible: false,
        },
      ]
    },
  },
}

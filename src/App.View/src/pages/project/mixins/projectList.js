import { mapState } from 'vuex'
import { env } from '../../../services/api'
import { isEmpty } from '../../../utils/function-helpers'
import UrlBuilder from '../../../utils/url-builder'
import { PROJECT_IMPLEMENTATION_STATE } from '../../../constants/general/projectImplementationState'

export default {
  data() {
    return {
      title: 'Проект',

      columns: [
        {
          name: 'code',
          required: true,
          label: 'Код проєкту',
          align: 'left',
          field: 'code',
        },
        {
          name: 'constructionObjectCode',
          required: true,
          label: `Код об'єкту`,
          align: 'left',
          field: 'constructionObjectCode',
        },
        {
          name: 'projectStatusName',
          required: true,
          label: 'Статус проекту',
          align: 'left',
          field: 'projectStatusName',
        },
        {
          name: 'regionName',
          required: true,
          label: 'Область',
          align: 'left',
          field: 'regionName',
        },
        {
          name: 'name',
          required: true,
          label: 'Назва проекту',
          align: 'left',
          field: 'name',
        },
        {
          name: 'generalContractorName',
          required: true,
          label: 'Виконавець',
          align: 'left',
          field: 'generalContractorName',
        },
        {
          name: 'customerName',
          required: true,
          label: 'Замовник',
          align: 'left',
          field: 'customerName',
        },
        {
          name: 'cost',
          required: true,
          label: 'Вартість проекту тис. грн',
          align: 'left',
          field: 'cost',
        },
        {
          name: 'date',
          required: true,
          label: 'Терміни виконання',
          align: 'left',
          field: p =>
            this.getDateFromTo({
              from: this.$options.filters.asDate(p.dateBegin, this.dateFormat),
              to: this.$options.filters.asDate(p.dateEnd, this.dateFormat),
            }),
        },
        {
          name: 'projectImplementationStateName',
          required: true,
          label: 'Статус виконання',
          align: 'left',
          field: 'projectImplementationStateName',
        },
        // for excel
        {
          name: 'districtName',
          required: false,
          label: 'Район',
          field: 'districtName',
        },
        {
          name: 'repairLength',
          required: false,
          label: 'Протяжність ділянки ремонту, км',
          field: 'repairLength',
        },
        {
          name: 'repairSquare',
          required: false,
          label: 'Площа ділянки ремонту, м2',
          field: 'repairSquare',
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
          value: env.PROJECT.EXPORT.XLSX,
          icon: 'far fa-file-excel',
        },
      ],
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

    onCreateProject() {
      const routeParams = {
        path: '/projects/create',
      }

      this.$router.push(routeParams)
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },

    getTypeOfProjectWorkList() {
      return new UrlBuilder({ host: env.PROJECT.TYPE_OF_PROJECT_WORK_PATH }).build()
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
  },

  computed: {
    ...mapState('tableList', ['tableData']),

    requestUrl() {
      return env.PROJECT.PATH
    },

    searchConfig() {
      return {
        searchUrl: env.PROJECT.PATH,

        mainInput: {
          type: 'text',
          label: 'Назва проекту',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'text',
            label: 'Назва проекту',
            key: 'name',
            value: '',
          },
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
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'number-range',
            label: 'Вартість, тис. грн',
            start: {
              key: 'cost_from',
              value: '',
            },
            end: {
              key: 'cost_to',
              value: '',
            },
          },
          {
            type: 'date-range',
            label: 'Терміни виконання',
            key: 'projectDate',
            start: {
              key: 'dateBegin_from',
              value: '',
            },
            end: {
              key: 'dateEnd_to',
              value: '',
            },
          },
          {
            type: 'multi-select',
            label: 'Статус проекту',
            url: this.getEnumByGroupUrl('ProjectStatus'),
            key: 'projectStatus',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'multi-select',
            label: 'Тип робіт',
            url: this.getTypeOfProjectWorkList(),
            key: 'typeOfProjectWorkId',
            optionValue: 'id',
            optionLabel: 'codeName',
            value: null,
          },
          {
            type: 'multi-select',
            label: 'Тип фінансування',
            url: this.getEnumByGroupUrl('TypeOfFinancing'),
            key: 'typeOfFinancing',
            optionValue: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'text',
            label: 'Учасник',
            key: 'participantOrgName',
            value: '',
          },
        ],
      }
    },

    actions() {
      return [
        {
          type: 'edit',
          label: 'Редагувати',
          icon: 'edit',
          visible: true,
        },
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
          action: this.onCreateProject,
        },
      ]
    },
  },
}

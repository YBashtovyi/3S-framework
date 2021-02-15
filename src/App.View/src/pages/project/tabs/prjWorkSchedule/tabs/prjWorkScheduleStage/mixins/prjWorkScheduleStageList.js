import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../../../services/api'
import get from 'lodash.get'
import { deleteProjectWorkScheduleStage } from '../../../../../../../services/prj-api/prj-work-schedule-stage'
import { stringEmpty } from '../../../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../../../utils/url-builder'
import isEmpty from 'lodash.isempty'

export default {
  data() {
    return {
      title: 'Етапи проєкту',

      columns: [
        {
          name: 'numberAndNameStage',
          required: true,
          label: 'Етап',
          align: 'left',
          field: row => this.numberAndNameStage(row),
        },
        {
          name: 'beginDate',
          required: true,
          label: 'Дата початку',
          align: 'left',
          field: 'beginDate',
          format: val => this.$options.filters.asDate(val, this.dateFormat),
        },
        {
          name: 'endDate',
          required: true,
          label: 'Дата закінчення',
          align: 'left',
          field: 'endDate',
          format: val => this.$options.filters.asDate(val, this.dateFormat),
        },
        {
          name: 'cost',
          required: true,
          label: 'Кошторисна вартість (тис. грн.)',
          align: 'left',
          field: 'cost',
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
    ...mapActions('tableList', ['init']),

    onCreatePrjWorkScheduleStage() {
      const routeParams = {
        path: 'stage/create',
      }

      this.$router.push(routeParams)
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },

    refreshTable() {
      return this.init({
        pagination: this.paginationTable,
        requestUrl: this.requestUrl,
      })
    },

    numberAndNameStage(row) {
      return `${row.stageNumber} ${row.stageName}`
    },

    // #region Actions

    editPage(row) {
      const routeParams = {
        path: `stage/edit/${row.id}`,
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      const routeParams = {
        path: `stage/details/${row.id}`,
      }

      this.$router.push(routeParams)
    },

    // #region Action Delete

    deleteDialog(row) {
      this.$q
        .dialog({
          title: 'Підтвердіть видалення',
          persistent: true,
          ok: {
            label: 'Видалити',
            color: 'negative',
            flat: true,
          },
          cancel: {
            flat: true,
            label: 'Відмінити',
            color: 'primary',
          },
        })
        .onOk(() => {
          this.deletePrjWorkScheduleStage(row)
        })
    },

    deletePrjWorkScheduleStage(row) {
      deleteProjectWorkScheduleStage(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Документ успішно видалений', icon: 'check' })
    },

    // #endregion

    // #endregion
  },

  computed: {
    ...mapState('tableList', ['tableData', 'paginationTable']),

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    documentId() {
      return get(this.$route, ['params', 'prjDocId'], stringEmpty())
    },

    requestUrl() {
      const url = new UrlBuilder({ host: env.PROJECT_WORK_SCHEDULE_STAGE.PATH })
        .param('PrjWorkScheduleId', this.documentId)
        .build()
      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: '№ етапу',
          key: 'stageNumber',
          value: '',
        },

        filters: [
          {
            type: 'date-range',
            label: 'Дата',
            key: 'projectDate',
            start: {
              key: 'beginDate',
            },
            end: {
              key: 'endDate',
            },
          },
          {
            type: 'text',
            label: 'Кошторисна вартість (тис. грн.)',
            key: 'cost',
            value: '',
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
          fn: row => this.detailsPage(row),
          visible: true,
        },
        {
          type: 'rowFn',
          fn: data => this.detailsPage(data.row),
        },
        {
          type: 'editFn',
          label: 'Редагувати',
          icon: 'edit',
          fn: row => this.editPage(row),
          visible: !isEmpty(this.projectId),
        },
        {
          type: 'deleteFn',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.deleteDialog(row),
          visible: true,
        },
      ]
    },

    tableHeaderButtons() {
      return isEmpty(this.projectId)
        ? []
        : [
            {
              name: 'createPrjWorkScheduleStage',
              icon: 'add',
              color: 'positive',
              label: 'Створити',
              iconClass: 'q-pr-sm',
              iconSize: '18px',
              action: this.onCreatePrjWorkScheduleStage,
            },
          ]
    },
  },
}

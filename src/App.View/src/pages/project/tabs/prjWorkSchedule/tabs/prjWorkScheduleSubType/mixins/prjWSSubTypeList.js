import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../../../services/api'
import get from 'lodash.get'
import { deleteProjectWorkScheduleSubType } from '../../../../../../../services/prj-api/prj-work-schedule-sub-type'
import { stringEmpty } from '../../../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../../../utils/url-builder'
import isEmpty from 'lodash.isempty'

export default {
  data() {
    return {
      title: 'Види робіт',

      columns: [
        {
          name: 'prjWorkScheduleStageName',
          required: true,
          label: 'Етап',
          align: 'left',
          field: 'prjWorkScheduleStageName',
        },
        {
          name: 'workSubTypeName',
          required: true,
          label: 'Вид робіт',
          align: 'left',
          field: 'workSubTypeName',
        },
        {
          name: 'amount',
          required: true,
          label: 'Кількість одиниць',
          align: 'left',
          field: 'amount',
        },
        {
          name: 'measurementUnitValue',
          required: true,
          label: 'Одиниця виміру',
          align: 'left',
          field: 'measurementUnitValue',
        },
        {
          name: 'target',
          required: true,
          label: 'Цільовий показник',
          align: 'left',
          field: 'target',
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

    onCreatePrjWorkScheduleSubType() {
      const routeParams = {
        path: 'subType/create',
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

    // #region Actions

    editPage(row) {
      const routeParams = {
        path: `subType/edit/${row.id}`,
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      const routeParams = {
        path: `subType/details/${row.id}`,
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
          this.deletePrjWorkScheduleSubType(row)
        })
    },

    deletePrjWorkScheduleSubType(row) {
      deleteProjectWorkScheduleSubType(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Вид робіт успішно видалений', icon: 'check' })
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
      const url = new UrlBuilder({ host: env.PROJECT_WORK_SCHEDULE_SUB_TYPE.PATH })
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
            type: 'multi-select',
            label: 'Вид робіт',
            url: this.getEnumByGroupUrl('DocType'),
            key: 'code',
            optionLabel: 'name',
            value: [],
          },
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
        ],
      }
    },

    actions() {
      return [
        {
          type: 'rowFn',
          fn: data => () => {},
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
              name: 'createPrjWorkScheduleSubType',
              icon: 'add',
              color: 'positive',
              label: 'Створити',
              iconClass: 'q-pr-sm',
              iconSize: '18px',
              action: this.onCreatePrjWorkScheduleSubType,
            },
          ]
    },
  },
}

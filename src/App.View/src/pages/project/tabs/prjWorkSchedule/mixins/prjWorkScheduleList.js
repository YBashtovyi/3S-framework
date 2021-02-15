import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../services/api'
import { stringEmpty } from '../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../utils/url-builder'
import get from 'lodash.get'
import { deleteProjectWorkSchedule } from '../../../../../services/prj-api/prj-work-schedule'

export default {
  data() {
    return {
      title: 'Документи проєкту',

      columns: [
        {
          name: 'dateAndNumber',
          required: true,
          label: 'Дата та номер',
          align: 'left',
          field: p => this.dateAndNumberDocument(p),
        },
        {
          name: 'docTypeName',
          required: true,
          label: 'Тип документу',
          align: 'left',
          field: 'docTypeName',
        },
        {
          name: 'docState',
          required: true,
          label: 'Статус',
          align: 'left',
          field: 'docStateName',
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
    ...mapActions('tableList', ['init']),

    onCreatePrjWorkSchedule() {
      const routeParams = {
        path: 'prjWorkSchedule/create/workSchedule',
      }

      this.$router.push(routeParams)
    },

    onCreatePrjChangesToWS() {
      const routeParams = {
        path: 'prjWorkSchedule/create/changesToWS',
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

    dateAndNumberDocument(data) {
      const dataTime = this.$options.filters.asDate(data.regDate, 'DD.MM.YYYY')
      return `${dataTime} - ${data.regNumber}`
    },

    // #region Actions

    editPage(row) {
      const routeParams = {
        path: `prjWorkSchedule/edit/${row.id}/${row.docType}`,
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      const routeParams = {
        path: `prjWorkSchedule/details/${row.id}`,
        params: { id: this.projectId, prjDocId: row.id },
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
          this.deletePrjDocument(row)
        })
    },

    deletePrjDocument(row) {
      deleteProjectWorkSchedule(row.id)
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

    requestUrl() {
      const url = new UrlBuilder({ host: env.PROJECT_WORK_SCHEDULE.PATH })
        .param('ProjectId', this.projectId)
        .build()
      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Тип документу',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'date-range',
            label: 'Дата',
            key: 'regDate',
            start: {
              key: 'regDate_start',
            },
            end: {
              key: 'regDate_end',
            },
          },
          {
            type: 'text',
            label: '№ документу',
            key: 'regNumber',
            value: '',
          },
          {
            type: 'multi-select',
            label: 'Тип документу',
            url: this.getEnumByGroupUrl('DocType'),
            key: 'code',
            optionLabel: 'name',
            value: [],
          },
          {
            type: 'multi-select',
            label: 'Статус документу',
            url: this.getEnumByGroupUrl('DocState'),
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
          visible: true,
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
      return [
        {
          name: 'createPrgParticipant',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreatePrjDocument,
        },
      ]
    },
  },
}

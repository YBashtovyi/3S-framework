import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../services/api'
import { stringEmpty } from '../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../utils/url-builder'
import get from 'lodash.get'
import { deleteProjectPhotoReport } from '../../../../../services/prj-api/prj-photo-report-api'

export default {
  data() {
    return {
      title: 'Фотозвіти',

      columns: [
        {
          name: 'docStateName',
          required: true,
          label: 'Статус',
          align: 'left',
          field: 'docStateName',
        },
        {
          name: 'regDate',
          required: true,
          label: 'Дата',
          align: 'left',
          field: 'regDate',
          format: val => this.$options.filters.asDate(val, this.dateFormat),
        },
        {
          name: 'regNumber',
          required: true,
          label: 'Номер',
          align: 'left',
          field: 'regNumber',
        },
        {
          name: 'docTypeName',
          required: true,
          label: 'Тип документу',
          align: 'left',
          field: 'docTypeName',
        },
        {
          name: 'fixationTypeName',
          required: true,
          label: 'Тип фіксації',
          align: 'left',
          field: 'fixationTypeName',
        },
        {
          name: 'fixationStateName',
          required: true,
          label: 'Стан фіксації',
          align: 'left',
          field: 'fixationStateName',
        },
        {
          name: 'customerName',
          required: true,
          label: 'Замовник',
          align: 'left',
          field: 'customerName',
        },
        {
          name: 'generalContractorName',
          required: true,
          label: 'Виконавець',
          align: 'left',
          field: 'generalContractorName',
        },
      ],

      pagination: {
        descending: true,
        page: 1,
        rowsPerPage: 50,
        rowsNumber: 50,
        sortBy: 'createdOn',
      },

      dateFormat: 'DD.MM.YYYY',
    }
  },

  methods: {
    ...mapActions('tableList', ['init']),

    onCreatePrjContract() {
      const routeParams = {
        path: 'prjPhotoReport/create',
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
        path: `prjPhotoReport/edit/${row.id}`,
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      const routeParams = {
        path: `prjPhotoReport/details/${row.id}`,
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
          this.deletePrjPhotoReport(row)
        })
    },

    deletePrjPhotoReport(row) {
      deleteProjectPhotoReport(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Фотозвіт успішно видалений', icon: 'check' })
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
      const url = new UrlBuilder({ host: env.PROJECT_PHOTO_REPORT.PATH })
        .param('ProjectId', this.projectId)
        .build()
      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Номер',
          key: 'regNumber',
          value: '',
        },

        filters: [],
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
          name: 'createPrjContract',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreatePrjContract,
        },
      ]
    },
  },
}

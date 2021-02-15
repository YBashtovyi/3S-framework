import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../services/api'
import { stringEmpty } from '../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../utils/url-builder'
import get from 'lodash.get'
import { deleteProjectContract } from '../../../../../services/prj-api/prj-contract-api'
import { deleteProjectAdditionalAgreement } from '../../../../../services/prj-api/prj-additional-agreement'

export default {
  data() {
    return {
      title: 'Деталі контрактів',

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
          name: 'projectName',
          required: true,
          label: 'Проєкт',
          align: 'left',
          field: 'projectName',
        },
        {
          name: 'cost',
          required: true,
          label: 'Вартість, тис. грн',
          align: 'left',
          field: 'cost',
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
        path: 'prjContract/create',
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
      let routeParams
      if (row.docType === 'Contract') {
        routeParams = {
          path: `prjContract/edit/${row.id}`,
        }
      } else {
        routeParams = {
          path: `prjContract/details/${row.parentId}/prjAdditionalAgreement/edit/${row.id}`,
        }
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      let routeParams
      if (row.docType === 'Contract') {
        routeParams = {
          path: `prjContract/details/${row.id}`,
        }
      } else {
        routeParams = {
          path: `prjContract/details/${row.parentId}/prjAdditionalAgreement/details/${row.id}`,
        }
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
          if (row.docType === 'Contract') {
            this.deletePrjContract(row)
          } else {
            this.deletePrjAddAgreement(row)
          }
        })
    },

    deletePrjContract(row) {
      deleteProjectContract(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    deletePrjAddAgreement(row) {
      deleteProjectAdditionalAgreement(row.id)
        .then(this.refreshTable)
        .then(this.notifyAddAgreementDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Договір успішно видалений', icon: 'check' })
    },

    notifyAddAgreementDeleteSuccess() {
      this.$q.notify({
        color: 'positive',
        message: 'Додаткова угода успішно видалена',
        icon: 'check',
      })
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
      const url = new UrlBuilder({ host: env.PROJECT_CONTRACT.PATH })
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

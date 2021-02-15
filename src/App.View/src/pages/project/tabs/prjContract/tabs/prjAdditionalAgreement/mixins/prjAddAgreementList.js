import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../../../services/api'
import { stringEmpty } from '../../../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../../../utils/url-builder'
import get from 'lodash.get'
import { deleteProjectAdditionalAgreement } from '../../../../../../../services/prj-api/prj-additional-agreement'
import isEmpty from 'lodash.isempty'

export default {
  data() {
    return {
      title: 'Додаткові угоди',

      columns: [
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
          name: 'docStateName',
          required: true,
          label: 'Статус',
          align: 'left',
          field: 'docStateName',
        },
        {
          name: 'cost',
          required: true,
          label: 'Вартість, тис. грн',
          align: 'left',
          field: 'cost',
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

    onCreatePrjAddAgreement() {
      const routeParams = {
        path: 'prjAdditionalAgreement/create',
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
        path: `prjAdditionalAgreement/edit/${row.id}`,
      }

      this.$router.push(routeParams)
    },

    detailsPage(row) {
      const routeParams = {
        path: `prjAdditionalAgreement/details/${row.id}`,
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
          this.deletePrjAddAgreement(row)
        })
    },

    deletePrjAddAgreement(row) {
      deleteProjectAdditionalAgreement(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
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

    prjContractId() {
      return get(this.$route, ['params', 'prjContractId'], stringEmpty())
    },

    prjAddAgreementId() {
      return get(this.$route, ['params', 'prjAddAgreementId'], stringEmpty())
    },

    requestUrl() {
      const url = new UrlBuilder({ host: env.PROJECT_ADDITIONAL_AGREEMENT.PATH })
        .param('parentId', this.prjContractId)
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
      return isEmpty(this.projectId)
        ? []
        : [
            {
              name: 'createPrjAddAgreement',
              icon: 'add',
              color: 'positive',
              label: 'Створити',
              iconClass: 'q-pr-sm',
              iconSize: '18px',
              action: this.onCreatePrjAddAgreement,
            },
          ]
    },
  },
}

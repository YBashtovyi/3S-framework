import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../services/'
import UrlBuilder from '../../../../../utils/url-builder'

import get from 'lodash.get'
import { stringEmpty } from '../../../../../utils/function-helpers'
import { deleteOrgUnitStaff } from '../../../../../services/org-api/org-unit-staff-api'

export default {
  data() {
    return {
      title: 'Організації',

      columns: [
        {
          name: 'personFullName',
          required: true,
          label: 'ПІБ',
          align: 'left',
          field: 'personFullName',
        },
        {
          name: 'orgUnitPositionName',
          required: true,
          label: 'Посада',
          align: 'left',
          field: 'orgUnitPositionName',
        },
        {
          name: 'startDate',
          required: true,
          label: 'Дата початку роботи на посаді',
          align: 'left',
          field: 'startDate',
          format: val => this.$options.filters.asDate(val, this.dateFormat),
        },
        {
          name: 'endDateFront',
          required: true,
          label: 'Дата припинення роботи на посаді',
          align: 'left',
          field: 'endDateFront',
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

    onCreateOrgUnitStaff() {
      const routeParams = {
        path: `/organization/${this.orgUnitId}/orgUnitStaff/create`,
      }

      this.$router.push(routeParams)
    },

    getOrgUnitPositionListUrl() {
      return new UrlBuilder({ host: env.ORG_UNIT_POSITION.PATH })
        .param('orgUnitId', this.orgUnitId)
        .build()
    },

    refreshTable() {
      return this.init({
        pagination: this.paginationTable,
        requestUrl: this.requestUrl,
      })
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
          this.deleteOrgUnitStaff(row)
        })
    },

    deleteOrgUnitStaff(row) {
      deleteOrgUnitStaff(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({
        color: 'positive',
        message: 'Співробітник успішно видалений',
        icon: 'check',
      })
    },

    // #endregion
  },

  computed: {
    ...mapState('tableList', ['tableData', 'paginationTable']),

    orgUnitId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    requestUrl() {
      const url = new UrlBuilder({ host: env.ORG_UNIT_STAFF.PATH })
        .param('orgUnitId', this.orgUnitId)
        .build()

      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'ПІБ',
          key: 'personFullName',
          value: '',
        },

        filters: [
          {
            type: 'multi-select',
            label: 'Посада',
            url: this.getOrgUnitPositionListUrl(),
            key: 'orgUnitPositionId',
            optionLabel: 'positionName',
            value: [],
          },
          {
            type: 'date-range',
            label: 'Дата початку роботи на посаді',
            key: 'startDate',
            start: {
              key: 'startDateFrom',
            },
            end: {
              key: 'startDateTo',
            },
          },
          {
            type: 'date-range',
            label: 'Дата припинення роботи на посаді',
            key: 'endDate',
            start: {
              key: 'endDateFrom',
            },
            end: {
              key: 'endDateTo',
            },
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
          name: 'createOrgUnitStaff',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreateOrgUnitStaff,
        },
      ]
    },
  },
}

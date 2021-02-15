import { mapState, mapActions } from 'vuex'
import { env } from '../../../../../services/api'
import { stringEmpty } from '../../../../../utils/function-helpers'
import UrlBuilder from '../../../../../utils/url-builder'
import get from 'lodash.get'
import { deletePrjParticipant } from '../../../../../services/prj-api/prj-participant-api'

export default {
  data() {
    return {
      title: 'Учасники проєкту',

      projectId: null,

      columns: [
        {
          name: 'projectRoleName',
          required: true,
          label: 'Роль',
          align: 'left',
          field: 'projectRoleName',
        },
        {
          name: 'participantName',
          required: true,
          label: 'Назва організації',
          align: 'left',
          field: 'participantName',
        },
        {
          name: 'responsiblePersonFullName',
          required: true,
          label: 'Відповідальна особа',
          align: 'left',
          field: 'responsiblePersonFullName',
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

    initDefaultFields() {
      this.projectId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreatePrgParticipant() {
      const routeParams = {
        path: 'prjParticipant/create',
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
          this.deleteParticipant(row)
        })
    },

    deleteParticipant(row) {
      deletePrjParticipant(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Учасник успішно видалений', icon: 'check' })
    },

    // #endregion
  },

  computed: {
    ...mapState('tableList', ['tableData', 'paginationTable']),
    requestUrl() {
      const url = new UrlBuilder({ host: env.PRJ_PARTICIPANT.PATH })
        .param('ProjectId', this.projectId)
        .build()
      return url
    },

    searchConfig() {
      return {
        searchUrl: this.requestUrl,

        mainInput: {
          type: 'text',
          label: 'Назва організації',
          key: 'name',
          value: '',
        },

        filters: [
          {
            type: 'multi-select',
            label: 'Роль',
            url: this.getEnumByGroupUrl('ProjectRole'),
            key: 'projectRole',
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
          name: 'createPrgParticipant',
          icon: 'add',
          color: 'positive',
          label: 'Створити',
          iconClass: 'q-pr-sm',
          iconSize: '18px',
          action: this.onCreatePrgParticipant,
        },
      ]
    },
  },

  created() {
    this.initDefaultFields()
  },
}

import { mapState, mapActions } from 'vuex'
import { env } from '../../../services/api'
import UrlBuilder from '../../../utils/url-builder'
import { downloadFile, deleteFile } from '../../../services/file-api'

export default {
  data() {
    return {
      title: 'Вміст',

      isVisibleFileUploadDialog: false,
      isVisibleSignFileDialog: false,

      columns: [
        {
          name: 'typeOfAttachedFileName',
          required: true,
          label: 'Тип документа',
          align: 'left',
          field: 'typeOfAttachedFileName',
        },
        {
          name: 'fileType',
          required: true,
          label: 'Тип файлу',
          align: 'left',
          field: 'fileType',
        },
        {
          name: 'fileName',
          required: true,
          label: "Ім'я файлу",
          align: 'left',
          field: 'fileName',
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

  props: {
    entityId: {
      type: String,
      required: true,
    },

    entityName: {
      type: String,
      required: true,
    },
    isVisibleBtnAdd: {
      type: Boolean,
      default: true,
    },
  },

  methods: {
    ...mapActions('tableList', ['init']),

    openFileUploadDialog() {
      this.isVisibleFileUploadDialog = true
    },

    signFileDialog() {
      this.isVisibleSignFileDialog = true
    },

    getEnumByGroupUrl(group) {
      return new UrlBuilder({ host: env.ENUMRECORD.PATH }).param('group', group).build()
    },

    onCloseDialog() {
      this.refreshTable()
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
          this.deleteAttachment(row)
        })
    },

    deleteAttachment(row) {
      deleteFile(row.id)
        .then(this.refreshTable)
        .then(this.notifyDeleteSuccess)
    },

    downloadFile(data) {
      downloadFile(data.row.id, data.row.fileName)
    },

    notifyDeleteSuccess() {
      this.$q.notify({ color: 'positive', message: 'Файл успішно видалений', icon: 'check' })
    },

    // #endregion
  },

  computed: {
    ...mapState('tableList', ['tableData', 'paginationTable']),
    requestUrl() {
      const url = new UrlBuilder({ host: env.FILESTORE.PATH })
        .param('EntityId', this.entityId)
        .param('EntityName', this.entityName)
        .build()
      return url
    },

    actions() {
      return [
        {
          type: 'deleteFn',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.deleteDialog(row),
          visible: true,
        },
        {
          type: 'rowFn',
          fn: data => this.downloadFile(data),
        },
      ]
    },

    tableHeaderButtons() {
      return !this.isVisibleBtnAdd
        ? []
        : [
            {
              name: 'openFileUploadDialog',
              icon: 'add',
              color: 'positive',
              label: 'Додати',
              iconClass: 'q-pr-sm',
              iconSize: '18px',
              action: this.openFileUploadDialog,
            },
            {
              name: 'signFileDialog',
              icon: 'add',
              color: 'positive',
              label: 'Підписати',
              iconClass: 'q-pr-sm',
              iconSize: '18px',
              action: this.signFileDialog,
            },
          ]
    },
  },
}

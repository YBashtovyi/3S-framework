import { getData, getFile, env } from '@/services/api'
import { format } from 'quasar'
const { humanStorageSize } = format

export default {
  data () {
    return {
      attachments: Array,
      meddocs: Array,
      visible: false,
      showSimulatedReturnData: false
    }
  },
  methods: {
    // get details meddocs
    getMeddocs () {
      getData(env.FILESTORE.PATH + '?entityId=' + this.$route.params.id, {
        params: {
          // disable filter on document type
          // here should be id related to enum record with enum_type = 'FileType' if required
          documentTypeId: null
        }
      })
        .then(response => {
          this.meddocs = response.data
          for (var i = 0; i < this.meddocs.length; i++) {
            this.meddocs[i].fileSize = humanStorageSize(
              this.meddocs[i].fileSize
            )
          }
        })
        .catch(error => console.log(error));
    },

    // get details attachments
    getAttachments () {
      getData(env.FILESTORE.PATH + '?entityId=' + this.$route.params.id, {
        params: {
          // disable filter on document type
          // here should be id related to enum record with enum_type = 'FileType' if required
          documentTypeId: null
        }
      })
        .then(response => {
          this.attachments = response.data
          for (var i = 0; i < this.attachments.length; i++) {
            this.attachments[i].fileSize = humanStorageSize(
              this.attachments[i].fileSize
            )
          }
        })
        .catch(error => console.log(error));
    },

    // download attachments & meddocs
    downloadFile (id, name) {
      getFile(env.FILESTORE.DOWNLOAD + id)
        .then(response => {
          let url = window.URL.createObjectURL(new Blob([response.data]))
          let link = document.createElement('a')
          link.href = url
          link.setAttribute('download', name)
          document.body.appendChild(link)
          link.click()
          setTimeout(() => {
            document.body.removeChild(link)
          }, 1000)
        })
        .catch(error => {
          this.$q.notify({
            position: 'top',
            timeout: 2000,
            message: 'Помилка загрузки файлу',
            color: 'warning',
            icon: 'warning',
            textColor: 'grey-9'
          })
          console.log(error)
        })
    }
  }
}

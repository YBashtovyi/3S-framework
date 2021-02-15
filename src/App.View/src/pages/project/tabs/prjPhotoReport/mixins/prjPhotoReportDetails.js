import get from 'lodash.get'
import { getDetailsProjectPhotoReportById } from '../../../../../services/prj-api/prj-photo-report-api'
import { stringEmpty } from '../../../../../utils/function-helpers'

export default {
  data() {
    return {
      details: null,
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
    }
  },

  methods: {
    fetchData() {
      getDetailsProjectPhotoReportById(this.prjPhotoReportId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjPhotoReport/edit/${this.prjPhotoReportId}`,
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    prjPhotoReportId() {
      return get(this.$route, ['params', 'prjPhotoReportId'], stringEmpty())
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    projectPhotoReportTitle() {
      return {
        label: this.titleName,
        content: 'Основна інформація',
        icon: 'description',
        actions: [
          {
            action: this.onEditActionClick,
            icon: 'far fa-edit',
            iconSize: '18px',
            label: 'Редагувати',
          },
        ],
      }
    },

    titleName() {
      const date = this.$options.filters.asDate(this.details.regDate, this.dateFormat)
      return `${this.details.docTypeName} ${this.details.regNumber} від ${date}`
    },

    regDateAndRegNumber() {
      const date = this.$options.filters.asDate(this.details.parentRegDate, this.dateFormat)
      return `${date} ${this.details.parentRegNumber}`
    },

    typeOfProjectWorkCodeAndName() {
      return `${this.details.typeOfProjectWorkCode} ${this.details.typeOfProjectWorkName}`
    },
  },

  created() {
    this.fetchData()
  },
}

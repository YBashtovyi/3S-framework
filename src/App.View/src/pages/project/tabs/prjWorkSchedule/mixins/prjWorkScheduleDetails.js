import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import { getDetailsProjectWorkScheduleById } from '../../../../../services/prj-api/prj-work-schedule'
import { stringEmpty } from '../../../../../utils/function-helpers'

export default {
  data() {
    return {
      details: null,
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
      dateTimeFormat: 'DD.MM.YYYY HH:mm',
    }
  },

  methods: {
    fetchData() {
      getDetailsProjectWorkScheduleById(this.documentId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/edit/${this.documentId}`, // TODO: Переделать в нормальный вид, потом еще в друнгих файлах
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    documentId() {
      return get(this.$route, ['params', 'prjDocId'], stringEmpty())
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    urlToDocument() {
      return !isEmpty(this.projectId)
        ? `/projects/details/${this.projectId}/prjWorkSchedule/details/${this.details.parentId}`
        : `/prjWorkSchedule/details/${this.details.parentId}`
    },

    projectWorkScheduleTitle() {
      return {
        label: `${this.details.regNumber}`,
        content: 'Основна інформація',
        icon: 'description',
        actions: [
          {
            action: this.onEditActionClick,
            icon: 'far fa-edit',
            iconSize: '18px',
            label: 'Редагувати',
            disable: isEmpty(this.projectId),
          },
        ],
      }
    },

    regDateAndRegNumber() {
      const date = this.$options.filters.asDate(this.details.parentRegDate, this.dateFormat)
      return `${date} ${this.details.parentRegNumber}`
    },
  },

  created() {
    this.fetchData()
  },
}

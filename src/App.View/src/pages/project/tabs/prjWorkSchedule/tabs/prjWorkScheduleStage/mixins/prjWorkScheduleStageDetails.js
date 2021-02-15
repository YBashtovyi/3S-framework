import get from 'lodash.get'
import { getDetailsProjectWorkScheduleStageById } from '../../../../../../../services/prj-api/prj-work-schedule-stage'
import { stringEmpty } from '../../../../../../../utils/function-helpers'

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
      getDetailsProjectWorkScheduleStageById(this.stageId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjWorkSchedule/details/${this.details.prjWorkScheduleId}/stage/edit/${this.details.id}`,
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    stageId() {
      return get(this.$route, ['params', 'prjDocStageId'], stringEmpty())
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    projectWorkScheduleStageTitle() {
      return {
        label: `${this.details.stageName}`,
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
  },

  created() {
    this.fetchData()
  },
}

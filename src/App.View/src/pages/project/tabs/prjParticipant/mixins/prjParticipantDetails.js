import { getDetailsPrjParticipantById } from '../../../../../services/prj-api/prj-participant-api'

export default {
  data() {
    return {
      details: null,
      label: 'Учасник проєету',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      const prjParticipantId = this.$route.params.prjParticipantId
      getDetailsPrjParticipantById(prjParticipantId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    prjParticipantTitle() {
      return {
        label: `Проєкт <${this.details.projectName}>`,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

import { getDetailsRightById } from '../../../../services/adm-api/right-api'

export default {
  data() {
    return {
      details: null,
      label: 'Право',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsRightById(this.rightId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    rightId() {
      return this.$route.params.id
    },

    rightTitle() {
      return {
        label: this.details.name,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

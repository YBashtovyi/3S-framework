import { getDetailsPersonById } from '../../../services/common-api/person-api'

export default {
  data() {
    return {
      details: null,
      label: 'Персона',
      pageReady: false,
      
      dateFormat: "DD.MM.YYYY",
    }
  },

  methods: {
    fetchData() {
        getDetailsPersonById(this.$route.params.id)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    enumTitle() {
      return {
        label: this.details.caption,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

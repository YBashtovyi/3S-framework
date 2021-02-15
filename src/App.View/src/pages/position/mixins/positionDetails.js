import { getPositionById } from '../../../services/cdn-api'

export default {
  data() {
    return {
      details: null,
      label: 'Посада',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
        getPositionById(this.$route.params.id)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    positionTitle() {
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

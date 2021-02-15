import { fetchEnumById } from '../../../services/enum-api'

export default {
  data() {
    return {
      details: null,
      label: 'Переліки',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      fetchEnumById(this.$route.params.id)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    enumTitle() {
      return {
        label: 'Переліки',
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

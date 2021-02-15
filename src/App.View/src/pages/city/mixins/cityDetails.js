import { getDetailsCityById } from '../../../services/org-api/city-api'

export default {
  data() {
    return {
      details: null,
      label: 'Населений пункт',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsCityById(this.cityId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    cityId() {
      return this.$route.params.id
    },

    cityTitle() {
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

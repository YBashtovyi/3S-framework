import { getDetailsOrganizationById } from '../../../services/org-api/organization-api'

export default {
  data() {
    return {
      details: null,
      label: 'Організація',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsOrganizationById(this.organizationId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    organizationId() {
      return this.$route.params.id
    },

    organizationTitle() {
      return {
        label: this.details.fullName,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

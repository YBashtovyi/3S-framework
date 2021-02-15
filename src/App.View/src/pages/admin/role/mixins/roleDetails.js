import { getDetailsRoleById } from '../../../../services/adm-api/role-api'

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
      getDetailsRoleById(this.roleId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    roleId() {
      return this.$route.params.id
    },

    roleTitle() {
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

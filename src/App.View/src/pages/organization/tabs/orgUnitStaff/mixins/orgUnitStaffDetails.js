import { getDetailsOrgUnitStaffById } from '../../../../../services/org-api/org-unit-staff-api'

export default {
  data() {
    return {
      details: null,
      label: 'Посада',
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
    }
  },

  methods: {
    fetchData() {
        getDetailsOrgUnitStaffById(this.orgUnitStaffId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    orgUnitStaffId() {
      return this.$route.params.orgUnitStaffId
    },

    orgUnitStaffTitle() {
      return {
        label: this.details.personFullName,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

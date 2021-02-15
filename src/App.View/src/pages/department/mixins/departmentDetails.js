import { getDetailsDepartmentById } from '../../../services/org-api/department-api'

export default {
  data() {
    return {
      details: null,
      label: 'Підрозділ',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsDepartmentById(this.departmentId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    departmentId() {
      return this.$route.params.id
    },

    departmentTitle() {
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

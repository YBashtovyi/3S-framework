import { getDetailsUserById } from '../../../../services/adm-api/user-api'

export default {
  data() {
    return {
      details: null,
      roles: [],
      label: 'Користувач системи',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsUserById(this.userId)
        .then(this.setDetailsData)
        .finally(p => (this.pageReady = true))
    },

    setDetailsData(data) {
      this.details = data
    },
  },

  computed: {
    userId() {
      return this.$route.params.id
    },

    userFullName() {
      return `${this.details.personLastName} ${this.details.personFirstName} ${this.details.personMiddleName}`
    },

    userTitle() {
      return {
        label: this.userFullName,
        content: 'Основна інформація',
        icon: 'description',
      }
    },
  },

  created() {
    this.fetchData()
  },
}

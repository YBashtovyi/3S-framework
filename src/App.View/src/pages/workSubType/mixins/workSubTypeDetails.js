import { getDetailsWorkSubTypeById } from '../../../services/classifiers/work-sub-type-api'

export default {
  data() {
    return {
      details: null,
      label: 'Види робіт',
      pageReady: false,
    }
  },

  methods: {
    fetchData() {
      getDetailsWorkSubTypeById(this.$route.params.id)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    workSubTypeTitle() {
      return {
        label: this.details.name,
        content: 'Основна інформація',
        icon: 'description',
      }
    },

    parentCodeAndName() {
      return `${this.details.parentCode} ${this.details.parentName}`
    },

    measurementUnitNameAndValue() {
      return `${this.details.measurementUnitName} (${this.details.measurementUnitValue}) `
    },
  },

  created() {
    this.fetchData()
  },
}

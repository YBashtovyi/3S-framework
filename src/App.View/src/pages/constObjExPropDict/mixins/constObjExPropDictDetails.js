import { getDetailsConstructionObjectExPropertyById } from '../../../services/classifiers/constuction-object-ex-property-dict'

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
      getDetailsConstructionObjectExPropertyById(this.$route.params.id)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },
  },

  computed: {
    exPropTitle() {
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

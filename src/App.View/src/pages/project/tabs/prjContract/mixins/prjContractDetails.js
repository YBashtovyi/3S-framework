import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import { getDetailsProjectContractById } from '../../../../../services/prj-api/prj-contract-api'
import { stringEmpty } from '../../../../../utils/function-helpers'

export default {
  data() {
    return {
      details: null,
      pageReady: false,

      dateFormat: 'DD.MM.YYYY',
    }
  },

  methods: {
    fetchData() {
      getDetailsProjectContractById(this.prjContractId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjContract/edit/${this.prjContractId}`,
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    prjContractId() {
      return get(this.$route, ['params', 'prjContractId'], stringEmpty())
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    projectContractTitle() {
      return {
        label: this.titleName,
        content: 'Основна інформація',
        icon: 'description',
        actions: [
          {
            action: this.onEditActionClick,
            icon: 'far fa-edit',
            iconSize: '18px',
            label: 'Редагувати',
            disable: isEmpty(this.projectId),
          },
        ],
      }
    },

    titleName() {
      const date = this.$options.filters.asDate(this.details.regDate, this.dateFormat)
      return `${this.details.docTypeName} ${this.details.regNumber} від ${date}`
    },

    regDateAndRegNumber() {
      const date = this.$options.filters.asDate(this.details.parentRegDate, this.dateFormat)
      return `${date} ${this.details.parentRegNumber}`
    },

    typeOfProjectWorkCodeAndName() {
      return `${this.details.typeOfProjectWorkCode} ${this.details.typeOfProjectWorkName}`
    },
  },

  created() {
    this.fetchData()
  },
}

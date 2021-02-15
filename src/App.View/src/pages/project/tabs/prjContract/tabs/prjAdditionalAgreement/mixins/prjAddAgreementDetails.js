import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import { getDetailsProjectAdditionalAgreementById } from '../../../../../../../services/prj-api/prj-additional-agreement'
import { stringEmpty } from '../../../../../../../utils/function-helpers'

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
      getDetailsProjectAdditionalAgreementById(this.prjAddAgreementId)
        .then(p => (this.details = p))
        .finally(p => (this.pageReady = true))
    },

    onEditActionClick() {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjContract/details/${this.prjContractId}/prjAdditionalAgreement/edit/${this.prjAddAgreementId}`,
      }

      this.$router.push(routeParams)
    },

    urlToDocument() {
      return !isEmpty(this.projectId)
        ? `/projects/details/${this.projectId}/prjContract/details/${this.prjContractId}`
        : `/prjContract/details/${this.prjContractId}`
    },
  },

  computed: {
    prjAddAgreementId() {
      return get(this.$route, ['params', 'prjAddAgreementId'], stringEmpty())
    },

    prjContractId() {
      return get(this.$route, ['params', 'prjContractId'], stringEmpty())
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    projectAdditionalAgreementTitle() {
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

    parentRegDateAndRegNumber() {
      const date = this.$options.filters.asDate(this.details.parentRegDate, this.dateFormat)
      return `${this.details.parentRegNumber} ${date}`
    },

    typeOfProjectWorkCodeAndName() {
      return `${this.details.typeOfProjectWorkCode} ${this.details.typeOfProjectWorkName}`
    },
  },

  created() {
    this.fetchData()
  },
}

import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../../../../../services/enum-api'
import {
  createProjectAdditionalAgreement,
  editProjectAdditionalAgreement,
  getEditProjectAdditionalAgreementById,
} from '../../../../../../../services/prj-api/prj-additional-agreement'
import { stringEmpty } from '../../../../../../../utils/function-helpers'

export default {
  data() {
    return {
      prjAddAgreement: {},

      pageReady: false,

      selectedDocType: null,
      selectedDocState: null,

      docTypes: [],
      docStates: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.prjAddAgreementId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    prjContractId() {
      return get(this.$route, ['params', 'prjContractId'], stringEmpty())
    },

    prjAddAgreementId() {
      return get(this.$route, ['params', 'prjAddAgreementId'], stringEmpty())
    },
  },

  methods: {
    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(this.getEditPageData)
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data (orgUnitStaff)
     */
    getEditPageData() {
      if (!this.isEditMode) {
        this.setDefaultValue()
        return Promise.resolve()
      }

      return getEditProjectAdditionalAgreementById(this.prjAddAgreementId).then(
        this.setEditPageData,
      )
    },

    /**
     * After loading the data, initialize the page
     * @param {*} project
     */
    setEditPageData(prjAddAgreement) {
      set(this, 'prjAddAgreement', prjAddAgreement)

      if (this.isEditMode) {
        const selectedDocType = find(
          this.docTypes,
          type => type.code === get(this, ['prjAddAgreement', 'docType'], null),
        )

        const selectedDocState = find(
          this.docStates,
          type => type.code === get(this, ['prjAddAgreement', 'docState'], null),
        )

        set(this, 'selectedDocType', selectedDocType)
        set(this, 'selectedDocState', selectedDocState)
      }

      return Promise.resolve()
    },

    setDefaultValue() {
      const selectedDocType = find(this.docTypes, type => type.code === 'AdditionalAgreement')
      set(this, 'selectedDocType', selectedDocType)
      set(this, ['prjAddAgreement', 'docType'], get(selectedDocType, 'code', stringEmpty()))
    },

    onSubmit() {
      if (!this.isEditMode) {
        this.prjAddAgreement.projectId = this.projectId
        this.prjAddAgreement.parentId = this.prjContractId
        createProjectAdditionalAgreement(this.prjAddAgreement)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editProjectAdditionalAgreement(this.prjAddAgreement.id, this.prjAddAgreement)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(prjAddAgreement) {
      if (isEmpty(prjAddAgreement)) {
        this.$router.push(
          `/projects/details/${this.projectId}/prjContract/details/${this.prjContractId}/prjAdditionalAgreement/details/${this.prjAddAgreement.id}`,
        )
      } else {
        this.$router.push(
          `/projects/details/${this.projectId}/prjContract/details/${this.prjContractId}/prjAdditionalAgreement/details/${prjAddAgreement.id}`,
        )
      }
    },

    // #region Event handlers

    onDocTypeChanged(docType) {
      set(this, 'selectedDocType', docType)
      set(this, ['prjAddAgreement', 'docType'], get(docType, 'code', stringEmpty()))
    },

    onDocStateChanged(docState) {
      set(this, 'selectedDocState', docState)
      set(this, ['prjAddAgreement', 'docState'], get(docState, 'code', stringEmpty()))
    },

    // #endregion

    getEnums() {
      const enumGroups = ['DocType', 'DocState']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.docTypes = p.DocType
        this.docStates = p.DocState
      })
    },

    // #region Change page states methods

    setPageLoading() {
      this.pageReady = false
    },

    setPageLoaded() {
      this.pageReady = true
    },

    // #endregion
  },

  created() {
    this.onCreate()
  },
}

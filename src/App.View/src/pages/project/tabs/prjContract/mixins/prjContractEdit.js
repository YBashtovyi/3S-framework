import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../../../services/enum-api'
import {
  createProjectContract,
  editProjectContract,
  getEditProjectContractById,
} from '../../../../../services/prj-api/prj-contract-api'
import { stringEmpty } from '../../../../../utils/function-helpers'

export default {
  data() {
    return {
      prjContract: {},

      pageReady: false,

      selectedDocType: null,
      selectedDocState: null,
      selectedBiddingType: null,

      docTypes: [],
      docStates: [],
      biddingTypes: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.prjContractId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    prjContractId() {
      return get(this.$route, ['params', 'prjContractId'], stringEmpty())
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

      return getEditProjectContractById(this.prjContractId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} project
     */
    setEditPageData(prjContract) {
      set(this, 'prjContract', prjContract)

      if (this.isEditMode) {
        const selectedDocType = find(
          this.docTypes,
          type => type.code === get(this, ['prjContract', 'docType'], null),
        )

        const selectedDocState = find(
          this.docStates,
          type => type.code === get(this, ['prjContract', 'docState'], null),
        )

        const selectedBiddingType = find(
          this.biddingTypes,
          type => type.code === get(this, ['prjContract', 'biddingType'], null),
        )

        set(this, 'selectedDocType', selectedDocType)
        set(this, 'selectedDocState', selectedDocState)
        set(this, 'selectedBiddingType', selectedBiddingType)
      }

      return Promise.resolve()
    },

    setDefaultValue() {
      const selectedDocType = find(this.docTypes, type => type.code === 'Contract')
      set(this, 'selectedDocType', selectedDocType)
      set(this, ['prjContract', 'docType'], get(selectedDocType, 'code', stringEmpty()))
    },

    onSubmit() {
      if (!this.isEditMode) {
        this.prjContract.projectId = this.projectId
        createProjectContract(this.prjContract)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editProjectContract(this.prjContract.id, this.prjContract)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(prjContractId) {
      if (isEmpty(prjContractId)) {
        this.$router.push(
          `/projects/details/${this.projectId}/prjContract/details/${this.prjContractId}`,
        )
      } else {
        this.$router.push(
          `/projects/details/${this.projectId}/prjContract/details/${prjContractId}`,
        )
      }
    },

    // #region Event handlers

    onDocTypeChanged(docType) {
      set(this, 'selectedDocType', docType)
      set(this, ['prjContract', 'docType'], get(docType, 'code', stringEmpty()))
    },

    onDocStateChanged(docState) {
      set(this, 'selectedDocState', docState)
      set(this, ['prjContract', 'docState'], get(docState, 'code', stringEmpty()))
    },

    onBiddingTypeChanged(biddingType) {
      set(this, 'selectedBiddingType', biddingType)
      set(this, ['prjContract', 'biddingType'], get(biddingType, 'code', stringEmpty()))
    },

    // #endregion

    getEnums() {
      const enumGroups = ['DocType', 'DocState', 'BiddingType']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.docTypes = p.DocType
        this.docStates = p.DocState
        this.biddingTypes = p.BiddingType
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

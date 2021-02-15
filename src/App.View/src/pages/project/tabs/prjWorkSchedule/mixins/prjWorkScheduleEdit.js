import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import {
  createProjectWorkScheduleWorkSchedule,
  editProjectWorkSchedule,
  getEditProjectWorkScheduleById,
} from '../../../../../services/prj-api/prj-work-schedule'

import { fetchListEnumByGroup } from '../../../../../services/enum-api'

import { stringEmpty } from '../../../../../utils/function-helpers'

import find from 'lodash.find'

export default {
  data() {
    return {
      editData: {},

      pageReady: false,

      selectedDocumentType: null,
      selectedDocumentState: null,

      documentTypes: [],
      documentStates: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.documentId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    // DocumentType - use if editMode create
    docType() {
      return get(this.$route, ['name'], stringEmpty())
    },

    // DocumentId (DocType WorkSchedule or ChangesToWS)
    documentId() {
      return get(this.$route, ['params', 'prjDocId'], stringEmpty())
    },
  },

  methods: {
    /**
     * Initializing and loading data for a page
     */
    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(p => this.getEditPageData())
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data
     */
    getEditPageData() {
      if (!this.isEditMode) {
        this.setDefaultValue()
        return Promise.resolve()
      }

      return getEditProjectWorkScheduleById(this.documentId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} document
     */
    setEditPageData(document) {
      set(this, 'editData', document)

      if (this.isEditMode) {
        const selectedDocumentType = find(
          this.documentTypes,
          type => type.code === get(this, ['editData', 'docType'], null),
        )

        const selectedDocumentState = find(
          this.documentStates,
          type => type.code === get(this, ['editData', 'docState'], null),
        )

        set(this, 'selectedDocumentType', selectedDocumentType)
        set(this, 'selectedDocumentState', selectedDocumentState)
      }

      return Promise.resolve()
    },

    setDefaultValue() {
      const selectedDocumentType = find(this.documentTypes, type => type.code === this.docType)
      set(this, 'selectedDocumentType', selectedDocumentType)
      set(this, ['editData', 'docType'], get(selectedDocumentType, 'code', stringEmpty()))
    },

    /**
     * Create or edit data
     */
    onSubmit() {
      if (!this.isEditMode) {
        this.editData.projectId = this.projectId
        createProjectWorkScheduleWorkSchedule(this.editData)
          .then(this.goToPrjDocumentList)
          .catch(this.setPageLoaded)
      } else {
        editProjectWorkSchedule(this.documentId, this.editData)
          .then(this.goToPrjDocumentList)
          .catch(this.setPageLoaded)
      }
    },

    /**
     * Back to unitStaff list page
     * @param {*} data
     */
    goToPrjDocumentList(data) {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjWorkSchedule`,
      }

      this.$router.push(routeParams)
    },

    // #region Event handlers

    onDocumentTypeChanged(docType) {
      set(this, 'selectedDocumentType', docType)
      set(this, ['editData', 'docType'], get(docType, 'code', stringEmpty()))
    },

    onDocumentStateChanged(docState) {
      set(this, 'selectedDocumentState', docState)
      set(this, ['editData', 'docState'], get(docState, 'code', null))
    },

    // #endregion

    getEnums() {
      const enumGroups = ['DocType', 'DocState']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.documentTypes = p.DocType
        this.documentStates = p.DocState
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

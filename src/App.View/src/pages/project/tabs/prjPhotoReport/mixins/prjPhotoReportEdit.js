import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../../../services/enum-api'
import {
  createProjectPhotoReport,
  editProjectPhotoReport,
  getEditProjectPhotoReportById,
} from '../../../../../services/prj-api/prj-photo-report-api'
import { getProjectParticipantEmployeeList } from '../../../../../services/prj-api/project-api'
import { stringEmpty } from '../../../../../utils/function-helpers'

export default {
  data() {
    return {
      prjPhotoReport: {},

      pageReady: false,

      selectedDocType: null,
      selectedDocState: null,
      selectedFixationType: null,
      selectedFixationState: null,
      selectedEmployee: null,

      docTypes: [],
      docStates: [],
      fixationTypes: [],
      fixationStates: [],
      employees: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.prjPhotoReportId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    prjPhotoReportId() {
      return get(this.$route, ['params', 'prjPhotoReportId'], stringEmpty())
    },
  },

  methods: {
    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(this.getParticipantEmployeeByProjectId)
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

      return getEditProjectPhotoReportById(this.prjPhotoReportId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} prjPhotoReport
     */
    setEditPageData(prjPhotoReport) {
      set(this, 'prjPhotoReport', prjPhotoReport)

      if (this.isEditMode) {
        const selectedDocType = find(
          this.docTypes,
          type => type.code === get(this, ['prjPhotoReport', 'docType'], null),
        )

        const selectedDocState = find(
          this.docStates,
          type => type.code === get(this, ['prjPhotoReport', 'docState'], null),
        )

        const selectedFixationType = find(
          this.fixationTypes,
          type => type.code === get(this, ['prjPhotoReport', 'fixationType'], null),
        )

        const selectedFixationState = find(
          this.fixationStates,
          type => type.code === get(this, ['prjPhotoReport', 'fixationState'], null),
        )

        const selectedEmployee = find(
          this.employees,
          type => type.employeeId === get(this, ['prjPhotoReport', 'responsibleEmployeeId'], null),
        )

        set(this, 'selectedDocType', selectedDocType)
        set(this, 'selectedDocState', selectedDocState)
        set(this, 'selectedFixationType', selectedFixationType)
        set(this, 'selectedFixationState', selectedFixationState)
        set(this, 'selectedEmployee', selectedEmployee)
      }

      return Promise.resolve()
    },

    setDefaultValue() {
      const selectedDocType = find(this.docTypes, type => type.code === 'PhotoReport')
      set(this, 'selectedDocType', selectedDocType)
      set(this, ['prjPhotoReport', 'docType'], get(selectedDocType, 'code', stringEmpty()))
    },

    onSubmit() {
      if (!this.isEditMode) {
        this.prjPhotoReport.projectId = this.projectId
        createProjectPhotoReport(this.prjPhotoReport)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editProjectPhotoReport(this.prjPhotoReport.id, this.prjPhotoReport)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(prjPhotoReportId) {
      if (isEmpty(prjPhotoReportId)) {
        this.$router.push(
          `/projects/details/${this.projectId}/prjPhotoReport/details/${this.prjPhotoReportId}`,
        )
      } else {
        this.$router.push(
          `/projects/details/${this.projectId}/prjPhotoReport/details/${prjPhotoReportId}`,
        )
      }
    },

    // #region Event handlers

    onDocTypeChanged(docType) {
      set(this, 'selectedDocType', docType)
      set(this, ['prjPhotoReport', 'docType'], get(docType, 'code', stringEmpty()))
    },

    onDocStateChanged(docState) {
      set(this, 'selectedDocState', docState)
      set(this, ['prjPhotoReport', 'docState'], get(docState, 'code', stringEmpty()))
    },

    onFixationTypeChanged(fixationType) {
      set(this, 'selectedFixationType', fixationType)
      set(this, ['prjPhotoReport', 'fixationType'], get(fixationType, 'code', stringEmpty()))
    },

    onFixationStateChanged(fixationState) {
      set(this, 'selectedFixationState', fixationState)
      set(this, ['prjPhotoReport', 'fixationState'], get(fixationState, 'code', stringEmpty()))
    },

    onEmployeeChanged(employee) {
      set(this, 'selectedEmployee', employee)
      set(
        this,
        ['prjPhotoReport', 'responsibleEmployeeId'],
        get(employee, 'employeeId', stringEmpty()),
      )
    },

    // #endregion

    getEnums() {
      const enumGroups = ['DocType', 'DocState', 'FixationType', 'FixationState']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.docTypes = p.DocType
        this.docStates = p.DocState
        this.fixationTypes = p.FixationType
        this.fixationStates = p.FixationState
      })
    },

    getParticipantEmployeeByProjectId() {
      const param = {
        id: this.projectId,
      }
      return getProjectParticipantEmployeeList(param).then(p => {
        this.employees = p
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

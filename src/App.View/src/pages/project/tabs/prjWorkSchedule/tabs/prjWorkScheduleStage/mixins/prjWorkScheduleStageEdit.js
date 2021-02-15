import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import {
  createProjectWorkScheduleStage,
  editProjectWorkScheduleStage,
  getEditProjectWorkScheduleStageById,
} from '../../../../../../../services/prj-api/prj-work-schedule-stage'

import { stringEmpty } from '../../../../../../../utils/function-helpers'

export default {
  data() {
    return {
      editData: {},

      pageReady: false,
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.stageId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    // DocumentId (DocType WorkSchedule or ChangesToWS)
    documentId() {
      return get(this.$route, ['params', 'prjDocId'], stringEmpty())
    },

    stageId() {
      return get(this.$route, ['params', 'prjDocStageId'], stringEmpty())
    },
  },

  methods: {
    /**
     * Initializing and loading data for a page
     */
    onCreate() {
      this.setPageLoading()
      this.getEditPageData()
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data
     */
    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditProjectWorkScheduleStageById(this.stageId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} stage
     */
    setEditPageData(stage) {
      set(this, 'editData', stage)

      return Promise.resolve()
    },

    /**
     * Create or edit data
     */
    onSubmit() {
      if (!this.isEditMode) {
        this.editData.prjWorkScheduleId = this.documentId
        createProjectWorkScheduleStage(this.editData)
          .then(this.goToPrjStageList)
          .catch(this.setPageLoaded)
      } else {
        editProjectWorkScheduleStage(this.stageId, this.editData)
          .then(this.goToPrjStageList)
          .catch(this.setPageLoaded)
      }
    },

    /**
     * Back to unitStaff list page
     * @param {*} data
     */
    goToPrjStageList(data) {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjWorkSchedule/details/${this.documentId}/stage`,
      }

      this.$router.push(routeParams)
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

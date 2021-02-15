import set from 'lodash.set'
import get from 'lodash.get'
import find from 'lodash.find'
import isEmpty from 'lodash.isempty'

import {
  createProjectWorkScheduleSubType,
  editProjectWorkScheduleSubType,
  getEditProjectWorkScheduleSubTypeById,
} from '../../../../../../../services/prj-api/prj-work-schedule-sub-type'

import { stringEmpty } from '../../../../../../../utils/function-helpers'
import { fetchListEnumByGroup } from '../../../../../../../services/enum-api'
import { fetchProjectWorkScheduleStage } from '../../../../../../../services/prj-api/prj-work-schedule-stage'
import { fetchWorkSubTypeList } from '../../../../../../../services/classifiers/work-sub-type-api'

export default {
  data() {
    return {
      editData: {},

      // WorkScheduleStage
      selectedWSStage: null,
      selectedWorkSubType: null,
      selectedMeasurementUnit: null,

      selectedMeasurementUnitValue: '',

      wsStages: [],
      workSubTypes: [],
      measurementUnits: [],

      pageReady: false,
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.subTypeId)
    },

    projectId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },

    // DocumentId (DocType WorkSchedule or ChangesToWS)
    documentId() {
      return get(this.$route, ['params', 'prjDocId'], stringEmpty())
    },

    subTypeId() {
      return get(this.$route, ['params', 'prjDocSubTypeId'], stringEmpty())
    },
  },

  methods: {
    /**
     * Initializing and loading data for a page
     */
    onCreate() {
      this.setPageLoading()

      this.getEnums()
        .then(this.getWSStages)
        .then(this.getWorkSubType)
        .then(() => this.getEditPageData())
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

      return getEditProjectWorkScheduleSubTypeById(this.subTypeId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} WSSubType
     */
    setEditPageData(WSSubType) {
      set(this, 'editData', WSSubType)

      if (this.isEditMode) {
        const selectedWSStage = find(
          this.wsStages,
          type => type.id === get(this, ['editData', 'prjWorkScheduleStageId'], null),
        )

        const selectedWorkSubType = find(
          this.workSubTypes,
          type => type.id === get(this, ['editData', 'workSubTypeId'], null),
        )

        const selectedMeasurementUnit = find(
          this.measurementUnits,
          type => type.code === get(this, ['editData', 'measurementUnit'], null),
        )
        this.setSelectedMeasurementUnit(selectedWorkSubType)

        set(this, 'selectedWSStage', selectedWSStage)
        set(this, 'selectedWorkSubType', selectedWorkSubType)
        set(this, 'selectedMeasurementUnit', selectedMeasurementUnit)
      }

      return Promise.resolve()
    },

    setSelectedMeasurementUnit(workSubType) {
      const selectedMeasurementUnit = find(
        this.measurementUnits,
        type => type.code === get(workSubType, ['measurementUnit'], stringEmpty()),
      )

      this.onMeasurementUnitChanged(selectedMeasurementUnit)
    },

    /**
     * Create or edit data
     */
    onSubmit() {
      if (!this.isEditMode) {
        this.editData.prjWorkScheduleId = this.documentId
        createProjectWorkScheduleSubType(this.editData)
          .then(this.goToPrjSubTypeList)
          .catch(this.setPageLoaded)
      } else {
        editProjectWorkScheduleSubType(this.subTypeId, this.editData)
          .then(this.goToPrjSubTypeList)
          .catch(this.setPageLoaded)
      }
    },

    /**
     * Back to unitStaff list page
     * @param {*} data
     */
    goToPrjSubTypeList(data) {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjWorkSchedule/details/${this.documentId}/subType`,
      }

      this.$router.push(routeParams)
    },

    // #region Event handlers

    onWSStageChanged(stage) {
      set(this, 'selectedWSStage', stage)
      set(this, ['editData', 'prjWorkScheduleStageId'], get(stage, 'id', stringEmpty()))
    },

    onWorkSubTypeChanged(workSubType) {
      set(this, 'selectedWorkSubType', workSubType)
      set(this, ['editData', 'workSubTypeId'], get(workSubType, 'id', null))

      this.setSelectedMeasurementUnit(workSubType)
    },

    onMeasurementUnitChanged(measurementUnit) {
      set(this, 'selectedMeasurementUnit', measurementUnit)
      set(this, ['editData', 'measurementUnit'], get(measurementUnit, 'code', null))
      set(this, 'selectedMeasurementUnitValue', get(measurementUnit, 'value', null))
    },

    // #endregion

    // #region Load dictionary and enums

    getWSStages() {
      const params = {
        prjWorkScheduleId: this.documentId,
      }
      return fetchProjectWorkScheduleStage(params).then(data => {
        this.wsStages = data
      })
    },

    getWorkSubType() {
      return fetchWorkSubTypeList().then(data => {
        this.workSubTypes = data.map(p => ({ ...p, codeAndName: `${p.code} ${p.name}` }))
      })
    },

    getEnums() {
      const enumGroups = ['MeasurementUnit']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.measurementUnits = p.MeasurementUnit
      })
    },

    // #endregion

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

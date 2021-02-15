import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import {
  fetchWorkSubTypeList,
  createWorkSubType,
  editWorkSubType,
  getEditWorkSubTypeById,
} from '../../../services/classifiers/work-sub-type-api'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      workSubType: {},

      pageReady: false,

      selectedClassifierType: null,
      selectedMeasurementUnit: null,
      selectedParent: null,

      classifierTypes: [],
      measurementUnits: [],
      workSubTypeParents: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.workSubTypeId)
    },

    workSubTypeId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },
  },

  methods: {
    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(this.getWorkSubTypeList)
        .then(this.getEditPageData)
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data (orgUnitStaff)
     */
    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditWorkSubTypeById(this.workSubTypeId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} project
     */
    setEditPageData(workSubType) {
      set(this, 'workSubType', workSubType)

      if (this.isEditMode) {
        const selectedClassifierType = find(
          this.classifierTypes,
          type => type.code === get(this, ['workSubType', 'classifierType'], null),
        )

        const selectedMeasurementUnit = find(
          this.measurementUnits,
          type => type.code === get(this, ['workSubType', 'measurementUnit'], null),
        )

        const selectedParent = find(
          this.workSubTypeParents,
          type => type.id === get(this, ['workSubType', 'parentId'], null),
        )

        set(this, 'selectedClassifierType', selectedClassifierType)
        set(this, 'selectedMeasurementUnit', selectedMeasurementUnit)
        set(this, 'selectedParent', selectedParent)
      }

      return Promise.resolve()
    },

    onSubmit() {
      if (!this.isEditMode) {
        createWorkSubType(this.workSubType)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editWorkSubType(this.workSubType.id, this.workSubType)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(workSubType) {
      if (isEmpty(workSubType)) {
        this.$router.push(`/workSubType/details/${this.workSubTypeId}`)
      } else {
        this.$router.push(`/workSubType/details/${workSubType.id}`)
      }
    },

    // #region Event handlers

    onClassifierTypeChanged(classifierType) {
      set(this, 'selectedClassifierType', classifierType)
      set(this, ['workSubType', 'classifierType'], get(classifierType, 'code', stringEmpty()))
    },

    onMeasurementUnitChanged(measurementUnit) {
      set(this, 'selectedMeasurementUnit', measurementUnit)
      set(this, ['workSubType', 'measurementUnit'], get(measurementUnit, 'code', stringEmpty()))
    },

    onWorkSubTypeParentChanged(workSubTypeParent) {
      set(this, 'selectedParent', workSubTypeParent)
      set(this, ['workSubType', 'parentId'], get(workSubTypeParent, 'id', stringEmpty()))
    },

    // #endregion

    getWorkSubTypeList() {
      return fetchWorkSubTypeList().then(p => {
        this.workSubTypeParents = p.map(z => ({
          ...z,
          codeAndName: `${z.code} ${z.name}`,
        }))
      })
    },

    getEnums() {
      const enumGroups = ['ClassifierType', 'MeasurementUnit']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.classifierTypes = p.ClassifierType
        this.measurementUnits = p.MeasurementUnit.map(z => ({
          ...z,
          nameAndValue: `${z.name} (${z.value})`,
        }))
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

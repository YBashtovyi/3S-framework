import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import {
  createObject,
  editObject,
  getEditObjectById,
} from '../../../services/common-api/construction-object-api'
import { getTypeOfObjectList } from '../../../services/classifiers/constuction-object-ex-property-dict'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      object: {},
      objectId: null,

      pageReady: false,

      selectedClassOfConsequence: null,
      selectedObjectStatus: null,
      selectedTypeOfConstructionObject: null,

      classesOfConsequence: [],
      objectStatuses: [],
      typesOfConstructionObject: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.objectId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.objectId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEnums()
        .then(this.getTypeOfObjectList)
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

      return getEditObjectById(this.objectId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} object
     */
    setEditPageData(object) {
      set(this, 'object', object)

      if (this.isEditMode) {
        const selectedObjectStatus = find(
          this.objectStatuses,
          type => type.code === get(this, ['object', 'objectStatus'], null),
        )

        const selectedClassOfConsequence = find(
          this.classesOfConsequence,
          type => type.code === get(this, ['object', 'classOfConsequence'], null),
        )

        const selectedTypeOfConstructionObject = find(
          this.typesOfConstructionObject,
          type => type.code === get(this, ['object', 'typeOfConstructionObject'], null),
        )

        set(this, 'selectedClassOfConsequence', selectedClassOfConsequence)
        set(this, 'selectedObjectStatus', selectedObjectStatus)
        set(this, 'selectedTypeOfConstructionObject', selectedTypeOfConstructionObject)
      }

      return Promise.resolve()
    },

    onSubmit() {
      if (!this.isEditMode) {
        createObject(this.object)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editObject(this.object.id, this.object)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(object) {
      if (isEmpty(object)) {
        this.$router.push('/objects/details/' + this.objectId)
      } else {
        this.$router.push('/objects/details/' + object.id)
      }
    },

    // #region Event handlers

    onClassOfConsequenceChanged(classOfConsequence) {
      set(this, 'selectedClassOfConsequence', classOfConsequence)
      set(this, ['object', 'classOfConsequence'], get(classOfConsequence, 'code', stringEmpty()))
    },

    onObjectStatusChanged(objectStatus) {
      set(this, 'selectedObjectStatus', objectStatus)
      set(this, ['object', 'objectStatus'], get(objectStatus, 'code', stringEmpty()))
    },

    onTypeOfConstructionObjectChanged(typeOfConstructionObject) {
      set(this, 'selectedTypeOfConstructionObject', typeOfConstructionObject)
      set(
        this,
        ['object', 'typeOfConstructionObject'],
        get(typeOfConstructionObject, 'code', stringEmpty()),
      )
    },

    // #endregion

    getEnums() {
      const enumGroups = ['ClassOfConsequence', 'ObjectStatus']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.classesOfConsequence = p.ClassOfConsequence
        this.objectStatuses = p.ObjectStatus
      })
    },

    getTypeOfObjectList() {
      return getTypeOfObjectList().then(data => {
        this.typesOfConstructionObject = data
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

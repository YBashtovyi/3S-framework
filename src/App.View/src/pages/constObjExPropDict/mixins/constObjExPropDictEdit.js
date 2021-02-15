import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import {
  fetchConstructionObjectExProperty,
  createConstructionObjectExProperty,
  editConstructionObjectExProperty,
  getEditConstructionObjectExPropertyById,
} from '../../../services/classifiers/constuction-object-ex-property-dict'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      exProperty: {},

      pageReady: false,

      selectedDataFormat: null,
      selectedParent: null,

      dataFormats: [],
      exPropParents: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.exPropertyId)
    },

    exPropertyId() {
      return get(this.$route, ['params', 'id'], stringEmpty())
    },
  },

  methods: {
    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(this.getExPropList)
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

      return getEditConstructionObjectExPropertyById(this.exPropertyId).then(this.setEditPageData)
    },

    /**
     * After loading the data, initialize the page
     * @param {*} project
     */
    setEditPageData(exProperty) {
      set(this, 'exProperty', exProperty)

      if (this.isEditMode) {
        const selectedDataFormats = find(
          this.dataFormats,
          type => type.code === get(this, ['exProperty', 'dataFormat'], null),
        )

        const selectedExPropParent = find(
          this.exPropParents,
          type => type.id === get(this, ['exProperty', 'parentId'], null),
        )

        set(this, 'selectedDataFormat', selectedDataFormats)
        set(this, 'selectedParent', selectedExPropParent)
      }

      return Promise.resolve()
    },

    onSubmit() {
      if (!this.isEditMode) {
        createConstructionObjectExProperty(this.exProperty)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editConstructionObjectExProperty(this.exProperty.id, this.exProperty)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(exProp) {
      if (isEmpty(exProp)) {
        this.$router.push(`/constObjExPropDict/details/${this.exPropertyId}`)
      } else {
        this.$router.push(`/constObjExPropDict/details/${exProp.id}`)
      }
    },

    // #region Event handlers

    onDataFormatChanged(dataFormat) {
      set(this, 'selectedDataFormat', dataFormat)
      set(this, ['exProperty', 'dataFormat'], get(dataFormat, 'code', stringEmpty()))
    },

    onExPropParentChanged(exPropParent) {
      set(this, 'selectedParent', exPropParent)
      set(this, ['exProperty', 'parentId'], get(exPropParent, 'id', stringEmpty()))
    },

    // #endregion

    getExPropList() {
      return fetchConstructionObjectExProperty().then(p => {
        this.exPropParents = p
      })
    },

    getEnums() {
      const enumGroups = ['DataFormat']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.dataFormats = p.DataFormat
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

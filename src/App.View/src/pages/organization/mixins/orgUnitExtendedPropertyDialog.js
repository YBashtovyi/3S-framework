import set from 'lodash.set'
import get from 'lodash.get'
import find from 'lodash.find'

import isEmpty from 'lodash.isempty'
import { stringEmpty } from '../../../utils/function-helpers'
import { fetchListEnumByGroup } from '../.././../services/enum-api'
import {
  getExtendedPropertyById,
  editExtendedProperty,
  createExtendedProperty,
} from '../../../services/org-api/organization-api'

export default {
  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
      required: true,
    },
    orgUnitId: {
      type: String,
      required: true,
    },
    orgUnitExtendedPropertyId: {
      type: String,
      required: false,
    },
  },

  data() {
    return {
      editData: {},

      pageReady: false,

      // Enum
      selectedOrgExtendedProperty: null,
      orgExtendedProperties: [],
    }
  },

  watch: {
    isVisibleDialog() {
      if (this.isVisibleDialog) {
        this.editData = {}
        this.selectedOrgExtendedProperty = null

        this.onCreate()
      }
    },
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.orgUnitExtendedPropertyId)
    },
  },

  methods: {
    onSubmit() {
      if (!this.isEditMode) {
        set(this, ['editData', 'orgUnitId'], this.orgUnitId)
        createExtendedProperty(this.editData)
          .then(this.setPageLoaded)
          .then(this.onCloseDialog)
          .catch(this.setPageLoaded)
      } else {
        editExtendedProperty(this.orgUnitExtendedPropertyId, this.editData)
          .then(this.setPageLoaded)
          .then(this.onCloseDialog)
          .catch(this.setPageLoaded)
      }
    },

    onCreate() {
      this.setPageLoading()
      this.getEnums()
        .then(this.getEditPageData)
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    onCloseDialog() {
      this.$emit('closeDialog')
    },

    onSelectOrgExtendedProperty(property) {
      set(this, 'selectedOrgExtendedProperty', property)
      set(this, ['editData', 'orgExtendedProperty'], get(property, 'code', stringEmpty()))
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getExtendedPropertyById(this.orgUnitExtendedPropertyId).then(this.setEditPageData)
    },

    setEditPageData(data) {
      set(this, 'editData', data)
      if (this.isEditMode) {
        const selectedOrgExtendedProperty = find(
          this.orgExtendedProperties,
          prop => prop.code === get(this.editData, 'orgExtendedProperty', null),
        )

        set(this, 'selectedOrgExtendedProperty', selectedOrgExtendedProperty)
      }

      return Promise.resolve()
    },

    getEnums() {
      const enumGroups = ['OrgExtendedProperty']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.orgExtendedProperties = p.OrgExtendedProperty
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
}

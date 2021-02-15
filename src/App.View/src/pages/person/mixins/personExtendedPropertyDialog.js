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
} from '../../../services/common-api/person-api'

export default {
  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
      required: true,
    },
    personId: {
      type: String,
      required: true,
    },
    personExtendedPropertyId: {
      type: String,
      required: false,
    },
  },

  data() {
    return {
      editData: {},

      pageReady: false,

      // Enum
      selectedPersonExtendedProperty: null,
      personExtendedProperties: [],
    }
  },

  watch: {
    isVisibleDialog() {
      if (this.isVisibleDialog) {
        this.editData = {}
        this.selectedPersonExtendedProperty = null

        this.onCreate()
      }
    },
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.personExtendedPropertyId)
    },
  },

  methods: {
    onSubmit() {
      if (!this.isEditMode) {
        set(this, ['editData', 'personId'], this.personId)
        createExtendedProperty(this.editData)
          .then(this.setPageLoaded)
          .then(this.onCloseDialog)
          .catch(this.setPageLoaded)
      } else {
        editExtendedProperty(this.personExtendedPropertyId, this.editData)
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

    onSelectPersonExtendedProperty(property) {
      set(this, 'selectedPersonExtendedProperty', property)
      set(this, ['editData', 'personExtendedProperty'], get(property, 'code', stringEmpty()))
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getExtendedPropertyById(this.personExtendedPropertyId).then(this.setEditPageData)
    },

    setEditPageData(data) {
      set(this, 'editData', data)
      if (this.isEditMode) {
        const selectedPersonExtendedProperty = find(
          this.personExtendedProperties,
          prop => prop.code === get(this.editData, 'personExtendedProperty', null),
        )

        set(this, 'selectedPersonExtendedProperty', selectedPersonExtendedProperty)
      }

      return Promise.resolve()
    },

    getEnums() {
      const enumGroups = ['PersonExtendedProperty']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.personExtendedProperties = p.PersonExtendedProperty
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

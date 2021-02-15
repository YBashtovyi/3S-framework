import get from 'lodash.get'
import set from 'lodash.set'
import find from 'lodash.find'

import { getEditRightById, createRight, editRight } from '../../../../services/adm-api/right-api'
import { fetchListEnumByGroup } from '../../../../services/enum-api'
import { isEmpty, stringEmpty } from '../../../../utils/function-helpers'

export default {
  data() {
    return {
      right: {},
      rightId: null,

      selectedRightType: null,

      rightTypes: [],

      pageReady: false,
    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.rightId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.rightId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEnums()
        .then(this.getEditPageData)
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    goToDetails(right) {
      if (this.isEditMode) {
        this.$router.push('/rights/details/' + this.rightId)
      } else {
        this.$router.push('/rights/details/' + right.id)
      }
    },

    // #region Load and send data to the server

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditRightById(this.rightId).then(this.setEditPageData)
    },

    setEditPageData(right) {
      set(this, 'right', right)

      if (this.isEditMode) {
        const selectedRightType = find(
          this.rightTypes,
          type => type.code === get(this, ['right', 'rightType'], null),
        )

        set(this, 'selectedRightType', selectedRightType)
      }

      return Promise.resolve()
    },

    onSubmit() {
      if (!this.isEditMode) {
        createRight(this.right)
          .then(this.goToDetails)
          .catch(p => console.log(p))
      } else {
        editRight(this.right, this.rightId)
          .then(this.goToDetails)
          .catch(p => console.log(p))
      }
    },

    getEnums() {
      const enumGroups = ['RightType']
      return fetchListEnumByGroup(enumGroups).then(this.setEnums)
    },

    setEnums(enumGroup) {
      this.rightTypes = enumGroup.RightType
      return Promise.resolve()
    },

    // #endregion

    // #region Event handlers

    onRightTypeChanged(rightType) {
      set(this, 'selectedRightType', rightType)
      set(this, ['right', 'rightType'], get(rightType, 'code', stringEmpty()))
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

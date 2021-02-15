import set from 'lodash.set'
import get from 'lodash.get'
import find from 'lodash.find'

import isEmpty from 'lodash.isempty'
import { stringEmpty } from '../../../utils/function-helpers'
import { fetchPositions } from '../.././../services/cdn-api'
import {
  getEditOrgUnitPositionById,
  editOrgUnitPosition,
  createOrgUnitPosition,
} from '../../../services/org-api/org-unit-position-api'
export default {
  props: {
    isVisible: {
      type: Boolean,
      default: false,
      required: true,
    },
    orgUnitId: {
      type: String,
      required: true,
    },
    orgUnitPositionId: {
      type: String,
      required: false,
    },
  },

  data() {
    return {
      editData: {
        staffUnitCount: 0,
      },

      pageReady: false,

      selectedPosition: null,
      positions: [],
    }
  },

  watch: {
    isVisible() {
      if (this.isVisible) {
        this.onCreate()
      }
    },
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.orgUnitPositionId)
    },
  },

  methods: {
    onSubmit() {
      if (!this.isEditMode) {
        set(this, ['editData', 'orgUnitId'], this.orgUnitId)
        createOrgUnitPosition(this.editData)
          .then(data => data.id)
          .then(this.setPageLoaded)
          .then(this.onCloseDialog)
          .catch(this.setPageLoaded)
      } else {
        editOrgUnitPosition(this.editData, this.orgUnitPositionId)
          .then(data => data.id)
          .then(this.setPageLoaded)
          .then(this.onCloseDialog)
          .catch(this.setPageLoaded)
      }
    },

    onCreate() {
      this.setPageLoading()
      this.fetchPositions()
        .then(() => this.getEditPageData())
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    onCloseDialog() {
      this.$emit('closeDialog')
    },

    onSelectPosition(position) {
      set(this, 'selectedPosition', position)
      set(this, ['editData', 'positionId'], get(position, 'id', stringEmpty()))
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditOrgUnitPositionById(this.orgUnitPositionId).then(this.setEditPageData)
    },

    setEditPageData(data) {
      set(this, 'editData', data)
      if (this.isEditMode) {
        const selectedPosition = find(
          this.positions,
          pos => pos.id === get(this.editData, 'positionId', null),
        )

        set(this, 'selectedPosition', selectedPosition)
      }
    },

    fetchPositions() {
      return fetchPositions().then(data => (this.positions = data))
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

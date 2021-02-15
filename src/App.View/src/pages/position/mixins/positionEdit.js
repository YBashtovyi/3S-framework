import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import { createPosition, editPosition, getPositionById } from '../../../services/cdn-api'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      position: {},
      positionId: null,

      pageReady: false,

    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.positionId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.positionId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
       this.getEditPageData()
        .then(this.setPageLoaded)
        .catch(this.handleError)
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getPositionById(this.positionId).then(this.setEditPageData)
    },

    setEditPageData(position) {
      set(this, 'position', position)
    },

    onSubmit() {
      if (!this.isEditMode) {
        createPosition(this.position)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      } else {
        editPosition(this.position, this.positionId)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      }
    },

    goToDetails(id) {
      this.$router.push('/position')
    },

    // #region Event handlers

    /**
     * Callback which is used for handling errors
     * @param {*} error recieved error
     */
    handleError(error) {
      this.setPageLoaded()
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

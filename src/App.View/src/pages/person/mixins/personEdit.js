import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import {
  createPerson,
  editPerson,
  getDetailsPersonById,
} from '../../../services/common-api/person-api'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  data() {
    return {
      /**
       * Stores the model of person from the page
       */
      person: {},
      personId: null,

      /**
       * Object which stores person after that was created
       */
      newPerson: {},
      isDialogMode: false,

      pageReady: false,
    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.personId)
    },
  },

  watch: {
    person: {
      deep: true,
      handler() {
        this.$emit('createPerson', this.person)
      },
    },
  },

  methods: {
    initializeDefaultFields() {
      this.personId = get(this.$route, ['params', 'personId'], stringEmpty())
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

      return getDetailsPersonById(this.personId).then(this.setEditPageData)
    },

    setEditPageData(person) {
      set(this, 'person', person)
    },

    onSubmit() {
      if (!this.isEditMode) {
        createPerson(this.person)
          .then(this.afterSuccessCreate)
          .catch(this.handleError)
      } else {
        editPerson(this.person, this.personId)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      }
    },

    afterSuccessCreate(data) {
      if (this.isVisibleDialog) {
        this.newPerson = data
        this.$emit('addPerson', this.newPerson)
      } else {
        this.goToDetails()
      }
    },

    goToDetails(id) {
      this.$router.push('/person')
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

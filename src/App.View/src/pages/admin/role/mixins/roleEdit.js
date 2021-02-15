import get from 'lodash.get'
import set from 'lodash.set'

import { getEditRoleById, createRole, editRole } from '../../../../services/adm-api/role-api'
import { isEmpty, stringEmpty } from '../../../../utils/function-helpers'

export default {
  data() {
    return {
      role: {},
      roleId: null,

      pageReady: false,
    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.roleId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.roleId = get(this.$route, ['params', 'id'], stringEmpty())
      if (!this.isEditMode) {
        this.role.isActive = true
      }
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEditPageData()
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditRoleById(this.roleId).then(this.setEditPageData)
    },

    setEditPageData(role) {
      set(this, 'role', role)
    },

    onSubmit() {
      if (!this.isEditMode) {
        createRole(this.role)
          .then(this.goToDetails)
          .catch(p => console.log(p))
      } else {
        editRole(this.role, this.roleId)
          .then(this.goToDetails)
          .catch(p => console.log(p))
      }
    },

    goToDetails(role) {
      if (this.isEditMode) {
        this.$router.push('/roles/details/' + this.roleId)
      } else {
        this.$router.push('/roles/details/' + role.id)
      }
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

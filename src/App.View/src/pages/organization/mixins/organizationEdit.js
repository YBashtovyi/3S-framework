import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import { createOrganization, editOrganization, getEditOrganizationById } from '../../../services/org-api/organization-api'
import { stringEmpty } from '../../../utils/function-helpers'
import find from 'lodash.find'

export default {
  data() {
    return {
      organization: {},
      organizationId: null,

      pageReady: false,

      selectedOrgType: null,
      selectedOrgState: null,
      selectedOrganizationCategory: null,

      orgTypes: [],
      orgStates: [],
      organizationCategories: [],
    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.organizationId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.organizationId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEnums()
        .then(() => this.getEditPageData())
        .then(this.setPageLoaded)
        .catch(this.handleError)
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditOrganizationById(this.organizationId).then(this.setEditPageData)
    },

    setEditPageData(organization) {
      set(this, 'organization', organization)

      if (this.isEditMode) {
        const selectedOrgType = find(this.orgTypes, type => type.code === get(this, ['organization', 'orgType'], null))
        const selectedOrgState = find(this.orgStates, type => type.code === get(this, ['organization', 'orgState'], null))
        const selectedOrgCategory = find(
          this.organizationCategories,
          type => type.code === get(this, ['organization', 'organizationCategory'], null),
        )

        set(this, 'selectedOrgType', selectedOrgType)
        set(this, 'selectedOrgState', selectedOrgState)
        set(this, 'selectedOrganizationCategory', selectedOrgCategory)
      }
    },

    onSubmit() {
      if (!this.isEditMode) {
        createOrganization(this.organization)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      } else {
        editOrganization(this.organization, this.organizationId)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      }
    },

    goToDetails(id) {
      this.$router.push('/organization')
    },

    // #region Event handlers

    onOrgTypeChanged(orgType) {
      set(this, 'selectedOrgType', orgType)
      set(this, ['organization', 'orgType'], get(orgType, 'code', stringEmpty()))
    },

    onOrgStateChanged(orgState) {
      set(this, 'selectedOrgState', orgState)
      set(this, ['organization', 'orgState'], get(orgState, 'code', stringEmpty()))
    },

    onOrganizationCategory(organizationCategory) {
      set(this, 'selectedOrganizationCategory', organizationCategory)
      set(this, ['organization', 'organizationCategory'], get(organizationCategory, 'code', stringEmpty()))
    },

    /**
     * Callback which is used for handling errors
     * @param {*} error recieved error
     */
    handleError(error) {
      this.setPageLoaded()
    },

    // #endregion

    getEnums() {
      const enumGroups = ['OrgType', 'OrgState', 'OrganizationCategory']

      return fetchListEnumByGroup(enumGroups).then(p => {
        this.orgTypes = p.OrgType
        this.orgStates = p.OrgState
        this.organizationCategories = p.OrganizationCategory
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

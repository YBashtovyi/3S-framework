import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import {
  createDepartment,
  editDepartment,
  getEditDepartmentById,
} from '../../../services/org-api/department-api'
import { fetchOrganiationList } from '../../../services/org-api/organization-api'

import { stringEmpty } from '../../../utils/function-helpers'
import find from 'lodash.find'

export default {
  data() {
    return {
      department: {},
      departmentId: null,

      pageReady: false,

      selectedDepartmentType: null,
      selectedDepartmentState: null,
      selectedOrganization: null,

      departmentTypes: [],
      departmentStates: [],
      organizationList: [],
    }
  },

  computed: {
    isEditMode() {
      return !isEmpty(this.departmentId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.departmentId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEnums()
        .then(this.getOrganization)
        .then(() => this.getEditPageData())
        .then(this.setPageLoaded)
        .catch(this.handleError)
    },

    getEditPageData() {
      if (!this.isEditMode) {
        return Promise.resolve()
      }

      return getEditDepartmentById(this.departmentId).then(this.setEditPageData)
    },

    setEditPageData(department) {
      set(this, 'department', department)

      if (this.isEditMode) {
        const selectedDepartmentType = find(
          this.departmentTypes,
          type => type.code === get(this, ['department', 'departmentType'], null),
        )

        const selectedDepartmentState = find(
          this.departmentStates,
          type => type.code === get(this, ['department', 'departmentState'], null),
        )

        const selectedOrganization = find(
          this.organizationList,
          type => type.id === get(this, ['department', 'parentId'], null),
        )

        set(this, 'selectedDepartmentType', selectedDepartmentType)
        set(this, 'selectedDepartmentState', selectedDepartmentState)
        set(this, 'selectedOrganization', selectedOrganization)
      }
    },

    onSubmit() {
      if (!this.isEditMode) {
        createDepartment(this.department)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      } else {
        editDepartment(this.department, this.departmentId)
          .then(data => data.id)
          .then(this.goToDetails)
          .catch(this.handleError)
      }
    },

    goToDetails(id) {
      this.$router.push('/department')
    },

    // #region Event handlers

    onDepartmentTypeChanged(departmentType) {
      set(this, 'selectedDepartmentType', departmentType)
      set(this, ['department', 'departmentType'], get(departmentType, 'code', stringEmpty()))
    },

    onDepartmentStateChanged(departmentState) {
      set(this, 'selectedDepartmentState', departmentState)
      set(this, ['department', 'departmentState'], get(departmentState, 'code', stringEmpty()))
    },

    onOrgUnitChanged(organization) {
      set(this, 'selectedOrganization', organization)
      set(this, ['department', 'parentId'], get(organization, 'id', stringEmpty()))
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
      const enumGroups = ['DepartmentType', 'DepartmentState']

      return fetchListEnumByGroup(enumGroups).then(p => {
        this.departmentTypes = p.DepartmentType
        this.departmentStates = p.DepartmentState
      })
    },

    getOrganization() {
      return fetchOrganiationList().then(organizations => {
        this.organizationList = organizations
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

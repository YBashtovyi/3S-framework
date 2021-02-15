import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import {
  createOrgUnitStaff,
  editOrgUnitStaff,
  getEditOrgUnitStaffById,
} from '../../../../../services/org-api/org-unit-staff-api'

import { fetchOrgUnitPosition } from '../../../../../services/org-api/org-unit-position-api'

import { createOrgEmployee } from '../../../../../services/org-api/org-employee-api'

import { stringEmpty } from '../../../../../utils/function-helpers'
import { env } from '../../../../../services/api'
import UrlBuilder from '../../../../../utils/url-builder'
import find from 'lodash.find'

export default {
  data() {
    return {
      editData: {},
      organizationId: null,
      orgUnitStaffId: null,

      pageReady: false,

      selectedOrgUnitPosition: null,
      selectedOrgEmployee: null,

      orgUnitPositions: [],
      orgEmployees: [],

      isVisibleCreatePersonDialog: false,
      person: {},
    }
  },

  watch: {
    person() {
      createOrgEmployee({ personId: this.person.id }).then(data =>
        this.onOrgEmployeeChanged({ id: data.id, personFullName: this.person.caption }),
      )
    },
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.orgUnitStaffId)
    },

    /**
     * Employee List Link
     */
    orgEmployeeRequest() {
      return new UrlBuilder({ host: env.ORG_EMPLOYEE.PATH }).build()
    },
  },

  methods: {
    initializeDefaultFields() {
      this.organizationId = get(this.$route, ['params', 'id'], stringEmpty())
      this.orgUnitStaffId = get(this.$route, ['params', 'orgUnitStaffId'], stringEmpty())
    },

    /**
     * Initializing and loading data for a page
     */
    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.fetchOrgUnitPosition()
        .then(() => this.getEditPageData())
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

      return getEditOrgUnitStaffById(this.orgUnitStaffId).then(this.setEditPageData)
    },

    /**
     * Fetch orgUnitPosition list
     */
    fetchOrgUnitPosition() {
      return fetchOrgUnitPosition(this.organizationId, {}).then(
        data => (this.orgUnitPositions = data),
      )
    },

    /**
     * After loading the data, initialize the page
     * @param {*} orgUnitStaff 
     */
    setEditPageData(orgUnitStaff) {
      set(this, 'editData', orgUnitStaff)

      if (this.isEditMode) {
        const selectedOrgUnitPosition = find(
          this.orgUnitPositions,
          type => type.id === get(this, ['editData', 'orgUnitPositionId'], null),
        )

        const selectedOrgEmployee = {
          id: get(this, ['editData', 'employeeId'], null),
          personFullName: get(this, ['editData', 'personFullName'], null),
        }

        if (isEmpty(this.editData.endDateFront)) {
          this.editData.endDate = null
        }

        set(this, 'selectedOrgUnitPosition', selectedOrgUnitPosition)
        set(this, 'selectedOrgEmployee', selectedOrgEmployee)
      }
    },

    /**
     * Create or edit data
     */
    onSubmit() {
      if (!this.isEditMode) {
        createOrgUnitStaff(this.editData)
          .then(this.goToOrgUnitStaffList)
          .catch(this.setPageLoaded)
      } else {
        editOrgUnitStaff(this.editData, this.orgUnitStaffId)
          .then(this.goToOrgUnitStaffList)
          .catch(this.setPageLoaded)
      }
    },

    /**
     * Show persona creation window
     */
    onCreatePerson() {
      this.isVisibleCreatePersonDialog = true
    },

    /**
     * Back to unitStaff list page
     * @param {*} data 
     */
    goToOrgUnitStaffList(data) {
      const routeParams = {
        path: `/organization/details/${this.organizationId}/orgUnitStaff`,
      }

      this.$router.push(routeParams)
    },

    // #region Event handlers

    onOrgPositionChanged(orgPosition) {
      set(this, 'selectedOrgUnitPosition', orgPosition)
      set(this, ['editData', 'orgUnitPositionId'], get(orgPosition, 'id', stringEmpty()))
    },

    onOrgEmployeeChanged(orgEmployee) {
      set(this, 'selectedOrgEmployee', orgEmployee)
      set(this, ['editData', 'employeeId'], get(orgEmployee, 'id', stringEmpty()))
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

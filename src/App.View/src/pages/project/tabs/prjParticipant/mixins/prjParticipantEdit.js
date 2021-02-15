import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'

import {
  createPrjParticipant,
  editPrjParticipant,
  getEditPrjParticipantById,
} from '../../../../../services/prj-api/prj-participant-api'

import { getDetailsProjectById } from '../../../../../services/prj-api/project-api'

import { fetchListEnumByGroup } from '../../../../../services/enum-api'
import { fetchOrganiationList } from '../../../../../services/org-api/organization-api'

import { createOrgEmployee } from '../../../../../services/org-api/org-employee-api'

import { stringEmpty } from '../../../../../utils/function-helpers'
import { env } from '../../../../../services/api'
import UrlBuilder from '../../../../../utils/url-builder'
import find from 'lodash.find'

export default {
  data() {
    return {
      editData: {},
      projectData: {},

      projectId: null,
      prgParticipantId: null,

      pageReady: false,

      selectedProjectRole: null,
      selectedOrganization: null,
      selectedResponsiblePerson: null,

      projectRoles: [],
      organizations: [],
      responsiblePersons: [],

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
      return !isEmpty(this.prgParticipantId)
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
      this.projectId = get(this.$route, ['params', 'id'], stringEmpty())
      this.prgParticipantId = get(this.$route, ['params', 'prjParticipantId'], stringEmpty())
    },

    /**
     * Initializing and loading data for a page
     */
    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.fetchOrganiationList()
        .then(this.getEnums)
        .then(() => this.getEditPageData())
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data (orgUnitStaff)
     */
    getEditPageData() {
      if (!this.isEditMode) {
        return getDetailsProjectById(this.projectId).then(this.setProjectData)
      }

      return getEditPrjParticipantById(this.prgParticipantId)
        .then(this.setEditPageData)
        .then(() => getDetailsProjectById(this.projectId))
        .then(this.setProjectData)
    },

    /**
     * Fetch orgUnitPosition list
     */
    fetchOrganiationList() {
      return fetchOrganiationList().then(data => (this.organizations = data))
    },

    /**
     * After loading the data, initialize the page
     * @param {*} prjParticipant
     */
    setEditPageData(prjParticipant) {
      set(this, 'editData', prjParticipant)

      if (this.isEditMode) {
        const selectedProjectRole = find(
          this.projectRoles,
          type => type.code === get(this, ['editData', 'projectRole'], null),
        )

        const selectedOrganization = find(
          this.organizations,
          type => type.id === get(this, ['editData', 'participantId'], null),
        )

        const selectedResponsiblePerson = {
          id: get(this, ['editData', 'responsiblePersonId'], null),
          personFullName: get(this, ['editData', 'responsiblePersonFullName'], null),
        }

        set(this, 'selectedProjectRole', selectedProjectRole)
        set(this, 'selectedOrganization', selectedOrganization)
        set(this, 'selectedResponsiblePerson', selectedResponsiblePerson)
      }

      return Promise.resolve()
    },

    setProjectData(data) {
      this.projectData = data

      set(this, ['editData', 'projectId'], data.id)
      return Promise.resolve()
    },

    /**
     * Create or edit data
     */
    onSubmit() {
      if (!this.isEditMode) {
        createPrjParticipant(this.editData)
          .then(this.goToPrgParticipantList)
          .catch(this.setPageLoaded)
      } else {
        editPrjParticipant(this.editData, this.orgUnitStaffId)
          .then(this.goToPrgParticipantList)
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
    goToPrgParticipantList(data) {
      const routeParams = {
        path: `/projects/details/${this.projectId}/prjParticipant`,
      }

      this.$router.push(routeParams)
    },

    // #region Event handlers

    onProjectRoleChanged(projectRole) {
      set(this, 'selectedProjectRole', projectRole)
      set(this, ['editData', 'projectRole'], get(projectRole, 'code', stringEmpty()))
    },

    onOrganizationChanged(organization) {
      set(this, 'selectedOrganization', organization)
      set(this, ['editData', 'participantId'], get(organization, 'id', null))
    },

    onResponsiblePersonChanged(orgEmployee) {
      set(this, 'selectedResponsiblePerson', orgEmployee)
      set(this, ['editData', 'responsiblePersonId'], get(orgEmployee, 'id', null))
    },

    // #endregion

    getEnums() {
      const enumGroups = ['ProjectRole']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.projectRoles = p.ProjectRole
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

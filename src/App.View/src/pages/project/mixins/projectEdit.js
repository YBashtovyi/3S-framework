import set from 'lodash.set'
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import find from 'lodash.find'

import { fetchListEnumByGroup } from '../../../services/enum-api'
import { fetchRegionList } from '../../../services/atu-api'
import { fetchConstructionObjectList } from '../../../services/common-api/construction-object-api'
import {
  createProject,
  editProject,
  getEditProjectById,
  getTypeOfProjectWorkList,
} from '../../../services/prj-api/project-api'
import { stringEmpty } from '../../../utils/function-helpers'
import { PROJECT_STATUSES_OBJECT } from '../../../constants/general/projectStatuses'

export default {
  data() {
    return {
      project: {},
      projectId: null,

      pageReady: false,

      selectedRegion: null,
      selectedDistrict: null,
      selectedConstructionObject: null,
      selectedProjectStatus: null,
      selectedTypeOfFinancing: null,
      selectedProjectImplementationState: null,
      selectedTypeOfProjectWork: null,

      regions: [],
      districts: [],
      constructionObjects: [],
      projectStatuses: [],
      typesOfFinancing: [],
      projectImplementationStates: [],
      typesOfProjectWork: [],
    }
  },

  computed: {
    /**
     * The page is editing and creating
     */
    isEditMode() {
      return !isEmpty(this.projectId)
    },
  },

  methods: {
    initializeDefaultFields() {
      this.projectId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    onCreate() {
      this.initializeDefaultFields()
      this.setPageLoading()
      this.getEnums()
        .then(this.getTypeOfProjectWorkList)
        .then(fetchRegionList)
        .then(this.setRegionData)
        .then(fetchConstructionObjectList)
        .then(this.setConstructionObjectData)
        .then(this.getEditPageData)
        .then(this.setPageLoaded)
        .catch(this.setPageLoaded)
    },

    /**
     * Loading edit data (orgUnitStaff)
     */
    getEditPageData() {
      if (!this.isEditMode) {
        this.setCreateDefaultData()
        return Promise.resolve()
      }

      return getEditProjectById(this.projectId).then(this.setEditPageData)
    },

    setCreateDefaultData() {
      const selectedProjectStatus = find(
        this.projectStatuses,
        type => type.code === PROJECT_STATUSES_OBJECT.DRAFT.code,
      )
      set(this, 'selectedProjectStatus', selectedProjectStatus)
      set(this, ['project', 'projectStatus'], get(selectedProjectStatus, 'code', stringEmpty()))
    },

    /**
     * After loading the data, initialize the page
     * @param {*} project
     */
    setEditPageData(project) {
      set(this, 'project', project)

      if (this.isEditMode) {
        const selectedRegion = find(
          this.regions,
          type => type.id === get(this, ['project', 'regionId'], null),
        )

        const selectedDistrict = find(
          this.districts,
          type => type.id === get(this, ['project', 'districtId'], null),
        )

        const selectedConstructionObject = find(
          this.constructionObjects,
          type => type.id === get(this, ['project', 'constructionObjectId'], null),
        )

        const selectedProjectStatus = find(
          this.projectStatuses,
          type => type.code === get(this, ['project', 'projectStatus'], null),
        )

        const selectedTypeOfFinancing = find(
          this.typesOfFinancing,
          type => type.code === get(this, ['project', 'typeOfFinancing'], null),
        )

        const selectedProjectImplementationState = find(
          this.projectImplementationStates,
          type => type.code === get(this, ['project', 'projectImplementationState'], null),
        )

        const selectedTypeOfProjectWork = find(
          this.typesOfProjectWork,
          type => type.id === get(this, ['project', 'typeOfProjectWorkId'], null),
        )

        set(this, 'selectedRegion', selectedRegion)
        set(this, 'selectedDistrict', selectedDistrict)
        set(this, 'selectedConstructionObject', selectedConstructionObject)
        set(this, 'selectedProjectStatus', selectedProjectStatus)
        set(this, 'selectedTypeOfFinancing', selectedTypeOfFinancing)
        set(this, 'selectedProjectImplementationState', selectedProjectImplementationState)
        set(this, 'selectedTypeOfProjectWork', selectedTypeOfProjectWork)
      }

      return Promise.resolve()
    },

    setRegionData(regions) {
      this.regions = regions

      return Promise.resolve()
    },

    setConstructionObjectData(objects) {
      this.constructionObjects = objects.map(p => {
        return {
          ...p,
          codeAndName: `${p.code} - ${p.name}`,
        }
      })

      return Promise.resolve()
    },

    onSubmit() {
      if (!this.isEditMode) {
        createProject(this.project)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      } else {
        editProject(this.project.id, this.project)
          .then(this.goToDetails)
          .catch(this.setPageLoaded)
      }
    },

    goToDetails(project) {
      if (isEmpty(project)) {
        this.$router.push('/projects/details/' + this.projectId)
      } else {
        this.$router.push('/projects/details/' + project.id)
      }
    },

    // #region Event handlers

    onProjectRegionChanged(region) {
      set(this, 'selectedRegion', region)
      set(this, ['project', 'regionId'], get(region, 'id', stringEmpty()))
    },

    onProjectDistrictChanged(district) {
      set(this, 'selectedDistrict', district)
      set(this, ['project', 'district'], get(district, 'id', stringEmpty()))
    },

    onProjectConstructionObjectChanged(constructionObject) {
      set(this, 'selectedConstructionObject', constructionObject)
      set(this, ['project', 'constructionObjectId'], get(constructionObject, 'id', stringEmpty()))
    },

    onProjectProjectStatusChanged(projectStatus) {
      set(this, 'selectedProjectStatus', projectStatus)
      set(this, ['project', 'projectStatus'], get(projectStatus, 'code', stringEmpty()))
    },

    onProjectTypeOfFinancingChanged(typeOfFinancing) {
      set(this, 'selectedTypeOfFinancing', typeOfFinancing)
      set(this, ['project', 'typeOfFinancing'], get(typeOfFinancing, 'code', stringEmpty()))
    },

    onProjectImplementationStateChanged(projectImplementationState) {
      set(this, 'selectedProjectImplementationState', projectImplementationState)
      set(
        this,
        ['project', 'ProjectImplementationState'],
        get(projectImplementationState, 'code', stringEmpty()),
      )
    },

    onProjectTypeOfProjectWorkChanged(typeOfProjectWork) {
      set(this, 'selectedTypeOfProjectWork', typeOfProjectWork)
      set(this, ['project', 'typeOfProjectWorkId'], get(typeOfProjectWork, 'id', stringEmpty()))
    },

    // #endregion

    getEnums() {
      const enumGroups = ['ProjectStatus', 'TypeOfFinancing', 'ProjectImplementationState']
      return fetchListEnumByGroup(enumGroups).then(p => {
        this.projectStatuses = p.ProjectStatus
        this.typesOfFinancing = p.TypeOfFinancing
        this.projectImplementationStates = p.ProjectImplementationState
      })
    },

    getTypeOfProjectWorkList() {
      return getTypeOfProjectWorkList().then(p => {
        this.typesOfProjectWork = p.map(z => ({ ...z, codeName: `${z.code} ${z.name}` }))
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

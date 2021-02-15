import { PROJECT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData } from '../api'

export const createProject = project => {
  const url = new UrlBuilder({ host: PROJECT.PATH }).build()

  return postData(url, project).then(response => response.data)
}

export const editProject = (id, project) => {
  const url = new UrlBuilder({ host: PROJECT.PATH }).path(id).build()

  return putData(url, project).then(response => response.data)
}

export const getDetailsProjectById = id => {
  const url = new UrlBuilder({ host: PROJECT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectById = id => {
  const url = new UrlBuilder({ host: PROJECT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getTypeOfProjectWorkList = () => {
  const url = new UrlBuilder({ host: PROJECT.TYPE_OF_PROJECT_WORK_PATH }).build()

  return getData(url).then(response => response.data)
}

export const getProjectParticipantEmployeeList = param => {
  const url = new UrlBuilder({ host: PROJECT.PARTICIPANT_EMPLOYEE_PATH })

  if (param.id) {
    url.param('id', param.id)
  }

  return getData(url).then(response => response.data)
}

export const getMapCoordinates = prjId => {
  const url = new UrlBuilder({ host: PROJECT.ATU_COORDINATES.GET }).path(prjId).build()

  return getData(url).then(response => response.data)
}

import { PROJECT_PHOTO_REPORT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const createProjectPhotoReport = projectPhotoReport => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.PATH }).build()

  return postData(url, projectPhotoReport).then(response => response.data)
}

export const editProjectPhotoReport = (id, projectPhotoReport) => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.PATH }).path(id).build()

  return putData(url, projectPhotoReport).then(response => response.data)
}

export const getDetailsProjectPhotoReportById = id => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectPhotoReportById = id => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectPhotoReport = id => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

export const getMapCoordinates = id => {
  const url = new UrlBuilder({ host: PROJECT_PHOTO_REPORT.ATU_COORDINATES.GET }).path(id).build()

  return getData(url).then(response => response.data)
}

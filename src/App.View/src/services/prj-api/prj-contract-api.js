import { PROJECT_CONTRACT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const createProjectContract = projectContract => {
  const url = new UrlBuilder({ host: PROJECT_CONTRACT.PATH }).build()

  return postData(url, projectContract).then(response => response.data)
}

export const editProjectContract = (id, projectContract) => {
  const url = new UrlBuilder({ host: PROJECT_CONTRACT.PATH }).path(id).build()

  return putData(url, projectContract).then(response => response.data)
}

export const getDetailsProjectContractById = id => {
  const url = new UrlBuilder({ host: PROJECT_CONTRACT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectContractById = id => {
  const url = new UrlBuilder({ host: PROJECT_CONTRACT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectContract = id => {
  const url = new UrlBuilder({ host: PROJECT_CONTRACT.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

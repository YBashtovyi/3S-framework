import { PROJECT_ADDITIONAL_AGREEMENT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const createProjectAdditionalAgreement = projectAdditionalAgreement => {
  const url = new UrlBuilder({ host: PROJECT_ADDITIONAL_AGREEMENT.PATH }).build()

  return postData(url, projectAdditionalAgreement).then(response => response.data)
}

export const editProjectAdditionalAgreement = (id, projectAdditionalAgreement) => {
  const url = new UrlBuilder({ host: PROJECT_ADDITIONAL_AGREEMENT.PATH }).path(id).build()

  return putData(url, projectAdditionalAgreement).then(response => response.data)
}

export const getDetailsProjectAdditionalAgreementById = id => {
  const url = new UrlBuilder({ host: PROJECT_ADDITIONAL_AGREEMENT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectAdditionalAgreementById = id => {
  const url = new UrlBuilder({ host: PROJECT_ADDITIONAL_AGREEMENT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectAdditionalAgreement = id => {
  const url = new UrlBuilder({ host: PROJECT_ADDITIONAL_AGREEMENT.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

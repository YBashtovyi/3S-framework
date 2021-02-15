import { ORGANIZATION } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData, deleteData } from '../api'

export const fetchOrganiationList = () => {
  const url = new UrlBuilder({ host: ORGANIZATION.PATH }).build()

  return getData(url).then(response => response.data)
}

export const createOrganization = organization => {
  const url = new UrlBuilder({ host: ORGANIZATION.PATH }).build()

  return postData(url, organization).then(response => response.data)
}

export const editOrganization = (organization, id) => {
  const url = new UrlBuilder({ host: ORGANIZATION.PATH }).path(id).build()

  return putData(url, organization).then(response => response.data)
}

export const getDetailsOrganizationById = id => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditOrganizationById = id => {
  const url = new UrlBuilder({ host: ORGANIZATION.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

// #region ExtendedProperty

export const getExtendedPropertyList = (orgId, param) => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXTENDED_PROPERTY.PATH }).path(orgId).build()
  // TODO: if need param - add
  return getData(url).then(response => response.data)
}

export const getExtendedPropertyById = id => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXTENDED_PROPERTY.GET_BY_ID }).path(id).build()

  return getData(url).then(response => response.data)
}

export const createExtendedProperty = prop => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXTENDED_PROPERTY.ADD }).build()

  return postData(url, prop).then(response => response.data)
}

export const editExtendedProperty = (propId, prop) => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXTENDED_PROPERTY.EDIT }).path(propId).build()

  return putData(url, prop).then(response => response.data)
}

export const deleteExtendedProperty = propId => {
  const url = new UrlBuilder({ host: ORGANIZATION.EXTENDED_PROPERTY.DELETE }).path(propId).build()

  return deleteData(url).then(response => response.data)
}

// #endregion

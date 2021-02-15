import { PERSON } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData, deleteData } from '../api'

export const createPerson = person => {
  const url = new UrlBuilder({ host: PERSON.PATH }).build()

  return postData(url, person).then(response => response.data)
}

export const editPerson = (person, id) => {
  const url = new UrlBuilder({ host: PERSON.PATH }).path(id).build()

  return putData(url, person).then(response => response.data)
}

export const getDetailsPersonById = id => {
  const url = new UrlBuilder({ host: PERSON.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

// #region ExtendedProperty

export const getExtendedPropertyList = (personId, param) => {
  const url = new UrlBuilder({ host: PERSON.EXTENDED_PROPERTY.PATH }).path(personId).build()
  // TODO: if need param - add
  return getData(url).then(response => response.data)
}

export const getExtendedPropertyById = id => {
  const url = new UrlBuilder({ host: PERSON.EXTENDED_PROPERTY.GET_BY_ID }).path(id).build()

  return getData(url).then(response => response.data)
}

export const createExtendedProperty = prop => {
  const url = new UrlBuilder({ host: PERSON.EXTENDED_PROPERTY.ADD }).build()

  return postData(url, prop).then(response => response.data)
}

export const editExtendedProperty = (propId, prop) => {
  const url = new UrlBuilder({ host: PERSON.EXTENDED_PROPERTY.EDIT }).path(propId).build()

  return putData(url, prop).then(response => response.data)
}

export const deleteExtendedProperty = propId => {
  const url = new UrlBuilder({ host: PERSON.EXTENDED_PROPERTY.DELETE }).path(propId).build()

  return deleteData(url).then(response => response.data)
}

// #endregion

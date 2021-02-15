import { DEPARTMENT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData } from '../api'

export const fetchDepartmentList = () => {
  const url = new UrlBuilder({ host: DEPARTMENT.PATH }).build()

  return getData(url).then(response => response.data)
}

export const createDepartment = department => {
  const url = new UrlBuilder({ host: DEPARTMENT.PATH }).build()

  return postData(url, department).then(response => response.data)
}

export const editDepartment = (department, id) => {
  const url = new UrlBuilder({ host: DEPARTMENT.PATH }).path(id).build()

  return putData(url, department).then(response => response.data)
}

export const getDetailsDepartmentById = id => {
  const url = new UrlBuilder({ host: DEPARTMENT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditDepartmentById = id => {
  const url = new UrlBuilder({ host: DEPARTMENT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

// #region ExtendedProperty

// export const getExtendedPropertyList = (orgId, param) => {
//   const url = new UrlBuilder({ host: DEPARTMENT.EXTENDED_PROPERTY.PATH }).path(orgId).build()
//   // TODO: if need param - add
//   return getData(url).then(response => response.data)
// }

// export const getExtendedPropertyById = id => {
//   const url = new UrlBuilder({ host: DEPARTMENT.EXTENDED_PROPERTY.GET_BY_ID }).path(id).build()

//   return getData(url).then(response => response.data)
// }

// export const createExtendedProperty = prop => {
//   const url = new UrlBuilder({ host: DEPARTMENT.EXTENDED_PROPERTY.ADD }).build()

//   return postData(url, prop).then(response => response.data)
// }

// export const editExtendedProperty = (propId, prop) => {
//   const url = new UrlBuilder({ host: DEPARTMENT.EXTENDED_PROPERTY.EDIT }).path(propId).build()

//   return putData(url, prop).then(response => response.data)
// }

// export const deleteExtendedProperty = propId => {
//   const url = new UrlBuilder({ host: DEPARTMENT.EXTENDED_PROPERTY.DELETE }).path(propId).build()

//   return deleteData(url).then(response => response.data)
// }

// #endregion

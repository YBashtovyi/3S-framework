import { ORG_UNIT_STAFF } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData, deleteData } from '../api'

export const createOrgUnitStaff = orgUnitStaff => {
  const url = new UrlBuilder({ host: ORG_UNIT_STAFF.PATH }).build()

  return postData(url, orgUnitStaff).then(response => response.data)
}

export const editOrgUnitStaff = (orgUnitStaff, id) => {
  const url = new UrlBuilder({ host: ORG_UNIT_STAFF.PATH }).path(id).build()

  return putData(url, orgUnitStaff).then(response => response.data)
}

export const deleteOrgUnitStaff = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_STAFF.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

export const getDetailsOrgUnitStaffById = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_STAFF.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditOrgUnitStaffById = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_STAFF.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

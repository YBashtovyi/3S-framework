import isEmpty from 'lodash.isempty'
import { ORG_UNIT_POSITION } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData, deleteData } from '../api'

export const fetchOrgUnitPosition = (orgUnitId, pagination) => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.PATH })

  if (isEmpty(orgUnitId)) {
    return
  } else {
    url.param('orgUnitId', orgUnitId)
  }

  if (pagination.page) {
    url.param('pageNumber', pagination.page)
  }
  if (pagination.rowsPerPage) {
    url.param('pageSize', pagination.rowsPerPage)
  }

  return getData(url).then(response => response.data)
}

export const createOrgUnitPosition = orgUnitPosition => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.PATH }).build()

  return postData(url, orgUnitPosition).then(response => response.data)
}

export const editOrgUnitPosition = (orgUnitPosition, id) => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.PATH }).path(id).build()

  return putData(url, orgUnitPosition).then(response => response.data)
}

export const getOrgUnitPositionById = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditOrgUnitPositionById = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteOrgUnitPosition = id => {
  const url = new UrlBuilder({ host: ORG_UNIT_POSITION.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

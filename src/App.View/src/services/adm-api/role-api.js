import { ROLE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { getData, putData, postData, deleteData } from '../api'

// #region ROLE CRUD

export const fetchRoles = () => {
  const url = new UrlBuilder({ host: ROLE.PATH }).build()

  return getData(url).then(response => response.data)
}

export const getDetailsRoleById = id => {
  const url = new UrlBuilder({ host: ROLE.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditRoleById = id => {
  const url = new UrlBuilder({ host: ROLE.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const createRole = role => {
  const url = new UrlBuilder({ host: ROLE.PATH }).build()

  return postData(url, role).then(response => response.data)
}

export const editRole = (role, id) => {
  const url = new UrlBuilder({ host: ROLE.PATH }).path(id).build()

  return putData(url, role).then(response => response.data)
}

// #endregion

// #region ROLE RIGHT

export const getRoleRight = roleId => {
  const url = new UrlBuilder({ host: ROLE.RIGHT.ROLE_RIGHT_PATH }).path(roleId).build()

  return getData(url).then(response => response.data)
}

export const createRoleRight = (roleId, rightIds) => {
  const url = new UrlBuilder({ host: ROLE.RIGHT.ROLE_RIGHT_PATH }).path(roleId).build()

  return postData(url, rightIds).then(response => response.data)
}

export const deleteRoleRight = roleRightId => {
  const url = new UrlBuilder({ host: ROLE.RIGHT.ROLE_RIGHT_PATH }).path(roleRightId).build()

  return deleteData(url).then(response => response.data)
}

// #endregion

// #region ROLE RLS

export const getRoleRls = id => {
  const url = new UrlBuilder({ host: ROLE.RLS.GET_RLS }).path(id).build()

  return getData(url).then(response => response.data)
}

export const addRls = (roleId, rlsType, crud, rlsData) => {
  const url = new UrlBuilder({ host: ROLE.RLS.ADD_RLS })
    .path(roleId)
    .param('rlsType', rlsType)
    .param('crud', crud)
    .build()

  return postData(url, rlsData).then(response => response.data)
}

export const editRls = (roleId, rlsType, crud, rlsData) => {
  const url = new UrlBuilder({ host: ROLE.RLS.EDIT_RLS })
    .path(roleId)
    .param('rlsType', rlsType)
    .param('crud', crud)
    .build()

  return putData(url, rlsData).then(response => response.data)
}

export const deleteRls = (roleId, rlsType, rlsId) => {
  const url = new UrlBuilder({ host: ROLE.RLS.DELETE_RLS })
    .path(roleId)
    .param('rlsType', rlsType)
    .param('rlsId', rlsId)
    .build()

  return deleteData(url).then(response => response.data)
}

// #endregion

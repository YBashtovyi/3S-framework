import { USER } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { getData, putData, postData, deleteData } from '../api'

export const fetchUsers = () => {
  const url = new UrlBuilder({ host: USER.PATH }).build()

  return getData(url).then(response => response.data)
}

export const getDetailsUserById = id => {
  const url = new UrlBuilder({ host: USER.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const createUser = user => {
  const url = new UrlBuilder({ host: USER.PATH }).build()

  return postData(url, user).then(response => response.data)
}

export const editUser = (user, id) => {
  const url = new UrlBuilder({ host: USER.PATH }).path(id).build()

  return putData(url, user).then(response => response.data)
}

export const getUserRoles = userId => {
  const url = new UrlBuilder({ host: USER.GET_ROLES }).path(userId).build()

  return getData(url).then(response => response.data)
}

export const addRolesToUser = (userId, rolesIds) => {
  const url = new UrlBuilder({ host: USER.ADD_ROLES }).path(userId).build()

  return postData(url, rolesIds).then(response => response.data)
}

export const deleteRoleFromUser = (userId, roleId) => {
  const url = new UrlBuilder({ host: USER.DELETE_ROLE })
    .path(userId)
    .param('roleId', roleId)
    .build()

  return deleteData(url).then(response => response.data)
}

export const getUserRls = id => {
  const url = new UrlBuilder({ host: USER.GET_RLS }).path(id).build()

  return getData(url).then(response => response.data)
}

export const addRls = (userId, rlsType, crud, rlsData) => {
  const url = new UrlBuilder({ host: USER.ADD_RLS })
    .path(userId)
    .param('rlsType', rlsType)
    .param('crud', crud)
    .build()

  return postData(url, rlsData).then(response => response.data)
}

export const editRls = (userId, rlsType, crud, rlsData) => {
  const url = new UrlBuilder({ host: USER.EDIT_RLS })
    .path(userId)
    .param('rlsType', rlsType)
    .param('crud', crud)
    .build()

  return putData(url, rlsData).then(response => response.data)
}

export const deleteRls = (userId, rlsType, rlsId) => {
  const url = new UrlBuilder({ host: USER.DELETE_RLS })
    .path(userId)
    .param('rlsType', rlsType)
    .param('rlsId', rlsId)
    .build()

  return deleteData(url).then(response => response.data)
}

import { RIGHT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { getData, putData, postData } from '../api'

export const fetchRights = () => {
  const url = new UrlBuilder({ host: RIGHT.PATH }).build()

  return getData(url).then(response => response.data)
}

export const getDetailsRightById = id => {
  const url = new UrlBuilder({ host: RIGHT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditRightById = id => {
  const url = new UrlBuilder({ host: RIGHT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const createRight = right => {
  const url = new UrlBuilder({ host: RIGHT.PATH }).build()

  return postData(url, right).then(response => response.data)
}

export const editRight = (right, id) => {
  const url = new UrlBuilder({ host: RIGHT.PATH }).path(id).build()

  return putData(url, right).then(response => response.data)
}

import { POSITION } from '../utils/axios-env-config'
import UrlBuilder from '../utils/url-builder'
import { postData, getData, putData } from './api'

export const createPosition = position => {
  const url = new UrlBuilder({ host: POSITION.PATH }).build()

  return postData(url, position).then(response => response.data)
}

export const editPosition = (position, id) => {
  const url = new UrlBuilder({ host: POSITION.PATH }).path(id).build()

  return putData(url, position).then(response => response.data)
}

export const getPositionById = id => {
  const url = new UrlBuilder({ host: POSITION.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const fetchPositions = () => {
  const url = new UrlBuilder({ host: POSITION.PATH }).build()

  return getData(url).then(response => response.data)
}

import { CITY } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData } from '../api'

export const fetchCityList = () => {
  const url = new UrlBuilder({ host: CITY.PATH }).build()

  return getData(url).then(response => response.data)
}

export const createCity = city => {
  const url = new UrlBuilder({ host: CITY.PATH }).build()

  return postData(url, city).then(response => response.data)
}

export const editCity = (city, id) => {
  const url = new UrlBuilder({ host: CITY.PATH }).path(id).build()

  return putData(url, city).then(response => response.data)
}

export const getDetailsCityById = id => {
  const url = new UrlBuilder({ host: CITY.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditCityById = id => {
  const url = new UrlBuilder({ host: CITY.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

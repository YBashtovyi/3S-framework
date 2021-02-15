import { WORK_SUB_TYPE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const fetchWorkSubTypeList = () => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.PATH }).build()

  return getData(url).then(response => response.data)
}

export const createWorkSubType = workSubType => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.PATH }).build()

  return postData(url, workSubType).then(response => response.data)
}

export const editWorkSubType = (id, workSubType) => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.PATH }).path(id).build()

  return putData(url, workSubType).then(response => response.data)
}

export const getDetailsWorkSubTypeById = id => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditWorkSubTypeById = id => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteWorkSubType = id => {
  const url = new UrlBuilder({ host: WORK_SUB_TYPE.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

import { PROJECT_WORK_SCHEDULE_SUB_TYPE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const createProjectWorkScheduleSubType = workScheduleSubType => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_SUB_TYPE.PATH }).build()

  return postData(url, workScheduleSubType).then(response => response.data)
}

export const editProjectWorkScheduleSubType = (id, workScheduleSubType) => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_SUB_TYPE.PATH }).path(id).build()

  return putData(url, workScheduleSubType).then(response => response.data)
}

export const getDetailsProjectWorkScheduleSubTypeById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_SUB_TYPE.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectWorkScheduleSubTypeById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_SUB_TYPE.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectWorkScheduleSubType = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_SUB_TYPE.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

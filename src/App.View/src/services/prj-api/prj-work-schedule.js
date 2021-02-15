import { PROJECT_WORK_SCHEDULE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const createProjectWorkScheduleWorkSchedule = workSchedule => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE.PATH }).build()

  return postData(url, workSchedule).then(response => response.data)
}

export const editProjectWorkSchedule = (id, workSchedule) => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE.PATH }).path(id).build()

  return putData(url, workSchedule).then(response => response.data)
}

export const getDetailsProjectWorkScheduleById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectWorkScheduleById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectWorkSchedule = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

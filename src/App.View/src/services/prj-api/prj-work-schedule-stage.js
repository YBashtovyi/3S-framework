import { PROJECT_WORK_SCHEDULE_STAGE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const fetchProjectWorkScheduleStage = param => {
  let url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.PATH })

  if (param.prjWorkScheduleId) {
    url = url.param('prjWorkScheduleId', param.prjWorkScheduleId)
  }

  return getData(url).then(response => response.data)
}

export const createProjectWorkScheduleStage = workScheduleStage => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.PATH }).build()

  return postData(url, workScheduleStage).then(response => response.data)
}

export const editProjectWorkScheduleStage = (id, workScheduleStage) => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.PATH }).path(id).build()

  return putData(url, workScheduleStage).then(response => response.data)
}

export const getDetailsProjectWorkScheduleStageById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditProjectWorkScheduleStageById = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const deleteProjectWorkScheduleStage = id => {
  const url = new UrlBuilder({ host: PROJECT_WORK_SCHEDULE_STAGE.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

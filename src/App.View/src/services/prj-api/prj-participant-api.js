import { PRJ_PARTICIPANT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData, deleteData } from '../api'

export const createPrjParticipant = prjParticipant => {
  const url = new UrlBuilder({ host: PRJ_PARTICIPANT.PATH }).build()

  return postData(url, prjParticipant).then(response => response.data)
}

export const editPrjParticipant = (id, prjParticipant) => {
  const url = new UrlBuilder({ host: PRJ_PARTICIPANT.PATH }).path(id).build()

  return putData(url, prjParticipant).then(response => response.data)
}

export const deletePrjParticipant = id => {
  const url = new UrlBuilder({ host: PRJ_PARTICIPANT.PATH }).path(id).build()

  return deleteData(url).then(response => response.data)
}

export const getDetailsPrjParticipantById = id => {
  const url = new UrlBuilder({ host: PRJ_PARTICIPANT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditPrjParticipantById = id => {
  const url = new UrlBuilder({ host: PRJ_PARTICIPANT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

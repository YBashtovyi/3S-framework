import { DOCUMENT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData, putData } from '../api'

export const createDocument = doc => {
  const url = new UrlBuilder({ host: DOCUMENT.PATH }).build()

  return postData(url, doc).then(response => response.data)
}

export const editDocument = (doc, id) => {
  const url = new UrlBuilder({ host: DOCUMENT.PATH }).path(id).build()

  return putData(url, doc).then(response => response.data)
}

export const getDetailsDocumentById = id => {
  const url = new UrlBuilder({ host: DOCUMENT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

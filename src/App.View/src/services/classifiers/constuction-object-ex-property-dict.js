import { CONSTRUCTION_OBJECT_EX_PROPERTY_DICT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData } from '../api'

export const fetchConstructionObjectExProperty = param => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.PATH })
  if (param?.id) {
    url.param('id', param.id)
  }
  if (param?.code) {
    url.param('code', param.code)
  }
  if (param?.parentId) {
    url.param('parentId', param.parentId)
  }

  return getData(url).then(response => response.data)
}

export const createConstructionObjectExProperty = property => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.PATH }).build()

  return postData(url, property).then(response => response.data)
}

export const editConstructionObjectExProperty = (id, property) => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.PATH }).path(id).build()

  return putData(url, property).then(response => response.data)
}

export const getDetailsConstructionObjectExPropertyById = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditConstructionObjectExPropertyById = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getTypeOfObjectList = () => {
  const url = new UrlBuilder({
    host: CONSTRUCTION_OBJECT_EX_PROPERTY_DICT.GET_TYPE_OF_OBJECT_LIST,
  }).build()

  return getData(url).then(response => response.data)
}

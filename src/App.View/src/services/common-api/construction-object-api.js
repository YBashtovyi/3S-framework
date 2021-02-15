import { CONSTRUCTION_OBJECT } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, putData, getData, deleteData } from '../api'

export const fetchConstructionObjectList = () => {
  // TODO: Потом доописать больше параметров, которые можно будет указывать

  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.PATH })
    .param('objectStatus', 'Active')
    .build()

  return getData(url).then(response => response.data)
}

export const createObject = object => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.PATH }).build()

  return postData(url, object).then(response => response.data)
}

export const editObject = (id, object) => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.PATH }).path(id).build()

  return putData(url, object).then(response => response.data)
}

export const getDetailsObjectById = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.EXT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getEditObjectById = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.EDIT }).path(id).build()

  return getData(url).then(response => response.data)
}

export const changeObjectStatus = (id, status) => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.CHANGE_OBJECT_STATUS })
    .path(id)
    .param('status', status)
    .build()

  return putData(url).then(response => response.data)
}

export const getMapCoordinates = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.ATU_COORDINATES.GET }).path(id).build()

  return getData(url).then(response => response.data)
}

export const getExtendedProperty = (objectId, param) => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.EXTENDED_PROPERTY.GET })
    .path(objectId)
    .build()

  return getData(url).then(response => response.data)
}

export const addExtendedProperty = (objectId, extendedProperty) => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.EXTENDED_PROPERTY.ADD })
    .path(objectId)
    .build()

  return postData(url, extendedProperty).then(response => response.data)
}

export const deleteExtendedProperty = id => {
  const url = new UrlBuilder({ host: CONSTRUCTION_OBJECT.EXTENDED_PROPERTY.DELETE })
    .path(id)
    .build()

  return deleteData(url).then(response => response.data)
}

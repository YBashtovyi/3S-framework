import { CONSTRUCTION_OBJECT, PROJECT, PROJECT_PHOTO_REPORT } from '../../utils/axios-env-config'

import UrlBuilder from '../../utils/url-builder'
import { postData } from '../api'

export const addMapCoordinate = (id, domen, atuCoordinate) => {
  let host = ''
  switch (domen) {
    case 'Project': {
      host = PROJECT.ATU_COORDINATES.ADD
      break
    }

    case 'ConstructionObject': {
      host = CONSTRUCTION_OBJECT.ATU_COORDINATES.ADD
      break
    }

    case 'PhotoReport': {
      host = PROJECT_PHOTO_REPORT.ATU_COORDINATES.ADD
      break
    }
    default:
      return
  }

  const url = new UrlBuilder({ host }).path(id).build()

  return postData(url, atuCoordinate).then(response => response.data)
}

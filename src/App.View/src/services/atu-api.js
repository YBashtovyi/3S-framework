import { ATU } from '../utils/axios-env-config'
import UrlBuilder from '../utils/url-builder'
import { getData } from './api'

export const fetchRegionList = () => {

    const url = new UrlBuilder({ host: ATU.REGION.ITEMS })
            .build()

    return getData(url).then(response => response.data)
}

export const fetchCoutryList = () => {

  const url = new UrlBuilder({ host: ATU.REGION.ITEMS })
          .build()

  return getData(url).then(response => response.data)
}

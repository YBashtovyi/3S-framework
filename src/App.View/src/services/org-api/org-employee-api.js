import { ORG_EMPLOYEE } from '../../utils/axios-env-config'
import UrlBuilder from '../../utils/url-builder'
import { postData, getData } from '../api'

export const fetchOrgEmployee = () => {
  const url = new UrlBuilder({ host: ORG_EMPLOYEE.PATH }).build()

  return getData(url).then(response => response.data)
}

export const createOrgEmployee = orgEmployee => {
  const url = new UrlBuilder({ host: ORG_EMPLOYEE.PATH }).build()

  return postData(url, orgEmployee).then(response => response.data)
}

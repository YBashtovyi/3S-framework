import { env, getData, postData } from '../../services/api'
import UrlBuilder from '../../utils/url-builder'
import { isNotEmpty } from '../../utils/function-helpers'
import get from 'lodash.get'
import first from 'lodash.first'

/**
 * Fetches all users with current user id from identity server api
 * @param {String} userId identifier of user from identity server (which is stored in token.sub)
 */
export const fetchMisUser = userId => {
  const urlBuilder = new UrlBuilder({ host: env.AUTH.IDENTITY_USERS })
  if (isNotEmpty(userId)) {
    urlBuilder.param('id', userId)
  }

  const url = urlBuilder.toString()
  return getData(url).then(response => {
    const misUsers = get(response, 'data', null)
    const misUser = first(misUsers)
    return misUser
  })
}

export const updateCredentials = context => {
  const url = new UrlBuilder({ host: env.MIS_USERS.UPDATE }).build()
  return postData(url, context)
}

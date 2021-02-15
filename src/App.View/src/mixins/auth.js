import { postData, postAuth, env } from '@/services/api'
import UrlBuilder from '../utils/url-builder'

export const tryAuthenticate = () => {
  const url = env.AUTH.AUTH_TRY_IDENTITY
  return postAuth(url)
    .then(response => {
      const result = { authenticated: false }
      if (response.status === 200) {
        // single user
        result.authenticated = true
        result.userProfileData = response.data.userProfileData
      } else if (response.status === 300) {
        // multi user
        result.userData = response.data
      }
      return result
    })
    .catch(error => {
      console.log(error)
    })
}

export const authenticate = profile => {
  const { profileId, userId, ehealthEmployeeId, specialityEhealthId, email } = profile
  const url = env.AUTH.AUTH_IDENTITY
  return postData(url, { profileId, userId, ehealthEmployeeId, specialityEhealthId, email })
    .then(response => {
      return response.data
    })
    .catch(error => error)
}

export const tryAuthenticateIdGovUa = code => {
  const url = new UrlBuilder({ host: env.AUTH.ID_GOV_UA_CODE }).param('code', code).build()
  return postAuth(url).then(response => {
    const result = { authenticated: false }
    if (response.status === 200) {
      // single user
      result.authenticated = true
      result.userProfileData = response.data.userProfileData
    }
    return result
  })
}

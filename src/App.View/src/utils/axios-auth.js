import axios from 'axios'
import Mgr from '../boot/security-oidc'
import get from 'lodash.get'

const $axios = axios.create({
  baseURL: process.env['OIDC_AUTHORITY'],
})

$axios.interceptors.request.use(
  function(config) {
    const userManager = new Mgr()

    const setAutorizationToken = user => {
      const accessToken = get(user, 'access_token', null)
      if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`
        return config
      }
    }
    return userManager.getUser().then(setAutorizationToken)
  },

  function(error) {
    return Promise.reject(error)
  },
)

export default $axios

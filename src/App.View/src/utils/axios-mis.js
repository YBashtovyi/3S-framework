import axios from 'axios'
import { LocalStorage, Notify } from 'quasar'
import get from 'lodash.get'

const axiosMis = axios.create({
  baseURL: process.env.API,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
})

axiosMis.interceptors.request.use(
  config => {
    let key = 'oidc.user:' + process.env.OIDC_ISSUER + ':MisFront'
    let storageToken = LocalStorage.getItem(key)
    let token = get(JSON.parse(storageToken), 'access_token', null)
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }
    if (!config.headers['Accept-Language']) {
      config.headers['Accept-Language'] = 'uk'
    }

    return config
  },
  error => {
    return Promise.reject(error)
  },
)

// response interceptor
axiosMis.interceptors.response.use(
  response => {
    return response
  },
  error => {
    if (/^[4-5]/.test(error.response.status)) {
      if (error.response.status === 401) {
        const isAutheticated = JSON.parse(localStorage.getItem('suip.authenticated'))
        if (isAutheticated) {
          localStorage.setItem('suip.authenticated', false)
        }
        return (window.location.href = process.env.OIDC_POST_LOGOUT_REDIRECT_URI)
      }
      let message = []
      if (error.response.data.detail) {
        message.push(error.response.data.detail)
      } else if (error.response.data.errors) {
        message.push(JSON.stringify(error.response.data.errors))
      } else {
        message.push('Невідома помилка')
      }
      const title = error.response.data.title || error.response.statusText
      Notify.create({
        html: true,
        color: 'amber-3',
        textColor: 'grey-9',
        icon: 'warning',
        message: `<div class="text-subtitle1">${title} - ${error.response.status}</div><div style="max-width: 380px">${message}</div>`,
      })
    }
    return Promise.reject(error)
  },
)

export default axiosMis

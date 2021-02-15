import axios from 'axios'
import { LocalStorage } from 'quasar'


const axiosMis = axios.create({
  baseURL: process.env.API,
  headers: { 'Content-Type': 'application/json' }
})


axiosMis.interceptors.request.use(
  config => {
    const key = `oidc.user:${process.env.OIDC_ISSUER}:MisFront`
    const storageToken = LocalStorage.getItem(key)
    const token = JSON.parse(storageToken).access_token

    if (token) config.headers['Authorization'] = `Bearer ${token}`
    if (!config.headers['Accept-Language']) config.headers['Accept-Language'] = 'uk'
    
    return config
  },
  error => Promise.reject(error)
)


export default axiosMis

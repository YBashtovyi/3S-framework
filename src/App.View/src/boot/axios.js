import axios from 'axios'
import Mgr from '@/boot/security-oidc'
import store from '../store'

const $axios = axios.create({
  baseURL: `${process.env.API}`,
})

$axios.defaults.withCredentials = true

$axios.interceptors.request.use(
  function(config) {
    if (store.state.context.jwtToken) return config
    let user = new Mgr()
    return user.getUser().then(
      res => {
        config.headers.Authorization = `Bearer ${res.id_token}`
        return config
      },
      err => {
        console.log(err)
      },
    )
  },
  function(error) {
    return Promise.reject(error)
  },
)

export default ({ Vue }) => {
  Vue.prototype.$axios = $axios
}

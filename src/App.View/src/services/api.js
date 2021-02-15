import axios from '../utils/axios-mis'
import envConfig from '../utils/axios-env-config'

export const env = envConfig

export const getData = (path, data) => axios.get(`${path}`, data)

export const postData = (path, data) => axios.post(`${path}`, data)

export const putData = (path, data) => axios.put(`${path}`, data)

export const patchData = (path, data) => axios.patch(`${path}`, data);

export const deleteData = (path, data) => axios.delete(`${path}`, { data })

export const postAuth = path =>
  axios({
    url: `${path}`,
    method: 'post',
    validateStatus: function (status) {
      return status >= 200 && status <= 300
    }
  })

export const getFile = path =>
  axios.get(`${path}`, {
    responseType: 'blob'
  })

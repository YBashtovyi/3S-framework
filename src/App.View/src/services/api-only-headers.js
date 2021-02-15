import axios from '../utils/axios-mis-api'

export const get = (path, data) => axios.get(`${path}`, data)
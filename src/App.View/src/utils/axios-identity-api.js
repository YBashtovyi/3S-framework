import axios from "./axios-auth"
import * as identityEnvConfig  from './axios-identity-env-config'

export const env = identityEnvConfig

export const getData = (path, headers) => axios.get(`${path}`, headers);

export const postData = (path, data, headers) =>
  axios.post(`${path}`, data, headers)

export const putData = (path, data, headers) =>
  axios.put(`${path}`, data, headers)

export const deleteData = (path, data, headers) =>
  axios.delete(`${path}`, { data }, headers)

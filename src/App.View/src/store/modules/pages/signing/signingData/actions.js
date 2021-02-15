import { SET_SIGNING_INFO, CLEAR_SIGNING_INFO } from './constants'

export default {
  [SET_SIGNING_INFO]: ({ commit }, signingData) => {
    commit(SET_SIGNING_INFO, signingData)
  },

  [CLEAR_SIGNING_INFO]: ({ commit }) => {
    commit(CLEAR_SIGNING_INFO)
  }
}
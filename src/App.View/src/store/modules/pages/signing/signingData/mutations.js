import { SET_SIGNING_INFO, CLEAR_SIGNING_INFO } from './constants'

export default {
  [SET_SIGNING_INFO]: (state, signingData) => {
    state.caServer = signingData.caServer
    state.caFile = signingData.caFile
  },

  [CLEAR_SIGNING_INFO]: (state) => {
    state.caServer = null
    state.caFile = null
  }
}
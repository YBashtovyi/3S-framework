import { getData, env } from '@/services/api'
export default {
  namespaced: true,
  state: () => ({
    details: {},
  }),
  actions: {
    getUserDetails({ commit }, { params }) {
      getData(env.USER.EXT + params)
        .then(response => {
          commit('setDetails', response.data)
        })
        .catch(error => console.log(error))
    },
  },
  mutations: {
    setDetails(state, data) {
      state.details = data
    },
  },
}

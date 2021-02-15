import { getData, env } from "@/services/api";
export default {
  namespaced: true,
  state: () => ({
    details: {},
    ehealthOrgProfile: {
      type: {}
    },
    edrOrgInfo: {
      edr: {}
    }
  }),
  actions: {
    getOrganization({ commit }, { params }) {
      getData(env.ORGANIZATION.EXT + "/" + params)
        .then(response => {
          commit("setDetails", response.data);
        })
        .catch(error => console.log(error));
    },

    setEhealthProfile({ commit }, id) {
      const uri = `${env.ORGANIZATION.EHEALTH.PATH}/${id}`
      getData(uri)
        .then(({data}) => {
          commit("setEhealthProfile", data);
          if (data.ehealthId) {
            const edrUri = `${env.ORGANIZATION.EHEALTH.LEGAL}/${data.id}`
            getData(edrUri)
              .then(({data}) => {
                  commit("setEdrOrgInfo", data);
              })            
          }
        })
    },

    updateEhealthStatus({ commit }, { data }) {
      commit("setEhealthStatus", data);
    }
  },
  mutations: {
    setDetails(state, data) {
      state.details = data;
    },
    setEdrOrgInfo(state, data) {
      state.edrOrgInfo = data;
    },
    setEhealthProfile(state, data) {
      state.ehealthOrgProfile = data;
    },
    setEhealthStatus(state, data) {
      state.ehealthOrgProfile.ehealthId = data;
      state.ehealthOrgProfile.isUpdatedDataInEhealth = true;
    }
  }
};

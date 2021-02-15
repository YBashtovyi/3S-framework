export default {
  namespaced: true,
  state: () => ({
    itemId: '',
    item: []
  }),
  actions: {
    fetch ({ commit }, { row }) {
      let itemId = row.id,
        item = row
      commit('setItemId', { itemId })
      commit('setItem', { item })
    }
  },
  mutations: {
    setItemId (state, { itemId }) {
      state.itemId = itemId
    },
    setItem (state, { item }) {
      state.item = item
    }
  }
}

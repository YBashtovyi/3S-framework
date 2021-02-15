import Vue from 'vue'
import Vuex from 'vuex'

import baseElements from './modules/components/baseElements.js'
import tableList from './modules/components/tableList.js'
import tableMenu from './modules/components/tableMenu.js'
import organizationCard from './modules/pages/organizationCard.js'
import adminUsers from './modules/pages/adminUsers.js'
import context from './context'
import '../filters'

import tableV2 from './modules/components/tableV2'

import signingData from './modules/pages/signing/signingData'

Vue.use(Vuex)

/*
 * If not building with SSR mode, you can
 * directly export the Store instantiation
 */
export default function(/* { ssrContext } */) {
  const Store = new Vuex.Store({
    modules: {
      tableV2,

      tableList,
      tableMenu,
      baseElements,

      organizationCard,
      adminUsers,
      signingData,

      context,
    },

    // enable strict mode (adds overhead!)
    // for dev mode only
    strict: process.env.DEV,
  })
  return Store
}

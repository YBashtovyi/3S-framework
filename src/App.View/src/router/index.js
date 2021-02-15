import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routes'
import entityRight from './../directives/entityRight'
import operationRight from './../directives/operationRight'
import interfaceRight from '../directives/interfaceRight'
import FSelect from './../components/forms/FilteredAutocomplete'
import { VueMaskDirective } from 'v-mask'
import moment from 'moment'

Vue.use(VueRouter)
Vue.component('f-select', FSelect)
Vue.directive('entityRight', entityRight)
Vue.directive('operationRight', operationRight)
Vue.directive('interfaceRight', interfaceRight)
Vue.directive('mask', VueMaskDirective)

Vue.filter('asDate', (value, format) => {
  if (!value) {
    return ''
  }

  return format ? moment(value).format(format) : moment(value).format('DD.MM.YYYY HH:mm')
})

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation
 */

export default function() {
  const Router = new VueRouter({
    scrollBehavior: () => ({
      x: 0,
      y: 0,
    }),
    routes,
    // Leave these as is and change from quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    // mode: process.env.VUE_ROUTER_MODE,
    // base: process.env.VUE_ROUTER_BASE
    // mode: 'history',
    // mode: process.env.VUE_ROUTER_MODE,
    base: process.env.VUE_ROUTER_BASE,
  })

  Router.beforeEach((to, from, next) => {
    if (from.name === 'callback') {
      localStorage.setItem('goBack', false)
    } else {
      localStorage.setItem('goBack', true)
    }
    next()
  })

  return Router
}

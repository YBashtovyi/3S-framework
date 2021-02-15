import { date } from 'quasar'
import { getData, env } from '../../../services/api'

export default {
  namespaced: true,
  state: () => ({
    drawer: true,
    miniState: false,
    todayDate: '',
    startMonth: '',
    user: '',
    // user application rights defines accessibility of elements
    rights: null,
    authenticated: false,
    authProvider: '',
    userName: 'Не авторизовано',
    status: '',
    isBackBtn: false,
    breadCrumbsList: [],
    // menuList: [],
    navMenu: [
      {
        header: 'Журнали',
        separator: false,
        children: [
          {
            icon: 'fa fa-building',
            iconSize: '24px',
            label: 'Організації',
            link: '/organization',
            interfaceRight: 'ORGANIZATION_LIST',
          },
          {
            icon: 'fa fa-object-ungroup',
            iconSize: '24px',
            label: `Об'єкти`,
            link: '/objects',
            interfaceRight: 'OBJECT_LIST',
          },
          {
            icon: 'architecture',
            iconSize: '24px',
            label: 'Проєкти',
            link: '/projects',
            interfaceRight: 'PROJECT_LIST',
          },
          {
            icon: 'fa fa-calculator',
            iconSize: '24px',
            label: 'Договори',
            link: '/prjContract',
            interfaceRight: 'PRJ_CONTRACT_LIST',
          },
          {
            icon: 'fa fa-folder-open',
            iconSize: '24px',
            label: 'Календарні плани',
            link: '/prjWorkSchedule',
            interfaceRight: 'PRJ_WORK_SCHEDULE_LIST',
          },
        ],
      },
      {
        header: 'Організаційна структура',
        separator: false,

        children: [
          {
            icon: 'fa fa-building',
            iconSize: '24px',
            label: 'Організації',
            link: '/organization',
            interfaceRight: 'ORGANIZATION_LIST',
          },
          {
            icon: 'domain',
            iconSize: '24px',
            label: 'Підрозділи',
            link: '/department',
            interfaceRight: 'DEPARTMENT_LIST',
          },
          {
            icon: 'people',
            iconSize: '24px',
            label: 'Персони',
            link: '/person',
            interfaceRight: 'PERSON_LIST',
          },
          {
            icon: 'business_center',
            iconSize: '24px',
            label: 'Посади',
            link: '/position',
            interfaceRight: 'POSITION_LIST',
          },
        ],
      },
      {
        header: 'Адм. територіальний устрій',
        separator: false,

        children: [
          {
            icon: 'language',
            iconSize: '24px',
            label: 'Країни',
            link: '/organization',
            interfaceRight: 'ORGANIZATION_LIST',
          },
          {
            icon: 'view_module',
            iconSize: '24px',
            label: "Тер. об'єднання",
            link: '/organization',
            interfaceRight: 'ORGANIZATION_LIST',
          },
          {
            icon: 'place',
            iconSize: '24px',
            label: 'Населені пункти',
            link: '/city',
            // interfaceRight: 'ORGANIZATION_LIST',
          },
        ],
      },
      {
        header: 'Загальні довідники',
        separator: false,
        children: [
          {
            icon: 'list',
            iconSize: '24px',
            label: 'Переліки',
            link: '/enum',
            interfaceRight: 'ENUM_LIST',
          },
          {
            icon: 'far fa-list-alt',
            iconSize: '24px',
            label: `Додаткові характеристики об'єктів`,
            link: '/constObjExPropDict',
            interfaceRight: 'CONSTRUCTION_OBJECT_EX_PROPERTY_DICTIONARY',
          },
          {
            icon: 'far fa-list-alt',
            iconSize: '24px',
            label: `Види робіт`,
            link: '/workSubType',
            interfaceRight: 'WORK_SUB_TYPE_LIST',
          },
        ],
      },
      {
        header: 'Адміністрування',
        separator: false,
        children: [
          {
            icon: 'fa fa-users',
            iconSize: '24px',
            label: 'Користувачі системи',
            link: '/users',
            interfaceRight: 'USER_LIST',
          },
          {
            icon: 'fa fa-cogs',
            iconSize: '24px',
            label: 'Ролі',
            link: '/roles',
            interfaceRight: 'ROLE_LIST',
          },
          {
            icon: 'fa fa-cog',
            iconSize: '24px',
            label: 'Права',
            link: '/rights',
            interfaceRight: 'RIGHT_LIST',
          },
          {
            icon: 'fa fa-id-card',
            iconSize: '24px',
            label: 'Облікові записи',
            link: '/accounts',
            interfaceRight: 'ACCOUNT_LIST',
          },
        ],
      },
    ],
  }),

  getters: {
    getUserInfo(state) {
      return state.user
    },

    getIsAuthenticated(state) {
      return state.authenticated
    },

    getAuthProvider(state) {
      return state.authProvider
    },

    getUserRights(state) {
      return state.rights
    },

    getNavMenu(state) {
      if (state.rights) {
        return state.navMenu
      } else {
        return []
      }
    },
  },

  actions: {
    initUser({ state, commit, dispatch }) {
      dispatch('fetchUserInfo').then(({ data }) => {
        commit('setUserInfo', data)
      })
    },

    setUserInfo({ commit, dispatch }, user) {
      if (user) {
        commit('setRights', user.rights)

        const userWithRightsRemoved = {}
        for (let key in user) {
          if (key !== 'rights' && user.hasOwnProperty(key)) {
            userWithRightsRemoved[key] = user[key]
          }
        }

        commit('setUserInfo', userWithRightsRemoved)
      } else {
        commit('setRights', null)
        commit('setUserInfo', null)
      }

      dispatch('transformName', user)
    },

    setAuthenticated({ commit }, authenticated) {
      localStorage.setItem('suip.authenticated', authenticated)
      commit('setAuthenticated', authenticated)
    },

    setAuthProvider({ commit }, authProvider) {
      localStorage.setItem('authProvider', authProvider)
      commit('setAuthProvider', authProvider)
    },

    // get user info from api
    fetchUserInfo({ state }) {
      return getData(env.AUTH.INFO)
    },

    // transform username
    transformName({ commit }, data) {
      if (data) {
        let name =
          data.personLastName +
          ' ' +
          (data.personName ? data.personName.split('')[0] + '. ' : '') +
          (data.personMiddleName ? data.personMiddleName.split('')[0] + '.' : '')
        commit('setUserName', { name })
      } else {
        commit('setUserName', { name: 'Не авторизовано' })
      }
    },

    // toggle mini state for drawer
    getMiniState({ commit, state }) {
      let data = JSON.parse(localStorage.getItem('miniState'))
      commit('setMiniState', { data })
    },
    // toggle drawer
    toggleDrawer({ commit }) {
      commit('setDrawer')
    },
    // get today for datepicker
    getTodayDate({ commit }) {
      let today = date.formatDate(Date.now(), 'DD.MM.YYYY')
      commit('setToday', { today })
    },
    // get first day of this month
    getFirstDayMonth({ commit, dispatch, state }) {
      dispatch('getTodayDate')
      let start = '01.' + state.todayDate.split('.')[1] + '.' + state.todayDate.split('.')[2]
      commit('setStartMonth', { start })
    },
    toggleBackBtn({ commit }) {
      let data = JSON.parse(localStorage.getItem('goBack'))
      commit('showBackBtn', { data })
    },

    getBreadCrumbs({ commit }, { route }) {
      let crumbs = route.meta.breadcrumb
      commit('setCrumbs', { crumbs })
    },
  },
  mutations: {
    setAuthenticated(state, authenticated) {
      state.authenticated = authenticated
    },

    setAuthProvider(state, authProvider) {
      state.authProvider = authProvider
    },

    setCrumbs(state, { crumbs }) {
      state.breadCrumbsList = crumbs
    },

    setUserInfo(state, data) {
      state.user = data
    },

    setRights(state, rights) {
      state.rights = rights
    },

    setUserName(state, { name }) {
      state.userName = name
    },
    setMiniState(state, { data }) {
      state.miniState = data
    },
    // setMenuList(state, { menu }) {
    //   state.menuList = menu
    // },
    setDrawer(state) {
      state.drawer = !state.drawer
    },
    setToday(state, { today }) {
      state.todayDate = today
    },
    setStartMonth(state, { start }) {
      state.startMonth = start
    },
    setStatus(state, { status }) {
      state.status = status
    },
    showBackBtn(state, { data }) {
      state.isBackBtn = data
    },
  },
}

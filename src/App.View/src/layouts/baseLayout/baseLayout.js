// CONSTANTS
// import { TABLE_LIST_OPTIONS } from '../../services/table-list-stored-options/constants'
import { mapState, mapActions } from 'vuex'
import { getData, env } from '../../services/api'
import cloneDeep from 'lodash.clonedeep'
import Oidc from '../../boot/security-oidc'
import drawer from '@/components/baseElements/menuDrawer.vue'
import { VIDEO_INSTRUCTION_URL, SUPPORT_NUMBER } from '../../constants/appsettings'
import get from 'lodash.get'
import { stringEmpty } from '../../utils/function-helpers'
import oneSignalHandler from '../../mixins/oneSignalHandler'

export default {
  mixins: [oneSignalHandler],

  data() {
    return {
      left: true,
      test: false,
      authProcessIsOn: false,
      oidcManager: new Oidc(),
      emptyGuid: '00000000-0000-0000-0000-000000000000',
      isChangeCredentialsDialogActive: false,
      isRightSideMenuActive: false,
      isPageLoaded: false,
    }
  },

  components: { drawer },

  computed: {
    ...mapState('baseElements', ['menuList', 'userName', 'isBackBtn', 'miniState', 'authProvider']),

    isLocalhost() {
      const currentHostName = get(window, ['location', 'hostname'], stringEmpty())
      return currentHostName.includes('localhost')
    },

    breadcrumbs() {
      // TODO: ПЕРЕДЕЛАТЬ ЭТУ ФИГНЮ НА УНИВЕРСАЛЬНУЮ!!!!!!! АААА
      let crumbs = cloneDeep(this.$route.meta.breadcrumb)
      for (let key in crumbs) {
        let regexpMisId = /:misId/,
          regexpId = /:id/,
          regexPrjDocId = /:prjDocId/,
          regesPrjContractId = /:prjContractId/
        if (regexpMisId.test(crumbs[key].path)) {
          crumbs[key].path = crumbs[key].path.replace(/:misId/, this.$route.params.misId)
        }
        if (regexpId.test(crumbs[key].path)) {
          crumbs[key].path = crumbs[key].path.replace(/:id/, this.$route.params.id)
        }
        if (regexPrjDocId.test(crumbs[key].path)) {
          crumbs[key].path = crumbs[key].path.replace(/:prjDocId/, this.$route.params.prjDocId)
        }
        if (regesPrjContractId.test(crumbs[key].path)) {
          crumbs[key].path = crumbs[key].path.replace(
            /:prjContractId/,
            this.$route.params.prjContractId,
          )
        }
      }
      return crumbs
    },

    supportNumber() {
      return SUPPORT_NUMBER
    },

    hasVideoInstructionUrl() {
      return !!VIDEO_INSTRUCTION_URL
    },

    isAuthenticated() {
      const isAutheticated = JSON.parse(localStorage.getItem('suip.authenticated'))
      return isAutheticated
    },

    authProvider() {
      const authProvider = localStorage.getItem('authProvider')
      return authProvider
    },

    isIdentityProvider() {
      const isIdentityProvider = this.authProvider === 'Identity'
      return isIdentityProvider
    },

    isIdGovUaProvider() {
      const isIdGovUaProvider = this.authProvider === 'IdGovUa'
      return isIdGovUaProvider
    },
  },

  methods: {
    ...mapActions('baseElements', [
      'fetchUserInfo',
      'setUserInfo',
      'setEhealthInfo',
      'toggleBackBtn',
      'toggleDrawer',
      'getMiniState',
      'setAuthenticated',
    ]),

    onCreated() {
      this.setAuthProcessOn()
      this.setPageNotLoaded()
      this.fetchUserInfo()
        .then(this.initializeUser)
        // .then(this.initOneSignal)
        .then(this.setAuthProcessOff)
        .then(this.setPageLoaded)
        .catch(this.onFetchUserError)
    },

    onMounted() {
      let logoutUrl = localStorage.getItem('LOG_OUT_CALLBACK', this.$route.path)
      if (logoutUrl) {
        localStorage.setItem('REDIRECT_CALLBACK', logoutUrl)
      } else {
        localStorage.setItem('REDIRECT_CALLBACK', this.$route.path)
      }

      if (this.authProvider === 'Identity') {
        // check user status
        this.oidcManager
          .getSignedIn()
          .then(this.signInIdentityUserIfNeeded)
          .then(this.getMiniState)
      } else {
        this.fetchUserInfo()
          .then(p => {
            this.signInIdentityUserIfNeeded(true).then(this.getMiniState)
          })
          .catch(p => {
            this.signInIdentityUserIfNeeded(false).then(this.getMiniState)
          })
      }
    },

    setAuthProcessOn() {
      this.authProcessIsOn = true
    },

    setAuthProcessOff() {
      this.authProcessIsOn = false
    },

    setPageLoaded() {
      this.isPageLoaded = true
    },

    setPageNotLoaded() {
      this.isPageLoaded = false
    },

    onFetchUserError(err) {
      console.error(err)
      this.setAuthProcessOff()
      this.setPageLoaded()
    },

    /**
     *
     * @param {*} response - response from back on fetchUserInfo action
     */
    initializeUser(response) {
      if (!response.data) {
        return
      }

      this.setUserInfo(response.data).then(() => this.setAuthenticated(true))

      return response.data
    },

    // logout user
    logOut() {
      // for OneSignal notifications
      // this.removeExternalUserIdForPlayerId()

      localStorage.setItem('LOG_OUT_CALLBACK', this.$route.path)
      // localStorage.removeItem(TABLE_LIST_OPTIONS)

      // signOut identity or other(idGovUa cookie)
      let signOut = () => {
        if (this.isIdentityProvider) {
          this.oidcManager.signOut()
        } else {
          this.$router.push({ path: '/authSelect' })
        }
      }

      this.setAuthenticated(false)
        .then(this.setAuthProcessOn)
        .then(() => getData(env.AUTH.LOG_OUT))
        .then(() => signOut())
    },

    toggleNavMenu() {
      if (this.$q.screen.lt.md) {
        this.toggleDrawer()
      } else {
        let savedState = JSON.parse(localStorage.getItem('miniState'))
        localStorage.setItem('miniState', !savedState)
        this.getMiniState()
      }
    },

    showChangePasswordDialog() {
      this.isChangeCredentialsDialogActive = true
      this.closeRightSideMenu()
    },

    closeRightSideMenu() {
      this.isRightSideMenuActive = false
    },

    onChangeCredentialsDialogSubmit() {
      this.isChangeCredentialsDialogActive = false
    },

    signInIdentityUserIfNeeded(isIdentityAuthenticated) {
      const needSignIn = !isIdentityAuthenticated || !this.isAuthenticated

      if (needSignIn) {
        this.setAuthenticated(false)
        this.$router.push({ path: '/authSelect' })
      }
      return Promise.resolve()
    },
  },

  mounted() {
    this.onMounted()
  },

  created() {
    this.onCreated()
  },

  updated() {
    this.toggleBackBtn()
  },

  beforeRouteLeave(to, from, next) {
    if (to.name === 'callback' && from.name) {
      localStorage.setItem('goBack', false)
      this.toggleBackBtn()
      next(false)
    } else {
      next()
    }
  },
}

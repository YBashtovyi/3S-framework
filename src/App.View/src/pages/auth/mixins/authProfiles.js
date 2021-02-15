import { authenticate } from '../../../mixins/auth'
import { mapActions } from 'vuex'

import isEmpty from 'lodash.isempty'

export default {
  data() {
    return {
      selectedUser: '',
    }
  },

  props: {
    profiles: {
      required: false,
      default: () => [],
      type: Array,
    },
  },

  computed: {
    userIsSelected() {
      return !this.selectedUser
    },

    profileUserCaption() {
      return !isEmpty(this.profiles) ? this.profiles[0].userCaption : ''
    },
  },

  methods: {
    ...mapActions('baseElements', ['setAuthenticated', 'setAuthProvider', 'initUser']),

    // authenticate selected user
    setUser() {
      authenticate(this.selectedUser)
        .then(this.initCurrentUser)
        .then(this.routeToMainPage)
        .catch(this.routeToAccessDenied)
    },

    initCurrentUser() {
      const setAuthenticated = () => this.setAuthenticated(true)

      this.initUser()
        .then(setAuthenticated)
        .then(this.routeToRedirectedPage)
    },

    routeToMainPage() {
      this.$router.push({ path: process.env.REDIRECT_CALLBACK })
    },

    routeToAccessDenied() {
      this.$router.push({ path: '/accessdenied' })
    },

    notifyUserIfWarning(message) {
      this.$q.notify({
        position: 'bottom',
        timeout: 5000,
        message: message,
        type: 'warning',
      })
    },
  },
}

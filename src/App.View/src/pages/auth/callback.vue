<template>
  <q-page class="window-height window-width column justify-center items-center bg-grey-2 q-pa-md">
    <div v-if="!hasMoreThanOneProfile" class="text-center">
      <img src="~assets/login.svg" style="max-width: 80v; width: 300px" />
      <div>
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <p class="text-subtitle2 text-uppercase">Триває процес авторизації</p>
      <p class="text-subtitle2 text-uppercase">Зачекайте, будь ласка</p>
    </div>

    <auth-profiles v-if="hasMoreThanOneProfile" :profiles="users" />
  </q-page>
</template>

<script>
import AuthProfiles from './authProfiles'
import Oidc from '../../boot/security-oidc'
import authProfilesMixin from './mixins/authProfiles'
import { tryAuthenticate } from '../../mixins/auth'
import { isEmpty } from '../../utils/function-helpers'

export default {
  components: { AuthProfiles },
  mixins: [authProfilesMixin],
  data() {
    return {
      oidcClient: new Oidc(),
      users: [],
    }
  },

  mounted() {
    this.oidcClient
      .signinRedirectCallback()
      .then(this.auth)
      .catch(this.redirectToMainPage)
  },

  methods: {
    redirectToMainPage() {
      this.oidcClient.signIn()
    },

    auth() {
      // check user in api
      tryAuthenticate()
        .then(this.afterSuccesTryAuthenticate)
        .catch(this.routeToAccessDenied)
    },

    afterSuccesTryAuthenticate(data) {
      // receive single user
      if (data.authenticated) {
        this.selectedUser = data.userProfileData
        const setAuthenticated = () => this.setAuthenticated(true)
        const setIdentityAuthProvider = () => this.setAuthProvider('Identity')

        this.initUser()
          .then(setAuthenticated)
          .then(setIdentityAuthProvider)
          .then(this.routeToRedirectedPage)
          .then(this.checkIsMisTokenAlive)
      } else {
        this.users = [...data.userData]
      }
    },

    routeToRedirectedPage() {
      this.$router.push(localStorage.getItem('REDIRECT_CALLBACK'))
    },
  },

  computed: {
    hasMoreThanOneProfile() {
      return !isEmpty(this.users) && this.users.length > 1
    },
  },
}
</script>

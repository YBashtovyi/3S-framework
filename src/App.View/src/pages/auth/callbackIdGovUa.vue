<template>
  <q-page class="window-height window-width column justify-center items-center bg-grey-2 q-pa-md">
    <div class="text-center">
      <img src="~assets/login.svg" style="max-width: 80v; width: 300px" />
      <div>
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <p class="text-subtitle2 text-uppercase">Триває процес авторизації</p>
      <p class="text-subtitle2 text-uppercase">Зачекайте, будь ласка</p>
    </div>
  </q-page>
</template>

<script>
import { tryAuthenticateIdGovUa } from '../../mixins/auth'
import authProfilesMixin from './mixins/authProfiles'
export default {
  mixins: [authProfilesMixin],

  mounted() {
    this.auth()
  },

  methods: {
    auth() {
      const code = this.$route.query.code
      tryAuthenticateIdGovUa(code).then(this.afterSuccesTryAuthenticate)
    },

    afterSuccesTryAuthenticate(data) {
      // receive single user
      if (data.authenticated) {
        this.selectedUser = data.userProfileData
        const setAuthenticated = () => this.setAuthenticated(true)
        const setIdGovUaAuthProvider = () => this.setAuthProvider('IdGovUa')

        this.initUser()
          .then(setAuthenticated)
          .then(setIdGovUaAuthProvider)
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
}
</script>

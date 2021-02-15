import 'babel-polyfill'
import Oidc from 'oidc-client'
import { Dialog } from 'quasar'

const mgr = new Oidc.UserManager({
  userStore: new Oidc.WebStorageStateStore({
    store: window.localStorage,
  }),

  authority: process.env.OIDC_AUTHORITY,
  client_id: process.env.OIDC_CLIENTID,
  redirect_uri: process.env.OIDC_REDIRECT_URI,
  post_logout_redirect_uri: process.env.OIDC_POST_LOGOUT_REDIRECT_URI,
  response_type: 'id_token token',
  scope: process.env.OIDC_SCOPE,
  silent_redirect_uri: window.location.origin + process.env.OIDC_SILENT_REDIRECT_URI,
  accessTokenExpiringNotificationTime: 2,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
  clockSkew: 900,
  silentRequestTimeout: 2,
  metadata: {
    issuer: process.env.OIDC_ISSUER,
    authorization_endpoint: process.env.OIDC_AUTH_ENDPOINT,
    userinfo_endpoint: process.env.OIDC_USERINFO_ENDPOINT,
    end_session_endpoint: process.env.OIDC_END_SESSION_ENDPOINT,
    jwks_uri: process.env.OIDC_JWKS_URI,
  },
  revokeAccessTokenOnSignout: true,
})

Oidc.Log.logger = console
Oidc.Log.level = Oidc.Log.INFO

mgr.events.addUserLoaded(function(user) {
  console.log('New User Loaded：', arguments)
  console.log('Acess_token: ', user.access_token)
})

mgr.events.addAccessTokenExpiring(function() {
  console.log('AccessToken Expiring：', arguments)
})

mgr.events.addAccessTokenExpired(function() {
  console.log('AccessToken Expired：', arguments)
  // alert('Session expired. Going out!')
  Dialog.create({
    title: 'Сеанс завершено',
    message: 'Перенаправлення для аутентифікації',
  })
    .onOk(() => {
      mgr
        .signoutRedirect()
        .then(function(resp) {
          console.log('signed out', resp)
        })
        .catch(function(err) {
          console.log(err)
        })
    })
    .onCancel(function(err) {
      console.log(err)
    })
    .onDismiss(function(err) {
      mgr.signoutRedirect()
      console.log(err)
    })
})

mgr.events.addSilentRenewError(function() {
  console.error('Silent Renew Error：', arguments)
})

mgr.events.addUserSignedOut(function() {
  mgr.removeUser().then(function(resp) {
    mgr.clearStaleState().then(() => {
      // alert(resp)
      mgr.signinRedirect()
    })
  })
})

export default class SecurityService {
  constructor() {
    console.log('Construtor')
  }

  getUser() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(user => {
          if (!user) {
            self.signIn()
            return resolve(null)
          } else {
            return resolve(user)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getSignedIn() {
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(user => resolve(!!user))
        .catch(err => reject(err))
    })
  }

  signIn() {
    mgr.signinRedirect().catch(function(err) {
      // alert(err)
      console.log(err)
    })
  }

  signOut() {
    this.getSignedIn().then(isSignedIn => {
      if (isSignedIn) {
        mgr.signoutRedirect()
      }
    })
  }

  getToken() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getProfile() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.profile)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getIdToken() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.id_token)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getSessionState() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.session_state)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getAcessToken() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.access_token)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getScopes() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.scopes)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  getRole() {
    let self = this
    return new Promise((resolve, reject) => {
      mgr
        .getUser()
        .then(function(user) {
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user.profile.role)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  signinRedirectCallback() {
    const self = this
    return new Promise((resolve, reject) => {
      mgr
        .signinRedirectCallback()
        .then(user => {
          // need for debug development
          if (user == null) {
            self.signIn()
            return resolve(false)
          } else {
            return resolve(user)
          }
        })
        .catch(function(err) {
          console.log(err)
          return reject(err)
        })
    })
  }

  clearOidcState() {
    const oidcKeyWord = 'oidc'
    const storedItems = window.localStorage

    const removeObjectFromStore = item => {
      return key => {
        const currentObjectKey = item[0]
        if (currentObjectKey.includes(oidcKeyWord)) {
          localStorage.removeItem(key)
        }
      }
    }

    Object.entries(storedItems).forEach(removeObjectFromStore(storedItems)(oidcKeyWord))
  }
}

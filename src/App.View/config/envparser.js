module.exports = function() {
  let baseConfig = {}

  var ENVIRONMENT_CONFIGURATION = process.env.ENVIRONMENT_CONFIGURATION
  console.log('ENVIRONMENT_CONFIGURATION :', '\x1b[36m', ENVIRONMENT_CONFIGURATION, '\x1b[0m')

  var BASE_ID_URI
  var BASE_IPM_URI
  var BASE_IPM_API_URI
  var ONE_SIGNAL_APPLICATION_ID
  var ONE_SIGNAL_SERVER_WORKERS_SCOPE

  if (ENVIRONMENT_CONFIGURATION === 'dev') {
    BASE_ID_URI = 'https://id2.bitsoft.group'
    BASE_IPM_URI = 'https://dev-suip.bitsoft.group'
    BASE_IPM_API_URI = 'https://dev-suip.bitsoft.group/api'
    ONE_SIGNAL_APPLICATION_ID = 'b8022ab0-076b-425b-8d02-3e258b53b71d'
    ONE_SIGNAL_SERVER_WORKERS_SCOPE = '/'
  } else if (ENVIRONMENT_CONFIGURATION === 'test') {
    BASE_ID_URI = 'https://id2.bitsoft.group'
    BASE_IPM_URI = 'https://test-suip.e-transport.gov.ua'
    BASE_IPM_API_URI = 'https://test-suip.e-transport.gov.ua/api'
    ONE_SIGNAL_APPLICATION_ID = 'f551832d-44b1-48e5-95d0-e57f78d497d8'
    ONE_SIGNAL_SERVER_WORKERS_SCOPE = '/'
  } else if (ENVIRONMENT_CONFIGURATION === 'master') {
    BASE_ID_URI = 'https://id-suip.e-transport.gov.ua'
    BASE_IPM_URI = 'https://suip.e-transport.gov.ua'
    BASE_IPM_API_URI = 'https://suip.e-transport.gov.ua/api'
    // Set real data after getting it4med credentials
    ONE_SIGNAL_APPLICATION_ID = '0b35159a-59d4-479e-9cd9-4c1d7c932fa3'
    ONE_SIGNAL_SERVER_WORKERS_SCOPE = '/'
  } else {
    // ENVIRONMENT_CONFIGURATION not set. This is local build for developer
    BASE_ID_URI = 'https://id2.bitsoft.group'
    BASE_IPM_URI = 'http://localhost:8080'
    BASE_IPM_API_URI = 'http://localhost:5051/api'
    ONE_SIGNAL_APPLICATION_ID = '8869741d-ea0a-43c0-9e98-97b378ea3820'
    ONE_SIGNAL_SERVER_WORKERS_SCOPE = '/'
  }

  console.log('---------------------------------------------------')
  console.log('BASE_ID_URI:', '\x1b[36m', BASE_ID_URI, '\x1b[0m')
  console.log('BASE_IPM_URI:', '\x1b[36m', BASE_IPM_URI, '\x1b[0m')
  console.log('BASE_IPM_API_URI:', '\x1b[36m', BASE_IPM_API_URI, '\x1b[0m')
  console.log('---------------------------------------------------')

  let configuration = {
    API: BASE_IPM_API_URI,
    STATICS_URI: BASE_IPM_URI + '/public/data',
    OIDC_AUTHORITY: BASE_ID_URI,
    OIDC_CLIENTID: 'MisFront',
    OIDC_REDIRECT_URI: BASE_IPM_URI + '/#/callback#',
    OIDC_POST_LOGOUT_REDIRECT_URI: BASE_IPM_URI + '/#/authSelect',
    OIDC_RESPONSE_TYPE: 'code id_token',
    OIDC_SCOPE: 'MisFrontScope IdentityApiScope openid offline_access',
    OIDC_SILENT_REDIRECT_URI: '/silent-renew.html',
    REDIRECT_CALLBACK: '/',
    OIDC_ISSUER: BASE_ID_URI,
    OIDC_AUTH_ENDPOINT: BASE_ID_URI + '/connect/authorize',
    OIDC_USERINFO_ENDPOINT: BASE_ID_URI + '/connect/userinfo',
    OIDC_END_SESSION_ENDPOINT: BASE_ID_URI + '/connect/endsession',
    OIDC_JWKS_URI: BASE_ID_URI + '/.well-known/openid-configuration/jwks',
    ONE_SIGNAL_APPLICATION_ID: ONE_SIGNAL_APPLICATION_ID,
    ONE_SIGNAL_SERVER_WORKERS_SCOPE: ONE_SIGNAL_SERVER_WORKERS_SCOPE,
  }

  for (let key in configuration) {
    if (typeof configuration[key] === 'string') {
      baseConfig[key] = process.env[key] || configuration[key]
      if (process.env[key]) {
        let source = process.env[key] ? ' <- CHANGED ON HOST' : ''
        console.log('\x1b[36m', key, '\x1b[0m', '=', baseConfig[key], '\x1b[31m', source, '\x1b[0m')
      }
    }
  }

  let finalConfig = {
    ...baseConfig,
  }

  for (let key in finalConfig) {
    if (typeof finalConfig[key] === 'string') {
      // finalConfig[key] = JSON.stringify(finalConfig[key])
      console.log('\x1b[36m', key, '\x1b[0m', '=', finalConfig[key])
    }
  }

  return finalConfig
}

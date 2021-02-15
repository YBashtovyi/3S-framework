// Configuration for your app
let path = require('path')
// Get our env variables
const envparser = require('./config/envparser.js')

module.exports = function() {
  return {
    // app plugins (/src/boot)
    boot: [
      'axios',
      'router-oidc',
      'i18n',
      'oidc',
      'vuelidate',
      'moment',
      'addressbar-color',
      'mask',
      'vue2-google-maps',
      'fragment',
    ],

    css: ['app.styl'],

    extras: [
      'roboto-font',
      'material-icons', // optional, you are not bound to it
      // 'ionicons-v4',
      // 'mdi-v3',
      'fontawesome-v5',
      // 'eva-icons'
    ],

    framework: {
      // importStrategy: true, // --- includes everything; for dev only!

      components: [
        'QLinearProgress',
        'QLayout',
        'QHeader',
        'QBreadcrumbs',
        'QBreadcrumbsEl',
        'QUploader',
        'QDrawer',
        'QPageContainer',
        'QPage',
        'QPagination',
        'QTooltip',
        'QToolbar',
        'QToolbarTitle',
        'QIcon',
        'QList',
        'QToggle',
        'QInput',
        'QField',
        'QSelect',
        'QCheckbox',
        'QIntersection',
        'QTime',
        'QDate',
        'QBtn',
        'QBtnDropdown',
        'QBanner',
        'QForm',
        'QItem',
        'QItemSection',
        'QItemLabel',
        'QTabs',
        'QTab',
        'QTabPanels',
        'QTabPanel',
        'QTable',
        'QTh',
        'QTr',
        'QTd',
        'QMarkupTable',
        'QSpace',
        'QRouteTab',
        'QScrollArea',
        'QPageScroller',
        'QSeparator',
        'QCard',
        'QCardSection',
        'QCardActions',
        'QSlideTransition',
        'QChip',
        'QImg',
        'QDialog',
        'QAvatar',
        'QBadge',
        'QPopupProxy',
        'QPopupEdit',
        'QMenu',
        'QInnerLoading',
        'QSpinnerGears',
        'QSpinnerBall',
        'QSpinner',
        'QSpinnerDots',
        'QExpansionItem',
        'QInfiniteScroll',
        'QStepper',
        'QStep',
        'QStepperNavigation',
        'QScrollObserver',
        'QSkeleton',
        'QFab',
        'QFabAction',
        'QOptionGroup',
        'QTree',
        'QRadio',
      ],

      directives: ['Ripple', 'ClosePopup', 'Scroll'],

      // Quasar plugins
      plugins: [
        'Dialog',
        'Notify',
        'LocalStorage',
        'SessionStorage',
        'Loading',
        'LoadingBar',
        'AddressbarColor',
      ],

      // iconSet: 'ionicons-v4'
      lang: 'uk', // Quasar language

      cssAddon: true,
      animations: 'all',
      config: {
        loadingBar: {
          color: 'primary-gradient',
          size: '4px',
          position: 'top',
        },
        loading: {
          spinnerColor: 'primary',
          spinnerSize: 120,
          backgroundColor: 'grey-9',
        },
      },
    },

    build: {
      env: envparser(),
      scopeHoisting: true,
      // vueRouterMode: 'history',
      // vueCompiler: true,
      // gzip: true,
      // analyze: true,
      // extractCSS: false,
      extendWebpack(cfg) {
        cfg.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /node_modules/,
          options: {
            fix: true,
          },
        })
        cfg.resolve.alias = {
          ...cfg.resolve.alias,
          '@': path.resolve(__dirname, './src'),
          '@components': path.resolve(__dirname, './src/components'),
          '@pages': path.resolve(__dirname, './src/pages'),
          '@assets': path.resolve(__dirname, './src/assets'),
        }
        // used for manual copy some files into /dist/spa deploy directory
        // const CopyWebpackPlugin = require('copy-webpack-plugin')
        // cfg.plugins.push(
        //   new CopyWebpackPlugin([
        //     {
        //       from: path.resolve(__dirname, 'src/statics/js/OneSignal', 'OneSignalSDKWorker.js'),
        //       to: get(cfg, ['output', 'path'], '')
        //     },
        //     {
        //       from: path.resolve(__dirname, 'src/statics/js/OneSignal', 'OneSignalSDKUpdaterWorker.js'),
        //       to: get(cfg, ['output', 'path'], '')
        //     }
        //   ])
        // )
      },

      devtool: 'source-map',
    },

    devServer: {
      // https: true,
      // port: 8080,
      open: false, // opens browser window automatically
    },

    // animations: 'all', // --- includes all animations
    animations: [],

    ssr: {
      pwa: false,
    },

    pwa: {
      // workboxPluginMode: 'InjectManifest',
      // workboxOptions: {}, // only for NON InjectManifest
      manifest: {
        // name: 'Quasar App',
        // short_name: 'Quasar-PWA',
        // description: 'Best PWA App in town!',
        display: 'standalone',
        orientation: 'portrait',
        background_color: '#ffffff',
        theme_color: '#027be3',
        icons: [
          {
            src: 'public/icons/icon-128x128.png',
            sizes: '128x128',
            type: 'image/png',
          },
          {
            src: 'public/icons/icon-192x192.png',
            sizes: '192x192',
            type: 'image/png',
          },
          {
            src: 'public/icons/icon-256x256.png',
            sizes: '256x256',
            type: 'image/png',
          },
          {
            src: 'public/icons/icon-384x384.png',
            sizes: '384x384',
            type: 'image/png',
          },
          {
            src: 'public/icons/icon-512x512.png',
            sizes: '512x512',
            type: 'image/png',
          },
        ],
      },
    },

    cordova: {
      // id: 'org.cordova.quasar.app'
      // noIosLegacyBuildFlag: true // uncomment only if you know what you are doing
    },

    electron: {
      // bundler: 'builder', // or 'packager'

      extendWebpack(cfg) {
        // do something with Electron main process Webpack cfg
        // chainWebpack also available besides this extendWebpack
      },

      packager: {
        // https://github.com/electron-userland/electron-packager/blob/master/docs/api.md#options
        // OS X / Mac App Store
        // appBundleId: '',
        // appCategoryType: '',
        // osxSign: '',
        // protocol: 'myapp://path',
        // Window only
        // win32metadata: { ... }
      },

      builder: {
        // https://www.electron.build/configuration/configuration
        // appId: 'quasar-app'
      },
    },
  }
}

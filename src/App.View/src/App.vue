<template>
  <div id="q-app">
    <router-view />
  </div>
</template>

<script>

export default {
  name: 'App',

  // need to think
  created() {

     this.registerServiceWorkers()
  },

  methods: {

    registerServiceWorkers() {

      // this.registerOneSignalServiceWorker()
    },

    registerOneSignalServiceWorker() {
      
      self.addEventListener('load', () => {

        if ('serviceWorker' in navigator) {

          const serviceWorkerOptions = {
            scope: process.env.ONE_SIGNAL_SERVER_WORKERS_SCOPE
          }
          navigator.serviceWorker.register('OneSignalSDKWorker.js', serviceWorkerOptions)
        
        }
      })
    }
  }
}
</script>

<style>
</style>

<template>
  <div>
    <q-page class="window-height window-width row justify-center items-center">
      <div>
        <div class="col-12 q-mb-md">
          <q-card class="my-card" flat>
            <q-card-section horizontal>
              <div>
                <img class="col-3 q-mt-lg" style="width: 40%; height: auto;" src="~assets/logo.jpg" />
                <img class="col-3" style="max-width: 80%; height: auto;" src="~assets/logo2.png" />
              </div>
              <div></div>
              <q-card-section class="col-6">
                <div
                  class="header-content"
                >Система управління інфраструктурними будівельними проєктами</div>
              </q-card-section>
            </q-card-section>
          </q-card>
        </div>
        <!-- <q-separator /> -->
        <div class="col-12 q-mt-md">
          <q-card class="my-card" flat bordered>
            <q-card-section horizontal>
              <img src="~assets/construction_object.jpg" class="col-6" />
              <q-card-section class="col-6">
                <q-card-section>
                  <img src="~/assets/idgov_logo.svg" />
                  <p>
                    Ідентифікуватись за допомогою "Інтегрованої системи
                    електронної ідентифікації"
                  </p>

                  <q-btn @click="idGovUa" color="primary" label="Увійти"></q-btn>
                </q-card-section>
                <q-separator />
                <q-card-section>
                  <p>
                    Ідентифікуватись за допомогою системи
                    електронної ідентифікації СУІП
                  </p>

                  <q-btn @click="identity" color="primary" label="Увійти"></q-btn>
                </q-card-section>
              </q-card-section>
            </q-card-section>
          </q-card>
        </div>
        <!-- <q-separator /> -->
        <div class="col-12">
          <q-card class="my-card" flat>
            <q-card-section>
              <div>
                <div class="row footer-content">
                  Портал працює в режимі дослідної експлуатації,
                  якщо у вас є зауваження, надсилайте їх за адресою &nbsp;
                  <a
                    class="text-grey-9"
                    href="mailto:support@e-transport.gov.ua"
                  >support@e-transport.gov.ua</a>
                </div>
              </div>
            </q-card-section>
          </q-card>
        </div>
      </div>
    </q-page>
  </div>
</template>

<script>
import Oidc from '../../boot/security-oidc'
import { AUTH } from '../../utils/axios-env-config'
import { getData } from '../../services/api'
import UrlBuilder from '../../utils/url-builder'
export default {
  data() {
    return {
      oidcManager: new Oidc(),
    }
  },

  computed: {
    isAuthenticated() {
      const isAutheticated = JSON.parse(localStorage.getItem('suip.authenticated'))
      return isAutheticated
    },
  },

  methods: {
    identity() {
      this.oidcManager.signIn()
    },

    idGovUa() {
      const url = new UrlBuilder({ host: AUTH.AUTH_ID_GOV_UA }).build()
      getData(url).then(p => {
        window.location.href = p.data
      })
    },
  },

  created() {
    if(this.isAuthenticated) {
      this.$router.push('/dashboard')
    }
  }
}
</script>

<style lang="sass" scoped>
.my-card
  width: 100%
  max-width: 800px

.header-content
  font-size: 30px
  font-family: e-Ukraine, -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji"
  font-weight: 400
  line-height: 40px

.footer-content
  font-size: 13px
</style>
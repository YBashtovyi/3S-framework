<template>
  <div>
    <q-layout view="hHh lpR fFf" class="bg-grey-1">
      <q-header elevated class="bg-white text-grey-8 q-py-xs" height-hint="58">
        <q-toolbar>
          <!-- toggle drawer btn -->
          <q-btn
            v-if="isAuthenticated && !authProcessIsOn && isPageLoaded"
            flat
            round
            dense
            :icon="miniState || $q.screen.lt.md ? 'menu' :'menu_open'"
            @click="toggleNavMenu"
          />

          <q-separator vertical inset spaced />

          <div class="row q-ml-sm">
            <div class="col-3">
              <img class="q-mr-md" style="max-width: 25%; height: auto;" src="~assets/logo.jpg" />
              <img style="max-width: 65%; height: auto;" src="~assets/logo2.png" />
            </div>

            <div class="col-8 row items-center">
              <div class="text-bold text-h6">Система управління інфраструктурними проєктами</div>
            </div>
          </div>

          <q-space />

          <q-separator vertical inset />

          <q-btn-dropdown
            v-if="!authProcessIsOn"
            unelevated
            rounded
            icon="fas fa-user-circle"
            :label="userName"
            class="text-grey-8"
            v-model="isRightSideMenuActive"
          >
            <q-list>
              <q-item v-if="isAuthenticated" clickable @click="showChangePasswordDialog">
                <q-item-section side class="q-pl-sm">
                  <q-icon name="lock_open" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Змінити логін/пароль</q-item-label>
                </q-item-section>
              </q-item>
              <q-item clickable v-close-popup @click="logOut">
                <q-item-section side class="q-pl-sm">
                  <q-icon name="exit_to_app" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{isAuthenticated ? 'Вийти' : 'Увійти'}}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </q-toolbar>
      </q-header>

      <!-- navigation drawer -->
      <drawer v-if="isAuthenticated && isPageLoaded" />

      <q-page-container v-if="isAuthenticated && isPageLoaded">
        <q-breadcrumbs
          v-if="breadcrumbs"
          class="text-grey-8 q-px-md q-mt-sm text-uppercase text-caption text-weight-light"
          active-color="primary"
          gutter="xs"
        >
          <q-btn
            v-if="isBackBtn && breadcrumbs.length > 1"
            flat
            dense
            rounded
            icon="arrow_back"
            @click="$router.go(-1)"
            class="q-ma-none q-mt-xs"
          >
            <q-tooltip
              anchor="center right"
              self="center left"
              :offset="[10, 10]"
              content-style="font-size: 13px"
            >Назад</q-tooltip>
          </q-btn>
          <q-breadcrumbs-el
            v-for="(item, index) in breadcrumbs"
            :key="index"
            :label="item.name"
            :to="item.path"
          />
        </q-breadcrumbs>
        <router-view />
      </q-page-container>
      <div v-if="authProcessIsOn" class="text-center">
        <img src="~assets/login.svg" style="max-width: 80vh; width: 300px" />
        <div>
          <q-spinner-dots color="primary" size="3em" />
        </div>
        <p class="text-subtitle2 text-uppercase">Триває процес авторизації</p>
        <p class="text-subtitle2 text-uppercase">Зачекайте, будь ласка</p>
      </div>
      <!-- Start of "Change password dialog window" -->
      <change-credentials-dialog
        v-if="isChangeCredentialsDialogActive"
        @dialogSubmitedEvent="onChangeCredentialsDialogSubmit"
        :isDialogVisible.sync="isChangeCredentialsDialogActive"
      />
      <!-- End of "Change password dialog window" -->
    </q-layout>
  </div>
</template>

<script>
import ChangeCredentialsDialog from '../dialogs/ChangeCredentialsDialog'
import baseLayout from './baseLayout'

export default {
  components: {
    ChangeCredentialsDialog,
  },
  mixins: [baseLayout],
}
</script>

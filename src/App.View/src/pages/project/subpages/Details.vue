<template>
  <q-card flat bordered>
    <q-tabs
      inline-label
      indicator-color="transparent"
      active-class="text-grey-1"
      active-color="white bg-primary"
      class="bg-transparent text-primary q-mr-xl"
      align="left"
      no-caps
    >
      <q-route-tab label="Основні дані" icon="far fa-id-card" exact :to="{ path: `info` }" />
      <q-route-tab label="Учасники" icon="groups" exact :to="{ path: 'prjParticipant' }" />
      <q-route-tab
        label="Деталі контрактів"
        icon="fa fa-calculator"
        exact
        :to="{ path: 'prjContract' }"
      />
      <q-route-tab
        label="Документи проєкту"
        icon="fa fa-folder-open"
        exact
        :to="{ path: 'prjWorkSchedule' }"
      />
      <q-route-tab label="Фотозвіти" icon="fa fa-camera" exact :to="{ path: 'prjPhotoReport' }" />
      <q-route-tab label="Мапа" icon="fa fa-map-pin" exact :to="{ path: 'map' }" />
      <q-route-tab label="Вміст" icon="fa fa-paperclip" exact :to="{ path: 'file' }" />
    </q-tabs>
    <q-separator />
    <transition appear enter-active-class="animated fadeIn">
      <router-view></router-view>
    </transition>
  </q-card>
</template>

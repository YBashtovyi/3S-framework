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
      <q-route-tab label="Етапи" icon="fa fa-list-ol" exact :to="{ path: `stage` }" />
      <q-route-tab label="Види робіт" icon="fa fa-cubes" exact :to="{ path: `subType` }" />
      <q-route-tab label="Вміст" icon="fa fa-paperclip" exact :to="{ path: 'file' }" />
    </q-tabs>
    <q-separator />
    <transition appear enter-active-class="animated fadeIn">
      <router-view></router-view>
    </transition>
  </q-card>
</template>

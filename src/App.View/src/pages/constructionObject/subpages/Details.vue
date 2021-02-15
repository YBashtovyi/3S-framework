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
      <q-route-tab label="Основні дані" icon="fa fa-object-ungroup" exact :to="{ path: `info` }" />
      <q-route-tab label="Проєкти" icon="architecture" exact :to="{ path: 'projects' }" />
      <q-route-tab label="Мапа" icon="fa fa-map-pin" exact :to="{ path: 'map' }" />
    </q-tabs>
    <q-separator />
    <transition appear enter-active-class="animated fadeIn">
      <router-view></router-view>
    </transition>
  </q-card>
</template>

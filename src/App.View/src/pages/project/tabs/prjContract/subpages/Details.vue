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
      <q-route-tab
        label="Додаткові угоди"
        icon="add"
        exact
        :to="{ path: 'prjAdditionalAgreement' }"
      />
      <q-route-tab label="Вміст" icon="fa fa-paperclip" exact :to="{ path: 'file' }" />
    </q-tabs>
    <q-separator />
    <transition appear enter-active-class="animated fadeIn">
      <router-view></router-view>
    </transition>
  </q-card>
</template>

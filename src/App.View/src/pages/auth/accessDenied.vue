<template>
  <q-page>
    <div class="fixed-center text-center">
      <img src="~assets/admin.svg" style="max-width: 80vw; width: 400px" />
      <p class="text-subtitle2 text-uppercase">У доступі відмовлено</p>
      <p class="text-subtitle2 text-uppercase">Зверніться до адміністратора</p>
      <q-btn @click="logout">
        <div>Назад</div>
      </q-btn>
    </div>
  </q-page>
</template>
<script>
export default {
  mounted() {
    this.$oidc.removeUser();
  },
  methods: {
    logout() {
      this.$oidc.signoutRedirect()
    }
  }
};
</script>

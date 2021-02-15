<template>
  <div class="fixed-center text-center">
    <!-- <i class="icon ico-404 q-mb-xl"></i> -->
    <img src="~assets/not-found.svg" style="max-width: 80vw; width: 480px" />
    <p class="text-subtitle2 text-uppercase">Отакої! Сторінку не знайдено</p>
    <q-btn flat size="1em" color="primary" @click="$router.go(-1)">
      <q-icon left size="1em" name="arrow_back" />Назад
    </q-btn>
  </div>
</template>

<script>
export default {
  name: "Error404"
};
</script>

<style lang="stylus" scoped>
.ico-404 {
  font-size: 346px;
  display: inline-block;
  gradient(1);
  transition: $transition;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;

  @media (max-width: $breakpoint-md) {
    font-size: 246px;
  }

  @media (max-width: $breakpoint-xs) {
    font-size: 186px;
  }
}
</style>

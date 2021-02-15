<template>
  <div>
    <q-tabs
      v-model="activeItem"
      active-color="primary"
      indicator-color="primary"
      active-class="primary"
      align="left"
      class="text-grey-8 q-pr-xl"
      no-caps
      inline-label
    >
      <q-tab
        v-for="(tabItem, index) in tabList"
        :key="index"
        :name="tabItem.name"
        :label="tabItem.label"
        :icon="tabItem.icon"
      />
    </q-tabs>
  </div>
</template>

<script>
export default {
  data() {
    return {
      activeItem: ''
    }
  },
  props: {
    tabList: Array
  },
  methods: {


  },
  watch: {
    activeItem: function () {
      this.$emit('changeTab')
    }
  },
  created() {
    this.activeItem = this.tabList[0].name
    if (this.tabList) {
      this.activeItem = this.tabList[0].name
    }
  },
  mounted() {

  },
  computed: {

  }
}
</script>

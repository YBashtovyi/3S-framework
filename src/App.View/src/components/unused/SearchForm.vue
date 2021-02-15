<template>
  <!-- search form template -->
  <div class="row no-wrap justify-between items-center q-mb-md full-width">
    <div class="fit q-mr-md">
      <q-form>
        <q-input dense filled type="search" label="Пошук" color="primary" v-model="search" disable>
          <template v-slot:append>
            <q-btn round dense flat icon="search" />
          </template>
        </q-input>
      </q-form>
    </div>
    <q-btn round size="13px" color="primary" icon="add" :to="createPageLink" :disable="!createBtn">
      <q-tooltip
        anchor="center left"
        self="center right"
        :offset="[10, 10]"
        content-style="font-size: 13px"
      >Створити</q-tooltip>
    </q-btn>
  </div>
</template>

<script>
export default {
  data() {
    return {
      search: ""
    };
  },
  props: {
    createBtn: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    createPageLink: function() {
      return this.$route.path + "/create";
    }
  }
};
</script>

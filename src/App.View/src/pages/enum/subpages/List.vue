<template>
  <div class="q-pa-md">
    <!-- search form -->
    <search :config="searchConfig" :createBtn="false" :withSave="true" />

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :fileFormats="fileFormats"
      :actions="actions"
      :visibleColumns="visibleColumns"
      :headerButtons="tableHeaderButtons"
      :withSave="true"
    />
  </div>
</template>

<script>
import enumList from '../mixins/enumList'
import search from '../../../components/search/search'
import tableList from '../../../components/table/tableList'

export default {
  mixins: [enumList],
  components: {
    tableList,
    search,
  },
}
</script>

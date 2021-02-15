<template>
  <div>
    <!-- search form -->
    <search :config="searchConfig" :createBtn="false" :withSave="true" />

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :headerButtons="tableHeaderButtons"
      :fileFormats="fileFormats"
      :actions="actions"
      :withSave="true"
    />
  </div>
</template>

<script>
import personList from '../mixins/personList'
import search from '../../../components/search/search'
import tableList from '../../../components/table/tableList'

export default {
  mixins: [personList],
  components: {
    tableList,
    search,
  },
}
</script>

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
      :withSave="true"
      :fileFormats="fileFormats"
      :actions="actions"
      :customRowColor="setRowColor"
    />
  </div>
</template>

<script>
import projectList from '../mixins/projectList'
import search from '../../../components/search/search'
import tableList from '../../../components/table/tableList'

export default {
  mixins: [projectList],
  components: {
    tableList,
    search,
  },
}
</script>


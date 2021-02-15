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
      :actions="actions"
      :withSave="true"
    />
  </div>
</template>

<script>
import roleList from '../mixins/roleList'
import search from '../../../../components/search/search'
import tableList from '../../../../components/table/tableList'

export default {
  mixins: [roleList],
  components: {
    tableList,
    search,
  },
}
</script>

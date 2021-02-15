<template>
  <div>
    <!-- search form -->
    <search class="q-ma-md" :config="searchConfig" :createBtn="false" :withSave="true" />

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :headerButtons="tableHeaderButtons"
      :actions="actions"
      :flat="true"
      :withSave="true"
    />
  </div>
</template>

<script>
import orgUnitStaffList from '../mixins/orgUnitStaffList'
import search from '../../../../../components/search/search'
import tableList from '../../../../../components/table/tableList'

export default {
  mixins: [orgUnitStaffList],
  components: {
    tableList,
    search,
  },
}
</script>

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
      :withSave="true"
    />
  </div>
</template>

<script>
import prjWorkScheduleStageList from '../mixins/prjWorkScheduleStageList'
import search from '../../../../../../../components/search/search'
import tableList from '../../../../../../../components/table/tableList'

export default {
  mixins: [prjWorkScheduleStageList],
  components: {
    tableList,
    search,
  },
}
</script>

<template>
  <div>
    <!-- search form -->
    <search class="q-ma-md" :config="searchConfig" :createBtn="false" :withSave="true" />

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :actions="actions"
      :customRowColor="setRowColor"
      :withSave="true"
    />
  </div>
</template>

<script>
import projectList from './mixins/projectConstructionObjectList'
import search from '../../../../components/search/search'
import tableList from '../../../../components/table/tableList'

export default {
  mixins: [projectList],
  components: {
    tableList,
    search,
  },
}
</script>

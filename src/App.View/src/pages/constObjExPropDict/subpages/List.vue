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
      :withSave="true"
      :actions="actions"
    />
  </div>
</template>

<script>
import constObjExPropDictList from '../mixins/constObjExPropDictList'
import search from '../../../components/search/search'
import tableList from '../../../components/table/tableList'

export default {
  mixins: [constObjExPropDictList],
  components: {
    tableList,
    search,
  },
}
</script>
<template>
  <div>
    <!-- search form -->
    <search :config="searchConfig" :createBtn="false" :withSave="true" />

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :withSave="true"
      :actions="actions"
    />
  </div>
</template>

<script>
import prjContractList from '../../project/tabs/prjContract/mixins/prjContractList'
import search from '../../../components/search/search'
import tableList from '../../../components/table/tableList'
import { env } from '../../../services'

export default {
  mixins: [prjContractList],
  components: {
    tableList,
    search,
  },

  methods: {
    detailsPage(row) {
      let routeParams
      if (row.docType === 'Contract') {
        routeParams = {
          path: `prjContract/details/${row.id}`,
        }
      } else {
        routeParams = {
          path: `prjContract/details/${row.parentId}/prjAdditionalAgreement/details/${row.id}`,
        }
      }

      this.$router.push(routeParams)
    },
  },

  computed: {
    requestUrl() {
      return env.PROJECT_CONTRACT.PATH
    },

    actions() {
      return [
        {
          type: 'detailFn',
          label: 'Переглянути',
          icon: 'fas fa-eye',
          fn: row => this.detailsPage(row),
          visible: true,
        },
        {
          type: 'rowFn',
          fn: data => this.detailsPage(data.row),
        },
        {
          type: 'deleteFn',
          label: 'Видалити',
          icon: 'delete',
          fn: row => this.deleteDialog(row),
          visible: true,
        },
      ]
    },
  },
}
</script>


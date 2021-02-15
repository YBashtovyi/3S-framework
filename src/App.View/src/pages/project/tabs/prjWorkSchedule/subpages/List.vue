<template>
  <div>
    <!-- search form -->
    <search class="q-ma-md" :config="searchConfig" :createBtn="false" :withSave="true" />

    <div class="row items-center justify-end q-my-md q-px-md">
      <div class="row">
        <q-btn-dropdown color="positive" label="Створення документу" class="on-right">
          <q-list>
            <q-item clickable v-close-popup @click="onCreatePrjWorkSchedule">
              <q-item-section>Календарний план</q-item-section>
            </q-item>
            <q-item clickable v-close-popup @click="onCreatePrjChangesToWS">
              <q-item-section>Зміни календарного плану</q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
      </div>
    </div>

    <!-- global list table -->
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :actions="actions"
      :withSave="true"
    />
  </div>
</template>

<script>
import prjWorkScheduleList from '../mixins/prjWorkScheduleList'
import search from '../../../../../components/search/search'
import tableList from '../../../../../components/table/tableList'

export default {
  mixins: [prjWorkScheduleList],
  components: {
    tableList,
    search,
  },
}
</script>

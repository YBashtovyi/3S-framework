<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card>
        <detail-title
          :label="orgUnitStaffTitle.label"
          :icon="orgUnitStaffTitle.icon"
          :content="orgUnitStaffTitle.content"
        />
        <div class="q-pa-md">
          <q-input :value="details.personFullName" label="ПІБ" readonly></q-input>
          <q-input :value="details.orgUnitPositionName" label="Посада" readonly></q-input>
          <q-input :value="details.startDate | asDate(dateFormat)" label="Дата початку роботи на посаді" readonly></q-input>
          <q-input :value="details.endDateFront | asDate(dateFormat)" label="Дата припинення роботи на посаді" readonly></q-input>
        </div>
        
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../../components/baseElements/detailTitle'
import orgUnitStaffDetails from '../mixins/orgUnitStaffDetails'


export default {
  mixins: [orgUnitStaffDetails],

  components: {
    detailTitle,
  },
}
</script>
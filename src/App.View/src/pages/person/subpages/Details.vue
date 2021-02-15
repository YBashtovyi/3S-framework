<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card>
        <detail-title :label="enumTitle.label" :icon="enumTitle.icon" :content="enumTitle.content" />
        <div class="q-pa-md">
          <q-input :value="details.lastName" label="Прізвище" readonly></q-input>
          <q-input :value="details.firstName" label="Ім'я" readonly></q-input>
          <q-input :value="details.middleName" label="По батькові" readonly></q-input>
          <q-input :value="details.birthday | asDate(dateFormat)" label="Дата народження" readonly></q-input>
          <q-input :value="details.genderName" label="Стать" readonly></q-input>
          <q-input
            :value="details.taxNumber"
            label="Індивідуальний податковий номер (ІПН)"
            readonly
          ></q-input>
          <q-input
            :value="details.identityDocumentName"
            label="Документ, що посвідчує особу"
            readonly
          ></q-input>
        </div>
        <person-extended-property-list :personId="details.id" />
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import personDetails from '../mixins/personDetails'

import PersonExtendedPropertyList from '../components/PersonExtendedPropertyList'

export default {
  mixins: [personDetails],

  components: {
    detailTitle,
    PersonExtendedPropertyList,
  },
}
</script>
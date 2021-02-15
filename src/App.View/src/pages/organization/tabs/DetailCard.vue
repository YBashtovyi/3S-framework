<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="organizationTitle.label"
          :icon="organizationTitle.icon"
          :content="organizationTitle.content"
        />
        <div class="q-pa-md">
          <q-input dense :value="details.name" label="Назва організації" readonly class="q-mb-md"></q-input>
          <q-input
            dense
            :value="details.fullName"
            label="Повна назва організації"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input
            dense
            :value="details.organizationCategoryName"
            label="Категорія організації"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input dense :value="details.edrpou" label="ЄДРПОУ" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.orgStateName" label="Стан" readonly></q-input>
        </div>

        <org-unit-position-list :orgUnitId="organizationId" />
        <org-unit-extended-property-list :orgUnitId="organizationId" />
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import organizationDetails from '../mixins/organizationDetails'

import OrgUnitPositionList from '../components/OrgUnitPositionList'
import OrgUnitExtendedPropertyList from '../components/OrgUnitExtendedPropertyList.vue'

export default {
  mixins: [organizationDetails],

  components: {
    detailTitle,
    OrgUnitPositionList,
    OrgUnitExtendedPropertyList,
  },
}
</script>
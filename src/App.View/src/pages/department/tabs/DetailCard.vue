<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="departmentTitle.label"
          :icon="departmentTitle.icon"
          :content="departmentTitle.content"
        />
        <div class="q-pa-md">
          <q-field
            dense
            label="Підпорядкування"
            stack-label
            readonly
            class="q-mb-md q-mr-md"
            v-if="details.parentId"
          >
            <router-link
              :to="`/organization/details/${details.parentId}`"
              class="text-primary"
              title="Перейти на картку підпорядкування"
            >{{ details.parentName }}</router-link>
            <template v-slot:before>
              <q-icon name="fa fa-building" size="20px" />
            </template>
          </q-field>
          <q-input dense :value="details.name" label="Назва підрозділу" readonly class="q-mb-md"></q-input>
          <q-input
            dense
            :value="details.fullName"
            label="Повна назва підрозділу"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input dense :value="details.code" label="Код" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.departmentTypeName" label="Тип" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.departmentStateName" label="Стан" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.description" label="Опис" readonly class="q-mb-md"></q-input>
        </div>

        <!-- <org-unit-position-list :orgUnitId="organizationId" />
        <org-unit-extended-property-list :orgUnitId="organizationId" />-->
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import departmentDetails from '../mixins/departmentDetails'

//  import OrgUnitPositionList from '../components/OrgUnitPositionList'
//  import OrgUnitExtendedPropertyList from '../components/OrgUnitExtendedPropertyList.vue'

export default {
  mixins: [departmentDetails],

  components: {
    detailTitle,
    // OrgUnitPositionList,
    // OrgUnitExtendedPropertyList,
  },
}
</script>
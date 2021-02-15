<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat bordered>
        <detail-title
          :label="workSubTypeTitle.label"
          :icon="workSubTypeTitle.icon"
          :content="workSubTypeTitle.content"
        />
        <div class="q-pa-md">
          <q-input :value="details.code" label="Код" readonly></q-input>
          <q-input :value="details.name" label="Назва" readonly></q-input>
          <q-input :value="details.classifierTypeName" label="Тип класифікатора" readonly></q-input>
          <q-input :value="measurementUnitNameAndValue" label="Одиниця виміру" readonly></q-input>
          <q-input :value="parentCodeAndName" label="Підпорядкування" readonly></q-input>
          <q-checkbox :value="details.isActive" readonly label="Активний" />
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import workSubTypeDetails from '../mixins/workSubTypeDetails'

export default {
  mixins: [workSubTypeDetails],

  components: {
    detailTitle,
  },
}
</script>
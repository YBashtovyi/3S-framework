<template>
  <div>
    <q-card>
      <div v-if="!pageReady" class="text-center">
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <q-form v-else @submit="onSubmit">
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="editData.stageNumber"
                dense
                label="№ етапу *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="editData.stageName"
                dense
                label="Назва *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="editData.beginDate = $event"
                label="Дата початку"
                :required="true"
                :value="editData.beginDate"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="editData.endDate = $event"
                label="Дата закінчення"
                :required="true"
                :value="editData.endDate"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="editData.cost"
                dense
                label="Кошторисна вартість, тис. грн. *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
          </div>
          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path: '/projects' }"></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import prjWorkScheduleStageEdit from '../mixins/prjWorkScheduleStageEdit'
import DatePicker from '../../../../../../../components/forms/datepicker'

export default {
  mixins: [prjWorkScheduleStageEdit],

  components: {
    DatePicker,
  },
}
</script>

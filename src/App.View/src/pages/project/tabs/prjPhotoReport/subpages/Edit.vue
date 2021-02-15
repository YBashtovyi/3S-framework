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
              <f-select
                dense
                label="Тип документу"
                required
                :value="selectedDocType"
                @input="onDocTypeChanged"
                :options="docTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
                disabled
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Статус документу"
                required
                :value="selectedDocState"
                @input="onDocStateChanged"
                :options="docStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="prjPhotoReport.regDate = $event"
                label="Дата документу"
                :required="true"
                :value="prjPhotoReport.regDate"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="prjPhotoReport.regNumber"
                dense
                label="№ документу *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип фіксації даних"
                required
                :value="selectedFixationType"
                @input="onFixationTypeChanged"
                :options="fixationTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Стан фіксації"
                required
                :value="selectedFixationState"
                @input="onFixationStateChanged"
                :options="fixationStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Відповідальна особа"
                required
                :value="selectedEmployee"
                @input="onEmployeeChanged"
                :options="employees"
                optionLabel="employeeFullName"
                optionValue="id"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="prjPhotoReport.description"
                dense
                label="Коментар"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
          </div>
          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path:'/projects' } "></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import prjPhotoReportEdit from '../mixins/prjPhotoReportEdit'

import DatePicker from '../../../../../components/forms/datepicker'

export default {
  mixins: [prjPhotoReportEdit],
  components: {
    DatePicker,
  },
}
</script>
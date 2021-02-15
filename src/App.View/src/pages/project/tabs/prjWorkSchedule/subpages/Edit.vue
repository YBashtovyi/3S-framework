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
              <date-picker
                @updateData="editData.regDate = $event"
                label="Дата документу"
                :required="true"
                :value="editData.regDate"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="editData.regNumber"
                dense
                label="№ документу *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип документу"
                required
                :value="selectedDocumentType"
                @input="onDocumentTypeChanged"
                :options="documentTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
                :disable="true"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Статус документу"
                required
                :value="selectedDocumentState"
                @input="onDocumentStateChanged"
                :options="documentStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>

            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                v-model="editData.description"
                label="Коментар"
                class="q-mb-md q-mr-md"
              />
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
import prjWorkScheduleEdit from '../mixins/prjWorkScheduleEdit'
import DatePicker from '../../../../../components/forms/datepicker'

export default {
  mixins: [prjWorkScheduleEdit],

  components: {
    DatePicker,
  },
}
</script>

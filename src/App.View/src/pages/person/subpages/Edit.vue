<template>
  <div>
    <q-card :flat="!header">
      <div v-if="!pageReady" class="text-center">
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <q-form v-else @submit="onSubmit">
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="person.lastName"
                dense
                label="Прізвище *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="person.firstName"
                dense
                label="Ім'я *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="person.middleName"
                dense
                label="По батькові *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="person.birthday = $event"
                label="Дата народження"
                :value="person.birthday"
                class="q-mb-md q-ml-md"
              />
            </div>
          </div>
          <div v-if="footer" class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path: '/person' }"></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import personEdit from '../mixins/personEdit'

import DatePicker from '../../../components/forms/datepicker'

export default {
  mixins: [personEdit],
  components: {
    DatePicker,
  },

  props: {
    header: {
      type: Boolean,
      default: true,
    },
    footer: {
      type: Boolean,
      default: true,
    },
  },
}
</script>

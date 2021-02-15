<template>
  <div>
    <q-card>
      <div v-if="!pageReady" class="text-center">
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <q-form v-else @submit="onSubmit">
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="position.name"
                dense
                label="Назва *"
                class="q-mb-md"
              ></q-input>
            </div>
            <div class="col-md-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="position.code"
                dense
                label="Код *"
                class="q-mb-md"
              ></q-input>
            </div>
            <div class="col-md-12">
              <q-input
                v-model="position.caption"
                dense
                label="Пояснення"
                class="q-mb-md"
              ></q-input>
            </div>
          </div>
          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path: '/position' }"></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import positionEdit from '../mixins/positionEdit'

export default {
  mixins: [positionEdit],
}
</script>

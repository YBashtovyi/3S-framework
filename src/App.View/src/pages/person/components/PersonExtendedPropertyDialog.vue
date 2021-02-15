<template>
  <div>
    <q-dialog persistent v-model="isVisibleDialog">
      <q-card style="width: 500px; max-width: 80vw;">
        <q-card-section>
          <div class="text-h6">Додати дані</div>
        </q-card-section>
        <div v-if="!pageReady" class="text-center">
          <q-spinner-dots color="primary" size="3em" />
        </div>
        <q-form v-else @submit="onSubmit">
          <q-card-section class="q-pt-none">
            <f-select
              dense
              label="Тип додаткових даних"
              required
              :value="selectedPersonExtendedProperty"
              @input="onSelectPersonExtendedProperty"
              :options="personExtendedProperties"
              optionLabel="name"
              optionValue="code"
              class="q-mb-md"
            />
            <q-input
              :rules="[val => !!val || 'Обов\'язкове поле']"
              dense
              class="q-mb-md"
              v-model="editData.value"
              label="Значення *"
            ></q-input>
          </q-card-section>

          <q-card-actions align="right" class="bg-white text-teal">
            <q-btn @click="onCloseDialog" flat label="Відміна" />
            <q-btn type="submit" flat label="Зберегти" />
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import personExtendedPropertyDialog from '../mixins/personExtendedPropertyDialog'
export default {
  mixins: [personExtendedPropertyDialog],
}
</script>

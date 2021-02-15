<template>
  <div>
    <q-dialog persistent v-model="isVisible">
      <q-card style="width: 700px; max-width: 80vw;">
        <q-card-section>
          <div class="text-h6">Додати штатну одиницю</div>
        </q-card-section>
        <div v-if="!pageReady" class="text-center">
          <q-spinner-dots color="primary" size="3em" />
        </div>
        <q-form v-else @submit="onSubmit">
          <q-card-section class="q-pt-none">
            <f-select
              dense
              label="Посада"
              required
              :value="selectedPosition"
              @input="onSelectPosition"
              :options="positions"
              optionLabel="name"
              optionValue="id"
              class="q-mb-md"
            />
            <q-input
              dense
              class="q-mb-md"
              v-model="editData.staffUnitCount"
              label="Кількість штатних одиниць посади"
            ></q-input>
            <q-input dense class="q-mb-md" v-model="editData.description" label="Опис"></q-input>
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
import orgUnitPositionEditDialog from '../mixins/orgUnitPositionEditDialog'
export default {
  mixins: [orgUnitPositionEditDialog],
}
</script>

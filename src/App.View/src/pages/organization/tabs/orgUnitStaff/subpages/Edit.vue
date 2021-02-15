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
              <div class="row">
                <div class="col-md-10">
                  <lazy-autocomplete
                    :label="'Співробітник'"
                    :icon="'person'"
                    :minFilterChars="3"
                    :value="selectedOrgEmployee"
                    :url="orgEmployeeRequest"
                    :optionLabel="`personFullName`"
                    hideDropdownIcon
                    @input="onOrgEmployeeChanged"
                    clearable
                  />
                </div>
                <div class="col-md-2">
                  <q-btn
                    @click="onCreatePerson"
                    color="positive"
                    icon="add"
                    flat
                    label="Створити"
                  />
                </div>
              </div>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Посада"
                required
                :value="selectedOrgUnitPosition"
                @input="onOrgPositionChanged"
                :options="orgUnitPositions"
                optionLabel="positionName"
                optionValue="id"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="editData.startDate = $event"
                label="Дата початку роботи на посаді"
                :value="editData.startDate"
                :required="true"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="editData.endDate = $event"
                label="Дата припинення роботи на посаді"
                :value="editData.endDate"
                class="q-mb-md q-ml-md"
              />
            </div>
          </div>
          <div class="row justify-end">
            <q-btn
              type="reset"
              color="negative"
              flat
              label="Відмінити"
              :to="{ path: '/organization' }"
            ></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
    <create-person-dialog
      :isVisibleDialog.sync="isVisibleCreatePersonDialog"
      :addNewPerson.sync="person"
    />
  </div>
</template>

<script>
import orgUnitStaffEdit from '../mixins/orgUnitStaffEdit'
import DatePicker from '../../../../../components/forms/datepicker'
import LazyAutocomplete from '../../../../../components/forms/LazyAutocomplete'
import CreatePersonDialog from '../../../../../components/dialogs/createPerson'

export default {
  mixins: [orgUnitStaffEdit],

  components: {
    DatePicker,
    LazyAutocomplete,
    CreatePersonDialog,
  },
}
</script>

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
                v-model="workSubType.name"
                dense
                label="Назва *"
                :rules="[val => !!val || 'Обов\'язкове поле']"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="workSubType.code"
                dense
                label="Код *"
                :rules="[val => !!val || 'Обов\'язкове поле']"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип класифікатора"
                required
                :value="selectedClassifierType"
                @input="onClassifierTypeChanged"
                :options="classifierTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Одиниця виміру"
                required
                :value="selectedMeasurementUnit"
                @input="onMeasurementUnitChanged"
                :options="measurementUnits"
                optionLabel="nameAndValue"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Підпорядкування"
                clearable
                :value="selectedParent"
                @input="onWorkSubTypeParentChanged"
                :options="workSubTypeParents"
                optionLabel="codeAndName"
                optionValue="id"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-checkbox v-model="workSubType.isActive" label="Активний" class="q-mb-md q-ml-xs" />
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
import workSubTypeEdit from '../mixins/workSubTypeEdit'

export default {
  mixins: [workSubTypeEdit],
}
</script>
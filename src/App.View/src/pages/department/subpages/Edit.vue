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
                label="Підпорядкування"
                :value="selectedOrganization"
                @input="onOrgUnitChanged"
                :options="organizationList"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="department.name"
                dense
                label="Назва організації *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="department.fullName"
                dense
                label="Повна назва організації *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="department.code"
                dense
                label="Код *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип"
                required
                :value="selectedDepartmentType"
                @input="onDepartmentTypeChanged"
                :options="departmentTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Стан"
                required
                :value="selectedDepartmentState"
                @input="onDepartmentStateChanged"
                :options="departmentStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input v-model="department.description" dense label="Опис" class="q-mb-md q-mr-md"></q-input>
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
  </div>
</template>

<script>
import departmentEdit from '../mixins/departmentEdit'

export default {
  mixins: [departmentEdit],
}
</script>

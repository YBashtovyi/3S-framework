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
                v-model="organization.name"
                dense
                label="Назва організації *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="organization.fullName"
                dense
                label="Повна назва організації *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="organization.edrpou"
                dense
                label="ЄДРПОУ *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Категорія організації"
                required
                :value="selectedOrganizationCategory"
                @input="onOrganizationCategory"
                :options="organizationCategories"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Організаційно правова форма"
                required
                :value="selectedOrgType"
                @input="onOrgTypeChanged"
                :options="orgTypes"
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
                :value="selectedOrgState"
                @input="onOrgStateChanged"
                :options="orgStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
          </div>
          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path: '/organization' }"></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import organizationEdit from '../mixins/organizationEdit'

export default {
  mixins: [organizationEdit],
}
</script>

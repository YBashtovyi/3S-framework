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
                v-model="object.code"
                dense
                label="Код"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Статус"
                required
                :value="selectedObjectStatus"
                @input="onObjectStatusChanged"
                :options="objectStatuses"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="object.name"
                dense
                label="Назва"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="object.fullName"
                dense
                label="Повна назва"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип об'єкту"
                required
                :value="selectedTypeOfConstructionObject"
                @input="onTypeOfConstructionObjectChanged"
                :options="typesOfConstructionObject"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Клас наслідків"
                required
                :value="selectedClassOfConsequence"
                @input="onClassOfConsequenceChanged"
                :options="classesOfConsequence"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input v-model="object.description" dense label="Коментар" class="q-mb-md q-mr-md"></q-input>
            </div>
          </div>
          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path:'/objects' } "></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import constructionObjectEdit from '../mixins/constructionObjectEdit'

export default {
  mixins: [constructionObjectEdit],
}
</script>
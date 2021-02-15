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
                label="Статус проекту"
                required
                :value="selectedProjectStatus"
                @input="onProjectProjectStatusChanged"
                :options="projectStatuses"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="project.code"
                dense
                label="Код проєкту"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="project.name"
                dense
                label="Назва"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input v-model="project.fullName" dense label="Повна назва" class="q-mb-md q-ml-md"></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Об'єкт"
                required
                :value="selectedConstructionObject"
                @input="onProjectConstructionObjectChanged"
                :options="constructionObjects"
                optionLabel="codeAndName"
                optionValue="id"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Область"
                required
                :value="selectedRegion"
                @input="onProjectRegionChanged"
                :options="regions"
                optionLabel="name"
                optionValue="id"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Район"
                :value="selectedDistrict"
                @input="onProjectDistrictChanged"
                :options="districts"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип робіт"
                required
                :value="selectedTypeOfProjectWork"
                @input="onProjectTypeOfProjectWorkChanged"
                :options="typesOfProjectWork"
                optionLabel="codeName"
                optionValue="id"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип фінансування"
                required
                :value="selectedTypeOfFinancing"
                @input="onProjectTypeOfFinancingChanged"
                :options="typesOfFinancing"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="project.cost"
                dense
                label="Вартість проєкту, тис. грн."
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Статус виконання проекту"
                required
                :value="selectedProjectImplementationState"
                @input="onProjectImplementationStateChanged"
                :options="projectImplementationStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="project.dateBegin = $event"
                label="Дата початку"
                :required="true"
                :value="project.dateBegin"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="project.dateEnd = $event"
                label="Дата завершення"
                :required="true"
                :value="project.dateEnd"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="project.repairLength"
                dense
                label="Протяжність ділянки ремонту, км"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="project.repairSquare"
                dense
                label="Площа ділянки ремонту, м2"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input v-model="project.description" dense label="Коментар" class="q-mb-md q-ml-md"></q-input>
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
import projectEdit from '../mixins/projectEdit'

import DatePicker from '../../../components/forms/datepicker'

export default {
  mixins: [projectEdit],
  components: {
    DatePicker,
  },
}
</script>
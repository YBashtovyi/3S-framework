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
                v-model="exProperty.name"
                dense
                label="Назва *"
                :rules="[val => !!val || 'Обов\'язкове поле']"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="exProperty.fullName"
                dense
                label="Повна назва *"
                :rules="[val => !!val || 'Обов\'язкове поле']"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="exProperty.code"
                dense
                label="Код *"
                :rules="[val => !!val || 'Обов\'язкове поле']"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Формат даних"
                required
                :value="selectedDataFormat"
                @input="onDataFormatChanged"
                :options="dataFormats"
                optionLabel="name"
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
                @input="onExPropParentChanged"
                :options="exPropParents"
                optionLabel="name"
                optionValue="id"
                class="q-mb-md q-mr-md"
              />
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
import constObjExPropDictEdit from '../mixins/constObjExPropDictEdit'

export default {
  mixins: [constObjExPropDictEdit],
}
</script>
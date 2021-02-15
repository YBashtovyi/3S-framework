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
                label="Тип документу"
                required
                :value="selectedDocType"
                @input="onDocTypeChanged"
                :options="docTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-mr-md"
                disabled
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Статус документу"
                required
                :value="selectedDocState"
                @input="onDocStateChanged"
                :options="docStates"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <date-picker
                @updateData="prjContract.regDate = $event"
                label="Дата документу"
                :required="true"
                :value="prjContract.regDate"
                class="q-mb-md q-mr-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="prjContract.regNumber"
                dense
                label="№ документу *"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="prjContract.cost"
                dense
                label="Вартість, тис. грн. *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Тип закупівлі"
                required
                :value="selectedBiddingType"
                @input="onBiddingTypeChanged"
                :options="biddingTypes"
                optionLabel="name"
                optionValue="code"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="prjContract.tenderCode"
                dense
                label="Ідентифікатор тендеру Prozorro"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="prjContract.biddingCode"
                dense
                label="Ідентифікатор закупівлі Prozorro"
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                :rules="[val => !!val || 'Обов\'язкове поле']"
                v-model="prjContract.biddingSubject"
                dense
                label="Предмет закупівлі *"
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                v-model="prjContract.description"
                dense
                label="Коментар"
                class="q-mb-md q-ml-md"
              ></q-input>
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
import prjContractEdit from '../mixins/prjContractEdit'

import DatePicker from '../../../../../components/forms/datepicker'

export default {
  mixins: [prjContractEdit],
  components: {
    DatePicker,
  },
}
</script>
<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="projectContractTitle.label"
          :icon="projectContractTitle.icon"
          :content="projectContractTitle.content"
          :actions="projectContractTitle.actions"
        />
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-12 col-xs-12">
              <q-field dense label="Проєкт" stack-label readonly class="q-mb-md q-mr-md">
                <router-link
                  :to="`/projects/details/${details.projectId}`"
                  class="text-primary"
                  title="Перейти на картку проєкту"
                >{{ details.projectName }}</router-link>
                <template v-slot:before>
                  <q-icon name="architecture" size="20px" />
                </template>
              </q-field>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-field dense label="Замовник" stack-label readonly class="q-mb-md q-mr-md">
                <router-link
                  :to="`/organization/details/${details.customerId}`"
                  class="text-primary"
                  title="Перейти на картку замовника"
                >{{ details.customerName }}</router-link>
                <template v-slot:before>
                  <q-icon name="fa fa-building" size="20px" />
                </template>
              </q-field>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-field dense label="Виконавець" stack-label readonly class="q-mb-md q-mr-md">
                <router-link
                  :to="`/organization/details/${details.generalContractorId}`"
                  class="text-primary"
                  title="Перейти на картку виконавця"
                >{{ details.generalContractorName }}</router-link>
                <template v-slot:before>
                  <q-icon name="fa fa-building" size="20px" />
                </template>
              </q-field>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="typeOfProjectWorkCodeAndName"
                label="Тип робіт"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.docStateName"
                label="Статус"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.regDate | asDate(dateFormat)"
                label="Дата"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.regNumber"
                label="№ документу"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.cost"
                label="Вартість, тис.грн."
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.biddingTypeName"
                label="Тип закупівлі"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.tenderCode"
                label="Ідентифікатор тендеру Prozorro"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.biddingCode"
                label="Ідентифікатор закупівлі Prozorro"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.biddingSubject"
                label="Предмет закупівлі"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.description"
                label="Коментар"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
          </div>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../../components/baseElements/detailTitle'
import prjContractDetails from '../mixins/prjContractDetails'

export default {
  mixins: [prjContractDetails],

  components: {
    detailTitle,
  },
}
</script>
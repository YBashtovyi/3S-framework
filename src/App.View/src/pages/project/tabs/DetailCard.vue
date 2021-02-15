<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="projectTitle.label"
          :icon="projectTitle.icon"
          :content="projectTitle.content"
          :actions="projectTitle.actions"
        />
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.createdFullName"
                label="Власник"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.createdOn | asDate(dateTimeFormat)"
                label="Дата створення"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>

            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.projectStatusName"
                label="Статус проекту"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.code"
                label="Код проєкту"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input dense :value="details.name" label="Назва" readonly class="q-mb-md q-mr-md"></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.fullName"
                label="Повна назва"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-field dense label="Об'єкт" stack-label readonly class="q-mb-md q-mr-md">
                <router-link
                  :to="`/objects/details/${details.constructionObjectId}`"
                  class="text-primary"
                  title="Перейти на картку об'єкту"
                >{{ details.constructionObjectName }}</router-link>
                <template v-slot:before>
                  <q-icon name="fa fa-object-ungroup" size="20px" />
                </template>
              </q-field>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="typeOfProjectWorkCodeAndName"
                label="Тип робіт"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.regionName"
                label="Область"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.districtName"
                label="Район"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.typeOfFinancingName"
                label="Тип фінансування"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.cost"
                label="Вартість проєкту, тис. грн."
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.dateBegin | asDate(dateFormat)"
                label="Дата початку"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.dateEnd | asDate(dateFormat)"
                label="Дата завершення"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.projectImplementationStateName"
                label="Статус виконання проекту"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.description"
                label="Коментар"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.repairLength"
                label="Протяжність ділянки ремонту, км"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.repairSquare"
                label="Площа ділянки ремонту, м2"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
          </div>
        </div>
        <map-coordinate-list
          :mapCoordinates.sync="details.atuCoordinateList"
          :domenId="projectId"
          :domen="'Project'"
        />
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import MapCoordinateList from '../../../components/map/mapCoordinateList.vue'
import projectDetails from '../mixins/projectDetails'

export default {
  mixins: [projectDetails],

  components: {
    detailTitle,
    MapCoordinateList,
  },
}
</script>
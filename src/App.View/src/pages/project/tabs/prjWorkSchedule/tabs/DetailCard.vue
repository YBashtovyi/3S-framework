<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="projectWorkScheduleTitle.label"
          :icon="projectWorkScheduleTitle.icon"
          :content="projectWorkScheduleTitle.content"
          :actions="projectWorkScheduleTitle.actions"
        />
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-12 col-xs-12">
              <q-input
                dense
                :value="details.docTypeName"
                label="Тип документу"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div v-if="details.parentId" class="col-md-12 col-xs-12">
              <q-field dense label="До документу" stack-label readonly class="q-mb-md q-mr-md">
                <router-link
                  :to="urlToDocument"
                  class="text-primary"
                  title="Перейти до документу"
                  target="_blank"
                >{{ regDateAndRegNumber }}</router-link>
                <template v-slot:before>
                  <q-icon name="fa fa-folder-open" size="20px" />
                </template>
              </q-field>
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
                label="Дата документу"
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
          </div>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../../components/baseElements/detailTitle'
import prjWorkScheduleDetails from '../mixins/prjWorkScheduleDetails'

export default {
  mixins: [prjWorkScheduleDetails],

  components: {
    detailTitle,
  },
}
</script>
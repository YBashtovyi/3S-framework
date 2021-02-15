<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat bordered>
        <detail-title
          :label="projectWorkScheduleStageTitle.label"
          :icon="projectWorkScheduleStageTitle.icon"
          :content="projectWorkScheduleStageTitle.content"
          :actions="projectWorkScheduleStageTitle.actions"
        />
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.stageNumber"
                label="№ етапу"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.stageName"
                label="Назва етапу"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.beginDate | asDate(dateFormat)"
                label="Дата початку"
                readonly
                class="q-mb-md q-mr-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.endDate | asDate(dateFormat)"
                label="Дата закінчення"
                readonly
                class="q-mb-md q-ml-md"
              ></q-input>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input
                dense
                :value="details.cost"
                label="Кошторисна вартість (тис. грн.)"
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
import detailTitle from '../../../../../../../components/baseElements/detailTitle'
import prjWorkScheduleStageDetails from '../mixins/prjWorkScheduleStageDetails'

export default {
  mixins: [prjWorkScheduleStageDetails],

  components: {
    detailTitle,
  },
}
</script>
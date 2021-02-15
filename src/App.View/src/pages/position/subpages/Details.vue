<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat bordered>
        <detail-title
          :label="positionTitle.label"
          :icon="positionTitle.icon"
          :content="positionTitle.content"
        />
        <div class="q-pa-md">
          <q-input :value="details.name" label="Назва" readonly></q-input>
          <q-input :value="details.code" label="Код" readonly></q-input>
          <q-input :value="details.caption" label="Пояснення" readonly></q-input>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import positionDetails from '../mixins/positionDetails'

export default {
  mixins: [positionDetails],

  components: {
    detailTitle,
  },
}
</script>
<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
         <detail-title
          :label="cityTitle.label"
          :icon="cityTitle.icon"
          :content="cityTitle.content"
        />
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import cityDetails from '../mixins/cityDetails'

export default {
  mixins: [cityDetails],

  components: {
    detailTitle
  },
}
</script>

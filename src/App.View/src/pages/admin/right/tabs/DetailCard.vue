<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title
          :label="rightTitle.label"
          :icon="rightTitle.icon"
          :content="rightTitle.content"
        />
        <div class="q-pa-md">
          <q-input dense :value="details.rightTypeName" label="Тип права" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.name" label="Назва" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.code" label="Код" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.description" label="Опис" readonly class="q-mb-md"></q-input>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../components/baseElements/detailTitle'
import rightDetails from '../mixins/rightDetails'

export default {
  mixins: [rightDetails],

  components: {
    detailTitle,
  },
}
</script>
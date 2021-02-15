<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card>
        <detail-title :label="enumTitle.label" :icon="enumTitle.icon" :content="enumTitle.content" />
        <div class="q-pa-md">
          <q-input v-model="details.group" label="Група (англ.)" readonly></q-input>
          <q-input v-model="details.groupName" label="Група (укр.)" readonly></q-input>
          <q-input v-model="details.name" label="Назва" readonly></q-input>
          <q-input v-model="details.code" label="Код" readonly></q-input>
          <q-input v-model="details.value" label="Значення" readonly></q-input>
          <q-input v-model="details.itemNumber" label="Порядок" readonly></q-input>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import enumDetails from '../mixins/enumDetails'

export default {
  mixins: [enumDetails],

  components: {
    detailTitle,
  },
}
</script>
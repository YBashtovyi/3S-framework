<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat bordered>
        <detail-title
          :label="exPropTitle.label"
          :icon="exPropTitle.icon"
          :content="exPropTitle.content"
        />
        <div class="q-pa-md">
          <q-input :value="details.name" label="Назва" readonly></q-input>
          <q-input :value="details.code" label="Код" readonly></q-input>
          <q-input :value="details.dataFormatName" label="Формат даних" readonly></q-input>
          <q-input :value="details.parentName" label="Підпорядкування" readonly></q-input>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import constObjExPropDictDetails from '../mixins/constObjExPropDictDetails'

export default {
  mixins: [constObjExPropDictDetails],

  components: {
    detailTitle,
  },
}
</script>
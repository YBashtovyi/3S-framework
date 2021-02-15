<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card>
        <detail-title
          :label="objectTitle.label"
          :icon="objectTitle.icon"
          :content="objectTitle.content"
          :actions="objectTitle.actions"
        />
        <div class="q-pa-md">
          <q-input dense :value="details.code" label="Код" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.objectStatusName" label="Статус" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.name" label="Назва" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.fullName" label="Повна назва" readonly class="q-mb-md"></q-input>
          <q-input
            dense
            :value="details.typeOfConstructionObjectName"
            label="Тип об'єкту"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input
            dense
            :value="details.classOfConsequenceName"
            label="Клас наслідків"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input dense :value="details.description" label="Коментар" readonly class="q-mb-md"></q-input>
        </div>
        <map-coordinate-list
          :mapCoordinates.sync="details.atuCoordinateList"
          :domenId="objectId"
          :domen="'ConstructionObject'"
        />
        <construction-object-ex-propery-list :typeOfObjectId="details.typeOfConstructionObjectId" />
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../components/baseElements/detailTitle'
import constructionObjectDetails from '../mixins/constructionObjectDetails'
import MapCoordinateList from '../../../components/map/mapCoordinateList.vue'
import ConstructionObjectExProperyList from '../components/constructionObjectExProperyList.vue'

export default {
  mixins: [constructionObjectDetails],

  components: {
    detailTitle,
    MapCoordinateList,
    ConstructionObjectExProperyList,
  },
}
</script>
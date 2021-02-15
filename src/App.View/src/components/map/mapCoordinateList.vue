<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Координати на мапі</div>
      <q-btn
        @click="onEditCoordinateDialog"
        icon="edit"
        rounded
        flat
        color="primary"
        label="Редагувати координати"
      />
    </div>
    <q-list>
      <div>
        <q-item clickable v-ripple v-for="(item, index) in mapCoordinates.lines" v-bind:key="index">
          <q-item-section>
            <q-icon name="fas fa-slash" />Лінія
          </q-item-section>
          <q-item-section>
            <q-item-label>Кількість точок: {{item.path.length}}</q-item-label>
          </q-item-section>
        </q-item>
      </div>
      <div>
        <q-item
          clickable
          v-ripple
          v-for="(item, index) in mapCoordinates.polygons"
          v-bind:key="index"
        >
          <q-item-section>
            <q-icon name="fas fa-vector-square" />Полігон
          </q-item-section>
          <q-item-section>
            <q-item-label>Кількість точок: {{item.path.length}}</q-item-label>
          </q-item-section>
        </q-item>
      </div>
      <div>
        <q-item
          clickable
          v-ripple
          v-for="(item, index) in mapCoordinates.markers"
          v-bind:key="index"
        >
          <q-item-section>
            <q-icon name="fas fa-map-marker-alt" />Маркер
          </q-item-section>
          <q-item-section>
            <q-item-label>Координати: {{item.position.lat}} {{item.position.lng}}</q-item-label>
            <q-item-label v-if="item.infoWindow">Опис: {{item.infoWindow}}</q-item-label>
          </q-item-section>
        </q-item>
      </div>
    </q-list>
    <map-coordinate-dialog
      :isVisibleDialog.sync="isVisibleMapCoordinateDialog"
      :mapCoordinates="mapCoordinates"
      @coordinateItem="onAddAtuCoordinate"
    />
  </div>
</template>

<script>
import MapCoordinateDialog from './mapCoordinateDialog'
import { addMapCoordinate } from '../../services/common-api/map-api'

export default {
  components: {
    MapCoordinateDialog,
  },
  props: {
    mapCoordinates: {
      type: Object,
      required: true,
    },

    // The Id of the entity to which the coordinates will be saved
    domenId: {
      type: String,
      required: true,
    },

    // The name of the entity to which the coordinates will be saved (for example controller name)
    domen: {
      type: String,
      required: true,
    },
  },

  data() {
    return {
      isVisibleMapCoordinateDialog: false,
      atuCoordinateItem: null,
    }
  },

  methods: {
    onEditCoordinateDialog() {
      this.isVisibleMapCoordinateDialog = true
    },

    onAddAtuCoordinate(item) {
      addMapCoordinate(this.domenId, this.domen, item).then(this.updateAtuCoordinateList)
    },

    updateAtuCoordinateList(mapCoordinates) {
      this.$emit('update:mapCoordinates', mapCoordinates)
    },
  },
}
</script>
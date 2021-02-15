<template>
  <q-list>
    <!-- <img src="../../assets/Google_Maps_icon(2020).svg" /> -->
    <q-img
      v-if="isCoordinatesEmpty"
      src="~/assets/Google_Maps_icon_(2020).svg"
      class="q-mt-xl q-ml-xl"
      style="opacity:1; width:60%"
      contain
    >
      <div
        class="absolute-bottom text-subtitle1 text-center"
      >Нанесіть лінію, полігон або маркер на карту</div>
    </q-img>
    <div>
      <q-expansion-item
        :key="index"
        v-for="(m, index) in coordinates.lines"
        popup
        default-opened
        icon="fas fa-slash"
        :label="'Лінія #' + (index + 1)"
        caption="Координати лінії"
      >
        <q-separator />
        <q-card>
          <map-coordinate-item
            :content="'Колір лінії'"
            :propType="'color'"
            :value.sync="m.color"
            @updateData="onChangeColor(coordinates.lines, index ,$event)"
            class="q-pl-md q-pt-md q-pb-md"
          />
          <q-separator />
          <q-expansion-item
            :key="index2"
            v-for="(pos, index2) in m.path"
            :header-inset-level="1"
            expand-separator
            icon="fas fa-map-marker"
            header-class="text-primary"
            :label="'Точка #' + (index2 + 1)"
          >
            <q-card-section>
              <map-coordinate-item
                :content="'Широта'"
                :propType="'number'"
                :value.sync="pos.lat"
                @updateData="onUpdateBindLatLng(coordinates.lines, index)"
              />
              <map-coordinate-item
                :content="'Довгота'"
                :propType="'number'"
                :value.sync="pos.lng"
                @updateData="onUpdateBindLatLng(coordinates.lines, index)"
              />
            </q-card-section>
          </q-expansion-item>
        </q-card>
      </q-expansion-item>
    </div>
    <div>
      <q-expansion-item
        :key="index"
        v-for="(m, index) in coordinates.polygons"
        popup
        default-opened
        icon="fas fa-vector-square"
        :label="'Полігон #' + (index + 1)"
        caption="Координати полігону"
      >
        <q-separator />
        <q-card>
          <map-coordinate-item
            :content="'Колір лінії'"
            :propType="'color'"
            :value.sync="m.color"
            @updateData="onChangeColor(coordinates.polygons, index ,$event)"
            class="q-pl-md q-pt-md"
          />
          <map-coordinate-item
            :content="'Колір заповнення'"
            :propType="'color'"
            :value.sync="m.fillColor"
            @updateData="onChangeColor(coordinates.polygons, index ,$event)"
            class="q-pl-md q-pt-md q-pb-md"
          />
          <q-separator />
          <q-expansion-item
            :key="index2"
            v-for="(pos, index2) in m.path"
            :header-inset-level="1"
            expand-separator
            icon="fas fa-map-marker"
            header-class="text-primary"
            :label="'Точка #' + (index2 + 1)"
          >
            <q-card-section>
              <map-coordinate-item
                :content="'Широта'"
                :propType="'number'"
                :value.sync="pos.lat"
                @updateData="onUpdateBindLatLng(coordinates.polygons, index)"
              />
              <map-coordinate-item
                :content="'Довгота'"
                :propType="'number'"
                :value.sync="pos.lng"
                @updateData="onUpdateBindLatLng(coordinates.polygons, index)"
              />
            </q-card-section>
          </q-expansion-item>
        </q-card>
      </q-expansion-item>
    </div>
    <div>
      <q-expansion-item
        :key="index"
        v-for="(m, index) in coordinates.markers"
        popup
        default-opened
        icon="fas fa-map-marker-alt"
        :label="'Макркер #' + (index + 1)"
        caption="Координати маркеру"
      >
        <q-separator />
        <q-card>
          <q-expansion-item
            :header-inset-level="1"
            expand-separator
            icon="fas fa-map-marker"
            header-class="text-primary"
            label="Координати"
          >
            <q-card-section>
              <map-coordinate-item
                :content="'Широта'"
                :propType="'number'"
                :value.sync="m.position.lat"
                @updateData="onUpdateBindLatLng(coordinates.markers, index)"
              />
              <map-coordinate-item
                :content="'Довгота'"
                :propType="'number'"
                :value.sync="m.position.lng"
                @updateData="onUpdateBindLatLng(coordinates.markers, index)"
              />
              <map-coordinate-item
                :content="'Опис'"
                :propType="'text'"
                :value.sync="m.infoWindow"
                @updateData="onUpdateBindLatLng(coordinates.markers, index)"
              />
            </q-card-section>
          </q-expansion-item>
        </q-card>
      </q-expansion-item>
    </div>
  </q-list>
</template>

<script>
import clone from 'lodash.clone'
import mapCoordinateItem from './mapCoordinateItem.vue'

export default {
  components: { mapCoordinateItem },
  props: {
    coordinates: {
      type: Object,
      required: true,
    },
  },

  computed: {
    isCoordinatesEmpty() {
      return (
        !this.coordinates.lines.length &&
        !this.coordinates.polygons.length &&
        !this.coordinates.markers.length
      )
    },
  },

  methods: {
    onChangeColor(list, index, val) {
      list[index].color = val
    },

    onUpdateBindLatLng(list, lineIdx) {
      const clonePath = clone(list[lineIdx].path)
      list[lineIdx].path = clonePath
    },
  },
}
</script>
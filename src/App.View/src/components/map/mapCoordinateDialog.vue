<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 950px; max-width: 80vw;">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Редагування координат</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>
      <q-card-section class="q-pa-none">
        <q-card-section horizontal>
          <q-card-section class="q-pt-none q-pr-none q-pl-xs">
            <q-card-section class="q-pa-none">
              <q-card-actions class="q-pt-none q-pl-md">
                <q-btn-toggle
                  v-model="editMode"
                  color="primary"
                  toggle-color="red"
                  flat
                  :options="
                  [
                  { icon: 'fas fa-arrows-alt', value: 'hand', disable: false,  },
                  { icon: 'fas fa-slash', value: 'line' },
                  { icon: 'fas fa-vector-square', value: 'polygon' },
                  { icon: 'fas fa-map-marker-alt', value: 'marker' },
                  { icon: 'fas fa-recycle', value: 'delete' }
                  ]"
                />
              </q-card-actions>
            </q-card-section>
            <q-card-section class="q-pt-none q-pr-none q-pl-xs">
              <q-scroll-area style="height: 513px; width:250px">
                <map-coordinate-list :coordinates.sync="coordinates" />
              </q-scroll-area>
            </q-card-section>
          </q-card-section>
          <q-card-section class="q-pt-none q-pl-xs">
            <gmap-map
              :center="{lat:48, lng:32}"
              :zoom="5"
              map-type-id="terrain"
              style="width: 670px; height: 100%"
              :options="mapOptions"
              @click="onMapClick"
            >
              <div>
                <gmap-marker
                  :key="index"
                  v-for="(marker, index) in coordinates.markers"
                  :position="marker.position"
                  :clickable="true"
                  :draggable="true"
                  @click="onMarkerClick($event, index)"
                  @dragend="onMarkerDragend($event, index)"
                >
                  <gmap-info-window :opened="(marker.infoWindow.length > 0)">{{marker.infoWindow}}</gmap-info-window>
                </gmap-marker>
              </div>
              <div>
                <gmap-polyline
                  :key="index"
                  v-for="(line, index) in coordinates.lines"
                  :path.sync="line.path"
                  :draggable="true"
                  :editable="true"
                  :options="{geodesic:true, strokeColor: line.color}"
                  @click="onLineClick($event, index)"
                  @path_changed="onLineChanged($event, index)"
                />
              </div>
              <div>
                <gmap-polygon
                  :key="index"
                  v-for="(polygon, index) in coordinates.polygons"
                  :path="polygon.path"
                  :editable="true"
                  :draggable="true"
                  :options="{geodesic:true, strokeColor: polygon.color, fillColor: polygon.fillColor}"
                  @click="onPolygonClick($event, index)"
                  @path_changed="onPolygonChanged($event, index)"
                />
              </div>
            </gmap-map>
          </q-card-section>
        </q-card-section>
      </q-card-section>
      <q-separator />
      <q-card-actions align="right" class="q-px-md q-pt-xs q-pb-xs">
        <q-form @submit="onSubmit">
          <q-btn
            label="Відмінити"
            color="negative"
            flat
            @click="$emit('update:isVisibleDialog', false)"
          />
          <q-btn type="submit" label="Зберегти" color="primary" class="on-right" />
        </q-form>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script>
import { gmapApi } from 'gmap-vue'
import MapCoordinateList from './mapCoordinateDialogList.vue'
import cloneDeep from 'lodash.clonedeep'

export default {
  components: {
    MapCoordinateList,
  },

  data() {
    return {
      coordinates: {
        markers: [],
        lines: [],
        polygons: [],
      },

      mapOptions: {
        zoomControl: true,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        mapTypeId: 'roadmap',
      },

      editMode: null,
      lineMode: 'new', // new, add
      polygonMode: 'new', // new, add

      isDeepUpdate: true,
    }
  },

  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },

    mapCoordinates: {
      type: Object,
      default() {
        return {
          markers: [],
          lines: [],
          polygons: [],
        }
      },
    },
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.coordinates = cloneDeep(this.mapCoordinates)
      }
    },
  },

  computed: {
    google: gmapApi,
  },

  methods: {
    onSubmit() {
      this.$emit('update:isVisibleDialog', false)
      this.$emit('coordinateItem', {
        lines: this.coordinates.lines,
        polygons: this.coordinates.polygons,
        markers: this.coordinates.markers,
      })
    },

    onMapClick(pos) {
      if (this.editMode === 'marker') {
        this.coordinates.markers.push({
          position: { lat: pos.latLng.lat(), lng: pos.latLng.lng() },
          infoWindow: '',
        })
      } else if (this.editMode === 'line') {
        if (this.lineMode === 'new') {
          this.coordinates.lines.push({
            path: [{ lat: pos.latLng.lat(), lng: pos.latLng.lng() }],
            color: '#FF0000',
          })
          this.lineMode = 'add'
        } else if (this.lineMode === 'add') {
          const lastIdx = this.coordinates.lines.length - 1
          this.coordinates.lines[lastIdx].path.push({
            lat: pos.latLng.lat(),
            lng: pos.latLng.lng(),
          })

          if (this.coordinates.lines[lastIdx].path.length === 2) {
            this.lineMode = 'new'
          }
        }
      } else if (this.editMode === 'polygon') {
        if (this.polygonMode === 'new') {
          this.coordinates.polygons.push({
            path: [{ lat: pos.latLng.lat(), lng: pos.latLng.lng() }],
            color: '#FF0000',
            fillColor: '#000000',
          })
          this.polygonMode = 'add'
        } else {
          const lastIdx = this.coordinates.polygons.length - 1
          this.coordinates.polygons[lastIdx].path.push({
            lat: pos.latLng.lat(),
            lng: pos.latLng.lng(),
          })

          if (this.coordinates.polygons[lastIdx].path.length === 3) {
            this.polygonMode = 'new'
          }
        }
      }
    },

    onMarkerClick(pos, index) {
      if (this.editMode === 'delete') {
        this.coordinates.markers.splice(index, 1)
      }
    },

    onMarkerDragend(pos, markerIndex) {
      this.coordinates.markers[markerIndex].position = {
        lat: pos.latLng.lat(),
        lng: pos.latLng.lng(),
      }
    },

    onLineChanged(pos, lineIndex) {
      this.coordinates.lines[lineIndex].path = []
      for (let i = 0; i < pos.Kb.length; i++) {
        this.coordinates.lines[lineIndex].path.push({ lat: pos.Kb[i].lat(), lng: pos.Kb[i].lng() })
      }
    },

    onLineClick(pos, lineIndex) {
      if (this.editMode === 'delete') {
        this.coordinates.lines[lineIndex].path.splice(pos.vertex, 1)
        if (this.coordinates.lines[lineIndex].path.length <= 1) {
          this.coordinates.lines.splice(lineIndex, 1)
        }
      }
    },

    onPolygonClick(pos, polygonIndex) {
      if (this.editMode === 'delete') {
        this.coordinates.polygons[polygonIndex].path.splice(pos.vertex, 1)
        if (this.coordinates.polygons[polygonIndex].path.length <= 2) {
          this.coordinates.polygons.splice(polygonIndex, 1)
        }
      }
    },

    onPolygonChanged(pos, polygonIndex) {
      this.coordinates.polygons[polygonIndex].path = []
      for (let i = 0; i < pos.Kb.length; i++) {
        this.coordinates.polygons[polygonIndex].path.push({
          lat: pos.Kb[i].lat(),
          lng: pos.Kb[i].lng(),
        })
      }
    },
  },
}
</script>
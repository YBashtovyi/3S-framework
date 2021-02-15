<template>
  <q-card>
    <q-card-section>
      <map-view :coordinates="coordinates" style="max-width:1600px; width: 100%; height: 70vh;" />
    </q-card-section>
  </q-card>
</template>

<script>
import { getMapCoordinates } from '../../../services/prj-api/project-api'
import mapView from '../../../components/map/mapView.vue'

export default {
  components: { mapView },
  data() {
    return {
      coordinates: {
        lines: [],
        polygons: [],
        markers: [],
      },
    }
  },

  computed: {
    projectId() {
      return this.$route.params.id
    },
  },

  methods: {
    initMap() {
      getMapCoordinates(this.projectId).then(this.setCoordinate)
    },

    setCoordinate(coordinates) {
      this.coordinates = coordinates
    },
  },

  created() {
    this.initMap()
  },
}
</script>
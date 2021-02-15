<template>
  <q-card>
    <q-card-section>
      <map-view :coordinates="coordinates" style="max-width:1600px; width: 100%; height: 70vh;" />
    </q-card-section>
  </q-card>
</template>

<script>
import { getMapCoordinates } from '../../../services/common-api/construction-object-api'
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
    constuctionObjectId() {
      return this.$route.params.id
    },
  },

  methods: {
    initMap() {
      getMapCoordinates(this.constuctionObjectId).then(this.setCoordinate)
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
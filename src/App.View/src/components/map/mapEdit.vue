<template>
  <div>
    <GmapMap
      :center="mapCenter"
      :zoom="zoom"
      map-type-id="terrain"
      class="google-map"
      :options="{
        mapTypeControl: false,
        streetViewControl: false,
        fullscreenControl: false
      }"
      @click="updateCoordinates"
    >
      <GmapMarker
        :position.sync="mapMarkerLocation.position"
        :clickable="true"
        :draggable="true"
        @mouseup="updateCoordinates"
      />
    </GmapMap>
  </div>
</template>

<script>
export default {
  name: "GoogleMapEdit",
  props: {
    zoom: { type: Number, default: 14 },
    latitude: { type: Number, default: 50.4461527 },
    longitude: { type: Number, default: 30.4257723 }
  },
  data() {
    return {
      mapMarkerLocation: Object,
      mapCenter: { lat: 50.4461527, lng: 30.4257723 }
    };
  },
  watch: {
    latitude: function() {
      this.mapMarkerLocation.position.lat = this.latitude;
      this.mapCenter.lat = this.latitude;
    },
    longitude: function() {
      this.mapMarkerLocation.position.lng = this.longitude;
      this.mapCenter.lng = this.longitude;
    }
  },
  methods: {
    updateCoordinates(event) {
      this.mapMarkerLocation = {
        position: { lat: event.latLng.lat(), lng: event.latLng.lng() }
      };
      this.$emit("updateMarkerPosition", this.mapMarkerLocation);
    }
  },
  created() {
    this.mapMarkerLocation = {
      position: { lat: this.latitude, lng: this.longitude }
    };
    if(this.latitude && this.longitude){
      this.mapCenter = { lat: this.latitude, lng: this.longitude };
    }
  }
};
</script>

<style>
.google-map {
  width: 100%;
  height: 350px;
  margin-top: 10px;
}
</style>

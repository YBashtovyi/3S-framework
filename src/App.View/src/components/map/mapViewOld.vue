<template>
  <div>
    <GmapMap
      ref="gmap"
      :center="mapCenter"
      :zoom="zoom"
      map-type-id="terrain"
      class="google-map"
      :options="{
        mapTypeControl: false,
        streetViewControl: false,
        fullscreenControl: false
      }"
    >
      <GmapMarker
        :key="index"
        v-for="(m, index) in markers"
        :position="m.position"
        :clickable="true"
        :draggable="false"
        @click="toggleInfoWindow(m, index)"
      />
      <gmap-info-window
        :options="infoOptions"
        :position="infoWindowPos"
        :opened="infoWinOpen"
        @closeclick="infoWinOpen = false"
      >
        <div v-html="infoContent"></div>
      </gmap-info-window>
    </GmapMap>
  </div>
</template>

<script>
import { gmapApi } from "vue2-google-maps";

export default {
  name: "GoogleMapView",
  props: {
    zoom: { type: Number, default: 12 },
    markers: Array
  },
  data() {
    return {
      mapMarkerLocation: Object,
      mapCenter: { lat: 50.4461527, lng: 30.4257723 },
      infoContent: "",
      infoWindowPos: {
        lat: 0,
        lng: 0
      },
      infoWinOpen: false,
      currentMidx: null,
      // optional: offset infowindow so it visually sits nicely on top of our marker
      infoOptions: {
        pixelOffset: {
          width: 0,
          height: -35
        }
      }
    };
  },
  computed: {
    google: gmapApi
  },
  watch: {},
  methods: {
    getInfoWindowContent: function(marker) {
      return `<div class="iw-container iw-bottom-gradient">
          <div class="iw-title">
              ${marker.name}
          </div>
          <div class="iw-content">
            <div class="iw-subTitle">${marker.description}</div>
          </div>
       </div>`;
    },
    toggleInfoWindow: function(marker, idx) {
      this.infoWindowPos = marker.position;
      this.infoContent = this.getInfoWindowContent(marker);
      // check if its the same marker that was selected if yes toggle
      if (this.currentMidx === idx) {
        this.infoWinOpen = !this.infoWinOpen;
      }
      // if different marker set infowindow to open and reset current marker index
      else {
        this.infoWinOpen = true;
        this.currentMidx = idx;
      }
    }
  },
  mounted() {
    this.$refs.gmap.$mapPromise.then(map => {
      let bounds = new this.google.maps.LatLngBounds();
      for (let m of this.markers) {
        bounds.extend(m.position);
      }
      map.fitBounds(bounds);
      map.panToBounds(bounds);
    });
  }
};
</script>

<style>
.google-map {
  width: 100%;
  height: 350px;
  margin-top: 10px;
}

.iw-container {
  width: 250px;
  top: 15px;
  left: 0px;
  font-family: "Source Sans Pro", sans-serif;
  margin-bottom: 5px;
}
.iw-container .iw-title {
  font-size: 18px;
  font-weight: 600;
  padding: 5px;
  margin: 0;
}
.iw-container .iw-content {
  font-size: 14px;
  line-height: 18px;
  font-weight: 400;
  margin-right: 1px;
  padding: 5px 5px 10px 10px;
  max-height: 200px;
}
.iw-content img {
  float: right;
  margin: 0 5px 5px 10px;
}
.iw-subTitle {
  font-size: 14px;
  font-weight: 450;
}
</style>

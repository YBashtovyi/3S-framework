<template>
  <gmap-map :center="{lat:48, lng:32}" :zoom="5" map-type-id="terrain" :options="mapOptions">
    <div>
      <gmap-marker
        :key="index"
        v-for="(marker, index) in coordinates.markers"
        :position="marker.position"
        :clickable="false"
        :draggable="false"
      >
        <gmap-info-window :opened="(marker.infoWindow.length > 0)">{{marker.infoWindow}}</gmap-info-window>
      </gmap-marker>
    </div>
    <div>
      <gmap-polyline
        :key="index"
        v-for="(line, index) in coordinates.lines"
        :path.sync="line.path"
        :draggable="false"
        :editable="false"
        :options="{geodesic:true, strokeColor: line.color}"
      />
    </div>
    <div>
      <gmap-polygon
        :key="index"
        v-for="(polygon, index) in coordinates.polygons"
        :path="polygon.path"
        :editable="false"
        :draggable="false"
        :options="{geodesic:true, strokeColor: polygon.color, fillColor: polygon.fillColor}"
      />
    </div>
  </gmap-map>
</template>

<script>
export default {
  data() {
    return {
      mapOptions: {
        zoomControl: true,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        mapTypeId: 'roadmap',
      },
    }
  },

  props: {
    coordinates: {
      type: Object,
      default() {
        return {
          lines: [],
          polygons: [],
          markers: [],
        }
      },
    },
  },
}
</script>
<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Координати об'єкту</div>
      <q-btn
        @click="onAddCoordinateDialog"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати координату"
      />
    </div>
    <q-list>
      <q-item clickable v-ripple v-for="item in atuCoordinates" v-bind:key="item.order">
        <q-item-section>
          <q-avatar size="24px" color="primary" text-color="white">{{item.order}}</q-avatar>
        </q-item-section>
        <q-item-section>
          <q-item-label>Широта:{{item.latitude}}</q-item-label>
          <q-item-label>Довгота:{{item.longitude}}</q-item-label>
        </q-item-section>
        <q-item-section side>
          <q-btn
            @click="onDeleteAtuCoordinate(item.order)"
            dense
            round
            flat
            color="negative"
            icon="delete"
          />
        </q-item-section>
      </q-item>
    </q-list>
    <atu-coordinate-dialog
      :isVisibleDialog.sync="isVisibleAtuCoordinateDialog"
      @coordinateItem="onAddAtuCoordinate"
    />
  </div>
</template>

<script>
import AtuCoordinateDialog from '../components/atuCoordinateDialog'
import {
  addAtuCoordinate,
  deleteAtuCoordinate,
} from '../../../services/common-api/construction-object-api'

export default {
  components: {
    AtuCoordinateDialog,
  },
  props: {
    atuCoordinates: {
      type: Array,
      required: true,
    },
  },

  data() {
    return {
      isVisibleAtuCoordinateDialog: false,
      atuCoordinateItem: null,
    }
  },

  methods: {
    onAddCoordinateDialog() {
      this.isVisibleAtuCoordinateDialog = true
    },

    onAddAtuCoordinate(item) {
      addAtuCoordinate(this.$route.params.id, item).then(this.updateAtuCoordinateList)
    },

    onDeleteAtuCoordinate(order) {
      deleteAtuCoordinate(this.$route.params.id, order).then(this.updateAtuCoordinateList)
    },

    updateAtuCoordinateList(atuCoordinates) {
      this.$emit('update:atuCoordinates', atuCoordinates)
    },
  },
}
</script>
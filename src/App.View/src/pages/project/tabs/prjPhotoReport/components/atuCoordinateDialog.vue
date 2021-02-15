<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 350px">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Додати координату</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>
      <q-form @submit="onSubmit">
        <q-card-section>
          <div>
            <q-input
              v-model="order"
              label="№ з/п координати"
              :rules="[val => !!val || 'Обов\'язкове поле']"
            />
            <q-input
              v-model="latitude"
              label="Широта"
              :rules="[val => !!val || 'Обов\'язкове поле']"
            />
            <q-input
              v-model="longitude"
              label="Довгота"
              :rules="[val => !!val || 'Обов\'язкове поле']"
            />
          </div>
        </q-card-section>

        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
          <q-btn
            label="Відмінити"
            color="negative"
            flat
            @click="$emit('update:isVisibleDialog', false)"
          />
          <q-btn type="submit" label="Зберегти" color="primary" class="on-right" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
export default {
  data() {
    return {
      order: '',
      latitude: '',
      longitude: '',
    }
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.order = ''
        this.latitude = ''
        this.longitude = ''
      }
    },
  },

  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },
  },

  methods: {
    onSubmit() {
      this.$emit('update:isVisibleDialog', false)
      this.$emit('coordinateItem', {
        order: this.order,
        latitude: this.latitude,
        longitude: this.longitude,
      })
    },
  },
}
</script>
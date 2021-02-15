<template>
  <div>
    <div v-if="isNumber">
      {{content}}: {{mutableValue}}
      <q-popup-edit v-model.number="mutableValue">
        <template v-slot="{ initialValue, value, emitValue, set, cancel }">
          <q-input
            type="number"
            autofocus
            dense
            :value="mutableValue"
            :hint="content"
            @input="emitValue"
            step="0.00001"
          >
            <template v-slot:after>
              <q-btn flat dense color="negative" icon="cancel" @click.stop="cancel" />
              <q-btn
                flat
                dense
                color="positive"
                icon="check_circle"
                @click="onUpdateData"
                @click.stop="set"
                :disable="initialValue === value"
              />
            </template>
          </q-input>
        </template>
      </q-popup-edit>
      <q-tooltip anchor="center right" self="center left">Натисніть, щоб відредагувати значення</q-tooltip>
    </div>
    <div v-else-if="isText">
      {{content}}: {{mutableValue}}
      <q-popup-edit v-model="mutableValue">
        <template v-slot="{ initialValue, value, emitValue, set, cancel }">
          <q-input autofocus dense :value="mutableValue" :hint="content" @input="emitValue">
            <template v-slot:after>
              <q-btn flat dense color="negative" icon="cancel" @click.stop="cancel" />
              <q-btn
                flat
                dense
                color="positive"
                icon="check_circle"
                @click="onUpdateData"
                @click.stop="set"
                :disable="initialValue === value"
              />
            </template>
          </q-input>
        </template>
      </q-popup-edit>
      <q-tooltip anchor="center right" self="center left">Натисніть, щоб відредагувати значення</q-tooltip>
    </div>
    <div v-else-if="isColor">
      <div class="row">
        {{content}}
        <q-space />
        <q-badge class="q-mr-md" :style="`background-color:${mutableValue};`">{{mutableValue}}</q-badge>
        <q-popup-edit v-model="mutableValue">
          <q-color no-header v-model="mutableValue" />
        </q-popup-edit>
        <q-tooltip anchor="center right" self="center left">Натисніть, щоб відредагувати колір</q-tooltip>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    content: {
      type: String,
      default: '',
    },
    propType: {
      type: String,
      default: 'text', // can be text, number, color
    },
    value: {
      required: true,
    },
  },

  data() {
    return {
      mutableValue: this.value,
    }
  },

  watch: {
    mutableValue() {
      if (this.isColor) {
        this.$emit('updateData', this.mutableValue)
      }
    },

    // update data when a component is moved on the map
    value(val) {
      this.mutableValue = val
    },
  },

  computed: {
    isText() {
      return this.propType === 'text'
    },

    isNumber() {
      return this.propType === 'number'
    },

    isColor() {
      return this.propType === 'color'
    },
  },

  methods: {
    onUpdateData() {
      this.$emit('update:value', this.mutableValue)
      this.$emit('updateData', this.mutableValue)
    },
  },
}
</script>
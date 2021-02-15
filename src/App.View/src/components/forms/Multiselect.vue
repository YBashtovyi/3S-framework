<template>
  <q-select
    dense
    options-dense
    use-chips
    multiple
    map-options
    transition-show="jump-down"
    transition-hide="jump-down"
    :outlined="outlined"
    :label="label"
    :options="options"
    v-model="model"
    :value="model"
    :option-value="optionValue"
    :option-label="optionLabel"
    @input="onInput"
    @remove="clearField()"
    @click.native="getOptions()"
    ref="select"
    color="primary"
    label-color="primary"
  >
    <template v-slot:no-option>
      <q-item>
        <q-item-section>
          <q-item-label class="text-grey">Відсутні значення</q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script>
import { getData } from '@/services/api'
export default {
  data() {
    return {
      model: [],
      options: [],
      chips: [],
      checkAll: false,
    }
  },
  props: {
    requestUrl: String,
    label: String,
    outlined: Boolean,
    catalogFn: Function,
    catalog: Array,
    value: Array,
    optionValue: {
      type: String,
      default: 'id',
    },
    optionLabel: {
      type: String,
      default: 'name',
    },
    labelBefore: String,
    isCleared: Boolean,
  },
  methods: {
    clearField() {
      this.$emit('isCleared', true)
    },

    getOptions() {
      if (this.catalog && this.catalog.length > 0) {
        // already loaded
        this.options = this.catalog
      } else if (this.catalogFn) {
        this.catalogFn()
      } else if (this.options.length <= 0) {
        getData(this.requestUrl)
          .then(response => {
            this.options = []
            this.options = response.data
          })
          .catch(error => console.log(error))
      }
    },

    // emits 'input' event to parent component, when option is selected or cleared
    onInput(value) {
      this.$emit('input', value)
    },
  },
  watch: {
    model() {
      this.$emit('updateData', this.model)
    },

    catalog() {
      this.options = this.catalog
    },

    value() {
      this.model = this.value
    },

    checkAll() {
      if (this.checkAll) {
        this.model = this.options
      } else {
        this.model = []
      }
    },
  },
  mounted() {
    this.model = this.value
  },
}
</script>

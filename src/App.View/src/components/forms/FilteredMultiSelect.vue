<template>
  <q-select
    dense
    options-dense
    :use-chips="useChips"
    use-input
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
    @filter="filterFn"
    @virtual-scroll="onScroll"
    ref="select"
    color="primary"
    label-color="primary"
    :hint="hint"
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
import filter from 'lodash.filter'

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
    useChips: {
      type: Boolean,
      default: false,
    },
    minFilterChars: {
      type: Number,
      default: 0,
    },
    requestUrl: {
      type: String,
      default: '',
    },
  },
  methods: {
    clearField() {
      this.$emit('isCleared', true)
    },

    getOptions() {
      if (this.catalog && this.catalog.length > 0) {
        this.options = this.catalog
      } else if (this.catalogFn) {
        this.catalogFn()
      } else if (this.options.length <= 0) {
        getData(this.requestUrl)
          .then(({ data }) => {
            this.options = []
            this.options = data
          })
          .catch(error => console.log(error))
      }
    },

    filterFn(val, update, abort) {
      update(() => {
        if (val.length < this.minFilterChars) {
          abort()
          return
        }
        const needle = val.toLowerCase()
        this.options = filter(this.options, this.filterOptionsByNeedle(needle))
      })
    },

    filterOptionsByNeedle(needle) {
      return option => option[this.optionLabel].toLowerCase().indexOf(needle) > -1
    },

    // emits 'input' event to parent component, when option is selected or cleared
    onInput(value) {
      this.$emit('input', value)
    },
    // triggered by q-select @virtual-scroll event
    onScroll({ index, to, direction, ref }) {
      // we do not need load options when move 'up'
      if (direction === 'decrease') {
        return
      }

      const lastIndex = this.filteredOptions.length - 1

      if (this.loading !== true && this.currentPage < this.nextPage && index === lastIndex) {
        this.loading = true
        this.fetchOptions()
          .then(opts => this.refreshOptions(opts, ref))
          .then(this.setPagesState)
      }
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
  computed: {
    // returns hint, that is displayed in input if minimumFilterCharCount prop is greater than zero
    hint() {
      if (this.model) {
        return ''
      }

      if (this.minFilterChars > 0) {
        let symbolPhrase = ''
        if (this.minFilterChars === 1) {
          symbolPhrase = 'символ'
        }
        // this won't work correctly on 21, 22 and so on sympbols
        // this is OK, because nobody uses such large starting phrases for filtering
        else if (this.minFilterChars > 4) {
          symbolPhrase = 'символів'
        }
        // for 2-4 symbols
        else {
          symbolPhrase = 'символи'
        }
        return `Введіть мінімум ${this.minFilterChars} ${symbolPhrase} для пошуку`
      }
      return ''
    },
  },

  mounted() {
    this.model = this.value
  },
}
</script>

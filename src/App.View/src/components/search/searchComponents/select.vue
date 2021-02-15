<template>
  <q-select
    v-model="model"
    :options="options"
    :option-value="optionValue"
    :option-label="optionLabel"
    @filter="filterFn"
    @filter-abort="abortFilterFn"
    @clear="clearField()"
    use-input
    clearable
    clear-icon="clear"
    hint="Введіть мінімум 2 символи"
    hide-hint
    dense
    options-dense
    hide-dropdown-icon
    transition-show="jump-up"
    transition-hide="jump-up"
    input-debounce="500"
    :input-class="'search-field_input'"
    class="search-field"
    popup-content-class="search-options"
  >
    <template v-slot:no-option>
      <q-item>
        <q-item-section>
          <q-item-label class="text-grey">Відсутні значення</q-item-label>
        </q-item-section>
      </q-item>
    </template>
    <template v-slot:before>
      <div class="filter-caption">{{ label }}</div>
    </template>
  </q-select>
</template>

<script>
import { getData } from '@/services/api'
export default {
  data() {
    return {
      model: undefined,
      options: [],
    }
  },
  props: {
    requestUrl: [String, Function],
    label: String,
    value: Object,
    optionValue: {
      type: String,
      default: 'id',
    },
    optionLabel: {
      type: String,
      default: 'name',
    },
    isCleared: Boolean,
  },
  computed: {
    cardId() {
      return this.$route.params.id
    },
  },
  methods: {
    clearField() {
      this.$emit('isCleared', true)
    },
    filterFn(val, update, abort) {
      if (val.length < 1) {
        abort()
        return
      }
      // if (this.options.length > 0) {
      //   // already loaded
      //   update();
      //   return;
      // }
      const url =
        typeof this.requestUrl === 'function' ? this.requestUrl() + val : this.requestUrl + val
      setTimeout(() => {
        update(() => {
          getData(url)
            .then(response => {
              this.options = []
              this.options = response.data
            })
            .catch(error => console.log(error))
        })
      }, 300)
    },
    abortFilterFn() {
      console.log('delayed filter aborted')
    },
  },
  watch: {
    model() {
      if (this.model) {
        this.$emit('updateData', this.model)
      } else {
        this.$emit('updateData', null)
      }
    },
    catalog() {
      this.options = this.catalog
    },
    value() {
      this.model = this.value
    },
  },
}
</script>

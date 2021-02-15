<template>
  <q-select
    dense
    options-dense
    use-chips
    multiple
    map-options
    transition-show="jump-up"
    transition-hide="jump-up"
    :outlined="outlined"
    :placeholder="label"
    :options="options"
    v-model="model"
    :option-value="optionValue"
    :option-label="optionLabel"
    :disable="disable"
    @filter="filterFn"
    @remove="clearField()"
    ref="select"
    options-selected-class="selected-options"
  >
    <template v-slot:selected-item="scope" class="q-pt-lg">
      <q-chip
        dense
        removable
        @remove="scope.removeAtIndex(scope.index)"
        :tabindex="scope.tabindex"
        class="q-mb-xs q-mt-none"
      >
        {{
        optionLabel === "code" ? scope.opt.code : scope.opt.name
        }}
      </q-chip>
    </template>
    <template v-slot:no-option>
      <q-item>
        <q-item-section>
          <q-item-label class="text-grey">Відсутні значення</q-item-label>
        </q-item-section>
      </q-item>
    </template>
    <!-- <template v-slot:before-options v-if="options && options.length > 0">
      <q-item clickable dense>
        <q-item-section>
          <q-checkbox
            v-model="checkAll"
            :label="checkAll?'Зняти всі позначки':'Обрати всі'"
            color="primary"
          />
        </q-item-section>
      </q-item>
    </template>-->
    <!-- <template v-slot:option="scope">
      <q-item v-bind="scope.itemProps" v-on="scope.itemEvents" ref="optionItem">
        <q-item-section>
          <q-checkbox
            v-model="model"
            :val="scope.opt"
            :label="optionLabel === 'code' ? scope.opt.code : scope.opt.caption"
            color="primary"
            @input="clearField()"
            ref="checkbox"
          />
        </q-item-section>
      </q-item>
    </template>-->
    <template v-slot:before v-if="labelBefore">
      <div class="filter-caption">{{ labelBefore }}</div>
    </template>
  </q-select>
</template>

<script>
import { getData } from '@/services/api'
export default {
  data() {
    return {
      model: null,
      options: null,
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
    disable: Boolean,
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
      } else if (!this.options || this.options.length <= 0) {
        return getData(this.requestUrl)
          .then(response => {
            this.options = response.data
          })
          .catch(error => console.log(error))
      }
      return Promise.resolve()
    },

    filterFn(val, update, abort) {
      if (this.options && this.options.length > 1) {
        update()
        return
      }

      this.getOptions().then(update)
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
      // if (this.cardId) {
      //   this.model = this.value
      // }
    },
    checkAll() {
      if (this.checkAll) {
        this.model = this.options
      } else {
        this.model = null
      }
    },
  },
  mounted() {
    this.model = this.value
    // this.getOptions();
  },
  // updated() {
  //   if (this.model && this.model.length > 0) {
  //     if (this.$refs.select.menu) {
  //       console.log(this.$refs.optionItem);
  //     }
  //   }
  // }
}
</script>

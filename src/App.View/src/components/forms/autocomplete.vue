<template>
  <q-select
    v-model="model"
    :label="labelValue"
    :bg-color="color"
    :rules="requiredRule"
    :use-input="useInput"
    :hide-selected="useInput"
    :fill-input="useInput"
    :outlined="outlined"
    :filled="filled"
    :options="options"
    :option-value="optionValue"
    :option-label="optionLabel"
    @filter="filterFn"
    @filter-abort="abortFilterFn"
    @input="onChanged"
    ref="valid"
    dense
    options-dense
    clearable
    :readonly="readonly"
    clear-icon="close"
    transition-show="jump-up"
    transition-hide="jump-up"
    options-selected-class="selected-options"
  >
    <template v-slot:no-option>
      <q-item>
        <q-item-section>
          <q-item-label class="text-grey">Відсутні значення</q-item-label>
        </q-item-section>
        <q-item-section side v-if="addOption">
          <q-btn flat color="positive" label="Створити" @click="$emit('update:createItem', true)" />
        </q-item-section>
      </q-item>
    </template>
    <template v-slot:append v-if="addBtn">
      <q-btn round dense flat icon="add" @click="$emit('update:createItem', true)" />
    </template>
    <template v-slot:option="scope" v-if="customOptions.length > 0">
      <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
        <q-item-section>
          <q-item-label v-html="scope.opt[optionLabel]"></q-item-label>
          <q-item-label caption>
            {{
            scope.opt[customOptions[0]] +
            scope.opt[customOptions[1]] +
            " " +
            scope.opt[customOptions[2]]
            }}
          </q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script>
import { getData } from "@/services/api";
export default {
  data() {
    return {
      model: undefined,
      options: [],
      requiredRule: [],
      color: "transparent"
    };
  },
  props: {
    // url for getting options for autoselect; is not required if catalog or catalog fn is passed
    requestUrl: [String, Function],

    // input label
    label: String,

    // is editable element
    readonly: {
      type: Boolean,
      default: false
    },

    list: {
      type: Boolean,
      default: false
    },
    createItem: Boolean,
    addOption: {
      type: Boolean,
      default: false
    },
    addBtn: {
      type: Boolean,
      default: false
    },
    clearable: Boolean,
    outlined: {
      type: Boolean,
      default: true
    },
    filled: {
      type: Boolean,
      default: false
    },

    // field is required
    required: {
      type: Boolean,
      default: false
    },

    // custom function to get options for select input; is not required if requestUrl or catalog is passed
    catalogFn: Function,

    onChangedFn: Function,

    // custom options for select input ; is not required if requestUrl or catalogFn is passed
    catalog: Array,
    useInput: Boolean,
    value: Object,

    // field name for select option id
    optionValue: {
      type: String,
      default: "id"
    },

    // field name for select opton display caption
    optionLabel: {
      type: String,
      default: "caption"
    },

    validate: Boolean,

    customOptions: {
      type: Array,
      default: function() {
        return [];
      }
    },

    // when options are loaded with request url, this function filters this options
    optionsFn: Function
  },
  computed: {
    cardId() {
      return this.$route.params.id;
    },
    hasError() {
      return this.$refs.valid.hasError;
    },
    labelValue() {
      if (this.required) {
        return this.label + " *";
      } else {
        return this.label;
      }
    },
    filterOptions() {
      return this.optionsFn();
    }
  },
  methods: {
    onChanged() {
      if (this.onChangedFn) {
        this.onChangedFn(this.model)
      }
      if (this.cardId) {
        
        if (!this.hasError) {
          this.color = "green-1"
        } else {
          this.color = "red-1"
        }
        if (!this.required && !this.model) {
          this.color = "transparent"
        }      
      }
    },

    // gets options to select in input
    // sources:
    //   catatalog prop is first priority
    //   catalogFn prop is second priority (if catalog was not passed)
    //   requestUrl - use backend request only if catalog or catalogFn were not passed
    getOptions() {
      if (this.catalog) {
        if (this.catalog.length > 0) {
          this.options = this.catalog;
        }
      } else if (this.catalogFn) {
        this.catalogFn();
      } else if (this.requestUrl) {
        getData(this.requestUrl)
          .then(response => {
            this.options = [];
            this.options = response.data;
            setTimeout(() => {
              if (this.optionsFn) {
                this.options = this.filterOptions;
              }
            }, 500);
          })
          .catch(error => {
            console.log('Error occurred in getOptions() function while trying getting data for autoselect ' + this.label + ' .Url ' + this.url + ':')
            console.log(error);
            });
      }
      else {
        console.log('Request url for autoselect is not defined. ' 
         + 'Make sure you passed to autoselect component the prepared catalog or request url. Label: ' + this.label)
      }
    },
    filterFn(val, update, abort) {
      if (!this.catalogFn && this.useInput) {
        // standard method

        if (this.options.length > 0) {
          // already loaded
          update();
          return;
        }
        const url =
          typeof this.requestUrl === "function"
            ? this.requestUrl() + val
            : this.requestUrl + val;
        setTimeout(() => {
          update(() => {
            getData(url)
              .then(response => {
                this.options = [];
                this.options = response.data;
              })
              .catch(error => {
                console.log('Error occurred in filterFn() function while trying getting data for autoselect ' + this.label + ' .Url ' + this.url + ':')
                console.log(error);
            });
          });
        }, 300);
      } else if (this.catalogFn) {
        // method for stored catalogs
        if (this.catalog.length > 0) {
          // already loaded
          this.options = this.catalog;
          update();
          return;
        }
        setTimeout(() => {
          update(() => {
            this.catalogFn();
          });
        }, 300);
      } else {
        update();
      }
    },
    abortFilterFn() {
      console.log("delayed filter aborted");
    }
  },
  watch: {
    model() {
      if (this.model) {
        this.$emit("updateData", this.model);
      } else {
        this.$emit("updateData", undefined);
      }

      // if (this.cardId) {
      //   setTimeout(() => {
      //     this.$refs.valid.validate();
      //     this.$emit("valid", this.hasError);
      //   }, 0);
      // }
    },
    catalog() {
      this.options = this.catalog;
    },
    value() {
      this.model = this.value;
    },
    validate() {
      this.$refs.valid.validate();
      this.$emit("valid", this.hasError);
    },
    options() {
      if (this.list) {
        this.$emit("getOptions", this.options);
      }
    }
  },

  created() {
    
    if (this.required) {
      this.requiredRule.push(val => !!val || "Обов'язкове поле");
    }

    if (!this.useInput) {
      this.getOptions();
    }
  },

  mounted() {
    
    // check value and set it
    if (this.value && Object.entries(this.value).length > 0) {
      this.model = this.value;
    } else {
      this.model = undefined;
    }

    if (this.list) {
      getData(this.requestUrl)
        .then(response => {
          this.options = [];
          this.options = response.data;
        })
        .catch(error => console.log(error));
    }
  }
};
</script>

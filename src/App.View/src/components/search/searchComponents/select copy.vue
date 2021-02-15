<template>
  <m-lazy-select
    :url="requestUrl"
    :value="model"
    @input="onInput"
    :options="options"
    :option-value="optionValue"
    :option-label="optionLabel"
    :label="label"
    :hide-dropdown-icon="false"
    :min-filter-chars="3"
    clearable
    :input-class="'search-field_input'"
    class="q-gutter-y-md column"
    popup-content-class="search-options"
  >
  </m-lazy-select>
</template>

<script>
import MLazySelect from "./../../forms/LazyAutocomplete"

export default {
  components: {
    MLazySelect
  },
  data() {
    return {
      model: null,
      options: []
    };
  },
  props: {
    requestUrl: [String, Function],
    label: String,
    value: Object,
    optionValue: {
      type: String,
      default: "id"
    },
    optionLabel: {
      type: String,
      default: "caption"
    }
  },
  
  methods: {
    //
    onInput(value) {
      
      this.model = value
      if (this.model) {
        this.$emit("updateData", this.model);
      } else {
        this.$emit("updateData", null);
        this.$emit("isCleared", true);
      }
    },
  },

  watch: {
    value() {
      this.model = this.value;
    }
  }
};
</script>

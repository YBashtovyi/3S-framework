<template>
  <q-select
    dense
    options-dense
    use-input
    hide-selected
    fill-input
    color="primary"
    label-color="primary"
    clear-icon="close"
    ref="filteredAutocomplete"
    :hint="hint"
    :name="name"
    :disable="disable"
    :clearable="clearable"
    :label="required? `${label} *`: label"
    :value="model"
    @input="onInput"
    @filter="filterFn"
    :hide-dropdown-icon="hideDropdownIcon"
    :options="filteredOptions"
    :option-value="optionValue"
    :option-label="optionLabel"
    :rules="rules ? rules : [value => !required || (!!value || 'Обов\'язкове поле')]"
  >
    <template v-slot:no-option>
      <q-item>
        <q-item-section class="text-grey">Відсутні значення</q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script>
export default {
  data() {
    return {
      // here we store original options prop or filtered options after user inputs some text in main input
      filteredOptions: [],
      // value (v-model) for input
      model: null
    }
  },

  props: {
    // options to select
    options: Array,
    // original value, if present
    value: Object,
    // input label
    label: {
      type: String,
      required: true
    },
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
    // name is used by form to validate inputs
    name: {
      type: String,
      default: ""
    },
    // filter function to use in input; if not set, default function will be used (by option label)
    // so pass this prop only if want to use some tricky search
    filter: Function,
    // field is required
    required: {
      type: Boolean,
      default: false
    },
    // minimum symbols to input to make filter works
    // now enabling minimum characters disables ability to use dropdown, so use it only if dropdown is so big
    // that you see decreasing render performance
    minFilterChars: {
      type: Number,
      default: 0
    },
    //
    hideDropdownIcon: {
      type: Boolean,
      default: false
    },
    // if true, clear-icon will be set, that sets value to null after click
    clearable: {
      type: Boolean,
      default: false
    },
    // input is disabled
    disable: {
      type: Boolean,
      default: false
    },
     rules: {
      type: Array,
      required: false
    },
  },

  computed: {
    // returns hint, that is displayed in input if minimumFilterCharCount prop is greater than zero
    hint() {
      if (this.model) {
        return ""
      }

      if (this.minFilterChars > 0) {
        let symbolPhrase = ""
        if (this.minFilterChars === 1) {
          symbolPhrase = "символ"
        }
        // this won't work correctly on 21, 22 and so on sympbols
        // this is OK, because nobody uses such large starting phrases for filtering
        else if (this.minFilterChars > 4) {
          symbolPhrase = "символів"
        }
        // for 2-4 symbols
        else {
          symbolPhrase = "символи"
        }
        return `Введіть мінімум ${this.minFilterChars} ${symbolPhrase} для пошуку`
      }
      return ""
    }
  },

  methods: {
    // function filters original options by value that user entered in main input field
    filterFn(val, update, abort) {
      if (this.filter) {
        this.filter(val, update, abort)
        return
      }

      // if filter is not set, show all options in dropdown if dropDown is not hidden
      if (val.length === 0 && !this.hideDropdownIcon) {
        update(() => {
          this.filteredOptions = this.options
        })
      }
      // else if started typing, then abort if below minimum
      else if (this.minFilterChars > 0 && val.length < this.minFilterChars) {
        abort()
      } else {
        update(() => {
          const needle = val.toLowerCase()
          this.filteredOptions = this.options.filter(
            v => v[this.optionLabel].toLowerCase().indexOf(needle) > -1
          )
        })
      }
    },

    // emits 'input' event to parent component, when option is selected or cleared
    onInput(value) {
      this.$emit('input', value)
    }
  },

  watch: {
    // watch value prop to update input v-model
    value() {
      this.model = this.value
    }
  },

  created() {
    this.filteredOptions = this.options
    this.model = this.value
  }
}
</script>
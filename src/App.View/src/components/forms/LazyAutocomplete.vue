<template>
  <q-select
    :dense="dense"
    :filled="filled"
    option-dense
    use-input
    :use-chips="multiple"
    :multiple="multiple"
    stack-label
    :hide-selected="!multiple"
    fill-input
    clear-icon="close"
    color="primary"
    :label-color="labelColor"
    ref="lazyAutocomplete"
    :icon="icon"
    :input-debounce="inputDebounce"
    :loading="loading"
    :hint="hint"
    :name="name"
    :disable="disable"
    :clearable="clearable"
    :label="required? `${label} *`: label"
    :value="model"
    @virtual-scroll="onScroll"
    @input="onInput"
    @filter="filterFn"
    :hide-dropdown-icon="hideDropdownIcon"
    :options="filteredOptions"
    :option-value="optionValue"
    :option-label="optionLabel"
    :rules="rules ? rules : [value => !required || (!!value || 'Обов\'язкове поле')]"
  >
    <template v-if="icon" v-slot:prepend>
      <q-icon :name="icon"></q-icon>
    </template>
    <template v-slot:no-option>
      <q-item>
        <q-item-section class="text-grey">Відсутні значення</q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script>
  import { getData } from '@/services/api'

  export default {
    data() {
      return {
        // here we store options fetched from backend
        filteredOptions: [],
        // value (v-model) for input
        model: null,
        // current filter value to search
        currentFilter: '',
        // new options portion is loading
        loading: false,
        // portion size to load at one time
        pageSize: 50,
        // current page number
        currentPage: 0,
        // next page number to load; if nextPage equals current, then all options are loaded
        nextPage: 1
      }
    },

    props: {
      // source of options to select
      url: {
        type: String,
        required: true
      },
      // original value, if present
      value: [Object, Array],
      // input label
      label: {
        type: String,
        required: false
      },
      // in ms, do not disturb backend on every symbol when user inputs several symbols to search
      inputDebounce: {
        type: Number,
        default: 1000
      },
      // field name for select option id
      optionValue: {
        type: String,
        default: 'id'
      },
      // field name for select opton display caption
      optionLabel: {
        type: String,
        default: 'caption'
      },
      // name is used by form to validate inputs
      name: {
        type: String,
        default: ''
      },
      // field is required
      required: {
        type: Boolean,
        default: false
      },
      rules: {
        type: Array,
        required: false
      },
      // minimum symbols to make filter work (this is needed to disable search only on one symbol)
      minFilterChars: {
        type: Number,
        default: 0
      },
      // works in combination with minFilterChars
      // if minFilterChars set, then dropdown won't be displayed when user clicks on input field
      // if minFilterChars is not set, then first options portions will be displayed when clicked on input
      //   and new options are loaded on scroll
      //   but when user starts input search phrase, options won't be loaded until minFilterChars is achieved
      // such 'strange' logic is implemented because quasar fires the same filter event when user clicks on input or dropdown icon
      // so just hiding dropdown is not enough to disable dropdonw when filter function is used
      hideDropdownIcon: {
        type: Boolean,
        default: false
      },
      // if true, clear-icon will be set, that resets value to null after clicked
      clearable: {
        type: Boolean,
        default: false
      },
      // input is disabled
      disable: {
        type: Boolean,
        default: false
      },
      // icon before input
      icon: {
        type: String,
        default: '',
        required: false
      },
      // color of input label text
      labelColor: {
        type: String,
        default: 'primary',
        required: false
      },
      // dense select if this option is enabled
      dense: {
        type: Boolean,
        default: true,
        required: false
      },
      // fill select if this option is enabled
      filled: {
        type: Boolean,
        default: false,
        required: false
      },
      multiple: {
        type: Boolean,
        default: false
      }
    },

    computed: {
      // returns url to fetch data from backend; consists of original url prop + pagination + filter
      requestUrl() {
        // paginated url
        const purl = `${this.url}${this.firstConcatSymbol}pageSize=${this.pageSize}&pageNumber=${this.nextPage}${this.urlOrderBy}`
        // add filter if set
        const url = this.currentFilter ? `${purl}&${this.optionLabel}=${this.currentFilter}` : purl
        return url
      },

      // if url already contains query parameters then reurns &, otherwise ?
      firstConcatSymbol() {
        return this.url.includes('?') ? '&' : '?'
      },

      // returns orderBy expression with & sign, that should be added to query params or empty string if orderBy already present
      urlOrderBy() {
        const alreadyPresent = this.url.toLowerCase().includes('orderby')
        return alreadyPresent ? '' : `&orderBy=${this.optionLabel}`
      },

      // all options fetched from database (last options portion fetched was less then page size)
      allOptionsLoaded() {
        return this.currentPage === this.nextPage
      },

      // returns hint, that is displayed in input if minimumFilterCharCount prop is greater than zero
      hint() {
        if (this.model) {
          return ''
        }

        let searchHint = ''

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
          searchHint = `Введіть мінімум ${this.minFilterChars} ${symbolPhrase} для пошуку`
        } else {
          searchHint = 'Введіть частину слова для пошуку'
        }

        if (!this.hideDropdownIcon) {
          searchHint += ' або скористайтесь випадаючим меню'
        }

        return searchHint
      }
    },

    methods: {
      // triggered by v @filter event
      filterFn(val, update, abort) {
        if (!val) {
          val = this.$refs.lazyAutocomplete.inputValue
        }
        // if dropdown is hidden and the filter is not ready, we should not load options
        if (!this.getShowDropdown) {
          abort()
          return
        }

        if (this.currentFilter === val) {
          this.processFilterIfNotChanged(update)
        } else {
          // if filter changed we need to clear options and then fetch data from backend if minimum filter chars allows
          this.processFilterIfChanged(val, update, abort)
        }
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

      // methods processes filtration situation when filter was not changed and
      //   fetches data from backend if no options were loaded yet, then updates rendered options
      // parameters:
      //   update - quasar function that is passed from filter event
      processFilterIfNotChanged(update) {
        if (!this.filteredOptions.length && !this.allOptionsLoaded && this.getShowDropdown()) {
          this.fetchOptions()
            .then(this.setPagesState)
            .then(() => update(() => true))
        } else {
          update(() => {}) // do nothing
        }
      },

      // methods processes filtration situation when filter is changed or cleared
      // parameters:
      //   filter - string value
      //   update, abort - quasar variables from @filter event
      processFilterIfChanged(filter, update, abort) {
        // set current filter state and reset variables to default values
        this.currentFilter = filter
        this.resetVariables()

        // if filter is not set we can again call filterFn(val, update, abort)
        // for this time the filterFn works for case when filter is not changed (after we reset variables to default)
        if (filter.length === 0) {
          this.filterFn(filter, update, abort)
        }
        // else if started typing and cannot show dropdown yet
        else if (!this.getShowDropdown(filter)) {
          abort()
          // fetch new options and update rendered quasar options
        } else {
          this.fetchOptions()
            .then(this.setPagesState)
            .then(() => update(() => true))
        }
      },

      // methods returns true if dropdown should be displayed when clicked on input or dropdown icon
      // parameters:
      //   filter - filter string value
      getShowDropdown(filter) {
        // if minimum is not set, then show dropdown anyway
        if (this.minFilterChars === 0) {
          return true
        }

        // if input is used without filter, then show dropdown if set
        const filterLength = filter ? filter.length : 0
        if (filterLength === 0) {
          return !this.hideDropdownIcon
        }

        // if minFilterChars is set and greater then current filter, do not show dropdown anyway
        return filterLength >= this.minFilterChars
      },

      // returns promise that fetches options portion from the database and appends them to filtereOptions array
      fetchOptions() {
        this.setLoadingState(true)

        return getData(this.requestUrl)
          .then(({ data }) => this.appendOptions(data))
          .then(() => this.setLoadingState(false))
      },

      // sets loading to true or false
      setLoadingState(value) {
        this.loading = value
      },

      // resets page and options variables to default
      // is used when filter value is changed
      resetVariables() {
        this.currentPage = 0
        this.nextPage = 1
        this.filteredOptions = []
      },

      // appends options to the already existing filteredOptions
      // parameters:
      //   opts - portion that is added to options list
      appendOptions(opts) {
        this.filteredOptions.push(...opts)
        return opts
      },

      // is called on scroll
      // parameters:
      //   opts - portion that is added to options list
      //   ref - quasar object, is a part of o @virtual-scroll api
      refreshOptions(opts, ref) {
        this.$nextTick(() => {
          ref.refresh()
          this.loading = false
        })
      },

      // sets currentPage and nextPage variables depending on last loaded portion
      // parameters:
      //   opts - options portion that is added to options list
      setPagesState() {
        this.currentPage++
        // if new portion has size less than page size, then we loaded all options
        if (this.filteredOptions.length < this.pageSize * this.currentPage) {
          this.nextPage = this.currentPage
        } else {
          this.nextPage = this.currentPage + 1
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
      this.model = this.value
    }
  }
</script>
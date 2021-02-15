<template>
  <div
    :class="`column search-form ` + activeFilters && activeFilters.length > 0 ? `q-mb-sm` : `q-mb-md`"
  >
    <q-form @submit="activateSearch" @reset="clearSearch(true)">
      <div class="row no-wrap justify-between items-start full-width">
        <div class="column full-width">
          <q-input
            v-if="mainInput"
            v-model="mainInputValue"
            :label="mainInput.label"
            :loading="loadingState"
            dense
            standout="bg-primary text-grey-1"
            debounce="700"
            clearable
            clear-icon="clear"
            @clear="onSearch()"
            @keydown.enter.prevent="onSearch()"
          >
            <template v-slot:append>
              <q-btn round dense flat icon="search" @click="onSearch()" />
            </template>
            <template v-slot:after>
              <q-btn
                v-if="filters && filters.length > 0"
                round
                dense
                flat
                @click="visibleFilters = !visibleFilters"
              >
                <q-icon
                  :class="!visibleFilters ? `icon ico-filter` : ``"
                  :name="visibleFilters ? `keyboard_arrow_up` : ``"
                  size="20px"
                />
              </q-btn>
            </template>
          </q-input>

          <div class="row justify-start items-center q-mt-sm" v-if="chips && chips.length > 0">
            <q-chip
              :removable="!visibleFilters && !item.disable"
              @remove="onChipRemove(item)"
              v-for="(item, index) in chips"
              :key="index"
              v-model="item.chip"
              dense
              square
              color="primary"
              text-color="white"
              class="q-ma-none q-mr-xs q-py-md q-mb-xs"
            >{{ item.label ? `${item.label} : ` : '' }} {{ item.name.substring(0,63) }}</q-chip>
            <q-btn
              v-if="
              !visibleFilters &&
                filters &&
                filters.length > 0 &&
                activeParams &&
                activeParams.length > 0
            "
              flat
              dense
              color="grey-8"
              size="12px"
              class="q-ml-auto self-start"
              no-caps
              label="Очистити пошук"
              @click="clearSearch(true)"
            />
          </div>
        </div>

        <q-btn
          v-if="createBtn"
          v-entity-right="config.entity"
          round
          size="13px"
          color="primary"
          icon="add"
          :to="{ path: `${$route.path}/create` }"
          :disable="!createBtn"
          class="q-ml-md"
        >
          <q-tooltip
            anchor="center left"
            self="center right"
            :offset="[10, 10]"
            content-style="font-size: 13px"
          >Створити</q-tooltip>
        </q-btn>
      </div>
      <q-slide-transition>
        <div
          v-show="visibleFilters"
          class="row q-mt-sm q-px-md q-pb-md q-pt-sm bg-white shadow-1 rounded-borders"
        >
          <div v-for="(item, index) in filters" :key="index" class="col-12 q-mb-xs">
            <search-select
              v-if="item.type === 'select'"
              v-model="item.value"
              @updateData="item.value = $event"
              :requestUrl="item.url"
              :label="item.label"
              :disable="item.disable"
              class="q-mb-xs"
            />
            <q-input
              v-else-if="item.type === 'text'"
              v-model="item.value"
              :disable="item.disable"
              v-mask="item.mask ? item.mask : emptyMask255"
              dense
              debounce="500"
              clearable
              clear-icon="clear"
              :input-class="'search-field_input'"
              class="search-field q-mb-xs"
              :rules="[val => !(!val && item.required) || 'Обов\'язкове поле']"
            >
              <template v-slot:before>
                <div class="filter-caption">{{ item.label }}</div>
              </template>
            </q-input>

            <multi-select
              v-else-if="item.type === 'multi-select'"
              v-model="item.value"
              @updateData="item.value = $event"
              :labelBefore="item.label"
              :requestUrl="item.url"
              :catalog="item.options"
              :optionLabel="item.optionLabel ? item.optionLabel : `code`"
              :optionValue="item.optionValue ? item.optionValue : `id`"
              :disable="item.disable"
              class="search-field multi-select q-mb-xs"
            />

            <!-- input  for numbers type s-->
            <q-input
              v-else-if="item.type === 'number'"
              v-model="item.value"
              @keypress="onlyNumber"
              dense
              debounce="500"
              clearable
              clear-icon="clear"
              :input-class="'search-field_input'"
              class="search-field q-mb-xs"
              :rules="[val => !(!val && item.required) || 'Обов\'язкове поле']"
            >
              <template v-slot:before>
                <div class="filter-caption">{{ item.label }}</div>
              </template>
            </q-input>

            <!-- input  for latin characters -->
            <q-input
              v-else-if="item.type === 'textEng'"
              v-model="item.value"
              v-mask="item.mask ? item.mask : emptyMask255"
              @keypress="latinCharacters"
              dense
              debounce="500"
              clearable
              clear-icon="clear"
              :input-class="'search-field_input'"
              class="search-field q-mb-xs"
            >
              <template v-slot:before>
                <div class="filter-caption">{{ item.label }}</div>
              </template>
            </q-input>

            <div v-else-if="item.type === 'number-range'" class="row no-wrap items-center q-mb-xs">
              <div class="filter-caption q-mr-xs">{{ item.label }}</div>
              <div class="row no-wrap" style="flex-grow: 1">
                <q-input
                  v-model="item.start.value"
                  @keypress="onlyNumber"
                  dense
                  debounce="500"
                  clearable
                  clear-icon="clear"
                  class="search-field col-6 q-pr-lg"
                  :rules="[val => !(!val && item.required) || 'Обов\'язкове поле']"
                >
                  <template v-slot:before>
                    <div
                      class="filter-caption"
                      style="width:10px"
                    >{{ item.start.label ? item.start.label : 'З' }}</div>
                  </template>
                </q-input>
                <q-input
                  v-model="item.end.value"
                  @keypress="onlyNumber"
                  dense
                  debounce="500"
                  clearable
                  clear-icon="clear"
                  class="search-field col-6"
                  :rules="[val => !(!val && item.required) || 'Обов\'язкове поле']"
                >
                  <template v-slot:before>
                    <div
                      class="filter-caption"
                      style="width:20px"
                    >{{ item.end.label ? item.end.label : 'По' }}</div>
                  </template>
                </q-input>
              </div>
            </div>

            <div v-else-if="item.type === 'date-range'" class="row no-wrap items-center q-mb-xs">
              <div class="filter-caption q-mr-xs">{{ item.label ? item.label : "Дата" }}</div>
              <div class="row no-wrap" style="flex-grow: 1">
                <date-picker
                  v-model="item.start.value"
                  @updateData="item.start.value = $event"
                  :outlined="false"
                  :lt="item.end.value"
                  class="search-field col-6 q-pr-lg"
                  :label="item.start.label"
                  :required="item.required"
                />
                <date-picker
                  v-model="item.end.value"
                  @updateData="item.end.value = $event"
                  :outlined="false"
                  :gt="item.start.value"
                  class="search-field col-6"
                  :label="item.end.label"
                  :required="item.required"
                />
              </div>
            </div>

            <div v-else-if="item.type === 'date'" class="row no-wrap items-center q-mb-xs">
              <div class="filter-caption q-mr-xs">{{ item.label ? item.label : "Дата" }}</div>
              <date-picker
                v-model="item.value"
                @updateData="item.value = $event"
                :outlined="false"
                class="search-field col-6 q-pr-lg"
                :label="``"
                :required="item.required"
              />
            </div>
            <div v-else-if="item.type === 'check-box'" class="row no-wrap items-center q-mb-xs">
              <div class="filter-caption q-mr-xs">{{ item.label }}</div>
              <q-checkbox size="xs" v-model="item.value" class="q-mb-xs" />
            </div>
          </div>

          <div class="col-12 row justify-end q-mt-xs">
            <q-btn
              v-if="
              visibleFilters &&
                filters &&
                filters.length > 0 &&
                activeParams &&
                activeParams.length > 0
            "
              flat
              color="primary"
              label="Очистити"
              class="q-mr-md"
              type="reset"
            />
            <q-btn
              type="submit"
              :disable="!isSearchFormActive"
              :outline="!isSearchFormActive"
              color="primary"
              label="Пошук"
            />
          </div>
        </div>
      </q-slide-transition>
    </q-form>
  </div>
</template>

<script>
// CONSTANTS
import {
  FILTER_OPTIONS,
  ACTIVE_FILTERS_OPTIONS,
  PAGINATION_OPTIONS,
} from '../../services/table-list-stored-options/constants'

import searchSelect from './searchComponents/select'
import multiSelect from '@/components/forms/multiselectDepricated.vue'
import datePicker from '@/components/forms/datepicker.vue'
import cloneDeep from 'lodash.clonedeep'
import isEqual from 'lodash.isequal'
import maskUtil from '@/mixins/maskUtil'
import moment from 'moment'
import { mapState, mapActions } from 'vuex'
import isEmpty from 'lodash.isempty'
import get from 'lodash.get'
import {
  getTableOptions,
  setTableOptions,
  removeTableOptions,
} from '../../services/table-list-stored-options'
import { stringToSnakeCase, stringEmpty } from '../../utils/function-helpers/string'
import UrlBuilder from '../../utils/url-builder'

export default {
  components: {
    searchSelect,
    multiSelect,
    datePicker,
  },
  mixins: [maskUtil],
  data() {
    return {
      mainInput: {},
      mainInputValue: null,
      filters: [],
      options: [],
      loadingState: false,
      activeFilters: [],
      chips: [],
      isSearchFormActive: false,
      visibleFilters: this.isVisibleFilters,
    }
  },
  props: {
    config: Object,
    // if not set, pagination object will be set from store
    pagination: {
      type: Object,
      required: false,
    },
    createBtn: {
      type: Boolean,
      default: true,
    },
    isLocalSearch: {
      type: Boolean,
      default: false,
    },
    isVisibleFilters: {
      type: Boolean,
      default: false,
    },
    /**
     * Determines whether or not to keep filter values ​​in local storage
     */
    withSave: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    filters: {
      deep: true,
      handler() {
        if (!isEqual(this.filters, this.config.filters)) {
          this.isSearchFormActive = true
        } else {
          this.isSearchFormActive = false
        }
      },
    },
    mainInputValue: function() {
      this.mainInput.value = this.mainInputValue
      this.$emit('updateData', this.mainInputValue)
    },
  },

  created() {
    this.setDefaultData()
    this.checkFilters()
    this.saveFiltersToStore()
  },

  mounted() {
    this.setSearchParams({ params: this.searchParams })
  },

  destroyed() {
    this.clearSearchParams({ params: '' })
  },

  methods: {
    ...mapActions('tableList', ['init', 'clearSearchParams', 'setSearchParams']),

    setDefaultData() {
      const savedFitlers = !isEmpty(this.componentKey)
        ? getTableOptions(this.componentKey)(FILTER_OPTIONS)
        : []
      this.mainInput = cloneDeep(this.config.mainInput)
      this.filters =
        this.withSave && !isEmpty(savedFitlers)
          ? cloneDeep(savedFitlers)
          : cloneDeep(this.config.filters)
    },

    // only numbers
    onlyNumber($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which
      if (keyCode < 48 || keyCode > 57) {
        // 46 is dot
        $event.preventDefault()
      }
    },

    onSearch() {
      if (this.isLocalSearch) {
        return
      }
      if (!this.checkFieldRules()) {
        return
      }

      this.saveFiltersToStore()
      // if own pagination is set we return search configuration without sending http request
      if (this.pagination) {
        this.loadingState = false
        this.$emit('onSearch', {
          pagination: this.pagination,
          params: this.searchParams,
          requestUrl: this.config.searchUrl,
          searchDictionary: this.activeParams,
        })
        return
      }

      setTimeout(() => {
        // After filters page should set to '1'
        const currentPaginationFilters = { ...this.paginationTable, page: 1 }
        if (this.withSave) {
          setTableOptions(this.componentKey, currentPaginationFilters, PAGINATION_OPTIONS)
        }
        this.init({
          pagination: currentPaginationFilters,
          params: this.searchParams,
          requestUrl: this.config.searchUrl,
          searchDictionary: this.activeParams,
        }).then(() => {
          setTimeout(() => {
            this.loadingState = false
          }, 500)
        })
      }, 500)
    },

    saveFiltersToStore() {
      if (this.withSave) {
        const mapActiveFitlers = af => ({ key: af.key, value: af.value })
        const mapedActiveFilters = this.activeFilters.map(mapActiveFitlers)
        setTableOptions(this.componentKey, mapedActiveFilters, ACTIVE_FILTERS_OPTIONS)
        setTableOptions(this.componentKey, this.filters, FILTER_OPTIONS)
      }
    },

    onChipRemove(selectedChip) {
      const mapRemoved = c => ({ ...c, isRemoved: c.key === selectedChip.key })
      this.activeFilters = this.activeFilters.map(mapRemoved)
      this.clearFilter(selectedChip)
    },

    clearSearch(withRequest) {
      const filterClear = c => c.disable === true
      this.activeFilters = this.activeFilters.filter(filterClear)
      this.chips = this.chips.filter(filterClear)
      if (this.withSave) {
        removeTableOptions(this.componentKey)
      }

      this.setDefaultData()

      if (withRequest) {
        this.onSearch()
      }
    },

    activateSearch() {
      this.checkFilters()
      this.loadingState = true
      this.onSearch()
    },
    changeControlUrl(el) {
      let paramsId = '',
        url = []

      for (let i in this.filters) {
        // if (this.filters[i].relation) {
        //   el = JSON.parse(JSON.stringify(this.filters[i]))
        // }
        if (this.filters[i].value && el.relation === this.filters[i].key) {
          // console.log(this.filters[i].value, this.filters[i].key)
          paramsId = this.filters[i].value.id
        }
      }
      if (paramsId) {
        for (let i in this.filters) {
          if (el.key === this.filters[i].key) {
            url = this.filters[i].url.split('?')
            url.splice(1, 0, '?', this.filters[i].relation, '=', paramsId, '&')
            url = url.join('')
            this.filters[i].url = url
          }
        }
      }
      // return url
    },

    checkFilters() {
      let renewedActiveFilters = []
      this.chips = []
      if (this.filters && this.config.savedFilters && isEqual(this.filters, this.config.filters)) {
        // check if saved filters exists and copy them to main filters
        this.copySavedFiltersToMainFilters(this.filters, this.config.savedFilters)
      }
      if (this.filters) {
        this.refreshFilters(this.filters, renewedActiveFilters, this.chips)
      }
      this.activeFilters = renewedActiveFilters
    },

    // todo in developing
    checkFieldRules() {
      for (let i in this.filters) {
        if (this.filters[i].rules) {
          for (let k in this.filters[i].rules) {
            if (!this.filters[i].rules[k](this.getFiltersFieldValue)) {
              return false
            } else {
              return true
            }
          }
        }
      }
      return true
    },

    copySavedFiltersToMainFilters(mainFilters, savedFilters) {
      savedFilters.forEach(savedFilter => this.copySavedFilter(savedFilter, mainFilters), this)
    },

    copySavedFilter(savedFilter, mainFilters) {
      mainFilters.forEach(mainFilter => this.processCopying(mainFilter, savedFilter), this)
    },

    processCopying(mainFilter, savedFilter) {
      if (savedFilter.key === mainFilter.key) {
        Object.assign(mainFilter, savedFilter)
      }
    },

    refreshFilters(filters, renewedActiveFilters, chips) {
      filters.forEach(mainFilter => {
        switch (mainFilter.type) {
          case 'text': {
            this.refreshTextFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          // only numbers
          case 'number': {
            this.refreshTextFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          // only latin characters and numbers
          case 'textEng': {
            this.refreshTextFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'number-range': {
            this.refreshNumberRangeFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'select': {
            this.refreshSelectFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'multi-select': {
            this.refreshMultiSelectFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'date-range': {
            this.refreshDateRangeFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'date': {
            this.refreshDateFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          case 'check-box': {
            this.refreshCheckBoxFilter(mainFilter, renewedActiveFilters, chips)
            break
          }
          default: {
            break
          }
        }
      }, this)
    },

    refreshCheckBoxFilter(mainFilter, renewedActiveFilters, chips) {
      if (typeof mainFilter.value === 'boolean') {
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: mainFilter.value,
        })

        if (mainFilter.value) {
          this.chips.push({
            name: mainFilter.label,
            key: mainFilter.key,
            type: mainFilter.type,
            disable: mainFilter.disable,
          })
        }
      }
    },

    refreshTextFilter(mainFilter, renewedActiveFilters, chips) {
      if (typeof mainFilter.value === 'string' && mainFilter.value.length > 0) {
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: mainFilter.value,
          label: mainFilter.value,
          chip: true,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
        chips.push({
          name: mainFilter.value,
          label: mainFilter.label,
          key: mainFilter.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    refreshNumberRangeFilter(mainFilter, renewedActiveFilters, chips) {
      // start number
      if (typeof mainFilter.start.value === 'string' && mainFilter.start.value.length > 0) {
        renewedActiveFilters.push({
          key: mainFilter.start.key,
          value: mainFilter.start.value,
        })
        this.chips.push({
          name: mainFilter.start.value,
          label: mainFilter.label + ' з',
          key: mainFilter.start.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }

      // end number
      if (typeof mainFilter.end.value === 'string' && mainFilter.end.value.length > 0) {
        renewedActiveFilters.push({
          key: mainFilter.end.key,
          value: mainFilter.end.value,
        })
        this.chips.push({
          name: mainFilter.end.value,
          label: mainFilter.label + ' по',
          key: mainFilter.end.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    refreshSelectFilter(mainFilter, renewedActiveFilters, chips) {
      if (
        mainFilter.value !== null &&
        typeof mainFilter.value === 'object' &&
        Object.keys(mainFilter.value).length > 0
      ) {
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: mainFilter.valueType === 'id' ? mainFilter.value.id : mainFilter.value.name,
          label: mainFilter.value.name,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
        this.chips.push({
          name: mainFilter.value.name,
          label: mainFilter.label,
          key: mainFilter.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    refreshMultiSelectFilter(mainFilter, renewedActiveFilters, chips) {
      if (
        mainFilter.value !== null &&
        typeof mainFilter.value === 'object' &&
        Object.keys(mainFilter.value).length > 0
      ) {
        let multiVal = []
        const optionValue = get(mainFilter, 'optionValue', 'id')
        for (let i in mainFilter.value) {
          const value = get(mainFilter.value[i], optionValue, null)
          multiVal.push(value)
          this.chips.push({
            name: mainFilter.value[i].name,
            label: mainFilter.label,
            key: mainFilter.key,
            value,
            type: mainFilter.type,
            disable: mainFilter.disable,
          })
        }
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: '[' + multiVal + ']',
          label: mainFilter.value.name,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    refreshDateRangeFilter(mainFilter, renewedActiveFilters, chips) {
      // start date
      if (mainFilter.start.value) {
        renewedActiveFilters.push({
          key: mainFilter.start.key,
          value: mainFilter.start.value,
        })
        this.chips.push({
          name: moment(mainFilter.start.value)
            .locale('uk')
            .format('L'),
          label: mainFilter.label + ' з',
          key: mainFilter.start.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }

      // end date
      if (mainFilter.end.value) {
        renewedActiveFilters.push({
          key: mainFilter.end.key,
          value: mainFilter.end.value.split('T')[0] + 'T23:59:59.99999',
        })
        this.chips.push({
          name: moment(mainFilter.end.value)
            .locale('uk')
            .format('L'),
          label: mainFilter.label + ' по',
          key: mainFilter.end.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    refreshDateFilter(mainFilter, renewedActiveFilters, chips) {
      if (typeof mainFilter.value === 'string' && mainFilter.value.length > 0) {
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: mainFilter.value,
        })
        this.chips.push({
          name: moment(mainFilter.value)
            .locale('uk')
            .format('L'),
          label: mainFilter.label,
          key: mainFilter.key,
          type: mainFilter.type,
          disable: mainFilter.disable,
        })
      }
    },

    // only for latin characters and numbers
    latinCharacters: function(evt) {
      var regex = new RegExp('^[a-zA-Z0-9]')
      var key = String.fromCharCode(evt.charCode ? evt.charCode : evt.which)
      if (!regex.test(key)) {
        event.preventDefault()
        return false
      }
    },

    clearFilter(chip) {
      let arr = [],
        removedItems = []

      for (let i in this.activeParams) {
        if (this.activeParams[i].isRemoved) {
          removedItems.push(this.activeParams[i])
        } else {
          arr.push(this.activeParams[i])
        }
      }
      this.activeFilters = arr
      for (let k in this.filters) {
        for (let i in removedItems) {
          // REMOVE FILTER FOR date range and number range
          if (
            (this.filters[k].type === 'date-range' && chip.type === 'date-range') ||
            (this.filters[k].type === 'number-range' && chip.type === 'number-range')
          ) {
            if (this.filters[k].start.key === removedItems[i].key) {
              this.filters[k].start.value = null
            } else if (this.filters[k].end.key === removedItems[i].key) {
              this.filters[k].end.value = null
            }
          } else if (
            this.filters[k].type === 'multi-select' &&
            chip.type === 'multi-select' &&
            chip.key === this.filters[k].key
          ) {
            this.filters[k].value = this.filters[k].value.filter(
              v => v.id !== chip.value && v.code !== chip.value,
            )
          } else if (this.filters[k].key === removedItems[i].key) {
            this.filters[k].value = null
          }
        }
      }

      this.activateSearch()
    },
  },
  computed: {
    ...mapState('tableList', ['paginationTable']),
    ...mapState('baseElements', ['rights']),
    activeParams() {
      let arr = []
      if (this.activeFilters && this.activeFilters.length > 0) {
        arr = [...this.activeFilters]
      }
      // add main input value
      if (this.mainInput && this.mainInput.value) {
        arr.push({
          key: this.mainInput.key,
          value: this.mainInput.value,
          label: this.mainInput.value,
          chip: true,
          main: true,
        })
      }
      return arr
    },

    requestUrl() {
      return this.config.searchUrl
    },

    searchParams() {
      const urlBuilder = new UrlBuilder({ withoutHost: true })
      const appendActiveParameterToUrl = activeParam => {
        const key = get(activeParam, 'key', stringEmpty())
        const value = get(activeParam, 'value', stringEmpty())
        urlBuilder.param(key, encodeURIComponent(value))
      }

      this.activeParams.forEach(appendActiveParameterToUrl)

      const requestUrlHasQueryParams = /[?]/.test(this.requestUrl)
      const queryParams = urlBuilder.toString()
      return requestUrlHasQueryParams ? `&${queryParams.substr(1)}` : queryParams
    },
    componentKey() {
      return !isEmpty(this.$route.name) ? stringToSnakeCase(this.$route.name, true) : ''
    },
    getFiltersFieldValue() {
      let result = {}
      this.filters.forEach(filter => {
        result[filter.key] = filter.value
      })
      return result
    },
  },
}
</script>

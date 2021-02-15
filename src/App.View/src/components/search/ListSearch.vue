<template>
  <div
    :class="`column search-form ` + activeFilters && activeFilters.length > 0 ? `q-mb-sm` : `q-mb-md`"
  >
    <div class="row no-wrap justify-between items-start full-width">
      <div class="column full-width">
        <q-input
          v-model="mainInputValue"
          :label="mainInput.label"
          :loading="loadingState"
          color="primary"
          outlined
          dense
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
            :removable="!visibleFilters"
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
            @click="clearSearch()"
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
        <div v-for="(item, index) in filters" :key="index" class="col-12 q-mb-xs q-pa-sm">
          <m-lazy-select
            v-if="item.type === 'select'"
            :value="item.value"
            @input="(value) => item.value = value"
            :url="item.url"
            :label="item.label"
            clearable
            dense
            class="q-mb-xs"
          />
          <q-input
            v-else-if="item.type === 'text' && !item.mask"
            v-model="item.value"
            dense
            clearable
            clear-icon="clear"
            :label="item.label"
            color="primary"
            label-color="primary"
          />

          <q-input
            v-else-if="item.type === 'text' && item.mask"
            v-model="item.value"
            v-mask="maskDeclarationNumber"
            dense
            clearable
            clear-icon="clear"
            :label="item.label"
            color="primary"
            label-color="primary"
          />

          <multi-select
            dense
            v-else-if="item.type === 'multi-select'"
            v-model="item.value"
            @updateData="item.value = $event"
            :label="item.label"
            :requestUrl="item.url"
            :catalog="item.options"
            :optionLabel="item.optionLabel ? item.optionLabel : `name`"
            :optionValue="item.optionValue ? item.optionValue : `id`"
          />

          <!-- input  for numbers type s-->
          <q-input
            v-else-if="item.type === 'number'"
            v-model="item.value"
            @keypress="onlyNumber"
            dense
            clearable
            clear-icon="clear"
            :label="item.label"
            color="primary"
            label-color="primary"
          />

          <!-- input  for latin characters -->
          <q-input
            v-else-if="item.type === 'textEng'"
            v-model="item.value"
            v-mask="item.mask ? item.mask : null"
            @keypress="latinCharacters"
            dense
            clearable
            clear-icon="clear"
            :label="item.label"
            color="primary"
            label-color="primary"
          />

          <div v-else-if="item.type === 'date-range'" class="row no-wrap items-center">
            <div class="filter-caption">{{ item.label ? item.label : "Дата" }}</div>
            <div class="row no-wrap" style="flex-grow: 1">
              <date-picker
                dense
                v-model="item.start.value"
                @updateData="item.start.value = $event"
                :outlined="false"
                :lt="item.end.value"
                class="search-field col-6 q-pr-lg"
              />
              <date-picker
                dense
                v-model="item.end.value"
                @updateData="item.end.value = $event"
                :outlined="false"
                :gt="item.start.value"
                class="search-field col-6"
              />
            </div>
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
            @click="clearSearch()"
          />
          <q-btn
            @click="activateSearch()"
            :disable="!isSearchFormActive"
            :outline="!isSearchFormActive"
            color="primary"
            label="Пошук"
          />
        </div>
      </div>
    </q-slide-transition>
  </div>
</template>

<script>
//   import Vue from 'vue'
// CONSTANTS
import {
  FILTER_OPTIONS,
  ACTIVE_FILTERS_OPTIONS,
  PAGINATION_OPTIONS,
} from '../../services/table-list-stored-options/constants'

import LazySelect from './../forms/LazyAutocomplete'
import multiSelect from '@/components/forms/Multiselect'
import datePicker from '@/components/forms/datepicker'
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
// import moment from "moment"

export default {
  components: {
    multiSelect,
    datePicker,
    'm-lazy-select': LazySelect,
  },
  mixins: [maskUtil],
  data() {
    return {
      mainInput: {},
      mainInputValue: null,
      filters: [],
      options: [],
      visibleFilters: false,
      loadingState: false,
      activeFilters: [],
      chips: [],
      hasSavedFilters: false,
      isSearchFormActive: false,
    }
  },
  props: {
    // search config
    config: {
      type: Object,
      required: true,
    },

    // if not set, pagination object will be set from store
    // TODO: mabe should not use store pagination will be required
    pagination: {
      type: Object,
      required: false,
    },

    // if need to display create button (obsolete, should be removed)
    createBtn: {
      type: Boolean,
      default: true,
    },

    // who knows...
    isLocalSearch: {
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
      handler(after, before) {
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
  },
  mounted() {
    if (this.config.savedFilters) {
      this.setSearchParams({ params: this.searchParams })
    }
  },
  destroyed() {
    this.clearSearchParams({
      params: '',
    })
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

      // if own pagination is set we return search configuration without sending http request
      if (this.pagination) {
        this.loadingState = false
        this.$emit('onSearch', {
          pagination: this.innerPagination,
          params: this.searchParams,
          requestUrl: this.config.searchUrl,
          searchDictionary: this.activeParams,
        })
        return
      }
      this.saveFiltersToStore()
      setTimeout(() => {
        const currentPaginationFilters = { ...this.innerPagination, page: 1 }

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

    onChipRemove(selectedChip) {
      const mapRemoved = c => ({ ...c, isRemoved: c.key === selectedChip.key })
      this.activeFilters = this.activeFilters.map(mapRemoved)
      this.clearFilter(selectedChip)
    },

    saveFiltersToStore() {
      if (this.withSave) {
        const mapActiveFitlers = af => ({ key: af.key, value: af.value })
        const mapedActiveFilters = this.activeFilters.map(mapActiveFitlers)

        setTableOptions(this.componentKey, mapedActiveFilters, ACTIVE_FILTERS_OPTIONS)
        setTableOptions(this.componentKey, this.filters, FILTER_OPTIONS)
      }
    },

    clearSearch() {
      this.activeFilters = []
      this.chips = []
      if (this.withSave) {
        removeTableOptions(this.componentKey)
      }
      this.setDefaultData()
      this.onSearch()
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
        })
        chips.push({
          name: mainFilter.value,
          label: mainFilter.label,
          key: mainFilter.key,
          type: mainFilter.type,
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
        })
        this.chips.push({
          name: mainFilter.value.name,
          label: mainFilter.label,
          key: mainFilter.key,
          type: mainFilter.type,
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
          })
        }
        renewedActiveFilters.push({
          key: mainFilter.key,
          value: '[' + multiVal + ']',
          label: mainFilter.value.name,
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
        })
      }
    },

    saveFilters() {
      localStorage.setItem(
        'savedFilters',
        JSON.stringify({
          page: this.config.title,
          params: this.activeParams,
        }),
      )
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

    setSavedFilters() {
      // check saved filters
      let saved = localStorage.getItem('savedFilters')
      if (saved && saved.length > 0) {
        this.hasSavedFilters = true
        saved = JSON.parse(localStorage.getItem('savedFilters'))
        if (this.config.title === saved.page) {
          this.activeFilters = JSON.parse(JSON.stringify(saved.params))
          // console.log(this.activeFilters)
        }
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
          // REMOVE FILTER FOR date range
          if (this.filters[k].type === 'date-range' && chip.type === 'date-range') {
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
            this.filters[k].value = this.filters[k].value.filter(v => v.id !== chip.value)
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

    // returns current pagination source from possbilble sources:
    //   this.paginationTable - pagination from store
    //   this.pagination - pagination from props
    innerPagination() {
      if (this.paginationTable || !this.pagination) {
        return this.paginationTable
      } else {
        return this.pagination
      }
    },

    activeParams() {
      let arr = []
      if (this.activeFilters && this.activeFilters.length > 0) {
        arr = [...this.activeFilters]
      }
      // add main input value
      if (this.mainInput.value) {
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
      let url = this.config.searchUrl
      return url
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
  },
}
</script>

<template>
  <q-table
    :data="tableData"
    :columns="columns"
    :visible-columns="visibleColumns"
    :loading="isLoading"
    :pagination.sync="innerPagination"
    :grid="$q.screen.lt.md && !expanded"
    :hide-header="$q.screen.lt.md"
    :flat="flat"
    :bordered="flat"
    :filter="localFilter"
    color="primary"
    binary-state-sort
    @request="onRequest"
    :selection="selection"
    :selected.sync="selected"
    :row-key="rowKey"
    :rows-per-page-options="[5, 10, 20, 50, 100]"
    class="data-table"
    ref="dtable"
  >
    <template v-slot:top="props">
      <div class="row no-wrap justify-end full-width">
        <!-- select all button -->
        <div class="q-pa-md">
          <q-btn
            v-if="selection !== 'none' && selection !== 'single'"
            @click="onSelectAllButtonClick(props)"
            :disable="isSelectAllButtonDisabled"
            :label="selected && selected.length > 0 ? `Відмінити` : `Обрати всі`"
            class="q-mr-sm"
            color="grey-3"
            text-color="primary"
          />
        </div>
        <!-- end of select all button -->

        <div v-if="columnsSelect || fileFormats" class="row no-wrap q-mr-auto q-pa-md">
          <q-select
            v-if="columnsSelect"
            v-model="visibleColumns"
            :display-value="$q.lang.table.columns"
            :options="columns"
            option-value="name"
            multiple
            dense
            borderless
            options-dense
            emit-value
            map-options
            style="min-width: 140px"
            class="q-mr-md text-grey-8"
          />

          <ColumnSelectedDialog
            :columns="modalColumns"
            :isDialogVisible="isColumnSelectDialogVisible"
            @dialogCloseEvent="dialogCloseHandler()"
            :searchDictionary="searchParams"
            :selectedfilter="selectedFileFormat"
            :columnsSelectListSkip="columnsSelectListSkip"
            :sort="sort"
          />

          <q-btn-dropdown v-if="fileFormats" unelevated icon="cloud_download" class="text-grey-8">
            <q-list v-for="(item, i) in fileFormats" :key="i">
              <q-item
                clickable
                :disable="item.disable"
                v-close-popup
                @click="onFormatButtonClick(i)"
              >
                <q-item-section side>
                  <q-icon :name="item.icon" size="20px" color="grey-8" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{ item.label }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </div>

        <!-- header buttons -->
        <div
          v-for="btn in headerButtons"
          :key="btn.name"
          :class="btn.parentClass ? btn.parentClass: ''"
          class="q-pa-md"
        >
          <span v-if="btn.desc" class="q-mr-md">{{ btn.desc}}</span>

          <q-btn
            v-entity-right="btn.entityRight"
            v-operation-right="btn.operationRight"
            :class="btn.class"
            :disable="btn.disable"
            @click="btn.action"
            :color="btn.color || 'primary'"
            :dense="btn.rounded"
          >
            <q-icon left :size="btn.iconSize" :name="btn.icon" :class="btn.iconClass" />

            <div>{{ btn.label }}</div>

            <q-tooltip
              v-if="btn.tooltip"
              anchor="center right"
              self="center left"
              :offset="[5, 10]"
              content-style="font-size: 13px"
            >{{ btn.tooltip }}</q-tooltip>
          </q-btn>
        </div>
      </div>
    </template>

    <template v-slot:body="props">
      <q-tr
        :props="props"
        :class="[
          actions ? 'cursor-pointer' : '',
          props.expand ? 'bg-primary-light' : '',
          setCustomRowColor(props)
        ]"
        @click.native="onRowClick(props)"
      >
        <q-td auto-width v-if="selection !== 'none'">
          <q-checkbox
            @click.native="onCheckBoxClick()"
            v-model="props.selected"
            v-if="canSelectRow(props)"
            dense
          />
        </q-td>

        <q-td v-for="col in props.cols" :key="col.name" :props="props">
          <template>
            <router-link v-if="col.linked" :to="`${col.url}`">{{col.value}}</router-link>

            <span
              v-else-if="col.colored"
              :class="'item-colored ' + setCustomColor(props)"
            >{{ col.statuses }}{{ col.value }}</span>

            <q-checkbox size="xs" disable v-else-if="col.isCheckBox" v-model="col.value" />

            <span v-else>{{ col.value }}</span>
          </template>
          <q-tooltip v-if="col.tooltip" :offset="[10, 10]">{{ getTooltip(col.tooltip, col) }}</q-tooltip>
        </q-td>
        <!-- expanded mode-->
        <q-td v-if="expanded" style="width: 30px; padding: 4px" :key="props.row.name">
          <q-btn
            dense
            round
            flat
            :icon="props.expand ? 'keyboard_arrow_up' : 'keyboard_arrow_down'"
            color="grey-8"
            @click.stop="onRowClick(props)"
          />
        </q-td>
        <td style="width: 30px; padding: 4px" v-if="isVisibleActionsBtn" class="table-btn-actions">
          <q-btn
            flat
            round
            dense
            icon="more_vert"
            color="primary"
            @click.stop="$emit('selectedRow', props.row), showMenu(props.row)"
          />
          <component
            :is="`tableMenu`"
            v-bind="{
              actions: actions,
              props: props,
              selectedItem: selectedItem,
              toggleShow: isVisibleMenu,
              deleteUrl: innerDeleteUrl,
              requestUrl: innerUrl
            }"
          />
        </td>
      </q-tr>

      <!-- row for expanded mode -->
      <q-tr v-if="props.expand" :props="props" class="table-expanded_row">
        <q-td colspan="100%">
          <component
            :is="`tableExpandRow`"
            v-bind="{
              expanded: expanded,
              row: props.row,
              visible: props.expand}"
          ></component>
        </q-td>
      </q-tr>
      <!--end of row for expanded mode -->

      <!-- end of expanded mode-->
    </template>

    <!-- table card grid(for tablet & mobile) -->
    <template v-slot:item="props" class="q-gutter-md">
      <div
        class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3 grid-style-transition cursor-pointer"
      >
        <q-card :class="props.selected ? 'bg-grey-2' : ''">
          <q-card-section v-if="selection !== 'none'">
            <q-checkbox
              v-model="props.selected"
              :label="props.row[headerCardLabel]"
              dense
              v-if="canSelectRow(props)"
            />
          </q-card-section>
          <q-separator v-if="selection !== 'none'" />

          <q-card-section
            class="q-pa-none"
            @click="$emit('selectedRow', props.row)"
            @click.native="onRowClick(props)"
          >
            <q-list no-border>
              <q-item
                v-for="col in props.cols.filter(col => col.name !== 'desc')"
                :key="col.name"
                class="q-py-sm q-px-md"
                style="min-height: 32px"
              >
                <q-item-section class="col-4" top>
                  <q-item-label>{{ col.label }}:</q-item-label>
                </q-item-section>
                <q-item-section top class="col-8">
                  <q-item-label>{{ col.value }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-card-section>
        </q-card>
      </div>
    </template>
    <!-- end of table card grid(for tablet & mobile) -->

    <!-- <template v-slot:pagination="props">
      <q-pagination v-model="innerPagination" :max="props.pagesNumber">
      </q-pagination>
    </template>-->
  </q-table>
</template>


<script>
import tableList from './mixins/tableList'
import ColumnSelectedDialog from 'src/components/table/ColumnsSelectDialog.vue'
import tableMenu from 'src/components/table/tableMenu.vue'
import tableExpandRow from 'src/components/table/tableExpandRow.vue'

export default {
  mixins: [tableList],

  components: {
    ColumnSelectedDialog,
    tableMenu,
    tableExpandRow,
  },

  mounted() {
    this.onMounted()
  },
}
</script>

<style lang="stylus">
.q-table td .item-colored {
  position: relative;
  padding-left: 10px;
  white-space: nowrap;

  &.active {
    &:before {
      background-color: $positive;
    }
  }

  &.stopped {
    &:before {
      background-color: $negative;
    }
  }

  &.waiting {
    &:before {
      background-color: $amber-5;
    }
  }

  &.canceled {
    &:before {
      background-color: $grey-6;
    }
  }

  &.empty {
    &:before {
      background-color: $blue-6;
    }
  }

  &.new {
    &:before {
      background-color: $primary;
    }
  }

  &.green {
    &:before {
      background-color: green;
    }
  }

  &.red {
    &:before {
      background-color: red;
    }
  }

  &.orange {
    &:before {
      background-color: orange;
    }
  }

  &:before {
    content: '';
    position: absolute;
    left: 0;
    top: 0;
    width: 8px;
    height: 8px;
    border-radius: 50%;
    transform: translate(-50%, 60%);
  }
}
</style>

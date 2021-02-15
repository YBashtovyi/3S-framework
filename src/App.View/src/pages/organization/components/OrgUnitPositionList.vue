<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Штатні одиниці</div>
      <q-btn
        @click="onAddOrgUnitPosition"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати штатну одиницю"
      />
    </div>

    <q-table
      :data="orgUnitPositionList"
      :columns="columns"
      :flat="true"
      dense
      row-key="id"
      :pagination.sync="pagination"
      @request="onPaginationChanged"
      :rows-per-page-options="[5, 10, 20, 50, 100]"
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td
            v-for="col in props.cols.filter(value => value.label !== '')"
            :key="col.name"
            :props="props"
          >
            <span>{{ col.value }}</span>
          </q-td>
          <q-td auto-width>
            <q-btn
              flat
              round
              dense
              icon="more_vert"
              color="primary"
              @click.stop="onShowMenu(props.row)"
            />
            <component
              :is="`listMenu`"
              v-bind="{
                actions: actions,
                props: props,
                selectedItem: selectedItem,
                toggleShow: isVisibleMenu,
              }"
            />
          </q-td>
        </q-tr>
      </template>
    </q-table>

    <org-unit-position-edit-dialog
      :isVisible="isVisibleEditOrgUnitPositionDialog"
      :orgUnitPositionId="selectedOrgUnitPositionId"
      :orgUnitId="orgUnitId"
      @closeDialog="onCloseOrgUnitPositionDialog"
    />
  </div>
</template>

<script>
import ListMenu from '../../../components/table/ListMenu'
import OrgUnitPositionEditDialog from './OrgUnitPositionEditDialog'
import orgUnitPositionList from '../mixins/orgUnitPositionList'

export default {
  mixins: [orgUnitPositionList],

  components: {
    ListMenu,
    OrgUnitPositionEditDialog,
  },
}
</script>

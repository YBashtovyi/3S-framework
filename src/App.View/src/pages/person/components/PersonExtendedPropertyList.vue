<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Додаткові дані</div>
      <q-btn
        @click="onAddOrEditPersonExtendedProperty"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати"
      />
    </div>

    <q-table
      :data="list"
      :columns="columns"
      :flat="true"
      dense
      row-key="id"
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
    <person-extended-property-dialog
      :isVisibleDialog="isVisibleEditPersonExtendedPropertyDialog"
      :personExtendedPropertyId="selectedPersonExtendedPropertyId"
      :personId="personId"
      @closeDialog="onClosePersonExtendedPropertyDialog"
    />
  </div>
</template>

<script>
import ListMenu from '../../../components/table/ListMenu'
import PersonExtendedPropertyDialog from './PersonExtendedPropertyDialog'

import personExtendedPropertyList from '../mixins/personExtendedPropertyList'

export default {
  mixins: [personExtendedPropertyList],

  components: {
    PersonExtendedPropertyDialog,
    ListMenu,
  },
}
</script>
<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Права</div>
      <q-btn
        @click="onOpenAddRightDialog"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати право"
      />
    </div>
    <q-table dense :data="roleRightList" :columns="columns" :flat="true" row-key="id">
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
    <search-right
      :isVisibleDialog.sync="isVisibleRoleRightDialog"
      @selectedItems="onAddRightToRole"
    />
  </div>
</template>

<script>
import SearchRight from '../../../../components/dialogs/searchRight'
import ListMenu from '../../../../components/table/ListMenu'
import roleRightList from '../mixins/roleRightList'

export default {
    mixins: [roleRightList],

    components: {
        ListMenu,
        SearchRight
    }
}
</script>
<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Ролі</div>
      <q-btn
        @click="onOpenAddRoleDialog"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати роль"
      />
    </div>
    <q-table :data="userRoleList" :columns="columns" :flat="true" row-key="id" hide-bottom>
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
    <search-role-dialog
      :isVisibleDialog.sync="isVisibleUserRoleEditDialog"
      @selectedItems="onAddRoleToUser"
    />
  </div>
</template>

<script>
import ListMenu from '../../../../components/table/ListMenu'
import SearchRoleDialog from '../../../../components/dialogs/searchRole'
import userRoleList from '../mixins/userRoleList'

export default {
    mixins: [userRoleList],

    components: {
        ListMenu,
        SearchRoleDialog
    }
}
</script>
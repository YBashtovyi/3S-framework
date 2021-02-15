<template>
  <div>
    <div class="row bg-grey-2 justify-between q-mt-sm">
      <div class="text-subtitle2 text-uppercase q-pa-md">Додаткові характеристики</div>
      <q-btn
        @click="onAddExtendedPropertyDialog"
        icon="add"
        rounded
        flat
        color="primary"
        label="Додати характеристику"
      />
    </div>
    <q-table :data="list" :columns="columns">
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
    <construction-object-ex-propery-dialog
      :isVisibleDialog.sync="isVisibleConstObjPropertyDialog"
      :typeOfObjectId="typeOfObjectId"
      @addExtendedProperty="onAddExtendedProperty"
      :savedExProperties="list"
    />
  </div>
</template>

<script>
import constructionObjectExPropertyList from '../mixins/constructionObjectExProperyList'
import constructionObjectExProperyDialog from './constructionObjectExProperyDialog'
import ListMenu from '../../../components/table/ListMenu'

export default {
  components: { constructionObjectExProperyDialog, ListMenu },

  mixins: [constructionObjectExPropertyList],
}
</script>
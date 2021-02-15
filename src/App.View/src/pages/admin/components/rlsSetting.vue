<template>
  <div class="q-ma-md row">
    <div
      v-for="rlsItem in rlsList"
      :key="rlsItem.rlsType"
      class="col-md-4 col-sm-12 col-xs-12 q-pa-md"
    >
      <q-list class="rounded-borders" bordered>
        <q-item-label header>{{rlsItem.label}}</q-item-label>
        <div class="q-pb-md q-pl-md q-pr-md">
          <div class="row">
            <q-input rounded outlined class="col-11" bottom-slots dense label="Пошук">
              <template v-slot:append>
                <q-btn round dense flat icon="search" />
              </template>
            </q-input>
            <q-btn
              @click="onAddDialog(rlsItem.rlsType)"
              class="col-1"
              round
              dense
              flat
              color="positive"
              icon="add"
            />
          </div>
        </div>
        <q-scroll-area style="height: 400px">
          <q-item class="q-mr-md" clickable v-ripple v-for="item in rlsItem.list" :key="item.id">
            <q-item-section>
              <q-item-label>{{item.name}}</q-item-label>
            </q-item-section>
            <q-item-section v-if="rlsItem.rlsType === 'OrgUnit'" side>
              <q-item-label>
                <q-badge>Організація</q-badge>
              </q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-btn
                @click="onOpenEditCrudDialog(item, rlsItem.rlsType)"
                dense
                round
                flat
                icon="edit"
              >
                <q-badge color="blue" floating transparent>{{getShorNameRls(item.accessLevel)}}</q-badge>
              </q-btn>
              <q-item-label></q-item-label>
            </q-item-section>
            <q-item-section side>
              <q-btn
                @click="onDelete(rlsItem.rlsType ,item)"
                round
                dense
                flat
                color="negative"
                icon="delete"
              />
            </q-item-section>
          </q-item>
        </q-scroll-area>
      </q-list>
    </div>
    <search-org-unit
      :isVisibleDialog.sync="isVisibleSearchOrgUnit"
      @selectedItems="selectOrgUnits"
    />
    <search-user :isVisibleDialog.sync="isVisibleSearchUser" @selectedItems="selectUsers" />
    <search-region :isVisibleDialog.sync="isVisibleSearchRegion" @selectedItems="selectRegions" />
    <crud-dialog
      :isVisibleDialog.sync="isVisibleCrudDialog"
      :crudData="selectedItem"
      @selectedCrud="saveRls"
    />
  </div>
</template>

<script>
import { getUserRls, addRls, editRls, deleteRls } from '../../../services/adm-api/user-api'
import { getRoleRls, addRls as addRoleRls, editRls as editRoleRls, deleteRls as deleteRoleRls  } from '../../../services/adm-api/role-api'
import { CRUD_OPERATION_LIST } from '../../../constants/rigths/crudOperation'

import get from 'lodash.get'

import SearchOrgUnit from '../../../components/dialogs/searchOrgUnit'

import CrudDialog from '../components/crudDialog'
import SearchUser from '../../../components/dialogs/searchUser'
import SearchRegion from '../../../components/dialogs/searchRegion'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  components: {
    SearchOrgUnit,
    CrudDialog,
    SearchUser,
    SearchRegion,
  },

  props: {
    // The table to which RLS belongs (User or Role)
    rlsOwner: {
      type: String,
      required: true
    }
  },

  data() {
    return {
      isVisibleSearchOrgUnit: false,
      isVisibleSearchUser: false,
      isVisibleSearchRegion: false,

      isVisibleCrudDialog: false,

      rlsList: [],

      // From SearchOrgUnit Dialog
      selectedOrgUnitList: [],
      selectedUserList: [],
      selectedRegionList: [],

      // Edit OrgUnit, User, AtuObject (select item)
      selectedItem: null,
      selectedRlsType: '',
    }
  },

  methods: {
    initializeDefaultFields() {
      this.rlsOwnerId = get(this.$route, ['params', 'id'], stringEmpty())
    },

    fetchRls() {
      if(this.rlsOwner === 'User') {
        getUserRls(this.rlsOwnerId).then(this.setRlsData)
      } else if (this.rlsOwner === 'Role') {
        getRoleRls(this.rlsOwnerId).then(this.setRlsData)
      }
      
    },

    setRlsData(data) {
      this.rlsList = [
        {
          label: 'Доступ до організаційної структури',
          rlsType: 'OrgUnit',
          list: data.OrgUnit
        },
        {
          label: 'Доступ до даних користувача',
          rlsType: 'User',
          list: data.User
        },
        {
          label: 'Доступ до регіону',
          rlsType: 'AtuObject',
          list: data.AtuObject
        }
      ]
    },

    getShorNameRls(crudCode) {
      let crudName = ''
      for (var i = 0; i < CRUD_OPERATION_LIST.length; i++) {
        if (crudCode & CRUD_OPERATION_LIST[i].code) {
          crudName += CRUD_OPERATION_LIST[i].shortName
        }
      }
      return crudName
    },

    // #region Handler

    onOpenEditCrudDialog(rlsItem, rlsType) {
      this.selectedItem = rlsItem
      this.selectedRlsType = rlsType
      this.isVisibleCrudDialog = true
    },

    onDelete(rlsType ,rlsItem) {
      this.deleteRls( rlsType, rlsItem)
      .then(() => this.notifyDeleteRlsSuccess(rlsItem.name))
      .then(this.fetchRls)
    },

    deleteRls(rlsType ,rlsItem) {
      if(this.rlsOwner === 'User') {
        return deleteRls(this.rlsOwnerId , rlsType, rlsItem.id)
      } else if (this.rlsOwner === 'Role') {
        return deleteRoleRls(this.rlsOwnerId , rlsType, rlsItem.id)
      }
    },

    onAddDialog(rlsType) {
      this.selectedItem = null
      this.selectedRlsType = rlsType
      switch(rlsType){
        case 'OrgUnit':
          this.isVisibleSearchOrgUnit = true
          break
        case 'User':
          this.isVisibleSearchUser = true
          break
        case 'AtuObject':
          this.isVisibleSearchRegion = true
          break
      }
    },

    // #endregion

    // #region Action from dialog

    selectOrgUnits(orgUnits) {
      this.selectedOrgUnitList = orgUnits
      this.isVisibleCrudDialog = true
    },

    selectUsers(users) {
      this.selectedUserList = users
      this.isVisibleCrudDialog = true
    },

    selectRegions(regions) {
      this.selectedRegionList = regions
      this.isVisibleCrudDialog = true
    },

    saveRls(crud) {
      if (this.selectedItem) {
        this.editRls( crud).then(() => this.notifyEditRlsSuccess(this.selectedItem.name)).then(this.fetchRls)
      } else {
        let rlsAdd = []
        switch(this.selectedRlsType){
          case 'OrgUnit':
            rlsAdd = this.selectedOrgUnitList.filter(p => p.selected)
            break
          case 'User':
            rlsAdd = this.selectedUserList.filter(p => p.selected)
            break
          case 'AtuObject':
            rlsAdd = this.selectedRegionList.filter(p => p.selected)
        }

        const rlsJoinName = rlsAdd.map(p => p.name).join(', ')
        
        this.addRls(crud, rlsAdd)
        .then(() => this.notifyCreateRlsSuccess(rlsJoinName))
        .then(this.fetchRls)
      }
    },

    editRls(crud) {
      if(this.rlsOwner === 'User') {
        return editRls(this.rlsOwnerId, this.selectedRlsType, crud, this.selectedItem)
      } else if(this.rlsOwner === 'Role') {
        return editRoleRls(this.rlsOwnerId, this.selectedRlsType, crud, this.selectedItem)
      }
    },

    addRls(crud, rlsAdd) {
      if(this.rlsOwner === 'User') {
        return addRls(this.rlsOwnerId, this.selectedRlsType, crud, rlsAdd)
      } else if(this.rlsOwner === 'Role') {
        return addRoleRls(this.rlsOwnerId, this.selectedRlsType, crud, rlsAdd)
      }
    },

    // #endregion

    // #region Notify

    notifyEditRlsSuccess(name){
      this.$q.notify({ icon: 'check', color: 'positive', message: `Доступ до даних '${name}' успішно змінено`, progress: true })
    },

    notifyCreateRlsSuccess(name) {
      this.$q.notify({ icon: 'check', color: 'positive', message: `Доступ до даних '${name}' успішно додано`, progress: true })
    },

    notifyDeleteRlsSuccess(name) {
      this.$q.notify({ icon: 'check', color: 'positive', message: `Доступ до даних '${name}' успішно видалено`, progress: true })
    }

    // #endregion
  },

  created() {
    this.initializeDefaultFields()
    this.fetchRls()
  },
}
</script>

<style>
</style>
<template>
  <q-menu
    v-model="show"
    content-class="bg-grey-1 text-grey-9"
    auto-close
    anchor="center left"
    transition-show="jump-down"
    transition-hide="jump-up"
    self="top right"
    :touch-position="touch"
  >
    <q-list v-for="(item, index) in menuActions" :key="index" separator>
      <q-item
        v-if="rowActionButtonIsVisible(item)"
        v-entity-right="item.entityRight"
        v-operation-right="item.operationRight"
        @click.native="setActionListener(item.type, props, item.fn)"
        dense
        clickable
        v-close-popup
        class="q-py-xs q-px-md"
      >
        <q-item-section v-if="item.icon" class="q-px-xs" style="flex: none;">
          <q-icon :name="item.icon" class="q-mx-xs" />
        </q-item-section>

        <q-item-section>{{ item.label }}</q-item-section>
      </q-item>
    </q-list>
  </q-menu>
</template>

<script>
  import { mapState, mapGetters, mapActions } from 'vuex'
  import { deleteData } from '@/services/api'

  export default {
    data() {
      return {
        show: false
      }
    },
    props: {
      id: String,
      requestUrl: String,
      deleteUrl: String,
      touch: {
        type: Boolean,
        default: false
      },
      actions: [Array, Boolean],
      useActionIcons: {
        type: Boolean,
        default: false
      },
      props: Object,
      toggleShow: Boolean,
      selectedItem: Object
    },
    watch: {
      toggleShow: function() {

        if (this.selectedItem.id === this.props.row.id) {
          this.show = true
        } else {
          this.show = false
        }
      }
    },
    methods: {
      ...mapActions('tableList', ['init']),
      setActionListener(type, props, fn) {
        if (type === 'detail') {
          this.goToDetails(props.row)
        } else if (type === 'edit') {
          this.goToEdit(props.row)
        } else if (type === 'delete') {
          this.deleteDialog(props.row)
        } else {
          fn(props.row)
        }
      },

      rowActionButtonIsVisible(actionItem) {
        
        if (typeof actionItem.visible === 'function') {
          return actionItem.visible(this.selectedItem)
        } else if (actionItem.visible) {
          return true
        }
        return false
      },

      // go to details page
      goToDetails(row) {
        this.$router.push(this.$route.path + '/details/' + row.id)
      },
      // go to edit form
      goToEdit(row) {
        this.$router.push(this.$route.path + '/edit/' + row.id)
      },
      // delete row dialog
      deleteDialog(row) {
        this.$q
          .dialog({
            title: 'Підтвердіть видалення',
            persistent: true,
            ok: {
              label: 'Видалити',
              color: 'negative',
              flat: true
            },
            cancel: {
              flat: true,
              label: 'Відмінити',
              color: 'primary'
            }
          })
          .onOk(() => {
            this.removeItem(row)
            console.log('>>>> OK')
          })
          .onOk(() => {
            // console.log('>>>> second OK catcher')
          })
          .onCancel(() => {
            console.log('>>>> Cancel')
          })
          .onDismiss(() => {
            // console.log('I am triggered on both OK and Cancel')
          })
      },
      // remove item from data table
      removeItem(row) {
        deleteData(this.deleteUrl + '/' + row.id)
          .then(response => {
            console.log('removed')
            this.init({
              pagination: this.paginationTable,
              requestUrl: this.requestUrl
            }).then(() => {
              this.$q.notify({
                position: 'top',
                timeout: 2000,
                message: 'Успішно видалено',
                color: 'positive',
                icon: 'check'
              })
            })
          })
          .catch(error => console.log(error))
      }
    },
    computed: {
      ...mapState('tableMenu', ['itemId']),
      ...mapState('tableList', ['paginationTable']),
      ...mapGetters('baseElements', { rights: 'getUserRights' }),

      menuActions() {
        return this.actions
      }
    }
  }
</script>

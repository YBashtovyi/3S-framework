<template>
  <q-menu
    v-model="show"
    content-class="bg-grey-1 text-grey-9"
    auto-close
    anchor="center left"
    transition-show="jump-down"
    transition-hide="jump-up"
    self="top right"
  >
    <q-list v-for="(item, index) in actions" :key="index" separator>
      <q-item
        v-if="rowActionButtonIsVisible(item)"
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
  export default {
    data() {
      return {
        show: false
      }
    },

    props: {
      id: String,
      closeUrl: String,
      cancelUrl: String,
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
      setActionListener(type, props, fn) {
        fn(props.row)
      },

      rowActionButtonIsVisible(actionItem) {
        if (typeof actionItem.visible === 'function') {
          return actionItem.visible(this.selectedItem)
        } else if (actionItem.visible) {
          return true
        }

        return false
      }
    }
  }
</script>
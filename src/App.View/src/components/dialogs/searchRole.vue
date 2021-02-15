<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 500px; ">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Пошук ролей</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>

      <q-card-section>
        <q-input rounded outlined dense label="Пошук">
          <template v-slot:append>
            <q-icon name="search"></q-icon>
          </template>
        </q-input>
      </q-card-section>
      <q-card-section>
        <q-list>
          <q-scroll-area style="height: 300px;">
            <q-item
              class="q-mr-md"
              tag="label"
              v-for="(item, index) in items"
              :key="index"
              v-ripple
            >
              <q-item-section avatar>
                <q-checkbox v-model="items[index].selected" :val="items[index].name"></q-checkbox>
              </q-item-section>
              <q-item-section>
                <q-item-label>{{item.name}}</q-item-label>
              </q-item-section>
            </q-item>
          </q-scroll-area>
        </q-list>
      </q-card-section>
      <q-form @submit="onSubmit">
        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
          <q-item-label>{{selectedCount}}</q-item-label>
          <q-space></q-space>
          <q-btn
            label="Відмінити"
            color="negative"
            flat
            @click="$emit('update:isVisibleDialog', false)"
          />
          <q-btn
            :disable="!isSelected"
            type="submit"
            label="Обрати"
            color="primary"
            class="on-right"
          />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { fetchRoles } from '../../services/adm-api/role-api'
import { stringEmpty } from '../../utils/function-helpers'

export default {
  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false
    },
    isMultiSelect: {
      type: Boolean,
      default: false,
    },
  },

  data() {
    return {
      items: [],
    }
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.fetchRoles()
      }
    },

    items: {
      deep: true,

      handler(value) {
        // TODO: Дописать проверку на мультвыбор
      },
    },
  },

  methods: {
    fetchRoles() {
      fetchRoles().then(this.setRole)
    },

    setRole(data) {
      this.items = data.map((p) => ({
        id: p.id,
        name: p.name,
        selected: false,
      }))
    },

    onSubmit() {
      this.$emit('update:isVisibleDialog', false)
      this.$emit('selectedItems', this.items)
    },
  },

  computed: {
    selectedCount() {
      const sel = this.items.filter((p) => p.selected)
      return sel.length > 0 ? `Обрано ${sel.length}` : stringEmpty()
    },

    isSelected() {
      return this.items.filter((p) => p.selected).length > 0
    },
  },

}
</script>
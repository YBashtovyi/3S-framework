<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 500px">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Доступ до даних (CRUD)</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>
      <q-card-section>
        <q-option-group v-model="group" :options="options" type="toggle" color="blue" />
      </q-card-section>
      <q-form @submit="onSubmit">
        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
          <q-space></q-space>
          <q-btn
            label="Відмінити"
            color="negative"
            flat
            @click="$emit('update:isVisibleDialog', false)"
          />
          <q-btn type="submit" label="Зберегти" color="primary" class="on-right" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { CRUD_OPERATION_LIST } from '../../../constants/rigths/crudOperation'
import { stringEmpty } from '../../../utils/function-helpers'

export default {
  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },
    crudData: {
      type: Object,
      default: null,
    },
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.group = []
        if (this.crudData) {
          const filterCrud = (p) => this.crudData.accessLevel & p.code
          this.group = CRUD_OPERATION_LIST.filter(filterCrud).map((p) => p.code)
        }
      }
    },
  },

  data() {
    return {
      group: [],
      options: [
        {
          label: 'C - Створення',
          value: 1,
        },
        {
          label: 'R - Читання',
          value: 2,
        },
        {
          label: 'U - Оновлення',
          value: 4,
        },
        {
          label: 'D - Видалення',
          value: 8,
        },
        {
          label: 'A - Повний доступ',
          value: 16,
        },
        {
          label: 'B - Заборона на будь-яку дію',
          value: 32,
        },
      ],
    }
  },

  methods: {
    onSubmit() {
      this.$emit('update:isVisibleDialog', false)
      this.$emit('selectedCrud', this.parseCodeToShortName(this.group))
    },

    parseCodeToShortName(crudCodeList) {
      return crudCodeList
        .sort()
        .map((p) => CRUD_OPERATION_LIST.find((x) => x.code & p)?.shortName ?? stringEmpty())
        .join('')
    },
  },
}
</script>
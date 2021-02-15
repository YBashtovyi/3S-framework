<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 700px; max-width: 80vw;">
      <q-form @submit="onSubmit">
        <create-person-form
          :header="false"
          :footer="false"
          @createPerson="person = $event"
          @addPerson="newPerson = $event"
        />
        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
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
import createPersonForm from '../../pages/person/subpages/Edit'
import createPersonMixin from '../../pages/person/mixins/personEdit'

export default {
  mixins: [createPersonMixin],

  components: {
    createPersonForm,
  },

  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },
  },

  watch: {
    newPerson: {
      deep: true,
      handler() {
        this.$emit('update:isVisibleDialog', false)
        this.$emit('update:addNewPerson', this.newPerson)
      },
    },
  },
}
</script>

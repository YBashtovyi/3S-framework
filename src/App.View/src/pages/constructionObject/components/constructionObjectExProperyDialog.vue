<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card style="width: 500px">
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Додати характеристику</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>
      <q-form @submit="onSubmit">
        <q-card-section>
          <q-select
            v-model="selectedTypeOfObjectProperty"
            :options="typeOfObjectProperties"
            @input="onPropertyChanged"
            option-value="id"
            option-label="name"
            label="Назва *"
          />
          <q-select
            v-model="selectedTypeOfObjectSubProperty"
            :options="typeOfObjectSubProperties"
            :disable="typeOfObjectSubProperties.length === 0"
            option-value="id"
            option-label="name"
            label="Значення *"
          />
          <q-input v-model="description" type="textarea" label="Коментар" counter maxlength="100" />
        </q-card-section>

        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md q-mt-md">
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
import constructionObjectExPropertyDialog from '../mixins/constructionObjectExProperyDialog'

export default {
  mixins: [constructionObjectExPropertyDialog],
}
</script>
<template>
  <div class="q-pa-md">
    <q-card>
      <q-form @submit="onSubmit">
        <div class="q-pa-md">
          <q-input
            :rules="[val => !!val || 'Обов\'язкове поле']"
            v-model="editData.group"
            dense
            label="Група (англ.)"
            class="q-mb-md"
          ></q-input>
          <q-input
            :rules="[val => !!val || 'Обов\'язкове поле']"
            v-model="editData.groupName"
            dense
            label="Група (укр.)"
            class="q-mb-md"
          ></q-input>
          <q-input
            :rules="[val => !!val || 'Обов\'язкове поле']"
            v-model="editData.name"
            dense
            label="Назва"
            class="q-mb-md"
          ></q-input>
          <q-input
            :rules="[val => !!val || 'Обов\'язкове поле']"
            v-model="editData.code"
            dense
            label="Код"
            class="q-mb-md"
          ></q-input>
          <q-input v-model="editData.value" dense label="Значення" class="q-mb-md"></q-input>
          <q-input v-model="editData.itemNumber" dense label="Порядок" class="q-mb-md"></q-input>

          <div class="row justify-end">
            <q-btn type="reset" color="negative" flat label="Відмінити" :to="{ path:'/enum' } "></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
  </div>
</template>

<script>
import enumEdit from '../mixins/enumEdit'

export default {
  mixins: [enumEdit],
}
</script>
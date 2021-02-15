<template>
  <q-dialog persistent v-model="isVisibleDialog">
    <q-card>
      <q-card-section class="row items-center q-pb-none">
        <div class="text-h6">Додати файл</div>
        <q-space />
        <q-btn icon="close" flat round dense @click="$emit('update:isVisibleDialog', false)" />
      </q-card-section>
      <q-form @submit="onSubmit">
        <q-card-section>
          <div>
             <q-select
              :rules="[value => !!value || 'Обов\'язкове поле']"
              label="Тип документа *"
              :value="selectedTypeOdAttachedFile"
              @input="onTypeOfAttachedFileChanged"
              :options="typeOfAttachedFile"
              optionLabel="name"
              optionValue="code"
              class="q-mb-md q-mr-md"
            />
           <upload-file ref="uploadFile" :entityId="entityId" :entityName="entityName" :typeOfAttachedFile="selectedTypeOdAttachedFile"></upload-file>
          </div>
        </q-card-section>

        <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
          <q-btn
            label="Закрити"
            color="negative"
            flat
            @click="onClose"
          />
          <q-btn type="submit" icon="fas fa-cloud-upload-alt" label="Завантажити" color="primary" class="on-right" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import UploadFile from './UploadFile.vue'
import set from 'lodash.set'
import { fetchListEnumByGroup } from '../../services/enum-api'
export default {
  components: { UploadFile },
  data() {
    return {
      selectedTypeOdAttachedFile: null,
      typeOfAttachedFile: []
    }
  },

  watch: {
    isVisibleDialog(value) {
      if (value) {
        this.selectedTypeOdAttachedFile = null
        this.getEnums()
      }
    },
  },

  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },

    entityId: {
      type: String,
      required: true
    },

    entityName: {
        type: String,
        required: true
      },
  },

  methods: {
    onSubmit() {
      this.$refs.uploadFile.$refs.upload.upload()
      // this.$emit('update:isVisibleDialog', false)
    },

    onClose() {
      this.$emit('onCloseDialog')
      this.$emit('update:isVisibleDialog', false)
    },

    getEnums() {
      const enumGroups = ['AttachedFile']

      return fetchListEnumByGroup(enumGroups).then(p => {
        this.typeOfAttachedFile = p.AttachedFile
      })
    },

    onTypeOfAttachedFileChanged(typeOfAttachedFile) {
      set(this, 'selectedTypeOdAttachedFile', typeOfAttachedFile)
    },
  },
}
</script>
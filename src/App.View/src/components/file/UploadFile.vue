<template>
  <div class="row q-px-md q-mb-md">
    <QUploader
      ref="upload"
      flat
      bordered
      :headers="getHeaders"
      :hide-upload-btn="true"
      :form-fields="formFields"
      label="Завантаження файлу"
      :url="postUrl"
      class="full-width"
      @uploaded="uploadedFile"
    ></QUploader>
   
    <q-input v-model="description" autogrow label="Коментар" class="full-width">
      <template v-slot:before>
        <q-icon name="comment" />
      </template>
    </q-input>

  </div>
</template>

<script>
import { env } from "../../services/api"
import { LocalStorage } from "quasar"

export default {
  props: {
    entityId: {
      type: String,
      required: true
    },
    entityName: {
      required: true
    },
    typeOfAttachedFile: {
      required: true,
    }
  },

  data() {
    return {
      description: "",
    };
  },

  computed: {
    getHeaders() {
      let key = "oidc.user:" + process.env.OIDC_ISSUER + ":MisFront",
        storageToken = LocalStorage.getItem(key),
        token = JSON.parse(storageToken).access_token,
        headers = [{ name: "Authorization", value: "Bearer " + token }];
      return headers;
    },
    postUrl() {
      return env.FILESTORE.FORM_UPLOAD;
    },
    formFields() {
      return [
        { name: "entityId", value: this.entityId },
        { name: "entityName", value: this.entityName },
        { name: "documentType", value: this.documentType },
        { name: "description", value: this.description },
        { name: "typeOfAttachedFile", value: this.typeOfAttachedFile?.code }
      ];
    }
  },
  methods: {
    uploadedFile() {
      this.$emit("uploaded", true);
    },
  },
};
</script>

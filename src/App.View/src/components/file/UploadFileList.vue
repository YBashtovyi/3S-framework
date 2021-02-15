<template>
  <div>
    <table-list
      :columns="columns"
      :pagination="pagination"
      :requestUrl="requestUrl"
      :headerButtons="tableHeaderButtons"
      :actions="actions"
      :withSave="false"
    />

    <upload-file-dialog
      :isVisibleDialog.sync="isVisibleFileUploadDialog"
      :entityName="entityName"
      :entityId="entityId"
      @onCloseDialog="onCloseDialog"
    />
    <signing :isVisibleDialog="isVisibleSignFileDialog" />
  </div>
</template>

<script>
import uploadFileList from './mixins/uploadFileList'
import TableList from '../table/tableList'
import UploadFileDialog from './UploadFileDialog'
import Signing from '../signing/idgovua/signing'

export default {
  components: { TableList, UploadFileDialog, Signing },
  mixins: [uploadFileList],
}
</script>
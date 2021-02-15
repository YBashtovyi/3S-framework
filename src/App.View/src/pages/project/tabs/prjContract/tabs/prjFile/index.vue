<template>
  <upload-file-list
    :entityId="prjContractId"
    :entityName="`ProjectContract`"
    :isVisibleBtnAdd="isVisibleBtnAdd"
  />
</template>

<script>
import UploadFileList from '../../../../../../components/file/UploadFileList'
import { isEmpty } from '../../../../../../utils/function-helpers'

export default {
  components: { UploadFileList },

  computed: {
    prjContractId() {
      return this.$route.params.prjContractId
    },

    projectId() {
      return this.$route.params.id
    },

    isVisibleBtnAdd() {
      return !isEmpty(this.projectId)
    },
  },
}
</script>
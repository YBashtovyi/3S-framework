<template>
  <upload-file-list
    :entityId="prjWorkScheduleId"
    :entityName="`ProjectWorkSchedule`"
    :isVisibleBtnAdd="isVisibleBtnAdd"
  />
</template>

<script>
import UploadFileList from '../../../../../../components/file/UploadFileList'
import { isEmpty } from '../../../../../../utils/function-helpers'

export default {
  components: { UploadFileList },

  computed: {
    prjWorkScheduleId() {
      return this.$route.params.prjDocId
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
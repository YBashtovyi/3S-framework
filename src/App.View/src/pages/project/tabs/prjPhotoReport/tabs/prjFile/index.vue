<template>

        <upload-file-list :entityId="prjPhotoReportId" :entityName="`ProjectPhotoReport`"/>

</template>

<script>
import UploadFileList from '../../../../../../components/file/UploadFileList'

export default {
    components: {UploadFileList},

    computed: {
        prjPhotoReportId() {
            return this.$route.params.prjPhotoReportId
        }
    }
}
</script>
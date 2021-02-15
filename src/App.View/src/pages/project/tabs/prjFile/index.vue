<template>

        <upload-file-list :entityId="projectId" :entityName="`Project`"/>

</template>

<script>
import UploadFileList from '../../../../components/file/UploadFileList'

export default {
    components: {UploadFileList},

    computed: {
        projectId() {
            return this.$route.params.id
        }
    }
}
</script>
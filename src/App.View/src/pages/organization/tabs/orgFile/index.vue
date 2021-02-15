<template>

        <upload-file-list :entityId="orgId" :entityName="`Organization`"/>

</template>

<script>
import UploadFileList from '../../../../components/file/UploadFileList'

export default {
    components: {UploadFileList},

    computed: {
        orgId() {
            return this.$route.params.id
        }
    }
}
</script>
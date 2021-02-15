<template>

    <div class="q-pa-md relative-position" style="min-height: 80vh">

        <!-- search component -->
        <search 
            :config="searchConfig" 
            :createBtn="false" />

        <!-- Table component -->
        <table-list
            :columns="columns"
            :pagination="pagination"
            :requestUrl="requestUrl"
            :actions="actions"
            @selectedRow="row = $event"
        />
    </div>
</template>

<script>

import TableList from '../../../../components/table/tableList'
import Search from '../../../../components/search/search'
import notificationByReceiverList from './../mixins/notificationByReceiverList'

export default {
    
    mixins: [notificationByReceiverList],

    components: {

        TableList,
        Search
    }
}
</script>
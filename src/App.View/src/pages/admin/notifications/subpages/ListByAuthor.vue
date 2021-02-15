<template>

    <div class="q-pa-md relative-position" style="min-height: 80vh">

        <!-- search component -->
        <search 
            :config="searchConfig" 
            :createBtn="false" />

        <div class="row justify-end q-my-md" >
            <q-btn
                v-if="createNotificationVisible"
                no-caps
                color="positive"
                class="on-left q-mr-none"
                @click="onNewNotification"
            >
            <q-icon left size="18px" name="add" />
                <div>Створити сповіщення</div>
            </q-btn>
        </div>

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
import notificationByAuthorList from './../mixins/notificationByAuthorList'

export default {
    
    mixins: [notificationByAuthorList],

    components: {

        TableList,
        Search
    }
}
</script>
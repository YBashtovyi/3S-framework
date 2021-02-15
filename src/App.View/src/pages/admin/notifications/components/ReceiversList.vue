<template>
    <div>
        <div class="row items-center q-px-md q-py-sm q-mb-xs bg-grey-2">
            <div class="text-subtitle2 text-uppercase q-py-sm">Отримувачі сповіщення</div>
            <q-btn @click="onAddReceiver"
                color="positive"
                class="q-ml-auto"
                flat
                rounded
            >            
                <q-icon left size="18px" name="add" />
                <div>Додати</div>
            </q-btn>
        </div>

        <add-receiver-dialog
            v-if="addReceiverVisible"            
            @close="onCloseReceiverDialog"
            @success="$event => onSuccessReceiverDialog($event)"
            :selectedEmployeeIds="employeesIds"
        />
        
        <q-scroll-area style="height: 10vw;">
            <q-list v-for="item in receivers" v-bind:key="item.receiverId">
                <q-item class="q-py-sm cursor-inherit" clickable>
                    <q-item-section side>
                        <q-avatar icon="person" color="grey" text-color="white" font-size="26px" />
                    </q-item-section>
                    <q-item-section>
                        <q-item-label>{{ item.receiverCaption }}</q-item-label>
                    </q-item-section>
                    <q-item-section top side>
                        <div class="text-grey-8 q-gutter-xs">
                            <q-btn
                                size="12px"
                                flat
                                round
                                icon="delete"
                                @click="onDeleteReceiver(item)"
                            />
                        </div>
                    </q-item-section>
                </q-item>
            </q-list>
        </q-scroll-area>
    </div>
</template>

<script>

import receiversList from './mixins/receiversList'
import AddReceiverDialog from './AddReceiverDialog'

export default {
    
    mixins: [receiversList],

    components: {

        AddReceiverDialog
    }
}
</script>
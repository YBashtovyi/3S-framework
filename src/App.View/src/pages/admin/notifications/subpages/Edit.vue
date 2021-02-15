<template>
    <div v-if="!loaded" class="text-center">
        <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>

        <div v-if="hasErrors" class="text-center text-bold q-px-md q-py-md bg-grey-3"> 
            Помилка при завантаженні данних. Спробуйте пізніше
        </div>
        <q-card v-else>
            <div class="text-subtitle2 text-uppercase q-px-md q-py-md bg-grey-3">
                {{ headerTitle }}
            </div>
            <q-form>
                <div class="q-px-md q-pt-md">
                    <q-input
                        label="Заголовок *"
                        options-dense
                        maxlength="60"
                        dense
                        counter
                        v-model="notification.title"
                        type="text"
                        :rules="requiredTitle"
                        class="q-mb-md"
                    />
                    <q-input
                        label="Текст сповіщення *"
                        options-dense
                        maxlength="1000"
                        dense
                        autogrow
                        counter
                        v-model="notification.message"
                        type="textarea"
                        :rules="requiredMessage"
                        input-style="min-height: 50px;"
                        class="q-mb-md"
                    />
                    <f-select
                        label="Тип сповіщення"
                        required
                        clearable
                        :value="notificationType"
                        @input="onNotificationTypeChanged"
                        :options="notificationTypes"
                        class="q-mb-md"
                    />
                    <q-input
                        label="Посилання"
                        options-dense
                        maxlength="60"
                        dense
                        v-model="notification.url"
                        type="url"
                        class="q-mb-md"
                    />
                    <div style="max-width: 250px">
                        <q-input 
                            label="Запланована дата та час відправлення"
                            options-dense
                            dense
                            labelColor="primary"
                            bg-color="transparent"
                            :outlined="false"
                            :filled="false"
                            v-model="particularTime"
                            :mask="dateTimeFormatMask"
                            class="q-mb-md"
                        >
                            <template v-slot:prepend>
                                <q-icon name="event" class="cursor-pointer">
                                <q-popup-proxy transition-show="scale" transition-hide="scale">
                                    <q-date v-model="particularTime" :mask="dateTimeFormat" />
                                </q-popup-proxy>
                                </q-icon>
                            </template>

                            <template v-slot:append>
                                <q-icon name="access_time" class="cursor-pointer">
                                <q-popup-proxy transition-show="scale" transition-hide="scale">
                                    <q-time v-model="particularTime" :mask="dateTimeFormat" format24h />
                                </q-popup-proxy>
                                </q-icon>
                            </template>
                        </q-input>
                    </div>

                    <receivers-list 
                        v-if="receiversVisible"
                        :receivers="receivers"
                        @remove="$event => onRemoveReceiver($event)"
                        @success="$event => onSuccessAddReceivers($event)"
                    />
                </div>
                <div class="row justify-end q-mt-md q-pt-md q-pb-md q-px-md">
                    <q-btn 
                        label="Відмінити" 
                        type="reset" 
                        olor="negative" 
                        flat 
                        @click="onCancel" 
                    />
                    <q-btn
                        :loading="savingNotification"
                        :disable="saveNotificationDisable"
                        label="Зберегти"
                        color="primary"
                        class="on-right"
                        @click="onSaveCard"
                    />
                </div>    
            </q-form>
        </q-card>
    </div>
</template>

<script>

import notificationEdit from './../mixins/notificationEdit'
import ReceiversList from '../components/ReceiversList'

export default {
    
    mixins: [notificationEdit],

    components: {

        ReceiversList
    }

}
</script>
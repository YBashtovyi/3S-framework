<template>
    <div v-if="!loaded" class="text-center">
        <q-spinner-dots color="primary" size="3em"/>
    </div>
    <div v-else class="q-px-md">
        <div v-if="hasErrors" class="text-center text-bold q-px-md q-py-md bg-grey-3"> 
            Помилка при завантаженні данних. Спробуйте пізніше
        </div>
        <q-card v-else>
            <detail-title
                :label="title.label"
                :icon="title.icon" 
                :content="title.content"
                :contentExt="title.contentExt"
                :actions="title.actions"
                :headerButtons="title.buttons"
            />
            <div class="q-mb-lg q-px-md q-mt-sm">
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Текст сповіщення"
                            v-model="notification.message"
                            autogrow
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>  
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Тип сповіщення"
                            v-model="notification.typeCaption"
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Автор"
                            v-model="notification.authorCaption"
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Посилання"
                            v-model="notification.url"
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Дата та час відправлення"
                            :value="notification.createInOneSignalDate | asDate(dateTimeFormat)"
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <q-input
                            label="Запланована дата та час відправлення"
                            :value="notification.particularTime | asDate(dateTimeFormat)"
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div>
                    <div class="col-xs-12 col-md-12">
                        <q-input v-if="errorVisible"
                            label="Текст помилки"
                            v-model="notification.error"
                            autogrow
                            stack-label
                            readonly
                            class="q-mr-xs-none q-mb-xs"
                        />
                    </div> 
                </div>
                <!-- Receivers -->
                <q-expansion-item default-opened
                    v-if="receiversVisible"
                    class="q-pb-md"
                    header-class="text-subtitle2 text-uppercase bg-grey-3"
                    label="Отримувачі сповіщення">

                    <q-scroll-area style="height: 10vw;">
                        <q-list v-for="item in receivers" v-bind:key="item.id">
                            <q-item class="q-py-sm cursor-inherit" clickable>
                                <q-item-section side>
                                    <q-avatar icon="person" color="grey" text-color="white" font-size="26px" />
                                </q-item-section>
                                <q-item-section>
                                    <q-item-label>{{ item.receiverCaption }}</q-item-label>
                                </q-item-section>
                            </q-item>
                        </q-list>
                    </q-scroll-area>
                </q-expansion-item>
            </div>
        </q-card>
    </div>
</template>

<script>

import notificationDetails from './../mixins/notificationDetails'
import DetailTitle from '../../../../components/baseElements/detailTitle'

export default {
    
    mixins: [notificationDetails],

    components: {
        DetailTitle
    }
}
</script>
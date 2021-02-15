<template>
    <q-dialog persistent v-model="dialogVisible">
        <q-card style="width: 55vw; max-width: 80vw; height: 33vw;">
            <q-card-section class="row items-center q-pb-none">
                <div class="text-h6">Додавання отримувачів сповіщення</div>
                <q-space />
                <q-btn icon="close"
                    flat 
                    round 
                    dense 
                    @click="onCancel" />
            </q-card-section>
            <q-scroll-area style="height: 26vw;">
                <q-card-section v-if="!dataLoaded && hasErrors" class="row items-center q-pb-none">
                    <div>
                        Помилка при завантаженні даних. Спробуйте пізніше або зверніться до адміністратора
                    </div>
                </q-card-section>

                <div v-if="!dataLoaded && !hasErrors" class="text-center">
                    <q-spinner-dots color="primary" size="3em" />
                </div>

                <q-card-section v-if="dataLoaded">
                    <q-card-section>
                        <q-input
                            ref="filterValue"
                            filled
                            clearable
                            clear-icon="clear"
                            debounce="700"
                            outlined
                            dense
                            v-model="mainFilter"
                            label="ПІБ співробітника"
                            @input="onFIOChanged"
                        />
                    </q-card-section>
                    <q-card-section>
                        <q-form >
                            <q-table 
                                :data="filteredEmployees"
                                :columns="columns"
                                row-key="id"
                                selection="multiple"
                                :selected.sync="selectedEmployees"
                            >

                                <template v-slot:header="props">
                                    <q-tr :props="props">
                                        <q-th>
                                            <q-checkbox v-model="checkBoxAll" />
                                        </q-th>
                                        <q-th
                                            v-for="col in props.cols"
                                            :key="col.name"
                                            :props="props"
                                        >{{ col.label }}</q-th>
                                    </q-tr>
                                </template>

                                <template v-slot:body="props">
                                    <q-tr :props="props">
                                        <!-- selected row column -->
                                        <q-td auto-width>
                                            <q-checkbox class="q-ml-sm"
                                                v-model="props.selected"
                                                dense
                                            />
                                        </q-td>

                                        <!-- table columns -->
                                        <q-td v-for="col in props.cols" :key="col.name" :props="props">
                                        <q-checkbox
                                                size="xs"
                                                disable
                                                v-if="col.isCheckBox"
                                                v-model="col.value"
                                            />
                                            <span v-else>{{ col.value }}</span>
                                        </q-td>
                                    </q-tr>
                                </template>
                            </q-table>
                        </q-form>
                    </q-card-section>
                </q-card-section>
            </q-scroll-area >

            <q-card-actions align="right" class="q-mr-md">
                <q-btn
                    label="Відмінити"
                    color="negative"
                    flat
                    @click="onCancel"
                />
                <q-btn
                    @click="onSubmit"
                    label="Зберегти"
                    color="primary"
                    class="on-right"
                />
            </q-card-actions>
        </q-card>
    </q-dialog>
</template>

<script>

import addReceiverDialog from './mixins/addReceiverDialog'

export default {
    
    mixins: [addReceiverDialog]

}
</script>
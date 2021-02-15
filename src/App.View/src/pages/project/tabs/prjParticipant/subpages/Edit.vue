<template>
  <div>
    <q-card>
      <div v-if="!pageReady" class="text-center">
        <q-spinner-dots color="primary" size="3em" />
      </div>
      <q-form v-else @submit="onSubmit">
        <div class="q-pa-md">
          <div class="row">
            <div class="col-md-6 col-xs-12">
              <q-input dense v-model="projectData.name" readonly label="Проект" />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Роль"
                required
                :value="selectedProjectRole"
                @input="onProjectRoleChanged"
                :options="projectRoles"
                optionLabel="name"
                optionValue="id"
                class="q-mb-md q-ml-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <f-select
                dense
                label="Організація"
                required
                :value="selectedOrganization"
                @input="onOrganizationChanged"
                :options="organizations"
                optionLabel="name"
                optionValue="id"
                class="q-mb-md"
              />
            </div>
            <div class="col-md-6 col-xs-12">
              <div class="row">
                <div class="col-md-10">
                  <lazy-autocomplete
                    class="q-mb-md q-ml-md"
                    :label="'Співробітник'"
                    :icon="'person'"
                    :minFilterChars="3"
                    :value="selectedResponsiblePerson"
                    :url="orgEmployeeRequest"
                    :optionLabel="`personFullName`"
                    hideDropdownIcon
                    @input="onResponsiblePersonChanged"
                    clearable
                  />
                </div>
                <div class="col-md-2">
                  <q-btn
                    class="q-mb-md"
                    @click="onCreatePerson"
                    color="positive"
                    icon="add"
                    flat
                    label="Створити"
                  />
                </div>
              </div>
            </div>
            <div class="col-md-6 col-xs-12">
              <q-input dense v-model="editData.description" label="Коментар" />
            </div>
          </div>
          <div class="row justify-end">
            <q-btn
              type="reset"
              color="negative"
              flat
              label="Відмінити"
              :to="{ path: '/projects' }"
            ></q-btn>
            <q-btn type="submit" color="primary" class="on-right" label="Зберегти"></q-btn>
          </div>
        </div>
      </q-form>
    </q-card>
    <create-person-dialog
      :isVisibleDialog.sync="isVisibleCreatePersonDialog"
      :addNewPerson.sync="person"
    />
  </div>
</template>

<script>
import prjParticipantEdit from '../mixins/prjParticipantEdit'
import LazyAutocomplete from '../../../../../components/forms/LazyAutocomplete'
import CreatePersonDialog from '../../../../../components/dialogs/createPerson'

export default {
  mixins: [prjParticipantEdit],

  components: {
    LazyAutocomplete,
    CreatePersonDialog,
  },
}
</script>

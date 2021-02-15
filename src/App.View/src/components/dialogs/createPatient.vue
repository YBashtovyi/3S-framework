<template>
  <div>
    <q-dialog persistent v-model="isVisibleDialog">
      <q-card class="fs-card">
        <q-form @submit="onSubmit" class="no-shadow">
          <create-patient-form
            :header="false"
            :footer="false"
            @createPatient="patient = $event"
            @addPatient="newPatient = $event"
          />
          <q-card-actions align="right" class="q-px-md q-pt-none q-pb-md">
            <q-btn
              label="Відмінити"
              color="negative"
              flat
              @click="$emit('update:isVisibleDialog', false)"
            />
            <q-btn type="submit" label="Зберегти" color="primary" class="on-right" />
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import createPatientForm from "@/pages/patient/subpages/Edit.vue";
import createPatientMixin from "@/pages/patient/mixins/editPatient.js";
export default {
  components: { createPatientForm },
  mixins: [createPatientMixin],
  props: {
    isVisibleDialog: Boolean
  },
  watch: {
    newPatient: {
      deep: true,
      handler() {
        this.$emit("update:isVisibleDialog", false);
        this.$emit("update:addNewPatient", this.newPatient);
        this.newPatient.caption =
          this.newPatient.personLastName +
          " " +
          this.newPatient.personName +
          " " +
          this.newPatient.personMiddleName;
      }
    }
  }
};
</script>

<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card>
        <detail-title
          :label="prjParticipantTitle.label"
          :icon="prjParticipantTitle.icon"
          :content="prjParticipantTitle.content"
        />
        <div class="q-pa-md">
          <q-field dense label="Проєкт" stack-label readonly class="q-mb-md">
            <router-link
              :to="`/projects/details/${details.projectId}`"
              class="text-primary"
              title="Перейти на картку проєкту"
            >{{ details.projectName }}</router-link>
            <template v-slot:before>
              <q-icon name="architecture" size="20px" />
            </template>
          </q-field>

          <q-input
            dense
            v-model="details.projectRoleName"
            label="Роль на проєкті"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-field dense label="Організація" stack-label readonly class="q-mb-md">
            <router-link
              :to="`/organization/details/${details.participantId}`"
              class="text-primary"
              title="Перейти на картку організації"
            >{{ details.participantName }}</router-link>
            <template v-slot:before>
              <q-icon name="fa fa-building" size="20px" />
            </template>
          </q-field>
          <q-field dense label="Відповідальна особа" stack-label readonly class="q-mb-md">
            <router-link
              :to="`/employeer/details/${details.responsiblePersonId}`"
              class="text-primary"
              title="Перейти на картку відповідальної особи"
            >{{ details.responsiblePersonFullName }}</router-link>
            <template v-slot:before>
              <q-icon name="fas fa-user-tie" size="20px" />
            </template>
          </q-field>
          <q-input dense v-model="details.description" label="Коментар" readonly class="q-mb-md"></q-input>
        </div>
      </q-card>
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../../components/baseElements/detailTitle'
import prjParticipantDetails from '../mixins/prjParticipantDetails'

export default {
  mixins: [prjParticipantDetails],

  components: {
    detailTitle,
  },
}
</script>
<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title :label="roleTitle.label" :icon="roleTitle.icon" :content="roleTitle.content" />
        <div class="q-pa-md">
          <q-input dense :value="details.name" label="Назва" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.code" label="Код" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.description" label="Опис" readonly class="q-mb-md"></q-input>
        </div>
      </q-card>
      <role-right-list :roleId="roleId" />
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../components/baseElements/detailTitle'
import RoleRightList from '../components/RoleRightList.vue'
import roleDetails from '../mixins/roleDetails'

export default {
  mixins: [roleDetails],

  components: {
    detailTitle,
    RoleRightList,
  },
}
</script>
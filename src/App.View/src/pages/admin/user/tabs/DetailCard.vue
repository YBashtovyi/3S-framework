<template>
  <div>
    <div v-if="!pageReady" class="text-center">
      <q-spinner-dots color="primary" size="3em" />
    </div>
    <div v-else>
      <q-card flat>
        <detail-title :label="userTitle.label" :icon="userTitle.icon" :content="userTitle.content" />
        <div class="q-pa-md">
          <q-input dense :value="userFullName" label="Співробітник" readonly class="q-mb-md"></q-input>
          <q-input dense :value="details.orgUnitName" label="Місце роботи" readonly class="q-mb-md"></q-input>
          <q-input
            dense
            :value="details.orgUnitPositionName"
            label="Посада"
            readonly
            class="q-mb-md"
          ></q-input>
          <q-input dense :value="details.accountId" label="Аккаунт" readonly class="q-mb-md"></q-input>
        </div>
      </q-card>
      <user-role-list :userId="userId" />
    </div>
  </div>
</template>

<script>
import detailTitle from '../../../../components/baseElements/detailTitle'
import userDetails from '../mixins/userDetails'

import UserRoleList from '../components/UserRoleList'

export default {
  mixins: [userDetails],

  components: {
    detailTitle,
    UserRoleList
  },
}
</script>
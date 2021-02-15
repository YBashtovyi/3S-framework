<template>
    <div
      class="text-center q-mb-xl"
      appear
      enter-active-class="animated fadeIn"
      leave-active-class="animated fadeOut"
    >
      <img src="~assets/choose-user.svg" style="max-width: 80vw; width: 450px" />
      <div class="q-mb-sm">
        <!-- <img src="~assets/evo-ehealth.svg" style="width: 140px" /> -->
      </div>
      <q-select
        transition-show="jump-up"
        transition-hide="jump-up"
        v-model="selectedUser"
        :label="`Оберіть профіль для користувача ${profileUserCaption}`"
        dense
        :options="profiles"
        option-value="userId"
        option-label="profileCaption"
        :rules="[val => !!val || 'Обов\'язкове поле']"
        class="q-mb-md"
        style="width: 450px; max-width: 80vw"
      >
        <template v-slot:option="scope">
          <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
            <q-item-section avatar>
              <q-icon name="person" class="text-grey-8" />
            </q-item-section>
            <q-item-section>
              <q-item-label>{{ scope.opt.profileCaption }} </q-item-label>
              <q-item-label caption v-if="scope.opt.ehealthPositionCaption">
                {{ scope.opt.ehealthPositionCaption }} {{ scope.opt.divisionCaption ? `(${scope.opt.divisionCaption})` : '' }}

              </q-item-label>
              <q-item-label caption v-if="scope.opt.organizationCaption">
                {{ scope.opt.organizationCaption }}
              </q-item-label>
            </q-item-section>
          </q-item>
        </template>

        <template v-slot:no-option>
          <q-item>
            <q-item-section class="text-grey">Відсутні значення</q-item-section>
          </q-item>
        </template>
      </q-select>

        <!-- :disable="userIsSelected" -->
      <q-btn
        unelevated
        color="primary"
        label="увійти в систему"
        @click="setUser"
      />
    </div>
</template>
<script>
    import authProfilesMixin from './mixins/authProfiles'
    export default {
        mixins: [authProfilesMixin]
    }
</script>

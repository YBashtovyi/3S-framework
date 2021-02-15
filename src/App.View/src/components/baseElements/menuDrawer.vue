<template>
  <q-drawer
    :value="drawer"
    :width="240"
    show-if-above
    :mini="miniState"
    bordered
    content-class="bg-grey-2"
  >
    <q-scroll-area class="fit">
      <q-list>
        <q-item clickable v-ripple :to="{ path: '/dashboard' }">
          <q-item-section avatar :style="!miniState ? 'min-width: 36px' : 'min-width: initial'">
            <q-icon class="icon ico-home" color="grey" size="22px" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Кабінет</q-item-label>
          </q-item-section>
          <q-tooltip
            v-if="!$q.screen.lt.md && miniState"
            anchor="center right"
            self="center left"
            :offset="[5, 10]"
            content-style="font-size: 13px"
          >Кабінет</q-tooltip>
        </q-item>
      </q-list>
      <q-separator v-if="!miniState" />

      <div>
        <template v-if="!miniState">
          <q-list v-for="(navItem, index) in navMenu" :key="index + 10">
            <q-item-label
              v-if="navMenu && navMenu.length > 0 && navItem.header"
              header
              class="text-weight-bold text-uppercase"
            >{{ navItem.header}}</q-item-label>
            <div v-for="(item, index) in navItem.children" :key="index + 10">
              <q-item
                v-if="!item.children"
                v-entity-right.enableIfRead="item.entity"
                v-operation-right="item.operationRight"
                v-interface-right="item.interfaceRight"
                clickable
                v-ripple
                :to="item.link"
                active-class="bg-grey-1 text-primary"
                @click="toggleNavMenu"
              >
                <q-item-section
                  avatar
                  :style="!miniState ? 'min-width: 36px' : 'min-width: initial'"
                >
                  <q-icon
                    color="grey"
                    :name="item.icon"
                    :size="item.iconSize ? item.iconSize : '22px'"
                    :class="item.iconClass"
                  />
                </q-item-section>
                <q-item-section>
                  <q-item-label>
                    {{
                    item.label
                    }}
                  </q-item-label>
                </q-item-section>
                <q-tooltip
                  v-if="!$q.screen.lt.md && miniState"
                  anchor="center right"
                  self="center left"
                  :offset="[5, 10]"
                  content-style="font-size: 13px"
                >{{ item.label }}</q-tooltip>
              </q-item>

              <q-expansion-item
                v-else-if="item.children && item.children.length > 0"
                :label="item.label"
                v-entity-right.enableIfRead="item.entity"
                v-operation-right="item.operationRight"
                v-interface-right="item.interfaceRight"
                group="drawergroup"
                expand-separator
              >
                <template v-slot:header>
                  <q-item-section
                    avatar
                    :style="
                      !miniState ? 'min-width: 36px' : 'min-width: initial'
                    "
                  >
                    <q-avatar
                      :icon="item.icon"
                      :size="item.iconSize ? item.iconSize : '22px'"
                      :class="item.iconClass"
                    />
                  </q-item-section>
                  <q-item-section>{{ item.label }}</q-item-section>
                </template>
                <q-list>
                  <q-item
                    clickable
                    v-ripple
                    v-for="(item, index) in item.children"
                    v-entity-right.enableIfRead="item.entity"
                    :key="index + 20"
                    :to="item.link"
                    @click="toggleNavMenu"
                    active-class="bg-grey-1 text-primary"
                    style="padding-left: 36px"
                  >
                    <q-item-section
                      avatar
                      :style="
                        !miniState ? 'min-width: 36px' : 'min-width: initial'
                      "
                    >
                      <q-icon
                        :name="item.icon"
                        :size="item.iconSize ? item.iconSize : '18px'"
                        :class="item.iconClass"
                      />
                    </q-item-section>
                    <q-item-section>
                      <q-item-label style="font-size: 14px">
                        {{
                        item.label
                        }}
                      </q-item-label>
                    </q-item-section>
                  </q-item>
                </q-list>
                <q-separator v-if="navItem.separator && !miniState" />
              </q-expansion-item>
            </div>
            <q-separator v-if="navItem.separator" />
          </q-list>
          <!-- <q-separator class="q-mt-sx q-mb-md" />
          <div class="q-py-md q-px-md text-grey-9">
            <div class="row items-center q-gutter-x-sm q-gutter-y-xs">
              Портал працює в режимі дослідної експлуатації,
              <br />якщо у вас є зауваження, надсилайте їх за адресою
              <a
                class="text-grey-9"
                href="mailto:support@e-transport.gov.ua"
              >support@e-transport.gov.ua</a>
            </div>
          </div>-->
        </template>

        <template v-if="miniState">
          <div v-for="(item, index) in miniStateNav" :key="index + 10">
            <q-item
              v-entity-right.enableIfRead="item.entity"
              clickable
              v-ripple
              :to="item.link"
              active-class="bg-grey-1 text-primary"
              @click="toggleNavMenu"
            >
              <q-item-section avatar :style="!miniState ? 'min-width: 36px' : 'min-width: initial'">
                <q-icon
                  :name="item.icon"
                  :size="item.iconSize ? item.iconSize : '22px'"
                  :class="item.iconClass"
                />
              </q-item-section>
              <q-item-section>
                <q-item-label style="font-size: 14px">
                  {{
                  item.label
                  }}
                </q-item-label>
              </q-item-section>
              <q-tooltip
                anchor="center right"
                self="center left"
                :offset="[5, 10]"
                content-style="font-size: 13px"
              >{{ item.label }}</q-tooltip>
            </q-item>
          </div>
        </template>
      </div>

      <!-- <q-list v-for="(menuItem, index) in menuList" :key="index">
        <q-item clickable v-ripple :to="menuItem.link" active-class="bg-grey-1 text-primary">
          <q-item-section avatar>
            <q-icon :name="menuItem.icon" size="22px" />
          </q-item-section>
          <q-item-section>
            <q-item-label style="font-size: 14px">{{ menuItem.label }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>-->
    </q-scroll-area>
  </q-drawer>
</template>

<script>
import { mapState, mapActions, mapGetters } from 'vuex'
import { getData, env } from '@/services/api'

// import entityRight from "./../../directives/entityRight"
export default {
  data() {
    return {}
  },
  computed: {
    ...mapState('baseElements', ['userName', 'isBackBtn', 'drawer', 'miniState']),

    ...mapGetters('baseElements', {
      user: 'getUserInfo',
      rights: 'getUserRights',
      navMenu: 'getNavMenu',
    }),

    miniStateNav() {
      return this.navMenu.reduce((acc, item) => {
        if (item.children && item.children.length > 0) {
          item.children.forEach(child => {
            if (child.children && item.children.length > 0) {
              child.children.forEach(innerChild => {
                acc.push(innerChild)
              })
            } else {
              acc.push(child)
            }
          })
        } else {
          acc.push(item)
        }
        return acc
      }, [])
    },
  },
  // directives: {
  // entityRight
  // },
  methods: {
    goToModdi() {
      getData(env.SYNC.GET_MODDI_URL.PATH).then(response => {
        window.location.href = response.data
      })
    },

    goToPbchSchedule(scheduleType) {
      const link = env.SYNC_PBCH.GET_MODDI_URL.PATH + scheduleType + '&isEhealth=true'
      getData(link).then(response => {
        window.location.href = response.data
      })
    },

    toggleNavMenu() {
      if (this.$q.screen.lt.md) {
        this.toggleDrawer()
      }
    },
    ...mapActions('baseElements', ['toggleDrawer']),
  },
  mounted() {
    if (this.$q.screen.lt.md) {
      this.toggleDrawer()
    }
  },
}
</script>

<style lang="stylus" scoped>
.nav-drawer {
  background: #333 !important;
}

.user-container {
  position: absolute;
  width: 100%;
  // height: 120px;
  background-color: $grey-8;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.nav_scroll-area {
  height: 100%;
}
</style>

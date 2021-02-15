<template>
  <div class="wrapper">
    <ul class="q-pa-md dashboard">
      <li :class="$q.screen.gt.xs ? 'dashboard_item span-2': 'dashboard_item user_card'">
        <q-card flat class="q-pa-md row items-center justify-center full-height">
          <div class="column items-center justify-center text-grey-7">
            <q-avatar
              icon="fas fa-user-tie"
              color="grey-7"
              text-color="white"
              size="70px"
              class="q-mb-lg"
            />
            <!-- <q-icon size="56px" class="icon ico-doctor q-mb-md" /> -->
            <div
              class="q-mb-sm text-uppercase text-center"
              style="font-weight: 600"
            >{{ user.positionName }}</div>
            <div class="text-body1 text-center">({{ user.personFullName }})</div>
            <div class="text-body1 text-center">{{ user.organizationCaption }}</div>
          </div>
        </q-card>
      </li>
      <li :class="$q.screen.gt.xs ? 'dashboard_item span-3': 'dashboard_item date'">
        <q-date
          v-model="todayDate"
          :landscape="$q.screen.gt.xs"
          flat
          format="DD.MM.YYYY"
          mask="DD.MM.YYYY"
          color="primary-gradient-bottom"
          class="date-item"
        />
      </li>
      <li
        v-for="(item, index) in dashboardCards"
        class="dashboard_item"
        :class="item.class  ? item.class : '' "
        :key="index"
        v-entity-right.enableIfRead="item.entity"
        v-operation-right="item.operationRight"
      >
        <router-link v-if="item.link" :to="item.link" class="link-item">
          <div class="inner">
            <q-icon :name="item.icon" size="38px" :class="'inner_icon '+ item.iconClass" />
            <p class="inner_text text-center">{{ item.label }}</p>
          </div>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
import moment from 'moment'
import { mapState } from 'vuex'
import isEmpty from 'lodash.isempty'

export default {
  data() {
    return {
      todayDate: '',
    }
  },
  created() {
    this.todayDate = moment()
      .locale('uk')
      .format('L')
  },
  computed: {
    ...mapState('baseElements', ['user', 'rights', 'navMenu']),
    dashboardCards() {
      return this.navMenu.reduce((dashboardCards, item) => {
        if (!isEmpty(item.children)) {
          item.children.forEach(child => {
            if (!isEmpty(child.children)) {
              child.children.forEach(innerChild => {
                dashboardCards.push(innerChild)
              })
            } else {
              dashboardCards.push(child)
            }
          })
        } else {
          dashboardCards.push(item)
        }
        return dashboardCards
      }, [])
    },
  },
}
</script>

<style lang="stylus">
.q-date .q-btn.q-btn--unelevated {
  background-color: $primary !important;
}

.wrapper {
  min-height: calc(100vh - 50px);
}

.dashboard {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(140px, 1fr));
  grid-gap: 10px;
  grid-auto-rows: minmax(140px, auto);
  grid-auto-flow: dense;
  list-style: none;

  // max-width: 1920px;
  // margin: 0 auto;
  &_item {
    border-radius: 8px;
    width: 100%;
    height: 100%;
    transition: $transition;
    overflow: hidden;
    border-radius: 6px;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);
  }

  .link-item {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    position: relative;
    width: 100%;
    height: 100%;
    z-index: 0;
    font-size: 14px;
    text-transform: uppercase;
    color: $grey-9;
    border-radius: 6px;
    background: #fff;
    border: 1px solid #fff;
    text-decoration: none;
    transition: $transition;

    &:hover {
      border-color: $info;
      // box-shadow: 0 1px 3px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
    }

    &:hover .inner .inner_icon {
      transition: $transition;
      gradient-diagonal(1);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
    }

    &:hover:before {
      transform: scale(2.5);
    }

    &:before {
      content: '';
      position: absolute;
      z-index: 1;
      top: -16px;
      right: -16px;
      height: 40px;
      width: 40px;
      gradient-green(1);
      border-radius: 32px;
      transform: scale(0);
      transition: $transition;
    }

    &:after {
      content: '→';
      position: absolute;
      z-index: 2;
      right: 8px;
      top: 4px;
      color: #fff;
      font-size: 16px;
    }

    .inner {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;

      &_icon {
        font-size: 48px;
        font-weight: 501;
        margin-bottom: 8px;
        transition: $transition;
        color: $info;
        width: 2em;
      }

      &_text {
        font-size: 14px;
        text-transform: uppercase;
        margin: 0;
        padding: 0 4px;
      }
    }
  }

  .date-item {
    width: 100%;
    height: 100%;
    color: #333;

    .q-date__today {
      background-color: $grey-3;
      border: 1px solid $grey-5;
      box-shadow: none;
    }

    .q-btn.q-btn--unelevated {
      background: $info !important;
    }
  }

  .span-2 {
    grid-column-end: span 2;
    grid-row-end: span 2;
  }

  .span-3 {
    grid-column-end: span 3;
    grid-row-end: span 2;
  }

  @media (max-width: $breakpoint-xs) {
    display: flex;
    flex-direction: column;

    &_item {
      margin-bottom: 10px;
      height: 140px;
    }

    .date, .user_card {
      height: auto;
    }
  }
}
</style>

<template>
  <div>
    <h4
      v-if="menuHeader"
      class="text-h6 text-center text-uppercase text-grey-8 q-my-md"
    >{{menuHeader}}</h4>

    <ul class="menu-cards">
      <li
        v-for="(menuItem, index) in menuList"
        :key="index"
        v-entity-right.enableIfRead="menuItem.entity"
        class="menu-cards_item"
      >
        <router-link :to="menuItem.link" class="item-link">
          <q-icon
            v-if="menuItem.svgIco"
            :name="`img:public/icons/dashboard/${menuItem.svgIco}.svg`"
            class="item-icon"
          />
          <i v-else :class="menuItem.icon" class="item-icon"></i>
          <p class="item-text">{{ menuItem.label }}</p>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: {
    menuList: Array,
    menuHeader: String,
  },

  computed: {
    ...mapGetters('baseElements', { user: 'getUserInfo', rights: 'getUserRights' }),
  },
}
</script>
<style lang="stylus" scoped>
.menu-cards {
  display: flex;
  flex-wrap: wrap;
  align-items: flex-start;
  justify-content: flex-start;
  list-style: none;
  padding: 0;
  margin: 0;

  &_item {
    flex: 1 1 45%;
    padding: 0;
    margin: 6px;

    @media (max-width: $breakpoint-xs) {
      flex: 1 1 100%;
    }
  }

  .item {
    &-link {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      text-decoration: none;
      height: auto;
      min-height: 140px;
      padding: 16px;
      text-align: center;
      color: $grey-9;
      background: $grey-1;
      border-radius: 3px;
      transition: $transition;
      box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);

      &:hover {
        transition: $transition;
        box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);         /* gradient(0.65);
                                                color: #fff;
        
                                                .item-icon {
                                                  background: #fff;
                                                  -webkit-background-clip: text;
                                                  -webkit-text-fill-color: transparent;
                                                }
                                                */
      }
    }

    &-icon {
      font-size: 48px;
      font-weight: 500;
      margin-bottom: 4px;
      gradient(1);
      transition: $transition;
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
    }

    &-text {
      font-size: 14px;
      text-transform: uppercase;
      margin: 0;
      padding: 0 4px;
    }
  }
}
</style>

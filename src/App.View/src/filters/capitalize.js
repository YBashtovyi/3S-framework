import Vue from 'vue';

/* Filter which makes first letter of string uppercase */
Vue.filter('capitalize', value => {
  return value ?
    `${value.charAt(0).toUpperCase()}${value.slice(1)}` :
    ''
})

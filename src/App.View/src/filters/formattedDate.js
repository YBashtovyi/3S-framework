import Vue from 'vue'
import moment from "moment";

/* Filter which formates date. */
Vue.filter('formattedDate', (value, format, toLocal = false) => {
  if (value) {
    let momentDate = moment(value)

    if (toLocal) {
      const utcOffset = moment().utcOffset()
      momentDate = momentDate.add(utcOffset, 'minutes')
    }

    return format 
      ? momentDate.locale("uk").format(format)
      : momentDate.locale("uk").format('L');
  }
})

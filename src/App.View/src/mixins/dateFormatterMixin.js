import moment from 'moment'
import { date } from 'quasar'

export default {
  data () {
    return {}
  },
  methods: {

    /**
     * return current date
     */
    getCurrentDate() {
      const currentDate = moment().format()
      return currentDate
    },

    /**
     * return formatted date
     */
    formatDate(date, format) {
      return moment(new Date(date))
            .format(format)
    },

    /**
     * Return full years beetween two dates. If endDate param is null
     * then endDate = current date
     * @param {*} startDate 
     * @param {*} endDate 
     */
    getDifferenceBetweenTwoDatesInFullYears(startDate, endDate) {
      if (startDate) {
        const localStartDate = this.getLocalDate(startDate)
        const localEndDate = endDate? this.getLocalDate(endDate): Date.now()
        const momentEndDate = moment(localEndDate)
        const momentStartDate = moment(localStartDate)
        return momentEndDate.diff(momentStartDate, 'years')
      }
      return 0
    },

    /**
     * Returns difference between two dates
     * @param {*} startDate start date
     * @param {*} endDate 
     * @param {*} unit indicates the unit of measurement, if not specified then it is days by default
     */
    getDifferenceBetweenTwoDates(startDate, endDate, unit) {
      const localStartDate = this.getLocalDate(startDate)
      const localEndDate = this.getLocalDate(endDate)
      return date.getDateDiff(localStartDate, localEndDate, unit)
    },

    /**
     * Convert local date to server date
     * @param {*} newValueDate new date value
     * @param {*} oldValue old date value
     * @param {*} time time
     */
    getServerFormatedDate (newValueDate, oldValue, time) {
      if (!newValueDate) {
        return null
      }
      const oldValueDate = moment(oldValue)
        .locale('uk')
        .format('L')
      if (newValueDate === oldValueDate) {
        return oldValue
      }
      if (!time) {
        time = '00:00'
      }
      return moment
        .utc(newValueDate + ' ' + time, ['DD.MM.YYYY HH:mm'], 'uk')
        .format()
    },

    /**
     * Convert data from server (UTC) to localeDate. Default Uk - locale, L - format
     * @param {*} utcDate 
     * @param {*} locale 
     * @param {*} format 
     */
    getFormattedLocalDate (utcDate, locale, format) {
      const localDate = this.getLocalDate(utcDate)
      if (localDate) {
        return moment(localDate)
          .locale(locale || "uk")
          .format(format || "L")
      }
      else {
        return ''
      }
    },

    /**
     * converts server utc date to local object Date
     */
    getLocalDate (utcDate) {
      if (!utcDate) {
        return;
      }
      if (utcDate.substr(utcDate.length-1) === 'Z') {
        return new Date(utcDate)
      }
      const localDate = new Date(utcDate)
      const timeZoneDifference = localDate.getTimezoneOffset() * (-1)
      return new Date(new Date(utcDate).getTime() + timeZoneDifference * 60 * 1000)

    },

    /**
     * converts server utc date to local object Date
     */
    convertToLocalDate (utcDate, format) {
      if (!utcDate) {
        return;
      }
      
      if (format) {
        const result = moment(utcDate).format(format)
        return result
      }
        
      else 
        return moment(utcDate).local()
    },

    /**
     * converts server utc date to local object Date
     */
    convertToUtcDate (localeDate, format) {
      if (!localeDate) {
        return;
      }
      
      if (format)
        return moment(localeDate).utc().format(format)
      else 
        return moment(localeDate).utc()
    },

    /**
     * add days to date
     * @param {*} date source date 
     * @param {*} days amount of days
     */
    addDays(date, days) {
      let result = new Date(date)
      result.setDate(result.getDate() + days)
      return result
    },

    /**
     * returns week start day
     * @param {*} date current date
     */
    getStartOfWeek(date) {
      date = moment(date)
      return date.startOf('isoWeek');
    },

    /**
     * returns week end day
     * @param {*} date current date
     */
    getEndOfWeek(date) {
      date = moment(date)
      return date.endOf('isoWeek');
    },

    
    /**
     * checks if date is iso formatted date
     * @param {*} date current date
     */
    isIsoDate(date) {
      if (!/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z/.test(date)) return false;
      var d = new Date(date); 
      return d.toISOString()===date;
    }
  }
}

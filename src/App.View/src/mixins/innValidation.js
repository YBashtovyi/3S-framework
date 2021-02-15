import moment from "moment"

export default {
  
  data() {
    return {
        maleGenderCode : 'MALE',
        femaleGenderCode : 'FEMALE'
      }
  },

  methods: {

    /**
     * validation ukrainian tax id function
     * by gender and checksum
     * @param {*} taxId
     * @param {*} gender
     */
    isValidUATaxId(taxId, genderCode, birthDate) {
      
      if (!taxId || !genderCode || !birthDate) {
        return false
      }
      const taxIdArray = taxId.split("")

      const checkSum = this.getTaxIdCheckSum(taxId)
      const genderFromTaxId = this.getGenderFromTaxId(taxId)
      const birthDateFromTaxId = this.getBirthDateFromTaxId(taxId)
      
      return checkSum === parseInt(taxIdArray[9]) && 
        genderFromTaxId === genderCode &&
        birthDateFromTaxId === moment(birthDate).locale('uk').format('L')
    },

    /**
     * get tax id checksum
     * @param {*} taxId 
     */
    getTaxIdCheckSum(taxId) {
      const taxIdArray = taxId.split("")
      let checkSum = (((-1)*taxIdArray[0] + 5 * taxIdArray[1] + 7 * taxIdArray[2] +
              9 * taxIdArray[3] + 4 * taxIdArray[4] + 6 * taxIdArray[5] +
              10 * taxIdArray[6] + 5 * taxIdArray[7] + 7 * taxIdArray[8]) % 11)
      if (checkSum === 10) {
        checkSum = 0
      }
      return checkSum
    },

    /**
     * get gender code from tax id
     * @param {*} taxId 
     */
    getGenderFromTaxId(taxId) {
      const taxIdArray = taxId.split("")
      const genderCheckSum = (Math.floor(taxIdArray[8]) % 2)
      if (genderCheckSum > 0) {
        return this.maleGenderCode
      }
      if (genderCheckSum === 0) {
        return this.femaleGenderCode
      }
      return null
    },

    /**
     * get birthday code from tax id
     * @param {*} taxId 
     */
    getBirthDateFromTaxId(taxId) {
      let days = taxId.substr(0, 5)
      let startYear = 1900
      let finalDate = ''
      while(days > 0) {
        const daysInYear = this.checkDate(29, 2, startYear)? 366: 365
        if (days > daysInYear) {
          days = days - daysInYear
          startYear = startYear + 1
        }
        else {
          finalDate = new Date(startYear, 0, 1)
          finalDate.setDate(finalDate.getDate() + (days - 1))
          days = 0
        }
      }
      return moment(finalDate)
        .locale('uk')
        .format('L')
    },

    /**
     * check is date valid
     * @param {*} day 
     * @param {*} month 
     * @param {*} year 
     */
    checkDate(day, month, year) {

      if (month === 2 && day === 29) {

        if (year%4 === 0 && (year%100 !==0 || year%400 === 0)) {
          return true
        }
        else {
          return false
        }
      }
      else {
        const trueDate = new Date()
        trueDate.setFullYear(year, (month-1), day)

        if ((trueDate.getMonth() + 1) === month && day < 32) {
          return true
        }
        else {
          return false
        }
      }
    }
  }
}

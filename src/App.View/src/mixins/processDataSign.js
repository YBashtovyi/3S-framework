import { getData, postData, env } from '@/services/api'

export default {
  data () {
    return {
      showSignMenu: false,
      // contains an array of object with
      signSettingFields: [],
      // data, that should be signed. will be created from sign settings and field values
      dataToSign: {},
      entityName: ''
    }
  },
  methods: {
    ehealthContractDataSignEvent (result) {
      this.showSignMenu = false
      if (result.success) {
        console.log('Good')
      } else {
        // here we can deal with errors if any
        if (result.errorMessage) {
          console.log(result.errorMessage)
        }
      }
    },
    ehealthOrgDataSignEvent (result) {
      this.showSignMenu = false
      if (result.success) {
        let data = {
          fileData: result.signedData,
          entityId: this.ehealthOrgProfile.id
        }
        postData(env.ORGANIZATION.EHEALTH.CREATE, data)
          .then(response => {
            if (response.data != null) {
              this.$q.notify({
                position: 'top',
                timeout: 2500,
                message: 'Організацію успішно зареєестровано в E-Health',
                color: 'positive',
                icon: 'check'
              })
            }
          })
          .catch(error => {
              console.log(error)
          })
      } else {
        // here we can deal with errors if any
        if (result.errorMessage) {
          console.log(result.errorMessage)
        }
      }
    },
    ehealthEmployeeDataSignEvent (result) {
      this.showSignMenu = false
      if (result.success) {
        let data = {
          fileData: result.signedData,
          entityId: this.ehealthDoctorProfile.id
        }
        postData(env.DOCTOR.EHEALTH.CREATE, data)
          .then(response => {
            if (response.data != null) {
              this.ehealthDoctorProfile.ehealthRequestId = response.data.data.id
              this.$q.notify({
                position: 'top',
                timeout: 2500,
                message: 'Запит на реєстрацію співробітника успішно додано в E-Health',
                color: 'positive',
                icon: 'check'
              })
            }
          })          
      }
    },
    processDataSignEvent (result) {
      this.showSignMenu = false
      if (result.success) {
        this.saveSignedData(result)
      } 
    },
    // should be
    saveSignedData (data) {
      const fileDto = {
        fileData: data.signedData,
        entityName: data.entityName,
        entityId: data.entityId,
        documentType: data.documentType
      }
      postData(env.FILESTORE.UPLOAD, fileDto)
    },
    openSignMenu (entityName, dataToSign) {
      if (dataToSign) {
        this.dataToSign = dataToSign
        this.showSignMenu = true
        return
      }
      if (this.signSetting && this.signSetting.keys.length !== 0) {
        if (this.initializeDataToSign(this.signSetting)) {
          this.showSignMenu = true
        }
        return
      }
      let settingUrl = env.SIGNING.SETTINGS + '?EntityName=' + entityName
      getData(settingUrl)
        .then(response => {
          this.signSettingFields = response.data
          this.initializeDataToSign()
          this.showSignMenu = true
        })
        .catch(error => console.log(error));
    },
    initializeDataToSign () {
      if (!this.signSettingFields) {
        console.log(
          'Sign settings are not set. Cannot initialize data to sign'
        )
        return false
      }

      if (!this.dataToSign || Object.keys(this.dataToSign).length === 0) {
        for (let i = 0; i < this.signSettingFields.length; i++) {
          const setting = this.signSettingFields[i]
          // config in a database may contain first letter in upper case, so we shoud convert it to lower
          const fieldName = this.makeFirstLetterLower(setting.fieldName)
          // this name should be signed along with value
          const signedFieldName = setting.signFieldName
          // get field value from ui and populate data to sign object
          this.dataToSign[signedFieldName] = this.details[fieldName]
        }
        return true
      }
      return false
    },
    makeFirstLetterLower (s) {
      return s.length ? s.charAt(0).toLowerCase() + s.slice(1) : s
    }
  }
}

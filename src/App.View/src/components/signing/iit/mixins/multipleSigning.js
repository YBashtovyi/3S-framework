import { mapActions, mapGetters } from "vuex"
import {
  SET_SIGNING_INFO,
  CLEAR_SIGNING_INFO
} from "../../../../store/modules/pages/signing/signingData/constants"
import { IITPlugin } from "../euscptest"
import { postData, env } from "@/services/api"

// Will need to do mixin
export default {
  data() {
    return {
      // Spinner for plugin
      visiblePluginTransition: false,
      // Spinner for FileDrive
      visibleFileDriveTransition: false,
      // Show user recommendation to download cryptography library
      errorLoadLib: false,
      keyText: "",
      keyFileLoaded: false,
      isPwdMask: true,
      // Selected Certificate Authority
      caServer: "",
      // Certificate authorities
      caServers: [],
      password: "",
      // IITPlugin
      lib: undefined,
      // Tab name, default first
      keyValue: "file",
      // Tabs
      keyOptions: [
        {
          label: "Файловий носій",
          value: "file"
        },
        {
          label: "Захищений носій",
          value: "flashDrive"
        }
      ],
      registerLoading: false,
      isSaveSigningInfo: false,
      // Data to be signed
      signedData: null,
      // Scripts that will dynamically load
      scripts: [
        { src: "statics/js/EUSign/jquery.js", name: "jquery.js" },
        { src: "statics/js/EUSign/Blob.min.js", name: "Blob.min.js" },
        {
          src: "statics/js/EUSign/jquery.blockUI.js",
          name: "jquery.blockUI.js"
        },
        { src: "statics/js/EUSign/jszip.min.js", name: "jszip.min.js" },
        { src: "statics/js/EUSign/appSign.js", name: "appSign.js" }
      ],
      // uri path (static/data)
      staticUri: "",
    }
  },
  props: {
    entityName: {
      type: String,
      required: false
    },
    entityId: {
      type: String,
      required: false
    },
    // The name of the signature button.
    registerButtonCaption: {
      type: String,
      required: false,
      default: "Зчитати"
    },
    // Form validation
    hasValidErrors: {
      type: Boolean,
      required: false,
      default: false
    },
    // Validation Errors
    validErrorMessage: {
      type: String,
      required: false,
      default: ""
    },
  },

  watch: {
    /*
     * Select tab
     */
    keyValue: function (val) {
      if (val === "flashDrive") {
        this.loadIITFlashDrive()
      } else {
        this.loadIITPlugin()
      }
    },

    isSaveSigningInfo: function (value) {
      if (!value && this.signingInfo.caServer) {
        this.clearSigningInfo()
        this.notifyUserDialog("Збережені дані цифрового ключа були видалені", "primary", "info")
      }
    }
  },

  computed: {
    ...mapGetters("signingData", ["signingInfo",]),

    notFoundExtention: function () {
      return (
        "Якщо ви бачите це повідомлення,  ймовірно, виникла помилка з криптографічною бібліотекою.  " +
        "У такому випадку її необхідно запустити або інсталювати до системи. Для цього виконайте кілька кроків:" +
        "<br><br><div>1. Ознайомтесь із інструкцією: <a style='text-indent:18px' href='http://iit.com.ua/download/productfiles/EUSignWebOManual.pdf'>" +
        "Завантажити</a></div><div>2. Встановіть веб-розширення: <a style='text-indent:18px' href='https://chrome.google.com/webstore/detail/%D1%96%D1%96%D1%82-%D0%BA%D0%BE%D1%80%D0%B8%D1%81%D1%82%D1%83%D0%B2%D0%B0%D1%87-%D1%86%D1%81%D0%BA-1-%D0%B1%D1%96%D0%B1%D0%BB/jffafkigfgmjafhpkoibhfefeaebmccg?utm_source=chrome-app-launcher-info-dialog'>" +
        "Завантажити</a></div><div>3. Завантажте та встановіть інсталяційний пакет файлів: <a style='text-indent:18px'" +
        "href='http://iit.com.ua/download/productfiles/EUSignWebInstall.exe'>Завантажити</a></div><div>" +
        "4. Повторіть процес електронної ідентифікації.</div>"
      )
    }
  },

  methods: {
    ...mapActions("signingData", {
      setSigningInfo: SET_SIGNING_INFO,
      clearSigningInfo: CLEAR_SIGNING_INFO
    }),

    /*
     * Load libraries for file drive
     */
    loadJS(url, onSuccess, location) {
      console.log("LIB LOADED")
      const scriptTag = document.createElement("script")

      scriptTag.src = url
      scriptTag.onload = onSuccess
      scriptTag.onreadystatechange = onSuccess

      location.appendChild(scriptTag)
    },

    /*
     * Unload libraries file drive
     */
    unloadJS() {
      console.log("UNLOADED")
      const scripts = document
        .getElementById("dialog")
        .getElementsByTagName("script")
      for (let i = 0; i < scripts.length; i++) {
        document.getElementById("dialog").removeChild(scripts[i])
      }
      window["unloadSignLib"] = true
    },

    /*
     * Select Key Certification Authority
     */
    caServerChanged() {
      this.lib.setCurrentCaServer(this.caServer)
    },

    /*
     * Signing data using a file
     */
    tryLoadKeyAndSignData() {
      if (this.hasValidErrors) {
        this.notifyUserDialog(
          this.validErrorMessage,
          "negative",
          "fas fa-minus"
        )
        return
      }
      if (!this.keyFiles || this.keyFiles.length === 0) {
        this.notifyUserDialog(
          "Необхідно обрати ключ",
          "negative",
          "fas fa-minus"
        )
        return
      }

      if (this.password.length === 0) {
        this.notifyUserDialog("Не вказано пароль доступу до особистого ключа", "negative", "fas fa-minus")
        return
      }
      
      this.registerLoading = true
      const file = this.keyFiles[0]
      const reader = new FileReader()
      reader.onloadend = evt => {
        this.lib.readPrivateKey(file, this.password, error => {
          if (error && error.message) {
            this.notifyUserDialog(error.message, "negative", "fas fa-minus")
            this.registerLoading = false
          } else {
            console.log('KEY LOADED')
            this.$emit('keyLoaded', true)
            this.registerLoading = false
            // this.signData()
          }
        })
      }
      reader.readAsArrayBuffer(file)
    },

    /*
     * Signing data using a flash drive
     */
    tryLoadKeyAndSignDataFileDrive() {
      if (this.hasValidErrors) {
        this.notifyUserDialog(
          this.validErrorMessage,
          "negative",
          "fas fa-minus"
        )
        return
      }

      this.signDataFileDrive()
    },

    /*
     * Get up-to-date certification authorities
     */
    getCaServers(val, update, abort) {
      if (this.caServers.length < 0) {
        update()
        return
      }
      setTimeout(() => {
        update(() => {
          if (this.caServers.length > 0) {
            return
          }
          let servers = this.lib.getCAServers()
          if (servers) {
            for (let i = 0; i < servers.length; i++) {
              let item = {
                code: servers[i].address,
                value: servers[i].issuerCNs[0]
              }
              this.caServers.push(item)
            }
          }
        })
      }, 1000)
    },

    loadFileData(event) {
      this.keyFileLoaded = false
      this.keyFiles = event.target.files
      this.keyText = ""
      for (let i = 0; i < this.keyFiles.length; i++) {
        this.keyText += this.keyFiles[i].name
      }
    },

    /*
     * Signing data using a file and send to ehealth
     */
    signData(dataToSign) {
      let signedData = this.lib.signData(dataToSign)
      if (signedData) {
        if (this.isSaveSigningInfo) {
          this.setSigningInfo({
            caServer: this.caServer,
            caFile: this.keyFiles[0]
          })
        }
        if (this.entityName && this.entityId) {
          this.saveSignedData(signedData)
        }
        return { success: true, signedData: signedData }
        // this.$emit("dataWasSigned", { success: true, signedData: signedData })
        // setTimeout(() => this.registerLoading = false, 5000)
      } else {
        this.registerLoading = false
        return { success: false, message: 'Помилка під час підпису файлу' }
      }
    },

    /*
     * Close the dialog
     */
    closeDialog() {
      if (!this.visiblePluginTransition && !this.visibleFileDriveTransition) {
        this.unloadJS()
        this.$emit("closeDialog")
      }
    },

    /*
     * Signing data using a flash drive and send to ehealth
     */
    signDataFileDrive(dataToSign) {
      const that = this
      this.visibleFileDriveTransition = true
      window["signPrivate"](dataToSign, function (base64) {
        that.visibleFileDriveTransition = false
        if (base64) {
          that.notifyUserDialog(
            "Документ підписано успішно",
            "positive",
            "check"
          )
          if (that.entityName && that.entityId) {
            that.saveSignedData(base64)
          }
          that.$emit("dataWasSigned", {
            success: true,
            signedData: base64
          })
          that.visible = false
        } else {
          that.notifyUserDialog(
            "Помилка при підписанні данних",
            "negative",
            "fas fa-minus"
          )
        }
      })
    },

    /*
     * Show notification
     */
    notifyUserDialog(message, color, icon) {
      this.$q.notify({
        position: "bottom",
        timeout: 5000,
        message: message,
        color: color,
        icon: icon
      })
    },

    /*
     * Send signed file to Ehealth
     */
    saveSignedData(signedData) {
      const fileDto = {
        fileData: signedData,
        entityName: this.entityName,
        entityId: this.entityId,
        documentType: "SignedFile"
      }
      postData(env.FILESTORE.UPLOAD, fileDto).then(response => { })
    },

    /*
     * Initializing the library for working with files
     */
    loadIITPlugin() {
      const that = this
      this.visiblePluginTransition = true
      this.registerLoading = false
      this.visibleFileDriveTransition = false

      this.lib = new IITPlugin()
      this.lib.initialize(function (response) {

        if (response) {

          if (that.signingInfo.caServer) {
            that.caServer = that.signingInfo.caServer
            that.keyFiles = [that.signingInfo.caFile]
            that.keyText = that.signingInfo.caFile.name

            that.isSaveSigningInfo = true
          } else {
            that.caServer = {
              code: response.address,
              value: response.issuerCNs[0]
            }
            that.keyText = ""
            that.isSaveSigningInfo = false
          }

          that.lib.setCurrentCaServer(that.caServer)
          that.visiblePluginTransition = false
        }

      })
    },

    /*
     * Initializing the library for working with flash drive
     */
    loadIITFlashDrive() {
      const that = this
      this.visibleFileDriveTransition = true
      this.errorLoadLib = false
      if (window["loadPrivateKeyLib"] === undefined) {
        this.loadJS(
          this.scripts[0].src,
          this.checkLib,
          document.getElementById("dialog")
        )
        return
      }
      window["loadPrivateKeyLib"](
        response => this.loadPrivateKeyLib(response, that),
        this.staticUri
      )
    },

    /*
     *  Response from the library (flash drive). library initialization success
     */
    loadPrivateKeyLib(response, that) {
      if (!response) {
        that.errorLoadLib = true
      } else {
        that.errorLoadLib = false
      }
      that.visibleFileDriveTransition = false
    },

    /*
     * Library loading one by one (flash drive)
     */
    checkLib(response) {
      const scriptName = response.srcElement.src.split("/").pop()
      for (let i = 0; i < this.scripts.length; i++) {
        if (
          this.scripts[i].name === scriptName &&
          i !== this.scripts.length - 1
        ) {
          this.loadJS(
            this.scripts[i + 1].src,
            this.checkLib,
            document.getElementById("dialog")
          )
          return
        }
      }
      const that = this
      window["loadPrivateKeyLib"](
        response => this.loadPrivateKeyLib(response, that),
        this.staticUri
      )
    }
  },

  mounted() {
    this.staticUri = process.env.STATICS_URI
  },

  created() {
    this.keyValue = "file"
    this.loadIITPlugin()
  }
}
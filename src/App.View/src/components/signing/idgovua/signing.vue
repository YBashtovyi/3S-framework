<template>
  <q-dialog persistent v-model="isVisibleDialog" @show="initializeSignFrame">
    <q-card style="max-width: 680px">
      <q-toolbar>
        <q-avatar>
          <i class="fas fa-key"></i>
        </q-avatar>

        <q-toolbar-title>Ідентифікація за цифровим підписом</q-toolbar-title>

        <q-btn flat round dense icon="close" @click="closeDialog" />
      </q-toolbar>
      <q-card-section>
        <div id="sign-widget-parent" style="width:650px;height:500px"></div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
export default {
  props: {
    isVisibleDialog: {
      type: Boolean,
      default: false,
    },
  },

  data() {
    return {
      frame: undefined,
      SIGN_WIDGET_PARENT_ID: 'sign-widget-parent',
      SIGN_WIDGET_ID: 'sign-widget',
      SIGN_WIDGET_URI: 'https://id.gov.ua/sign-widget/v20200922',
    }
  },

  methods: {
    /*
     * Initialize frame to read the key and sign file
     */
    initializeSignFrame() {
      this.frame = new window.EndUser(
        this.SIGN_WIDGET_PARENT_ID,
        this.SIGN_WIDGET_ID,
        this.SIGN_WIDGET_URI,
        window.EndUser.FormType.SignFile,
      )

      this.frame
        .ReadPrivateKey()
        .then(keyInfo => {
          this.signData()
        })
        .catch(function(e) {
          alert('Виникла помилка при зчитуванні ос. ключа. ' + 'Опис помилки: ' + (e.message || e))
        })
    },

    /*
     * Close the dialog
     */
    closeDialog() {
      this.frame.destroy()
      this.$emit('closeDialog')
    },

    /*
     * Signing data using a file and send to ehealth
     */
    signData() {
      var dataArray = new Uint8Array(this._base64ToBytaArray(this.dataToSign))
      this.frame
        .SignData(
          dataArray,
          false,
          true,
          window.EndUser.SignAlgo.DSTU4145WithGOST34311,
          null,
          window.EndUser.SignType.CAdES_X_Long,
        )
        .then(signedData => {
          if (signedData) {
            alert('Документ підписано успішно')
            // this.notifyUserDialog(
            //   "Документ підписано успішно",
            //   "positive",
            //   "check"
            // )

            // TO DO: do we need this methods

            // if (this.isSaveSigningInfo) {
            // this.setSigningInfo({
            //   caServer: this.caServer,
            //   caFile: this.keyFiles[0]
            // })
            // }

            // if (this.entityName && this.entityId) {
            // this.saveSignedData(signedData)
            // }

            this.$emit('dataWasSigned', { success: true, signedData: signedData })
            setTimeout(() => (this.registerLoading = false), 5000)
          } else {
            alert('Помилка при підписанні данних')
            // this.notifyUserDialog(
            //   "Помилка при підписанні данних",
            //   "negative",
            //   "fas fa-minus"
            // )
            // this.registerLoading = false
          }
        })
    },

    /*
     * Decode base64 to Uint8Array
     */
    _base64ToBytaArray(base64) {
      var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'
      var lookup = new Uint8Array(256)
      for (var i = 0; i < chars.length; i++) {
        lookup[chars.charCodeAt(i)] = i
      }
      var bufferLength = base64.length * 0.75,
        len = base64.length,
        p = 0,
        encoded1,
        encoded2,
        encoded3,
        encoded4
      var j = 0
      if (base64[base64.length - 1] === '=') {
        bufferLength--
        if (base64[base64.length - 2] === '=') {
          bufferLength--
        }
      }
      var arraybuffer = new ArrayBuffer(bufferLength),
        bytes = new Uint8Array(arraybuffer)
      for (j = 0; j < len; j += 4) {
        encoded1 = lookup[base64.charCodeAt(j)]
        encoded2 = lookup[base64.charCodeAt(j + 1)]
        encoded3 = lookup[base64.charCodeAt(j + 2)]
        encoded4 = lookup[base64.charCodeAt(j + 3)]
        bytes[p++] = (encoded1 << 2) | (encoded2 >> 4)
        bytes[p++] = ((encoded2 & 15) << 4) | (encoded3 >> 2)
        bytes[p++] = ((encoded3 & 3) << 6) | (encoded4 & 63)
      }
      return arraybuffer
    },
  },
}
</script>
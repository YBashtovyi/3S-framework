/* eslint-disable no-unused-vars */
var URL_GET_CERTIFICATES = process.env.STATICS_URI + '/CACertificates.p7b?version=1.0.17'
var URL_CAS = process.env.STATICS_URI + '/CAs.json?version=1.0.17'
var URL_XML_HTTP_PROXY_SERVICE = process.env.API + '/DigitalSignature'
//= ============================================================================

var SubjectCertTypes = [
  { 'type': window.EU_SUBJECT_TYPE_UNDIFFERENCED, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_UNDIFFERENCED },
  { 'type': window.EU_SUBJECT_TYPE_CA, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_UNDIFFERENCED },
  { 'type': window.EU_SUBJECT_TYPE_CA_SERVER, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_UNDIFFERENCED },
  { 'type': window.EU_SUBJECT_TYPE_CA_SERVER, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_CMP },
  { 'type': window.EU_SUBJECT_TYPE_CA_SERVER, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_OCSP },
  { 'type': window.EU_SUBJECT_TYPE_CA_SERVER, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_TSP },
  { 'type': window.EU_SUBJECT_TYPE_END_USER, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_UNDIFFERENCED },
  { 'type': window.EU_SUBJECT_TYPE_RA_ADMINISTRATOR, 'subtype': window.EU_SUBJECT_CA_SERVER_SUB_TYPE_UNDIFFERENCED }
]

var CertKeyTypes = [
  window.EU_CERT_KEY_TYPE_UNKNOWN,
  window.EU_CERT_KEY_TYPE_DSTU4145,
  window.EU_CERT_KEY_TYPE_RSA
]

var KeyUsages = [
  window.EU_KEY_USAGE_UNKNOWN,
  window.EU_KEY_USAGE_DIGITAL_SIGNATURE,
  window.EU_KEY_USAGE_KEY_AGREEMENT
]
//= ============================================================================

var EUSignCPTest = window.NewClass({
  'Vendor': 'JSC IIT',
  'ClassVersion': '1.0.0',
  'ClassName': 'EUSignCPTest',
  'CertsLocalStorageName': 'Certificates',
  'CRLsLocalStorageName': 'CRLs',
  'recepientsCertsIssuers': null,
  'recepientsCertsSerials': null,
  'PrivateKeyNameSessionStorageName': 'PrivateKeyName',
  'PrivateKeySessionStorageName': 'PrivateKey',
  'PrivateKeyPasswordSessionStorageName': 'PrivateKeyPassword',
  'PrivateKeyCertificatesSessionStorageName': 'PrivateKeyCertificates',
  'PrivateKeyCertificatesChainSessionStorageName': 'PrivateKeyCertificatesChain',
  'CACertificatesSessionStorageName': 'CACertificates',
  'CAServerIndexSessionStorageName': 'CAServerIndex',
  'CAsServers': null,
  'CAServer': null,
  'offline': false,
  'useCMP': false,
  'loadPKCertsFromFile': false,
  'privateKeyCerts': null
},
function () {
},
{
  initialize: function (callback) {
    var _onSuccess = function () {
      try {
        euSign.Initialize()
        euSign.SetJavaStringCompliant(true)
        euSign.SetCharset('UTF-16LE')

        if (euSign.DoesNeedSetSettings()) {
          euSignTest.setDefaultSettings()
        }
        euSignTest.loadCertsFromServer()
        euSignTest.setCASettings(0)
        if (utils.IsSessionStorageSupported()) {
          var _readPrivateKeyAsStoredFile = function () {
            euSignTest.readPrivateKeyAsStoredFile()
          }
          setTimeout(_readPrivateKeyAsStoredFile, 10)
        }
        euSignTest.updateCertList()
        // set eusing server
        if (euSignTest && euSignTest.CAServer) {
          callback(euSignTest.CAServer)
        }
      } catch (e) {
        setStatus('не ініціалізовано')
        alert(e)
      }
    }
    var _onError = function () {
      setStatus('Не ініціалізовано')
      alert('Виникла помилка при завантаженні криптографічної бібліотеки')
    }
    euSignTest.loadCAsSettings(_onSuccess, _onError)
  },
  loadCAsSettings: function (onSuccess, onError) {
    var pwindow = window

    var _onSuccess = function (casResponse) {
      try {
        var servers = JSON.parse(casResponse.replace(/\\'/g, "'"))
        servers.unshift({
          address: 'auto',
          issuerCNs: ['Визначити автоматично'],
          cmpAddress: '',
          tspAddress: '',
          tspAddressPort: '80',
          ocspAccessPointAddress: '',
          ocspAccessPointPort: ''
        })
        pwindow.CAsServers = servers
        onSuccess()
      } catch (e) {
        onError()
      }
    }

    euSign.LoadDataFromServer(URL_CAS, _onSuccess, onError, false)
  },
  loadCertsFromServer: function () {

    var certificates = utils.GetSessionStorageItem(
      euSignTest.CACertificatesSessionStorageName, true, false)
    if (certificates != null) {
      try {
        euSign.SaveCertificates(certificates)
        euSignTest.updateCertList()
        return
      } catch (e) {
        alert('Виникла помилка при імпорті завантажених з сервера сертифікатів до файлового сховища')
      }
    }
    var _onSuccess = function (certificates) {
      try {
        euSign.SaveCertificates(certificates)
        utils.SetSessionStorageItem(
          euSignTest.CACertificatesSessionStorageName,
          certificates, false)
        euSignTest.updateCertList()
      } catch (e) {
        alert('Виникла помилка при імпорті завантажених з сервера сертифікатів до файлового сховища')
      }
    }
    var _onFail = function (errorCode) {
    }
    utils.GetDataFromServerAsync(URL_GET_CERTIFICATES, _onSuccess, _onFail, true)
  },
  setDefaultSettings: function () {
    try {
      euSign.SetXMLHTTPProxyService(URL_XML_HTTP_PROXY_SERVICE)

      var settings = euSign.CreateFileStoreSettings()
      settings.SetPath('/certificates')
      settings.SetSaveLoadedCerts(true)
      euSign.SetFileStoreSettings(settings)

      settings = euSign.CreateProxySettings()
      euSign.SetProxySettings(settings)

      settings = euSign.CreateTSPSettings()
      euSign.SetTSPSettings(settings)

      settings = euSign.CreateOCSPSettings()
      euSign.SetOCSPSettings(settings)

      settings = euSign.CreateCMPSettings()
      euSign.SetCMPSettings(settings)

      settings = euSign.CreateLDAPSettings()
      euSign.SetLDAPSettings(settings)

      settings = euSign.CreateOCSPAccessInfoModeSettings()
      settings.SetEnabled(true)
      euSign.SetOCSPAccessInfoModeSettings(settings)

      var CAs = window.CAsServers
      settings = euSign.CreateOCSPAccessInfoSettings()
      for (var i = 0; i < CAs.length; i++) {
        settings.SetAddress(CAs[i].ocspAccessPointAddress)
        settings.SetPort(CAs[i].ocspAccessPointPort)

        for (var j = 0; j < CAs[i].issuerCNs.length; j++) {
          settings.SetIssuerCN(CAs[i].issuerCNs[j])
          euSign.SetOCSPAccessInfoSettings(settings)
        }
      }
    } catch (e) {
      alert('Виникла помилка при встановленні налашувань: ' + e)
    }
  },
  setCASettings: function (caIndex) {
    try {

      var caServer = (caIndex < window.CAsServers.length)
        ? window.CAsServers[caIndex] : null
      var offline = !!(((caServer === null) ||
                    (caServer.address === '')))
      var useCMP = (!offline && (caServer.cmpAddress !== ''))
      var loadPKCertsFromFile = (caServer === null) ||
                    (!useCMP && !caServer.certsInKey)

      euSignTest.CAServer = caServer
      euSignTest.offline = offline
      euSignTest.useCMP = useCMP
      euSignTest.loadPKCertsFromFile = loadPKCertsFromFile

      var settings

      euSignTest.clearPrivateKeyCertificatesList()

      settings = euSign.CreateTSPSettings()
      if (!offline) {
        settings.SetGetStamps(true)
        if (caServer.tspAddress !== '') {
          settings.SetAddress(caServer.tspAddress)
          settings.SetPort(caServer.tspAddressPort)
        } else {
          settings.SetAddress('acskidd.gov.ua')
          settings.SetPort('80')
        }
      }
      euSign.SetTSPSettings(settings)

      settings = euSign.CreateOCSPSettings()
      if (!offline) {
        settings.SetUseOCSP(true)
        settings.SetBeforeStore(true)
        settings.SetAddress(caServer.ocspAccessPointAddress)
        settings.SetPort(caServer.ocspAccessPointPort)
      }
      euSign.SetOCSPSettings(settings)

      settings = euSign.CreateCMPSettings()
      settings.SetUseCMP(useCMP)
      if (useCMP) {
        settings.SetAddress(caServer.cmpAddress)
        settings.SetPort('80')
      }
      euSign.SetCMPSettings(settings)

      settings = euSign.CreateLDAPSettings()
      euSign.SetLDAPSettings(settings)
    } catch (e) {
      alert('Виникла помилка при встановленні налашувань: ' + e)
    }
  },
  // -----------------------------------------------------------------------------
  chooseCertsAndCRLs: function (event) {
    var files = event.target.files
    var certsFiles = []
    var crlsFiles = []

    if (utils.IsStorageSupported()) {
      utils.ClearFolder(euSignTest.CertsLocalStorageName)
      utils.ClearFolder(euSignTest.CRLsLocalStorageName)
    }

    for (var i = 0; i < files.length; i++) {
      var file = files[i]
      if (euSignTest.isCertificateExtension(file.name)) { certsFiles.push(file) } else if (euSignTest.isCRLExtension(file.name)) { crlsFiles.push(file) } else { continue }

      var fileReader = new FileReader()
      fileReader.onloadend = (function (fileName) {
        return function (evt) {
          if (evt.target.readyState === FileReader.DONE) {
            euSignTest.saveFileToModuleFileStorage(fileName,
              evt.target.result)
          }
        }
      })(file.name)

      fileReader.readAsArrayBuffer(file)
    }

    if (certsFiles.length > 0) {
      euSignTest.setFileItemsToList('SelectedCertsList', certsFiles)
    } else {
      document.getElementById('SelectedCertsList').innerHTML =
                    'Не обрано жодного сертифіката'
    }

    if (crlsFiles.length > 0) {
      euSignTest.setFileItemsToList('SelectedCRLsList', crlsFiles)
    } else {
      document.getElementById('SelectedCRLsList').innerHTML =
                    'Не обрано жодного СВС'
    }
  },
  updateCertList: function () {
    var certSubjType = SubjectCertTypes[5]
    var certKeyType = CertKeyTypes[0]
    var keyUsage = KeyUsages[0]

    try {
      var index = 0
      var cert
      var certs = []

      while (true) {
        cert = euSign.EnumCertificatesEx(
          certSubjType.type, certSubjType.subtype,
          certKeyType, keyUsage, index)
        if (cert == null) { break }

        certs.push(cert)
        index++
      };

      if (certs.length === 0) {
        return
      }

      var _makeCertField = function (name, value, addNewLine) {
        return name + ': ' +
                        value +
                        (addNewLine ? '<br>' : '')
      }

      var certInfos = []

      for (var i = 0; i < certs.length; i++) {
        var certInfoStr = ''
        var certInfo = certs[i].GetInfoEx()
        var publicKeyType = ''
        switch (certInfo.GetPublicKeyType()) {
          case window.EU_CERT_KEY_TYPE_DSTU4145:
            publicKeyType += 'ДСТУ-4145'
            break
          case window.EU_CERT_KEY_TYPE_RSA:
            publicKeyType += 'RSA'
            break
          default:
            publicKeyType = 'Невизначено'
            break
        }

        certInfoStr += _makeCertField('Власник', certInfo.GetSubjCN(), true)
        certInfoStr += _makeCertField('ЦСК', certInfo.GetIssuerCN(), true)
        certInfoStr += _makeCertField('Серійний номер', certInfo.GetSerial(), true)
        certInfoStr += _makeCertField('Тип', publicKeyType, true)
        certInfoStr += _makeCertField('Призначення', certInfo.GetKeyUsage(), false)

        certInfos.push(certInfoStr)
      }
    } catch (e) {
      alert('Виникла помилка при отриманні сертифікатів з файлового сховища: ' + e)
    }
  },
  // -----------------------------------------------------------------------------
  getCAServer: function () {
    return euSignTest.CAServer
  },
  storeCAServer: function () {
    var index = window.CAsServers.findIndex(x => x.address === euSignTest.CAServer.address)
    // var index = document.getElementById('CAsServersSelect').selectedIndex
    return utils.SetSessionStorageItem(
      euSignTest.CAServerIndexSessionStorageName, index.toString(), false)
  },
  removeCAServer: function () {
    utils.RemoveSessionStorageItem(
      euSignTest.CAServerIndexSessionStorageName)
  },
  // -----------------------------------------------------------------------------
  storePrivateKey: function (keyName, key, password, certificates) {
    if (!utils.SetSessionStorageItem(
      euSignTest.PrivateKeyNameSessionStorageName, keyName, false) ||
                !utils.SetSessionStorageItem(
                  euSignTest.PrivateKeySessionStorageName, key, false) ||
                !utils.SetSessionStorageItem(
                  euSignTest.PrivateKeyPasswordSessionStorageName, password, true) ||
                !euSignTest.storeCAServer()) {
      return false
    }

    if (Array.isArray(certificates)) {
      if (!utils.SetSessionStorageItems(
        euSignTest.PrivateKeyCertificatesSessionStorageName,
        certificates, false)) {
        return false
      }
    } else {
      if (!utils.SetSessionStorageItem(
        euSignTest.PrivateKeyCertificatesChainSessionStorageName,
        certificates, false)) {
        return false
      }
    }

    return true
  },
  removeStoredPrivateKey: function () {
    utils.RemoveSessionStorageItem(
      euSignTest.PrivateKeyNameSessionStorageName)
    utils.RemoveSessionStorageItem(
      euSignTest.PrivateKeySessionStorageName)
    utils.RemoveSessionStorageItem(
      euSignTest.PrivateKeyPasswordSessionStorageName)
    utils.RemoveSessionStorageItem(
      euSignTest.PrivateKeyCertificatesChainSessionStorageName)
    utils.RemoveSessionStorageItem(
      euSignTest.PrivateKeyCertificatesSessionStorageName)

    euSignTest.removeCAServer()
  },
  // -----------------------------------------------------------------------------
  selectPrivateKeyFile: function (event) {
    var enable = (event.target.files.length === 1)
    document.getElementById('PKeyPassword').disabled =
                enable ? '' : 'disabled'
    document.getElementById('PKeyFileName').value =
                enable ? event.target.files[0].name : ''
    document.getElementById('PKeyPassword').value = ''
  },
  // -----------------------------------------------------------------------------
  getPrivateKeyCertificatesByCMP: function (key, password, onSuccess, onError) {
    try {
      // var cmpAddress = euSignTest.getCAServer().cmpAddress + ':80'
      var cmpAddresses = window.CAsServers.filter(p => p.cmpAddress !== '').map(p => p.cmpAddress + ':80')
      var keyInfo = euSign.GetKeyInfoBinary(key, password)
      onSuccess(euSign.GetCertificatesByKeyInfo(keyInfo, cmpAddresses))
    } catch (e) {
      onError(e)
    }
  },
  getPrivateKeySelectCertificatesByCMP: function (key, password, onSuccess, onError) {
    try {
      var cmpAddress = euSignTest.getCAServer().cmpAddress + ':80'
      var keyInfo = euSign.GetKeyInfoBinary(key, password)
      onSuccess(euSign.GetCertificatesByKeyInfo(keyInfo, [cmpAddress]))
    } catch (e) {
      onError(e)
    }
  },
  getPrivateKeyCertificates: function (key, password, fromCache, onSuccess, onError) {
    var certificates

    if (euSignTest.CAServer != null &&
                euSignTest.CAServer.certsInKey) {
      onSuccess([])
      return
    }

    if (fromCache) {
      if (euSignTest.useCMP) {
        certificates = utils.GetSessionStorageItem(
          euSignTest.PrivateKeyCertificatesChainSessionStorageName, true, false)
      } else if (euSignTest.loadPKCertsFromFile) {
        certificates = utils.GetSessionStorageItems(
          euSignTest.PrivateKeyCertificatesSessionStorageName, true, false)
      }

      onSuccess(certificates)
    } else if (euSignTest.useCMP) {
      euSignTest.getPrivateKeySelectCertificatesByCMP(
        key, password, onSuccess, onError)
    } else if (euSignTest.loadPKCertsFromFile) {
      euSignTest.getPrivateKeyCertificatesByCMP(
        key, password, onSuccess, onError)
      // var _onSuccess = function (files) {
      //   var certificates = []
      //   debugger
      //   for (var i = 0; i < files.length; i++) {
      //     certificates.push(files[i].data)
      //   }

      //   onSuccess(certificates)
      // }
      // debugger
      // euSign.ReadFiles(
      //   euSignTest.privateKeyCerts,
      //   _onSuccess, onError)
    }
  },
  readPrivateKey: function (keyName, key, password, certificates, fromCache, callback) {
    var _onError = function (e) {
      if (fromCache) {
        euSignTest.removeStoredPrivateKey()
        // euSignTest.privateKeyReaded(false)
      } else {
        if (callback) {
          callback(e)
        } else {
          alert(e)
        }
      }
    }

    if (certificates == null) {
      var _onGetCertificates = function (certs) {
        if (certs == null) {
          _onError(euSign.MakeError(window.EU_ERROR_CERT_NOT_FOUND))
          return
        }
        euSignTest.readPrivateKey(keyName, key, password, certs, fromCache, callback)
      }

      euSignTest.getPrivateKeyCertificates(
        key, password, fromCache, _onGetCertificates, _onError)
      return
    }

    try {
      if (Array.isArray(certificates)) {
        for (var i = 0; i < certificates.length; i++) {
          euSign.SaveCertificate(certificates[i])
        }
      } else {
        euSign.SaveCertificates(certificates)
      }
      euSign.ReadPrivateKeyBinary(key, password)

      if (!fromCache && utils.IsSessionStorageSupported()) {
        if (!euSignTest.storePrivateKey(
          keyName, key, password, certificates)) {
          euSignTest.removeStoredPrivateKey()
        }
      }

      // euSignTest.privateKeyReaded(true)

      euSignTest.updateCertList()
      if (callback) {
        callback()
      }
    } catch (e) {
      _onError(e)
    }
  },
  readPrivateKeyAsImage: function (file, onSuccess, onError) {
    var image = new Image()
    image.onload = function () {
      try {
        var qr = new window.QRCodeDecode()

        var canvas = document.createElement('canvas')
        var context = canvas.getContext('2d')

        canvas.width = image.width
        canvas.height = image.height

        context.drawImage(image, 0, 0, canvas.width, canvas.height)
        var imagedata = context.getImageData(0, 0, canvas.width, canvas.height)
        var decoded = qr.decodeImageData(imagedata, canvas.width, canvas.height)
        var arr = []
        for (var i = 0; i < decoded.length; i++) { arr.push(decoded.charCodeAt(i)) }
        onSuccess(file.name, arr)
      } catch (e) {
        onError()
      }
    }

    image.src = utils.CreateObjectURL(file)
  },
  readPrivateKeyAsStoredFile: function () {
    // remove from key info session storage
    euSignTest.removeStoredPrivateKey()

    var keyName = utils.GetSessionStorageItem(
      euSignTest.PrivateKeyNameSessionStorageName, false, false)
    var key = utils.GetSessionStorageItem(
      euSignTest.PrivateKeySessionStorageName, true, false)
    var password = utils.GetSessionStorageItem(
      euSignTest.PrivateKeyPasswordSessionStorageName, false, true)
    if (keyName == null || key == null || password == null) { }
  },
  readKey: function (key, password, callback) {
    var _onError = function (e) {
      alert(e)
    }
    var _onSuccess = function (keyName, key) {
      euSignTest.readPrivateKey(keyName, new Uint8Array(key), password, null, false, callback)
    }
    try {
      var res = true
      // document.getElementById('PKeyReadButton').title === 'Зчитати'
      if (res) {
        if (password === '') {
          _onError('Виникла помилка при зчитуванні особистого ключа. Опис помилки: не вказано пароль доступу до особистого ключа')
          return
        }
        if(euSignTest.loadPKCertsFromFile) {
          euSignTest.privateKeyCerts = [key]
          window.privateKeyCerts = [key]
        }
        if (euSignTest.loadPKCertsFromFile && (window.privateKeyCerts == null || window.privateKeyCerts.length <= 0)) {
          _onError('Виникла помилка при зчитуванні особистого ключа. Опис помилки: не обрано жодного сертифіката відкритого ключа')
          return
        }
        if (utils.IsFileImage(key)) {
          euSignTest.readPrivateKeyAsImage(key, _onSuccess, _onError)
        } else {
          var _onFileRead = function (readedFile) {
            _onSuccess(readedFile.file.name, readedFile.data)
          }
          euSign.ReadFile(key, _onFileRead, _onError)
        }
      }
    } catch (e) {
      _onError(e)
    }
  },

  readPrivateKeyButtonClick: function () {
    var passwordTextField = document.getElementById('PKeyPassword')
    // var certificatesFiles = euSignTest.privateKeyCerts;

    var _onError = function (e) {
      alert(e)
    }

    var _onSuccess = function (keyName, key) {
      euSignTest.readPrivateKey(keyName, new Uint8Array(key),
        passwordTextField.value, null, false)
    }

    try {
      if (document.getElementById('PKeyReadButton').title === 'Зчитати') {
        // setStatus('зчитування ключа');

        var files = document.getElementById('PKeyFileInput').files

        if (files.length !== 1) {
          _onError('Виникла помилка при зчитуванні особистого ключа. Опис помилки: файл з особистим ключем не обрано')
          return
        }

        if (passwordTextField.value === '') {
          passwordTextField.focus()
          _onError('Виникла помилка при зчитуванні особистого ключа. Опис помилки: не вказано пароль доступу до особистого ключа')
          return
        }

        if (euSignTest.loadPKCertsFromFile &&
                        (window.certificatesFiles == null ||
                            window.certificatesFiles.length <= 0)) {
          _onError('Виникла помилка при зчитуванні особистого ключа. ' +
                            'Опис помилки: не обрано жодного сертифіката відкритого ключа')
          return
        }

        if (utils.IsFileImage(files[0])) {
          euSignTest.readPrivateKeyAsImage(files[0], _onSuccess, _onError)
        } else {
          var _onFileRead = function (readedFile) {
            _onSuccess(readedFile.file.name, readedFile.data)
          }

          euSign.ReadFile(files[0], _onFileRead, _onError)
        }
      } else {
        euSignTest.removeStoredPrivateKey()
        euSign.ResetPrivateKey()
        passwordTextField.value = ''
        euSignTest.clearPrivateKeyCertificatesList()
      }
    } catch (e) {
      _onError(e)
    }
  },
  showOwnCertificates: function () {
    try {
      var splitLine = '--------------------------------------------------'
      var message = 'Інформація про сертифікат(и) користувача:\n'
      var i = 0
      while (true) {
        var info = euSign.EnumOwnCertificates(i)
        if (info == null) { break }

        var isNationalAlgs = (info.GetPublicKeyType() === window.EU_CERT_KEY_TYPE_DSTU4145)

        message += splitLine + '\n'
        message += 'Сертифікат № ' + (i + 1) + '\n' +
                        'Власник: ' + info.GetSubjCN() + '\n' +
                        'ЦСК: ' + info.GetIssuerCN() + '\n' +
                        'Серійний номер: ' + info.GetSerial() + '\n' +
                        'Призначення: ' + info.GetKeyUsage() +
                        (isNationalAlgs ? ' в державних ' : ' в міжнародних ') +
                        'алгоритмах та протоколах' + '\n'
        message += splitLine + '\n'

        i++
      }

      if (i === 0) { message += 'Відсутня' }

      alert(message)
    } catch (e) {
      alert(e)
    }
  },
  blockOwnCertificates: function () {
    if (!confirm('Після блокування сертифікатів ос. ключа ' +
                'їх розблокування можливе лише при особистому ' +
                'зверненні до АЦСК. Продовжити?')) {
      return
    }

    try {
      euSign.ChangeOwnCertificatesStatus(window.EU_CCS_TYPE_HOLD, window.EU_REVOCATION_REASON_UNKNOWN)
      alert('Сертифікати ос. ключа успішно заблоковано')
    } catch (e) {
      alert(e)
    }
  },
  revokeOwnCertificates: function () {
    if (!confirm('Після скасування сертифікатів ос. ключа ' +
                'використання ос. ключа буде не можливе. Продовжити?')) {
      return
    }

    try {
      var revocationReason = parseInt(
        document.getElementById('PKeyRevokationReasonSelect').value)

      euSign.ChangeOwnCertificatesStatus(
        window.EU_CCS_TYPE_REVOKE, revocationReason)
      alert('Сертифікати ос. ключа успішно скасовано')
    } catch (e) {
      alert(e)
    }
  },
  // -----------------------------------------------------------------------------
  changePrivKeyType: function () {
    var useUA = document.getElementById('ChooseKeysUARadioBtn').checked
    var useRSA = document.getElementById('ChooseKeysRSARadioBtn').checked

    if (document.getElementById('ChooseKeysUARSARadioBtn').checked) { useUA = useRSA = true }

    document.getElementById('UAPrivKeyParams').style.display =
                useUA ? 'block' : 'none'
    document.getElementById('InternationalPrivKeyParams').style.display =
                useRSA ? 'block' : 'none'
  },
  generatePK: function () {
    var pkPassword = document.getElementById('PGenKeyPassword').value

    if (pkPassword === '') {
      alert('Пароль особистого ключа не вказано')
      document.getElementById('PGenKeyPassword').focus()
      return
    }

    var useUA = document.getElementById('ChooseKeysUARadioBtn').checked
    var useRSA = document.getElementById('ChooseKeysRSARadioBtn').checked

    if (document.getElementById('ChooseKeysUARSARadioBtn').checked) { useUA = useRSA = true }

    var uaKeysType = useUA
      ? window.EU_KEYS_TYPE_DSTU_AND_ECDH_WITH_GOST : window.EU_KEYS_TYPE_NONE
    var uaDSKeysSpec = useUA
      ? parseInt(document.getElementById('UAKeySpecSelect').value) : 0
    var uaKEPSpec = useUA
      ? parseInt(document.getElementById('UAKEPKeySpecSelect').value) : 0

    var intKeysType = useRSA
      ? window.EU_KEYS_TYPE_RSA_WITH_SHA : window.EU_KEYS_TYPE_NONE

    var intKeysSpec = useRSA
      ? parseInt(document.getElementById('InternationalKeySpecSelect').value) : 0

    var userInfo = window.EndUserInfo()
    userInfo.commonName = 'User 1'
    userInfo.locality = 'Kharkov'
    userInfo.state = 'Kharkovska'

    var _generatePKFunction = function () {
      try {
        euSign.SetRuntimeParameter(
          window.EU_MAKE_PKEY_PFX_CONTAINER_PARAMETER,
          document.getElementById('PKPFXContainerCheckbox').checked)

        var privKey = euSign.GeneratePrivateKey(
          pkPassword, uaKeysType, uaDSKeysSpec, false, uaKEPSpec,
          intKeysType, intKeysSpec, null, null)

        saveFile(privKey.privateKeyName, privKey.privateKey)
        /* saveFile(privKey.privateKeyInfoName, privKey.privateKeyInfo); */

        if (useUA) {
          saveFile(privKey.uaRequestName, privKey.uaRequest)
          saveFile(privKey.uaKEPRequestName, privKey.uaKEPRequest)
        }

        if (useRSA) {
          saveFile(privKey.internationalRequestName,
            privKey.internationalRequest)
        }
      } catch (e) {
        alert('Виникла помилка при генерації особистого ключа. ' +
                        'Опис помилки: ' + e)
      }
    }

    setStatus('генерація ключа')
    setTimeout(_generatePKFunction, 10)
  },
  // -----------------------------------------------------------------------------
  base64ToBytaArray: function (base64) {
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'

    // Use a lookup table to find the index.
    var lookup = new Uint8Array(256)
    for (var i = 0; i < chars.length; i++) {
      lookup[chars.charCodeAt(i)] = i
    }
    var bufferLength = base64.length * 0.75, len = base64.length, p = 0, encoded1, encoded2, encoded3, encoded4
    var i1 = 0

    if (base64[base64.length - 1] === '=') {
      bufferLength--
      if (base64[base64.length - 2] === '=') {
        bufferLength--
      }
    }

    var arraybuffer = new ArrayBuffer(bufferLength),
      bytes = new Uint8Array(arraybuffer)

    for (i1 = 0; i1 < len; i1 += 4) {
      encoded1 = lookup[base64.charCodeAt(i1)]
      encoded2 = lookup[base64.charCodeAt(i1 + 1)]
      encoded3 = lookup[base64.charCodeAt(i1 + 2)]
      encoded4 = lookup[base64.charCodeAt(i1 + 3)]

      bytes[p++] = (encoded1 << 2) | (encoded2 >> 4)
      bytes[p++] = ((encoded2 & 15) << 4) | (encoded3 >> 2)
      bytes[p++] = ((encoded3 & 3) << 6) | (encoded4 & 63)
    }

    return arraybuffer
  },
  signDataNew: function (data) {
    return euSign.SignDataInternal(true, data, true)
  },

  b64EncodeUnicode: function (str) {
    // first we use encodeURIComponent to get percent-encoded UTF-8,
    // then we convert the percent encodings into raw bytes which
    // can be fed into btoa.
    return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g,
      function toSolidBytes (match, p1) {
        return String.fromCharCode('0x' + p1)
      })
    )
  },

  signData: function (urlActionSave) {
    var signedFileModel = {}
    signedFileModel.id = FileModel.id
    signedFileModel.files = []
    var _signDataFunction = function () {
      FileModel.files.forEach(function (item, i, files) {
        try {
          var sign = ''
          if (item.id === '00000000-0000-0000-0000-000000000000') {
            if (item.isSystemFile) { item.file = '@@startFile@@' + item.file + '@@endFile@@' } else {
              item.file = euSign.Base64Decode(item.file)
            }
            sign = euSign.SignDataInternal(true, item.file, true)
          } else {
            sign = euSign.SignData(item.file, true)
          }
          var obj = Object.create(item)
          obj.file = sign
          signedFileModel.files.push(obj)
          var statusName = 'підпис данних  ' + obj.name
          setStatus(statusName)
        } catch (e) {
          alert(e)
        }
      })
      window.$.ajax({
        url: urlActionSave,
        type: 'POST',
        dataType: 'json',
        data: signedFileModel,
        success: function (data) {
          if (data.success === false) { alert(data.errorMessage) }
          window.location.replace(data.returnUrl)
        }

      })
    }

    setStatus('підпис данних')
    setTimeout(_signDataFunction, 10)
  },
  verifyData: function () {
    var data = document.getElementById('DataToSignTextEdit').value + ''
    var signedData = document.getElementById('SignedDataText').value
    var isInternalSign =
                document.getElementById('InternalSignCheckbox').checked
    var isSignHash =
                document.getElementById('SignHashCheckbox').checked
    var isGetSignerInfo =
                document.getElementById('GetSignInfoCheckbox').checked
    var verifiedDataText = document.getElementById('VerifiedDataText')
    var dsAlgType = parseInt(document.getElementById('DSAlgTypeSelect').value)
    verifiedDataText.value = ''

    var _verifyDataFunction = function () {
      try {
        var info = ''
        if (isInternalSign) {
          info = euSign.VerifyDataInternal(signedData)
        } else {
          if (isSignHash && dsAlgType === 1) {
            var hash = euSign.HashData(data)
            info = euSign.VerifyHash(hash, signedData)
          } else {
            info = euSign.VerifyData(data, signedData)
          }
        }

        var message = 'Підпис успішно перевірено'

        if (isGetSignerInfo) {
          var ownerInfo = info.GetOwnerInfo()
          var timeInfo = info.GetTimeInfo()

          message += '\n'
          message += 'Підписувач: ' + ownerInfo.GetSubjCN() + '\n' +
                            'ЦСК: ' + ownerInfo.GetIssuerCN() + '\n' +
                            'Серійний номер: ' + ownerInfo.GetSerial() + '\n'
          if (timeInfo.IsTimeAvail()) {
            message += (timeInfo.IsTimeStamp()
              ? 'Мітка часу:' : 'Час підпису: ') + timeInfo.GetTime()
          } else {
            message += 'Час підпису відсутній'
          }
        }

        if (isInternalSign) {
          message += '\n'
          verifiedDataText.value = euSign.ArrayToString(info.GetData())
          message += 'Підписані дані: ' + verifiedDataText.value + '\n'
        }
        alert(message)
      } catch (e) {
        alert(e)
      }
    }

    setStatus('перевірка підпису даних')
    setTimeout(_verifyDataFunction, 10)
  },
  // -----------------------------------------------------------------------------
  signFile: function () {
    var file = document.getElementById('FileToSign').files[0]

    if (file.size > window.Module.MAX_DATA_SIZE) {
      alert('Розмір файлу для піпису занадто великий. Оберіть файл меншого розміру')
      return
    }

    var fileReader = new FileReader()

    fileReader.onloadend = (function (fileName) {
      return function (evt) {
        if (evt.target.readyState !== FileReader.DONE) { return }

        var isInternalSign =
                        document.getElementById('InternalSignCheckbox').checked
        var isAddCert = document.getElementById(
          'AddCertToInternalSignCheckbox').checked
        var dsAlgType = parseInt(
          document.getElementById('DSAlgTypeSelect').value)

        var data = new Uint8Array(evt.target.result)

        try {
          var sign

          if (dsAlgType === 1) {
            if (isInternalSign) { sign = euSign.SignDataInternal(isAddCert, data, false) } else { sign = euSign.SignData(data, false) }
          } else {
            sign = euSign.SignDataRSA(data, isAddCert,
              !isInternalSign, false)
          }

          saveFile(fileName + '.p7s', sign)
          alert('Файл успішно підписано')
        } catch (e) {
          alert(e)
        }
      }
    })(file.name)

    setStatus('підпис файлу')
    fileReader.readAsArrayBuffer(file)
  },
  verifyFile: function () {
    var isInternalSign =
                document.getElementById('InternalSignCheckbox').checked
    var isGetSignerInfo =
                document.getElementById('GetSignInfoCheckbox').checked
    var files = []

    files.push(document.getElementById('FileToVerify').files[0])
    if (!isInternalSign) { files.push(document.getElementById('FileWithSign').files[0]) }

    if ((files[0].size > (window.Module.MAX_DATA_SIZE + window.EU_MAX_P7S_CONTAINER_SIZE)) ||
                (!isInternalSign && (files[1].size > window.Module.MAX_DATA_SIZE))) {
      alert('Розмір файлу для перевірки підпису занадто великий. Оберіть файл меншого розміру')
      return
    }

    var _onSuccess = function (files) {
      try {
        var info = ''
        if (isInternalSign) {
          info = euSign.VerifyDataInternal(files[0].data)
        } else {
          info = euSign.VerifyData(files[0].data, files[1].data)
        }

        var message = 'Підпис успішно перевірено'

        if (isGetSignerInfo) {
          var ownerInfo = info.GetOwnerInfo()
          var timeInfo = info.GetTimeInfo()

          message += '\n'
          message += 'Підписувач: ' + ownerInfo.GetSubjCN() + '\n' +
                            'ЦСК: ' + ownerInfo.GetIssuerCN() + '\n' +
                            'Серійний номер: ' + ownerInfo.GetSerial() + '\n'
          if (timeInfo.IsTimeAvail()) {
            message += (timeInfo.IsTimeStamp()
              ? 'Мітка часу:' : 'Час підпису: ') + timeInfo.GetTime()
          } else {
            message += 'Час підпису відсутній'
          }
        }

        if (isInternalSign) {
          saveFile(files[0].name.substring(0,
            files[0].name.length - 4), info.GetData())
        }

        alert(message)
      } catch (e) {
        alert(e)
      }
    }

    var _onFail = function (files) {
      alert('Виникла помилка при зчитуванні файлів для перевірки підпису')
    }

    setStatus('перевірка підпису файлів')
    utils.LoadFilesToArray(files, _onSuccess, _onFail)
  },
  // -----------------------------------------------------------------------------
  envelopData: function () {
    var issuers = euSignTest.recepientsCertsIssuers
    var serials = euSignTest.recepientsCertsSerials

    if (issuers == null || serials == null ||
                issuers.length <= 0 || serials.length <= 0) {
      alert('Не обрано жодного сертифіката отримувача')
      return
    }

    var isAddSign = document.getElementById('AddSignCheckbox').checked
    var data = document.getElementById('DataToEnvelopTextEdit').value
    var envelopedText = document.getElementById('EnvelopedDataText')
    var developedText = document.getElementById('DevelopedDataText')
    var kepAlgType = parseInt(document.getElementById('KEPAlgTypeSelect').value)

    envelopedText.value = ''
    developedText.value = ''

    var _envelopDataFunction = function () {
      try {
        if (kepAlgType === 1) {
          envelopedText.value = euSign.EnvelopDataEx(
            issuers, serials, isAddSign, data, true)
        } else {
          envelopedText.value = euSign.EnvelopDataRSAEx(
            kepAlgType, issuers, serials, isAddSign, data, true)
        }
      } catch (e) {
        alert(e)
      }
    }

    setStatus('зашифрування даних')
    setTimeout(_envelopDataFunction, 10)
  },
  developData: function () {
    var envelopedText = document.getElementById('EnvelopedDataText')
    var developedText = document.getElementById('DevelopedDataText')

    developedText.value = ''

    var _developDataFunction = function () {
      try {
        var info = euSign.DevelopData(envelopedText.value)
        var ownerInfo = info.GetOwnerInfo()
        var timeInfo = info.GetTimeInfo()

        var message = 'Дані успішно розшифровано'
        message += '\n'
        message += 'Відправник: ' + ownerInfo.GetSubjCN() + '\n' +
                        'ЦСК: ' + ownerInfo.GetIssuerCN() + '\n' +
                        'Серійний номер: ' + ownerInfo.GetSerial() + '\n'
        if (timeInfo.IsTimeAvail()) {
          message += (timeInfo.IsTimeStamp()
            ? 'Мітка часу:' : 'Час підпису: ') + timeInfo.GetTime()
        } else {
          message += 'Підпис відсутній'
        }

        developedText.value = euSign.ArrayToString(info.GetData())
        alert(message)
      } catch (e) {
        alert(e)
      }
    }

    setStatus('розшифрування даних')
    setTimeout(_developDataFunction, 10)
  },
  // -----------------------------------------------------------------------------
  envelopFile: function () {
    var issuers = euSignTest.recepientsCertsIssuers
    var serials = euSignTest.recepientsCertsSerials

    if (issuers == null || serials == null ||
                issuers.length <= 0 || serials.length <= 0) {
      alert('Не обрано жодного сертифіката отримувача')
      return
    }

    var file = document.getElementById('EnvelopFiles').files[0]
    var fileReader = new FileReader()

    fileReader.onloadend = (function (fileName) {
      return function (evt) {
        if (evt.target.readyState !== FileReader.DONE) { return }

        var fileData = new Uint8Array(evt.target.result)
        var isAddSign = document.getElementById('AddSignCheckbox').checked
        var kepAlgType = parseInt(document.getElementById('KEPAlgTypeSelect').value)
        var envelopedFileData
        try {
          if (kepAlgType === 1) {
            envelopedFileData = euSign.EnvelopDataEx(
              issuers, serials, isAddSign, fileData, false)
          } else {
            envelopedFileData = euSign.EnvelopDataRSAEx(
              kepAlgType, issuers, serials, isAddSign, fileData, false)
          }
          saveFile(fileName + '.p7e', envelopedFileData)
          alert('Файл успішно зашифровано')
        } catch (e) {
          alert(e)
        }
      }
    })(file.name)

    fileReader.readAsArrayBuffer(file)
  },
  developFile: function () {
    var file = document.getElementById('EnvelopFiles').files[0]
    var fileReader = new FileReader()

    if (file.size > (window.Module.MAX_DATA_SIZE + window.EU_MAX_P7E_CONTAINER_SIZE)) {
      alert('Розмір файлу для розшифрування занадто великий. Оберіть файл меншого розміру')
      return
    }

    fileReader.onloadend = (function (fileName) {
      return function (evt) {
        if (evt.target.readyState !== FileReader.DONE) { return }

        var fileData = new Uint8Array(evt.target.result)

        try {
          var info = euSign.DevelopData(fileData)
          var ownerInfo = info.GetOwnerInfo()
          var timeInfo = info.GetTimeInfo()

          var message = 'Файл успішно розшифровано'
          message += '\n'
          message += 'Відправник: ' + ownerInfo.GetSubjCN() + '\n' +
                            'ЦСК: ' + ownerInfo.GetIssuerCN() + '\n' +
                            'Серійний номер: ' + ownerInfo.GetSerial() + '\n'
          if (timeInfo.IsTimeAvail()) {
            message += (timeInfo.IsTimeStamp()
              ? 'Мітка часу:' : 'Час підпису: ') + timeInfo.GetTime()
          } else {
            message += 'Підпис відсутній'
          }
          alert(message)

          saveFile(fileName.substring(0, fileName.length - 4), info.GetData())
        } catch (e) {
          alert(e)
        }
      }
    })(file.name)
    setStatus('розшифрування файлу')
    fileReader.readAsArrayBuffer(file)
  },
  // -----------------------------------------------------------------------------
  getOwnCertificateInfo: function (keyType, keyUsage) {
    try {
      var index = 0
      while (true) {
        var info = euSign.EnumOwnCertificates(index)
        if (info == null) { return null }

        if ((info.GetPublicKeyType() === keyType) &&
                        ((info.GetKeyUsageType() & keyUsage) === keyUsage)) {
          return info
        }

        index++
      }
    } catch (e) {
      alert(e)
    }

    return null
  },
  getOwnCertificate: function (keyType, keyUsage) {
    try {
      var info = euSignTest.getOwnCertificateInfo(
        keyType, keyUsage)
      if (info == null) { return null }

      return euSign.GetCertificate(
        info.GetIssuer(), info.GetSerial())
    } catch (e) {
      alert(e)
    }

    return null
  },
  recepientCertLoaded: function (files, curIndex, processedFiles) {
    return function (evt) {
      if (evt.target.readyState !== FileReader.DONE) { return }

      var file = {}
      file.name = files[curIndex].name
      file.isCertificate =
                    euSignTest.isCertificateExtension(file.name)
      if (file.isCertificate) {
        file.data = new Uint8Array(evt.target.result)
      }

      processedFiles.push(file)
      curIndex++

      if (curIndex < files.length) {
        var fileReader = new FileReader()
        fileReader.onloadend = euSignTest.recepientCertLoaded(
          files, curIndex, processedFiles)
        fileReader.readAsArrayBuffer(files[curIndex])
        return
      }

      euSignTest.recepientCertsLoaded(processedFiles)
    }
  },
  recepientCertsLoaded: function (processedFiles) {
    var loadedFiles = []
    var issuers = []
    var serials = []

    for (var i = 0; i < processedFiles.length; i++) {
      var file = processedFiles[i]
      var fileInfo = file.name
      if (!file.isCertificate) {
        fileInfo += '<br>(Не завантажено: ' +
"не вірне розширення файлу '.cer')"
      } else {
        try {
          var certInfo = euSign.ParseCertificate(file.data)
          fileInfo += '<br>Власник: ' + certInfo.subjCN + '<br>' +
                            'ЦСК: ' + certInfo.issuerCN + '<br>' +
                            'Серійний номер: ' + certInfo.serial
          issuers.push(certInfo.issuer)
          serials.push(certInfo.serial)
          euSign.SaveCertificate(file.data)
        } catch (e) {
          fileInfo += '<br>(Не завантажено: ' + e.toString() + ')'
        }
      }

      loadedFiles.push(fileInfo)
    }

    euSignTest.setItemsToList(
      'SelectedRecipientsCertsList', loadedFiles)

    euSignTest.recepientsCertsIssuers = issuers
    euSignTest.recepientsCertsSerials = serials
  },
  chooseRecepientsCertificates: function (event) {
    euSignTest.recepientsCertsIssuers = []
    euSignTest.recepientsCertsSerials = []

    var files = event.target.files
    if (files.length <= 0) {
      document.getElementById('SelectedRecipientsCertsList').innerHTML = 'Не обрано жодного сертифіката'
      return
    }
    document.getElementById('SelectedRecipientsCertsList').innerHTML = ''
    var fileReader = new FileReader()
    fileReader.onloadend = euSignTest.recepientCertLoaded(
      files, 0, [])
    fileReader.readAsArrayBuffer(files[0])
  },
  // -----------------------------------------------------------------------------
  loadFilesFromLocalStorage: function (localStorageFolder, loadFunc) {
    if (!utils.IsStorageSupported()) { euSign.RaiseError(window.EU_ERROR_NOT_SUPPORTED) }

    if (utils.IsFolderExists(localStorageFolder)) {
      var files = utils.GetFiles(localStorageFolder)
      for (var i = 0; i < files.length; i++) {
        var file = utils.ReadFile(
          localStorageFolder, files[i])
        loadFunc(files[i], file)
      }
      return files
    } else {
      utils.CreateFolder(localStorageFolder)
      return null
    }
  },
  saveFileToModuleFileStorage: function (fileName, fileData) {
    var filesListName = null
    try {
      var array = new Uint8Array(fileData)
      var folderName = null

      if (fileName.indexOf('.cer') >= 0) {
        filesListName = 'SelectedCertsList'
        euSign.SaveCertificate(array)
        folderName = euSignTest.CertsLocalStorageName
      } else if (fileName.indexOf('.p7b') >= 0) {
        euSign.SaveCertificates(array)
        folderName = euSignTest.CertsLocalStorageName
      } else if (fileName.indexOf('.crl') >= 0) {
        filesListName = 'SelectedCRLsList'
        try {
          euSign.SaveCRL(true, array)
        } catch (e) {
          euSign.SaveCRL(false, array)
        }
        folderName = euSignTest.CRLsLocalStorageName
      }

      if (folderName != null && utils.IsStorageSupported()) {
        utils.WriteFile(folderName, fileName, array)
      }
    } catch (e) {
      if (filesListName != null) {
        var filesList = document.getElementById(
          filesListName).getElementsByTagName('li')
        var filesNames = []
        for (var i = 0; i < filesList.length; i++) {
          var fileNameInList = filesList[i].innerText
          if (fileNameInList === fileName) { fileNameInList += ' (Не завантажено)' }

          filesNames.push(fileNameInList)
        }

        euSignTest.setItemsToList(
          filesListName, filesNames)
      }
      alert(e)
    }

    euSignTest.updateCertList()
  },
  isCertificateExtension: function (fileName) {
    if ((fileName.indexOf('.cer') >= 0) || (fileName.indexOf('.p7b') >= 0)) {
      return true
    }
    return false
  },
  isCRLExtension: function (fileName) {
    if ((fileName.indexOf('.crl') >= 0)) { return true }
    return false
  },
  // -----------------------------------------------------------------------------
  clearPrivateKeyCertificatesList: function () {
    euSignTest.privateKeyCerts = null
  },
  setItemsToList: function (listId, items) {
    var output = []
    for (var i = 0; i < items[i].length; i++) {
      var item = items[i]
      output.push('<li><strong>', item, '</strong></li>')
    }

    document.getElementById(listId).innerHTML =
                '<ul>' + output.join('') + '</ul>'
  },
  setFileItemsToList: function (listId, items) {
    var output = []
    for (var i = 0; i < items[i].length; i++) {
      var item = items[i]
      output.push('<li><strong>', item.name, '</strong></li>')
    }

    document.getElementById(listId).innerHTML =
                '<ul>' + output.join('') + '</ul>'
  }
})

//= ============================================================================
const euSignTest = EUSignCPTest()
var euSign = window.EUSignCP()
var utils = window.Utils(euSign)
var FileModel = ''
//= ============================================================================
function setStatus (message) {
  if (message !== '') { message = '(' + message + '...)' }
}
function saveFile (fileName, array) {
  var blob = new Blob([array], { type: 'application/octet-stream' })
  window.saveAs(blob, fileName)
}
// IIT plugin for Vue pages
const IITPlugin = function () {
  function _initialize (callback) {
    euSignTest.initialize(callback)
  }
  function _getCaServers () {
    return window.CAsServers
  }
  function _readPrivateKey (key, password, callback) {
    euSignTest.readKey(key, password, callback)
  }
  function _setCurrentCaServer (caServer) {
    if (window.CAsServers) {
      var index = window.CAsServers.findIndex(x => x.address === caServer.code)
      euSignTest.setCASettings(index)
    }
  }
  function _base64ToBytaArray (base64) {
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'
    var lookup = new Uint8Array(256)
    for (var i = 0; i < chars.length; i++) {
      lookup[chars.charCodeAt(i)] = i
    }
    var bufferLength = base64.length * 0.75, len = base64.length, p = 0, encoded1, encoded2, encoded3, encoded4
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
  }
  function _signData (data) {
    var dataArray = new Uint8Array(_base64ToBytaArray(data))
    return euSign.SignDataInternal(true, dataArray, true)
  }
  return {
    initialize: function (callback) {
      _initialize(callback)
    },
    getCAServers: function () {
      return _getCaServers()
    },
    readPrivateKey: function (key, password, callback) {
      _readPrivateKey(key, password, callback)
    },
    setCurrentCaServer: function (caServer) {
      _setCurrentCaServer(caServer)
    },
    signData: function (data) {
      return _signData(data)
    }
  }
}
export { IITPlugin, euSignTest }

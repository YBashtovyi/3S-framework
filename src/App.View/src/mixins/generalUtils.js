const GuidUtils = function() {
  function _generateNewGuid () {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
      (
        c ^
        (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
      ).toString(16)
    )
  }
  function _isEmptyGuid (value) {
    var regexEmptyGuid = /^(?:[0-9a-f]{8}-?[0-9a-f]{4}-?[1-5][0-9a-f]{3}-?[89ab][0-9a-f]{3}-?[0-9a-f]{12} |00000000-0000-0000-0000-000000000000)$/i
    return regexEmptyGuid.test(value)
  }
  function _emtpyGuid () {
    return '00000000-0000-0000-0000-000000000000'
  }
  return {
    newGuid: function() {
      return _generateNewGuid();
    },
    isEmptyGuid: function(value) {
      return _isEmptyGuid(value);
    },
    // TODO: WTF??? is 'emTpty'
    emtpyGuid: function() {
      return _emtpyGuid()
    }
  }
}
export { GuidUtils }

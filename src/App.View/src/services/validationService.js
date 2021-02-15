/**
 * @summary Checks every step for errors and returns error messages array for all empty fields
 * @param {Object} steps - object to validate; key - step, value - object with key - field name and value - another object
 * @param {Function} [notifyFn] - this.$q.notify, pass it if want to display error message at the place
 * @returns {string[]} error messages
 */
export const validateSteps = function (steps, notifyFn) {
  const errors = []
  if (!steps) {
    return errors
  }

  Object.keys(steps).forEach(key => {
    let stepErrors = validateFields(steps[key], false)
    if (stepErrors.length) {
      errors.push(stepErrors)
    }
  })

  notifyIfError(errors, notifyFn)

  return errors
}

/**
 * @summary Works by analyzing fields object and returns error messages array
 * @param {Object} fields - object to validate; key - field name, value - another object with value and errorMessage fields
 *  value - field value, that is checked; errorMessage - message to show if value is empty 
 * @param {string} fields.key - validated field name
 * @param {object} fields.value - object with field value and error message to display
 * @param {*} fields.value.value - value of the field, if empty, then should display error
 * @param {string} fields.value.errorMessage - message to display if validation failed 
 * @param {Function} notifyFn - this.$q.notify, pass it if want to display error message at the place
 * @returns {string[]} string array of messages or empty array if errors are absent
 */
export const validateFields = function (fields, notifyFn) {
  const errors = []
  if (!fields) {
    return errors
  }
  
  Object.keys(fields).forEach(key => {
    if (!fields[key].value) {
      errors.push(fields[key].errorMessage)
    }
  })

  notifyIfError(errors, notifyFn)

  return errors
}

/**
 * @summary Converts array of errors to multiline html string, separated by </br>
 * @param {string[]} arr - array of string errors
 * @returns {string} html error message multiline string where each message is separated by html </br> tag
 */
export const convertErrorArrayToHtmlMultilineString = function (arr) {
  if (arr.length === 0) {
    return ""
  }

  let htmlErrors = ""
  for (let i = 0; i < arr.length; i++) {
    // for every element after first, use </br>
    if (i === 0) {
      htmlErrors = arr[i]
    }
    else {
      htmlErrors += "</br>" + arr[i]
    }
  }

  return htmlErrors
}

/**
 * @summary Displays errors if errors are present and notify function is passed
 * @param {string[]} errors - array of error strings to display
 * @param {Function} notifyFn - this.$q.notify
 */
export const notifyIfError = function (errors, notifyFn) {
  if (notifyFn && errors.length) {
    const message = convertErrorArrayToHtmlMultilineString(errors)
    notify(message, notifyFn)
  }
}

/**
 * @summary Displays error notification
 * @param {string} message - message to display (supports html, so be carefull about injection)
 * @param {Function} notifyFn - this.$q.notify
 */
export const notify = function (message, notifyFn) {
  notifyFn({
    position: "top",
    timeout: 5000,
    html: true,
    message: message,
    color: "warning",
    icon: "warning",
    textColor: "grey-9"
  })
}

// object with all defined functions above
export default {
  validateFields, validateSteps, convertErrorArrayToHtmlMultilineString
}
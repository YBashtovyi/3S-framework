

export default function (options) {
  let key = 0

  const withRegex = (option, input) => {
    if (!option.fn || typeof option.fn !== 'function') return input

    if (!option.regex || !(option.regex instanceof RegExp)) return input

    if (typeof input === 'string') {
      const regex = option.regex
      const output = []
      let result = null

      while ((result = regex.exec(input)) !== null) {
        const index = result.index;
        const match = result[0]

        output.push(input.substring(0, index))
        output.push(option.fn(++key, result))

        input = input.substring(index + match.length, input.length + 1)
        regex.lastIndex = 0
      }

      output.push(input)
      return output.reduce((acc, newValue) => acc += newValue, '')
    } else if (Array.isArray(input)) {
      return input.map(chunk => withRegex(option, chunk))
    } else return input
  }

  return input => {
    if (!options || !Array.isArray(options) || !options.length) {
      return input
    }

    options.forEach(option => input = withRegex(option, input))

    return input
  }
}
// Предложение выносить паттерны в константы ! аля все regex и тд

export const floatRegex   = /^[+-]?\d+(\.\d+)?$/

export const integerRegex = /^\d+$/

export const dataRegex    = /^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d$/

export const loginRegex   = /^[a-zA-Z0-9_.-@]{4,40}$/

export const hasSymbolInLowercaseRegex = /(?=.*[a-z])/

export const hasSymbolInUppercaseRegex = /(?=.*[A-Z])/

export const hasDigitRegex = /(?=.*[0-9])/

export const min8SymbolsLength = /.{8,}/
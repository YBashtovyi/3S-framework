export const UNKNOWN  = {
    code: 0,
    color: "negative"
}

export const HAS_ERRORS  = {
    code: 1,
    color: "negative"
}

export const HAS_WARNINGS  = {
    code: 2,
    color: "warning"
}

export const SUCCESS  = {
    code: 3,
    color: "positive"
}

export const PROCESS_STATUSES = [
    UNKNOWN,
    HAS_ERRORS,
    HAS_WARNINGS,
    SUCCESS
]
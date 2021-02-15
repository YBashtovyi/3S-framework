import {
    env,
    getData
} from '../api'

import UrlBuilder from '../../utils/url-builder'

export const getUserProfiles = () => {
    const url = new UrlBuilder({ host: env.ACCOUNT.USER_PROFILES}).build()
    return getData(url).then(response => response.data)
}
import sortby from 'lodash.sortby'
import get from 'lodash.get'

import { diagnosisTypes } from '../../constants/ehealth'
import { stringEmpty } from '../function-helpers'


export default function (diagnoses) {

    const compare = diagnosis => {
        const diagnosisType = get(diagnosis, ['type', 'code'], stringEmpty())

        if (diagnosisType === diagnosisTypes.PRIMARY)       return 0
        if (diagnosisType === diagnosisTypes.COMORBIDITY)   return 1
        if (diagnosisType === diagnosisTypes.COMPLICATION)  return 2

        console.error('Detected unknown type of the diagnosis')
        return -1
    }

    const sortedDiagnoses = sortby(diagnoses, compare)

    return sortedDiagnoses
}
import isEmpty from 'lodash.isempty'
import sortby from 'lodash.sortby'
import get from 'lodash.get'

import { diagnosisTypes } from '../../constants/ehealth'
import { stringEmpty } from '../function-helpers'


export default function(diagnoses) {

    if (isEmpty(diagnoses)) {
        throw new Error('diagnosis is null or undefined')
    }

    if (!Array.isArray(diagnoses)) {
        throw new Error('diagnoses is not an array')
    }

    const diagnosisTypeCode = diagnosisTypeCode => diagnosis => 
        get(diagnosis, ['type', 'code'], stringEmpty()) === diagnosisTypeCode
    
    const diagnosisRank = diagnosis => get(diagnosis, 'rank', stringEmpty())

    const primary = diagnoses.filter(diagnosisTypeCode(diagnosisTypes.PRIMARY)) || []
    const comorbidity = diagnoses.filter(diagnosisTypeCode(diagnosisTypes.COMORBIDITY)) || []
    const comlication = diagnoses.filter(diagnosisTypeCode(diagnosisTypes.COMPLICATION)) || []

    const sortedDiagnoses = [
        ...primary,
        ...sortby(comorbidity, diagnosisRank),
        ...sortby(comlication, diagnosisRank)
    ]

    return sortedDiagnoses
}
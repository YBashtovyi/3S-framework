
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import moment from 'moment'

import { isNotEmpty, stringFormat } from '../function-helpers'
import generateHtmlDocument from './generate-html-document'

/**
* DUMMY 
const episodes = [
    {
        ehealthId: "1fe4ebf8-ea18-46a4-b73f-1173cccd293d",
        codeAndName: "P36.4 Сепсис новонародженого, спричинений кишковою паличкою Escherichia coli",
        type: "Лікування",
        startDate: "17.06.2020",
        currentDiagnoses: [
            {
                id: "91e3c3f8-3ea5-476b-af69-1f3e8ba73978",
                typeCode: "primary",
                typeCaption: "основний",
                name: "B48.1 Риноспоридіоз",
                rank: "0"
            },
            {
                id: "91e3c3f8-3ea5-476b-af69-1f3e8ba73978",
                typeCode: "primary",
                typeCaption: "основний",
                name: "B48.1 Риноспоридіоз",
                rank: "1"
            }
        ],
        diagnosesHistory: [
            {
                id: "91e3c3f8-3ea5-476b-af69-1f3e8ba73978",
                typeCode: "primary",
                typeCaption: "основний",
                name: "B48.1 Риноспоридіоз",
                rank: "0"
            },
             {
                id: "91e3c3f8-3ea5-476b-af69-1f3e8ba73978",
                typeCode: "primary",
                typeCaption: "основний",
                name: "B48.1 Риноспоридіоз",
                rank: "1"
            }
        ],
        encounters: [
            {
                ehealthId: "5d38be61-9961-40c2-a76e-a3e10f017b02",
                startDate: "20.06.2020 19:21",
                class: "Амбулаторна медична допомога",
                type: "Візит пацієнта в заклад",
                prescriptions: "prescriptions",
                paperReferral: {
                    requisition: "12345678",
                    requesterLegalEntityName: "НАН України",
                    requesterLegalEntityEdrpou: "1234488888",
                    requesterEmployeeName: "Ляшко Олег Валерійович",
                    serviceRequestDate: "2020-06-15 12:36:10",
                    note: "Вр без ляшка як Жінка без сраки"
                },
                reasons: [
                    {
                        caption: "reasons caption",
                        code: "reasons code",
                        value: "reasons value"
                    }
                ],
                diagnoses: [
                    {
                        id: "91e3c3f8-3ea5-476b-af69-1f3e8ba73978",
                        typeCode: "primary",
                        typeCaption: "основний",
                        name: "B48.1 Риноспоридіоз",
                        rank: "0"
                    }
                ],
                actions: [
                    {
                        caption: "actions caption",
                        code: "actions code",
                        value: "actions value"
                    }
                ],
                actionReferences: [
                    {
                        caption: "actionReferences caption",
                        code: "actionReferences code",
                        value: "actionReferences value"
                    }
                ],
                hospitalization: {
                    admitSource: "За направленням паперовим",
                    reAdmission: "Так, протягом року",
                    legalEntityName: "НАН України",
                    dischargeDisposition: "Виписаний з поліпшенням",
                    dischargeDepartment: "Трансплантології",
                    preAdmissionIdentifier: "112"
                }
            }
        ]
    }
]
*/


const generatePrintableDiagnosis = (diagnosis, index) => ({
    type: 'span',
    value: `${index + 1}) ${diagnosis.name} (${diagnosis.typeCaption})${isNotEmpty(diagnosis.rank) ? stringFormat(', Значимість діагнозу: {0}', [diagnosis.rank]) : ''}`
})

const br = { type: 'br' }

const generatePaperReferral = paperReferral => {

    const generateRequisition = {
        type: 'span',
        value: `Номер направлення: ${paperReferral.requisition}`
    }

    const generateServiceRequestDate = {
        type: 'span',
        value: `Дата направлення: ${moment(paperReferral.serviceRequestDate).format('DD.MM.YYYY')}`
    }
   
    const generateRequesterLegalEntityName = {
        type: 'span',
        value: `Організація, що направляє: ${paperReferral.requesterLegalEntityName}`
    }
    
    const generateRequesterLegalEntityEdrpou = {
        type: 'span',
        value: `ЄДРПОУ організації, що направляє: ${paperReferral.requesterLegalEntityEdrpou}`
    }
    
    const generateRequesterEmployeeName = {
        type: 'span',
        value: `Лікар, який направляє: ${paperReferral.requesterEmployeeName}`
    }

    const generatePaperReferralBody = {
        type: 'div',
        style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
        children: [
            generateRequisition, br,
            generateServiceRequestDate, br,
            generateRequesterLegalEntityName, br,
            generateRequesterLegalEntityEdrpou, br,
            generateRequesterEmployeeName, br
        ]
    }

    if (isNotEmpty(get(paperReferral, 'note', null))) {
        const generateNote = { 
            type: 'span',
            value: `Коментар до паперового направлення: ${paperReferral.note}`
        }
        generatePaperReferralBody.children.push(generateNote)
    }
    
    const generatePaperReferral = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Паперове направлення',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            generatePaperReferralBody
        ]
    }
    
    return generatePaperReferral
}

const generateHospitalization = hospitalization => {

    const hospitalizationChildren = []

    if (isNotEmpty(get(hospitalization, 'admitSource', null))) {
        const admitSource = {
            type: 'span',
            value: `Ким направлений: ${hospitalization.admitSource}`
        }
        hospitalizationChildren.push(admitSource)
        hospitalizationChildren.push(br)
    }
    
    if (isNotEmpty(get(hospitalization, 'reAdmission', null))) {
        const reAdmission = {
            type: 'span',
            value: `Ознака повторної госпіталізації: ${hospitalization.reAdmission}`
        }
        hospitalizationChildren.push(reAdmission)
        hospitalizationChildren.push(br)
    }

    if (isNotEmpty(get(hospitalization, 'legalEntityName', null))) {
        const legalEntityName = {
            type: 'span',
            value: `Заклад, у який переведено пацієнта: ${hospitalization.legalEntityName}`
        }
        hospitalizationChildren.push(legalEntityName)
        hospitalizationChildren.push(br)
    }


    if (isNotEmpty(get(hospitalization, 'dischargeDisposition', null))) {
        const dischargeDisposition = {
            type: 'span',
            value: `Результат лікування: ${hospitalization.dischargeDisposition}`
        }
        hospitalizationChildren.push(dischargeDisposition)
        hospitalizationChildren.push(br)
    }


    if (isNotEmpty(get(hospitalization, 'dischargeDepartment', null))) {
        const dischargeDepartment = {
            type: 'span',
            value: `Відділення виписки: ${hospitalization.dischargeDepartment}`
        }
        hospitalizationChildren.push(dischargeDepartment)
        hospitalizationChildren.push(br)
    }


    if (isNotEmpty(get(hospitalization, 'preAdmissionIdentifier', null))) {
        const preAdmissionIdentifier = {
            type: 'span',
            value: `Номер виклику швидкої допомоги: ${hospitalization.preAdmissionIdentifier}`
        }
        hospitalizationChildren.push(preAdmissionIdentifier)
        hospitalizationChildren.push(br)
    }
   

    const hospitalizationBody = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Госпіталізація',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                children: hospitalizationChildren
            }
        ]
    }

    return hospitalizationBody
}

const generateEncounterResources = (encounterResources, title) => {

    const generateEncounterResource = (encounterResource = [], index) => {
        const name = `${index + 1}) ${encounterResource.caption}`
        const value = `${name} ${isNotEmpty() ? stringFormat('(0)', [encounterResource.value]) : ''}`
        return { type: 'span', value }
    }

    const generatePaperReferral = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: title,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                children: encounterResources.reduce((accumulator, currentValue, index) => {
                    accumulator.push(generateEncounterResource(currentValue, index))
                    accumulator.push(br)
                    return accumulator
                }, [])
            }
        ]
    }

    return generatePaperReferral
}

const generatePrintableEncounter = (encounter, index) => {

    const encounterIndex = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Порядковий номер взаємодії',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: `${index + 1}`,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const encounterStartDate = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Дата взаємодії',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: moment(encounter.startDate).format('DD.MM.YYYY'),
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const encounterClass = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Клас взаємодії',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: encounter.class,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const encounterType = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Тип взаємодії',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: encounter.type,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const printableEncounterInfo = [
        encounterIndex, 
        encounterStartDate, 
        encounterClass, 
        encounterType
    ]  

    if (isNotEmpty(get(encounter, 'paperReferral', null))) {
        const encounterPaperReferral = generatePaperReferral(encounter.paperReferral)
        printableEncounterInfo.push(encounterPaperReferral)
    }

    const diagnoses = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Діагнози',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                children: get(encounter, 'diagnoses', []).reduce((accumulator, currentValue, index) => {
                    accumulator.push(generatePrintableDiagnosis(currentValue, index))
                    accumulator.push(br)
                    return accumulator
                }, [])
            }
        ]
    }

    const prescriptions = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Призначення',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: encounter.prescriptions,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }


    if (!isEmpty(get(encounter, 'reasons', []))) {
        printableEncounterInfo.push(generateEncounterResources(encounter.reasons, 'Причини звернення'))
    }
   
    printableEncounterInfo.push(diagnoses)

    if (!isEmpty(get(encounter, 'actions', []))) {
        printableEncounterInfo.push(generateEncounterResources(encounter.actions, 'Дії'))
    }

    if (!isEmpty(get(encounter, 'actionReferences', []))) {
        printableEncounterInfo.push(generateEncounterResources(encounter.actionReferences, 'Послуги'))
    }

    if (!isEmpty(get(encounter, 'prescriptions', null))) {
        printableEncounterInfo.push(prescriptions)
    }

    if (!isEmpty(get(encounter, 'hospitalization', null))) {
        printableEncounterInfo.push(generateHospitalization(encounter.hospitalization))
    }
   
    return printableEncounterInfo
    
 
}

const generatePrintableEpisode = (episode, index) => {

    const episodeIndex = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Порядковий номер епізоду',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: `${index + 1}`,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
       ]
    }

    const episodeName = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Найменування епізоду',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: episode.codeAndName,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const episodeType = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Тип епізоду',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: episode.type,
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const episodeStartDate = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Дата початку епізоду',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                value: moment(episode.startDate).format('DD.MM.YYYY'),
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            }
        ]
    }

    const currentDiagnoses = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Поточні діагнози',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                children: get(episode, 'currentDiagnoses', []).reduce((accumulator, currentValue, index) => {
                    accumulator.push(generatePrintableDiagnosis(currentValue, index))
                    accumulator.push({ type: 'br' })
                    return accumulator
                }, [])
            }
        ]
    }

    const diagnosesHistory = {
        type: 'div',
        style: 'display: table-row;',
        children: [
            {
                type: 'div',
                value: 'Історія діагнозів',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
            },
            {
                type: 'div',
                style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                children: get(episode, 'currentDiagnoses', []).reduce((accumulator, currentValue, index) => {
                    accumulator.push(generatePrintableDiagnosis(currentValue, index))
                    accumulator.push({ type: 'br' })
                    return accumulator
                }, [])
            }
        ]
    }

    const encounters = get(episode, 'encounters', []).reduce((accumulator, currentValue, index) => {
        accumulator.push(...generatePrintableEncounter(currentValue, index))
        return accumulator
    }, [])

    const printableEpisodeInfo = {
        type: 'div',
        style: 'display: table; width: 100%;',
        children: [
            episodeIndex, 
            episodeName, 
            episodeType, 
            episodeStartDate, 
            currentDiagnoses, 
            diagnosesHistory,
            ...encounters
        ]
    }

    return printableEpisodeInfo
}


export default function (episodeEncounters = []) {
    const episodesHtml = { type: 'div', children: episodeEncounters.map(generatePrintableEpisode) }
    const printableEpisodeEncountersInfo = generateHtmlDocument(episodesHtml)

    return printableEpisodeEncountersInfo
}
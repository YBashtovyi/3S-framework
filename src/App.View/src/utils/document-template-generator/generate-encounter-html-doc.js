import moment from 'moment'
import get from 'lodash.get'
import { stringFormat, isEmpty, stringEmpty, isNotEmpty } from '../function-helpers'
import { ENCOUNTER_ADMIT_SOURCES } from '../../constants/ehealth'
import generateHtmlDocument from './generate-html-document'


export default function (printableEncounter) {

    const printableEncounterInfo = {
        type: 'div',
        style: 'font-family: Georgia, Times New Roman, Times, serif; margin-bottom: 25px;',
        children: []
    }


    // Title
    printableEncounterInfo.children.push({
        type: 'div',
        value: 'МІНІСТЕРСТВО ОХОРОНИ ЗДОРОВ’Я УКРАЇНИ',
        style: 'display: flex; justify-content: center; padding: 10px 0; font-weight: 600;'
    })

    
    // Organization and Department
    printableEncounterInfo.children.push({
        type: 'table',
        style: 'width: 100%; border: 1px solid black; margin-bottom: 45px;',
        children: [
            { 
                type: 'tr',
                children: [
                    {
                        type: 'td',
                        style: 'display: flex; flex-direction: column; align-items: center; border-right: 1px solid black; padding: 10px 0;',                        
                        children: [
                            { type: 'div', value: 'Міністерство охорони здоров’я', style: 'font-weight: 600; font-size: 12pt;' },
                            { type: 'div', value: get(printableEncounter, ['organization', 'name']), style: 'font-weight: 600; font-size: 14pt; margin: 10px 0;' },
                            { type: 'div', value: printableEncounter.department, style: 'font-weight: 700; font-size: 15pt; margin: 10px 0;' },
                            { type: 'div', value: get(printableEncounter, ['organization', 'address'], stringEmpty()) },
                            { 
                                type: 'div', 
                                value: isEmpty(get(printableEncounter, ['organization', 'edrpou'], stringEmpty()))
                                    ? stringEmpty()
                                    : stringFormat("Код за ЄДРПОУ {0}", [get(printableEncounter, ['organization', 'edrpou'])])
                            },
                        ]
                    },
                    { 
                        type: 'td',
                        style: 'text-align: center; font-weight: 600; font-size: 12pt;',
                        value: 'МЕДИЧНА ДОКУМЕНТАЦІЯ' 
                    },
                ]
            }
        ]
    })
    

    // Encounter Title
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'display: flex; flex-direction: row; justify-content: center; margin-bottom: 15px;',
        children: [{ 
            type: 'div',
            value: stringFormat('{0} від {1}', [printableEncounter.encounterTitle, moment(printableEncounter.encounterCreateOn).format('DD.MM.YYYY')]), 
            style: 'margin-right: 10px; font-weight: 600;' 
        }]
    })


    // Patient
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Пацієнт:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.patient }
        ]
    })


    // Employee
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Лікар:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.employee }
        ]
    })


    // EncounterClass
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Клас взаємодії:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.encounterClass }
        ]
    })


    // EncounterType
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Тип взаємодії:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.encounterType }
        ]
    })


    // Priority
    if (isNotEmpty(printableEncounter.encounterPriority)) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Пріорітет:', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.encounterPriority }
            ]
        })
    }
    

    // AdmitSource
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Ким направлений:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.admitSource }
        ]
    })


    if (printableEncounter.admitSourceCode === ENCOUNTER_ADMIT_SOURCES.SYSTEM_REFERRAL) {

        // IncomingMedicalReferral
        if (isNotEmpty(printableEncounter.incomingMedicalReferral)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Електронне направлення:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.incomingMedicalReferral }                
                ]
            })
        }


        // ReferralNumber
        if (isNotEmpty(printableEncounter.referralNumber)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Номер направлення:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.referralNumber }                
                ]
            })
        }
        
    }


    if (printableEncounter.admitSourceCode === ENCOUNTER_ADMIT_SOURCES.BLANK_REFERRAL) {

        if (isNotEmpty(printableEncounter.paperReferralNumber)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Номер направлення:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.paperReferralNumber }                
                ]
            })
        }


        if (isNotEmpty(printableEncounter.paperReferralDate)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Дата направлення:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.paperReferralDate }                
                ]
            })
        }


        if (isNotEmpty(printableEncounter.requesterLegalEntityName)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Організація, що направляє:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: stringFormat('{0} ({1})', [printableEncounter.requesterLegalEntityName, printableEncounter.requesterLegalEntityEdrpou]) }                
                ]
            })
        }


        if (isNotEmpty(printableEncounter.requesterLegalEntityName)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Лікар, який направляє:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.requesterEmployeeName }                
                ]
            })
        }


        if (isNotEmpty(printableEncounter.requesterNote)) {
            printableEncounterInfo.children.push({
                type: 'div',
                style: 'margin-bottom: 10px;',
                children: [
                    { type: 'div', value: 'Коментар до паперового направлення:', style: 'margin-right: 10px; font-weight: 600;' },
                    { type: 'div', value: printableEncounter.requesterNote }                
                ]
            })
        }
    }


    // AppointmentPeriod
    if (isNotEmpty(printableEncounter.appointmentPeriod)) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Дата початку та закінчення прийму:', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.appointmentPeriod }                
            ]
        })
    }


    // Episode
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Епізод (проблема зі здоров’ям):', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.episode }
        ]
    })


    // Reasons
    if (printableEncounter.reasons && printableEncounter.reasons.length) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Причини звернення:', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.reasons }
            ]
        })
    }


    // Observations
    if (printableEncounter.observations && printableEncounter.observations.length) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Огляд (спостереження):', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.observations }
            ]
        })
    }
  

    // Diagnoses
    printableEncounterInfo.children.push({
        type: 'div',
        style: 'margin-bottom: 10px;',
        children: [
            { type: 'div', value: 'Діагнози:', style: 'margin-right: 10px; font-weight: 600;' },
            { type: 'div', value: printableEncounter.diagnoses }
        ]
    })


    // Services
    printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Послуги:', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.services }
            ]
    })


    // Prescribings
    if (isNotEmpty(printableEncounter.encounterPrescribings)) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Призначення:', style: 'margin-right: 10px; font-weight: 600;' },
                { type: 'div', value: printableEncounter.encounterPrescribings }
            ]
        })
    }


    // Hospitalization
    if (isNotEmpty(printableEncounter.hospitalization) && printableEncounter.hospitalization.length) {
        printableEncounterInfo.children.push({
            type: 'div',
            style: 'margin-bottom: 10px;',
            children: [
                { type: 'div', value: 'Госпіталізація:', style: 'margin-right: 10px; font-weight: 600;' },
                ...printableEncounter.hospitalization.map(value => ({ type: 'div', value }))
            ]
        })
    }

    
    const encounterRawHtml = generateHtmlDocument(printableEncounterInfo, 'Взаємодія') 

    return encounterRawHtml
}

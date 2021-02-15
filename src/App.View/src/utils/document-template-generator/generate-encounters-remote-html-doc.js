
import get from 'lodash.get'
import isEmpty from 'lodash.isempty'
import moment from 'moment'

import { isNotEmpty, stringFormat, stringCapitalize, sortBy } from '../function-helpers'
import generateHtmlDocument from './generate-html-document'

/**
* DUMMY 
const encounters = [
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
 
    const printableEncounterInfo = {
      type: 'div',
      style: 'display: table; width: 100%;',
      children: [
        encounterStartDate, 
        encounterClass, 
        encounterType
      ]  
    }

    if (isNotEmpty(get(encounter, 'performer', null))) {
      const generatedPerformer = {
          type: 'div',
          style: 'display: table-row;',
          children: [
              {
                  type: 'div',
                  value: 'Лікар',
                  style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
              },
              {
                  type: 'div',
                  value: encounter.performer,
                  style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
              }
          ]
      }
      printableEncounterInfo.children.push(generatedPerformer)
    }

    if (isNotEmpty(get(encounter, 'incomingReferral', null))) {
      const generatedIncomingReferral = {
          type: 'div',
          style: 'display: table-row;',
          children: [
              {
                  type: 'div',
                  value: 'Електронне направлення',
                  style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
              },
              {
                  type: 'div',
                  value: encounter.incomingReferral + ' (' + moment(encounter.referralDate).format('DD.MM.YYYY HH:mm') + ')',
                  style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
              }
          ]
      }
      
      printableEncounterInfo.children.push(generatedIncomingReferral)
    }

    if (isNotEmpty(get(encounter, 'paperReferral', null))) {
        const encounterPaperReferral = generatePaperReferral(encounter.paperReferral)
        printableEncounterInfo.children.push(encounterPaperReferral)
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
                children: get(encounter, 'diagnoses', []).sort(sortBy('rank')).reduce((accumulator, currentValue, index) => {
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

    const generatedCancellationReason = {
      type: 'div',
      style: 'display: table-row;',
      children: [
          {
              type: 'div',
              value: 'Причина відкликання',
              style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
          },
          {
              type: 'div',
              value: stringCapitalize(encounter.cancellationReason),
              style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
          }
      ]
    }

    const generatedExplanatoryLetter = {
      type: 'div',
      style: 'display: table-row;',
      children: [
          {
              type: 'div',
              value: 'Обґрунтування підстав визначення помилкового внесення медичної документації',
              style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
          },
          {
              type: 'div',
              value: encounter.explanatoryLetter,
              style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
          }
      ]
    }

    if (!isEmpty(get(encounter, 'reasons', []))) {
        printableEncounterInfo.children.push(generateEncounterResources(encounter.reasons, 'Причини звернення'))
    }
   
    printableEncounterInfo.children.push(diagnoses)

    if (!isEmpty(get(encounter, 'actions', []))) {
        printableEncounterInfo.children.push(generateEncounterResources(encounter.actions, 'Дії'))
    }

    if (!isEmpty(get(encounter, 'actionReferences', []))) {
        printableEncounterInfo.children.push(generateEncounterResources(encounter.actionReferences, 'Послуги'))
    }

    if (!isEmpty(get(encounter, 'prescriptions', null))) {
        printableEncounterInfo.children.push(prescriptions)
    }

    if (!isEmpty(get(encounter, 'hospitalization', null))) {
        printableEncounterInfo.children.push(generateHospitalization(encounter.hospitalization))
    }

    if (!isEmpty(get(encounter, 'cancellationReason', null))) {
      printableEncounterInfo.children.push(generatedCancellationReason)
    }

    if (!isEmpty(get(encounter, 'explanatoryLetter', null))) {
      printableEncounterInfo.children.push(generatedExplanatoryLetter)
    }
   
    return printableEncounterInfo
}

export default function (encounters = []) {
  const encounterHtml = { type: 'div', children: encounters.map(generatePrintableEncounter) }
  const printableEncountersInfo = generateHtmlDocument(encounterHtml)

  return printableEncountersInfo
}

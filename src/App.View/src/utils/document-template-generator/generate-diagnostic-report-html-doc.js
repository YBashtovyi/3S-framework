import moment from 'moment'
import get from 'lodash.get'
import first from 'lodash.first'

import { isNotEmpty, stringCapitalize } from '../function-helpers'
import generateHtmlDocument from './generate-html-document'



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

const generateDiagnosticReports = (diagnosticReport, index) => {

    const diagnosticReportsBody = []

    if (Number.isInteger(index)) {
        const diagnosticReportCounter = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Порядковий номер діагностичного звіту',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: `${index + 1}`,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(diagnosticReportCounter)
    }


    if (isNotEmpty(get(diagnosticReport, 'issued', null))) {
        const generatedIssued = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Дата створення',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: moment(diagnosticReport.issued).format('DD.MM.YYYY'),
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedIssued)
    }


    if (isNotEmpty(get(diagnosticReport, 'paperReferral', null))) {
        diagnosticReportsBody.push(generatePaperReferral(diagnosticReport.paperReferral))
    }

    if (isNotEmpty(get(diagnosticReport, 'referral', null))) {
      const generatedreferral = {
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
                  value: diagnosticReport.referral + ' (' + moment(diagnosticReport.referralDate).format('DD.MM.YYYY HH:mm') + ')',
                  style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
              }
          ]
      }
      
      diagnosticReportsBody.push(generatedreferral)
    }

    if (isNotEmpty(get(diagnosticReport, 'category', null))) {
        const generatedCategory = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Категорія діагностичного звіту',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.category,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedCategory)
    }


    if (isNotEmpty(get(diagnosticReport, 'serviceCatalogItemName', null))) {
        const generatedServiceCatalogItemName = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Послуга',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.serviceCatalogItemName,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedServiceCatalogItemName)
    }


    if (isNotEmpty(get(diagnosticReport, 'periodStart', null))) {
        const generatedPeriodStart = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Період релевантності (початок)',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: moment(diagnosticReport.periodStart).format('DD.MM.YYYY HH:mm'),
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedPeriodStart)
    }


    if (isNotEmpty(get(diagnosticReport, 'periodEnd', null))) {
        const generatedPeriodEnd = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Період релевантності (кінець)',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: moment(diagnosticReport.periodEnd).format('DD.MM.YYYY HH:mm'),
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedPeriodEnd)
    }


    if (isNotEmpty(get(diagnosticReport, 'conclusion', null))) {
        const generatedConclusion = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Заключення лікаря',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.conclusion,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedConclusion)
    }


    if (isNotEmpty(get(diagnosticReport, 'conclusionCode', null))) {
        const generatedConclusionCode = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Заключення лікаря (діагноз)',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.conclusionCode,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedConclusionCode)
    }


    if (isNotEmpty(get(diagnosticReport, 'managingOrganization', null))) {
        const generatedManagingOrganization = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Заклад, що надає медичні послуги',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.managingOrganization,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedManagingOrganization)
    }


    if (isNotEmpty(get(diagnosticReport, 'division', null))) {
        const generatedDivision = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Місце надання послуг',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.division,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }

        diagnosticReportsBody.push(generatedDivision)
    }


    if (isNotEmpty(get(diagnosticReport, 'performer', null))) {
        const generatedPerformer = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Виконавець діагностики',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.performer,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedPerformer)
    }


    if (isNotEmpty(get(diagnosticReport, 'resultsInterpreter', null))) {
        const generatedResultsInterpreter = {
            type: 'div',
            style: 'display: table-row;',
            children: [
                {
                    type: 'div',
                    value: 'Працівник, що інтерпретував результати',
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                },
                {
                    type: 'div',
                    value: diagnosticReport.resultsInterpreter,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedResultsInterpreter)
    }


    if (isNotEmpty(get(diagnosticReport, 'cancellationReason', null))) {
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
                    value: stringCapitalize(diagnosticReport.cancellationReason),
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedCancellationReason)
    }


    if (isNotEmpty(get(diagnosticReport, 'explanatoryLetter', null))) {
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
                    value: diagnosticReport.explanatoryLetter,
                    style: 'display: table-cell; padding: 3px 10px; border: 1px solid #999999;',
                }
            ]
        }
        diagnosticReportsBody.push(generatedExplanatoryLetter)
    }

    return diagnosticReportsBody
}


export default function (diagnosticReports = []) {

    const diagnosticBody = { 
        type: 'div',
        style: 'display: table; width: 100%;',
        children: diagnosticReports.length === 1
            ? [...generateDiagnosticReports(first(diagnosticReports), null)]
            : diagnosticReports.reduce((accumulator, currentValue, index) => { 
                accumulator.push(...generateDiagnosticReports(currentValue, index))
                return accumulator
            }, [])
    }

    const diagnosticRoot = {
        type: 'div',
        children: [diagnosticBody]
    }
    
    const printableEpisodeEncountersInfo = generateHtmlDocument(diagnosticRoot)

    return printableEpisodeEncountersInfo
}
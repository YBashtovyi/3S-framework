import generateHtmlDocument from './generate-html-document'
import isEmpty from 'lodash.isempty'
import get from 'lodash.get'
/**
 * Generates html document for report statistics data for preview mode
 * @param {Array} reportData - report statistics data 
 * @param {Array} summaryReportData - summary of all columns
 * @param {Object} reportParameters - key value pair of filters and parameters for report
 */
export default function(reportData, summaryReportData, reportParameters) {
    const printableInfo = {
        type: 'div',
        style: 'font-family: Georgia, Times New Roman, Times, serif; margin-top: 20px;',
        children: []
    }

    printableInfo.children.push({
        type: 'div',
        value: 'Створені та надіслані до ЦБД e-Health взаємодії',
        style: 'display: flex; justify-content: center; padding: 10px 0; font-weight: 600; font-size: 30px;'
    })
    
    if (!isEmpty(reportParameters)) {
        
        const reportParametersPrintable = Object.keys(reportParameters).map(key => {
            const values = get(reportParameters, [key, 'filterValue'])
            const valueKey = get(reportParameters, [key, 'filterCaption'])
            if (!isEmpty(values)) {
                return {
                    type: 'tr',
                    children: [
                        {
                            type: 'td',
                            style: 'text-align: center; width: 40%;',
                            children: [{
                                type: 'strong',
                                value: `${valueKey}: `
                            }]
                        },
                        {
                            type: 'td',
                            style: "padding: 10px; text-align: center; width: 60%;",
                            value: values
                        }
                    ]
                }
            }
        }).filter(item => !isEmpty(item))

        printableInfo.children.push({
            type: 'table',
            style: 'width: 80%; margin-left:auto;margin-right:auto; margin-top: 30px;',
            children: reportParametersPrintable
        })
    }

    const htmlTableHeader = {
        type: 'tr',
        style: 'border: 1px solid black; height: 25px;',
        children: [
            { type: 'td',       value: 'Лікар',                                             style: 'border: 1px solid black; width:300px;'},
            { type: 'td',       value: 'Підрозділ',                                         style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Спеціалізація ehealth',                             style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Створені взаємодії',                                style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Відправлені взаємодії (успішно)',                   style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Відправлені взаємодії (не успішно)',                style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Потребують додаткових дій щодо відправки',          style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Відкликані взаємодії',                              style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Потребують додаткових дій щодо відкликання',        style: 'border: 1px solid black;'},
            { type: 'td',       value: 'Не відправлені взаємодії',                          style: 'border: 1px solid black;'},
        ]

    }
    
    const htmlTableBody = reportData.map((item) => {
        return {
            type: 'tr',
            style: 'border: 1px solid black;',
            children: Object.values(item).map(itemValue => {
                return {
                    type: 'td',
                    style: 'border: 1px solid black; height: 45px;',
                    value: typeof itemValue === 'number' 
                            ? itemValue.toString() 
                            : itemValue
                }
            })
        }
    })

    let htmlTableFooter = {}

    if (!isEmpty(summaryReportData)) {
        htmlTableFooter = {
            type: 'tr',
            style: 'border: 1px solid black; width: 50px;',
            children: [{
                type: 'td',
                value: 'Загальна кількість',
                attributes: [{ 
                    name: 'colspan',
                    value: '3'
                }]
            }, 
            ...Object.values(summaryReportData).map(itemValue => {
            return {
                type: 'td',
                style: 'border: 1px solid black; height: 50px;',
                value: typeof itemValue === 'number' 
                        ? itemValue.toString() 
                        : itemValue
            }
            })]
        }
    }

    const reportDataAsHtmlTable = [htmlTableHeader, ...htmlTableBody, htmlTableFooter]
    // Organization and Department
    printableInfo.children.push({
        type: 'table',
        style: 'width: 100%; border: 1px solid black; border-collapse: collapse; text-align: center;  margin-top:20px;',
        children: reportDataAsHtmlTable
    })

    const encounterRawHtml = generateHtmlDocument(printableInfo) 
    return encounterRawHtml
}
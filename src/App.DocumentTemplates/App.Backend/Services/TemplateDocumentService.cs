using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Core.Services.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using TemplateEngine.Docx;

namespace App.DocumentTemplates.Services
{
    public class TemplateDocumentService
    {
        private readonly ICommonDataService _dataService;
        private readonly IConfiguration _configuration;

        public TemplateDocumentService(ICommonDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
        }

        //public async Task<MemoryStream> GenerateMedDocFile(Guid entityId, string fileType = "pdf")
        //{
        //    var file = await GeneratePdf(entityId, fileType);
        //    return file;
        //}

        public async Task<MemoryStream> GenerateTemplateDocument(Guid id, string templateName, Dictionary<string,string> templateData, string extension)
        {
            //_repository.ChangeTracker(QueryTrackingBehavior.NoTracking);
            //var card = await (from md in _repository.All<MedDoc>().Where(md => md.Id == id)
            //                  join docTypes in _repository.All<DocType>() on md.ParentDocTypeId equals docTypes.Id into docTypesGroup
            //                  from docTypes in docTypesGroup.DefaultIfEmpty()
            //                  join appointments in _repository.All<Appointment>() on md.ParentDocId equals appointments.Id into appGroup
            //                  from appointments in appGroup.DefaultIfEmpty()
            //                  join s in _repository.All<ScheduleSlot>() on appointments.Id equals s.AppointmentId into sGroup
            //                  from s in sGroup.DefaultIfEmpty()
            //                  join extRequests in _repository.All<ExtRequest>() on md.ParentDocId equals extRequests.Id into extRequestGroup
            //                  from extRequests in extRequestGroup.DefaultIfEmpty()
            //                  join patientPersons in _repository.All<Person>() on md.PatientId equals patientPersons.Id into patGroup
            //                  from patientPersons in patGroup.DefaultIfEmpty()
            //                  join employeeCards in _repository.All<EmployeeCard>() on md.EmployeeCardId equals employeeCards.Id into employeeCardsGroup
            //                  from employeeCards in employeeCardsGroup.DefaultIfEmpty()
            //                  join employeePersons in _repository.All<Person>() on employeeCards.PersonId equals employeePersons.Id into emplGroup
            //                  from employeePersons in emplGroup.DefaultIfEmpty()
            //                  join managerPersons in _repository.All<Person>() on md.ManagerEmployeeId equals managerPersons.Id into managGroup
            //                  from managerPersons in managGroup.DefaultIfEmpty()
            //                  join departments in _repository.All<Department>() on employeeCards.DepartmentId equals departments.Id into depGroup
            //                  from departments in depGroup.DefaultIfEmpty()
            //                  join departmentsLinks in _repository.All<DepartmentLink>() on departments.Id equals departmentsLinks.DepartmentId into depLinkGroup
            //                  from departmentsLinks in depLinkGroup.DefaultIfEmpty()
            //                  join highDepartments in _repository.All<Department>() on departmentsLinks.ParentDepartmentId equals highDepartments.Id into highDepGroup
            //                  from highDepartments in highDepGroup.DefaultIfEmpty()
            //                  select new
            //                  {
            //                      ParentDocTypeCode = docTypes != null ? docTypes.Code : null,
            //                      ParentDocId = md != null ? md.ParentDocId : null,
            //                      ActualStartDateTimeUtc = s != null ? s.TimeFrom : (DateTime?)null,
            //                      OwnerId = md != null ? md.OwnerId : (long?)null,
            //                      DocTypeId = md != null ? md.DocTypeId : (long?)null,
            //                      RegDateUtc = md != null ? md.RegDateUtc : null,
            //                      DtmTemplateCode = md != null ? md.DtmTemplateCode : null,
            //                      DtmTemplateName = md != null ? md.DtmTemplateName : null,
            //                      PatientFullName = patientPersons != null ? patientPersons.FullName : null,
            //                      PatientDOB = patientPersons != null ? patientPersons.DOB : null,
            //                      EmployeeId = employeePersons != null ? (long?)employeePersons.Id : null,
            //                      EmployeeFullName = employeePersons != null ? employeePersons.FullName : null,
            //                      EmployeeShortName = employeePersons != null ? employeePersons.ShortName : null,
            //                      DepartmentId = employeeCards != null ? employeeCards.DepartmentId : null,
            //                      EmployeeDepartmentFullName = departments != null ? departments.FullName : null,
            //                      EmployeeHighDepartmentFullName = highDepartments != null ? highDepartments.FullName : null,
            //                      ManagerEmployeeId = md != null ? md.ManagerEmployeeId : null,
            //                      ManagerEmployeeName = managerPersons != null ? managerPersons.ShortName : null
            //                  }).FirstOrDefaultAsync();

            //var templateDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates");
            var templateDirectoryPath = _configuration.GetValue<string>("DtmSettings:TemplatesPath");
            //var sourceTemplatePath = Path.Combine(templateDirectoryPath, $"{card.DtmTemplateCode}.docx");
            var sourceTemplatePath = Path.Combine(templateDirectoryPath, $"{templateName}.docx");

            var definedTemplateProps = GetDefinedTemplateProperties(sourceTemplatePath);
            definedTemplateProps = definedTemplateProps.ToArray();

            if (!Directory.Exists(_configuration["DtmSettings:tmpTemplatePath"]))
            {
                Directory.CreateDirectory(_configuration["DtmSettings:tmpTemplatePath"]);
            }
                
            var destinationTemplatePath = Path.Combine(_configuration["DtmSettings:tmpTemplatePath"],
            //                                           $"{card.ParentDocTypeCode}{id}.docx");
                                                         $"{templateName}{id}.docx");

            File.Delete(destinationTemplatePath);
            File.Copy(sourceTemplatePath, destinationTemplatePath);

            //var medDocData = await _dtmService.GetDocumentCard((long)card.ParentDocId, true, card.ParentDocTypeCode);

            IDictionary<string, string> paramsList = new Dictionary<string, string>
            {
                { "OriginDbRecordId", id.ToString() },
                { "EntityTypeCode", "MedDocs" }
            };

            //List<DocDataVm> dtmDocData = await _dtmService.GetDocDataList(paramsList);

            //var orgInfo = await _repository.All<DataLayer.Models.DbModelsOrgs.Organization>().SingleAsync(o => o.Id == Convert.ToInt64(ownerId));

            var valuesToFill = new Content();
            //valuesToFill.Fields.Add(new FieldContent("ClinicName", orgInfo?.OfficialName ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("DepartmentName", card?.EmployeeHighDepartmentFullName?.ToUpper() ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("CompartmentName", card?.EmployeeDepartmentFullName?.ToUpper() ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("Adress", $"{orgInfo?.TaxCode ?? string.Empty}, {orgInfo?.ResAddress ?? string.Empty}"));
            //valuesToFill.Fields.Add(new FieldContent("Email", orgInfo?.Email ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("Phone", orgInfo?.Phone ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("PatientName", card?.PatientFullName ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("PatientDOB", card?.PatientDOB?.ToShortDateString() ?? string.Empty));
            foreach (var templateKv in templateData)
            {
                valuesToFill.Fields.Add(new FieldContent(templateKv.Key, templateKv.Value ?? string.Empty));
            }

            //void DtmDataFunc(IEnumerable<string> temProps, string docDataCode)
            //{
            //    if (temProps.Any(tp => tp == docDataCode))
            //    {
            //        var element = medDocData.DocDataList.FirstOrDefault(d => d.Code.Equals(docDataCode));
            //        if (element != null)
            //        {
            //            var value = _dtmDocManager.NormilizeDtmString(element.Value, element.ControlTypeCode);
            //            valuesToFill.Fields.Add(new FieldContent(docDataCode, value ?? string.Empty));
            //            return;
            //        }

            //        valuesToFill.Fields.Add(new FieldContent(docDataCode, string.Empty));
            //    }
            //}

            //if (medDocData != null)
            //{
            //    DtmDataFunc(definedTemplateProps, "ReferringDiagnosis");
            //    DtmDataFunc(definedTemplateProps, "ResearchArea");
            //    DtmDataFunc(definedTemplateProps, "ContrastEnhancement");
            //    DtmDataFunc(definedTemplateProps, "EED");
            //}
            //else
            //{
            //    if (definedTemplateProps.Any(tp => tp == "ReferringDiagnosis")) valuesToFill.Fields.Add(new FieldContent("ReferringDiagnosis", string.Empty));
            //    if (definedTemplateProps.Any(tp => tp == "ResearchArea")) valuesToFill.Fields.Add(new FieldContent("ResearchArea", string.Empty));
            //    if (definedTemplateProps.Any(tp => tp == "ContrastEnhancement")) valuesToFill.Fields.Add(new FieldContent("ContrastEnhancement", string.Empty));
            //    if (definedTemplateProps.Any(tp => tp == "EED")) valuesToFill.Fields.Add(new FieldContent("EED", string.Empty));
            //}

            //valuesToFill.Fields.Add(new FieldContent("ResearchDate", card?.ActualStartDateTimeUtc?.ToShortDateString() ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("DoctorName", card?.EmployeeShortName ?? string.Empty));
            //valuesToFill.Fields.Add(new FieldContent("DocumentDate", card?.RegDateUtc?.ToString("dd.MM.yyyy") ?? string.Empty));

            //if (card.ManagerEmployeeId.HasValue && card.ManagerEmployeeId.Value != card.EmployeeId)
            //{
            //    valuesToFill.Fields.Add(new FieldContent("ManagerDoctor", "Керівник:"));
            //    valuesToFill.Fields.Add(new FieldContent("ManagerDoctorName", card?.ManagerEmployeeName ?? string.Empty));
            //}

            var dtmList = new ListContent("DtmList");
            //foreach (var d in dtmDocData)
            //{
            //    var checkInnerEl = dtmDocData.Where(x => x.ParentId == d.TemplateElementId).Select(x => x.Value)
            //        .Except(new[] { "{\"ops\":[{\"insert\":\"\\n\"}]}", "", null }).ToList();

            //    if (d.ControlTypeCode == "SECTOR" && checkInnerEl.Count > 0)
            //    {
            //        dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name}"),
            //                                            new FieldContent("paragraphValue", $": ")));
            //    }

            //    if (string.IsNullOrWhiteSpace(d.Value)) continue;
            //    var value = _dtmDocManager.NormilizeDtmString(d.Value, d.ControlTypeCode);

            //    if (d.ParentId == null && dtmDocData.Where(x => x.Id == d.Id).Select(x => x.Value).Any(x => x != "{\"ops\":[{\"insert\":\"\\n\"}]}"))
            //    {
            //        dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name ?? string.Empty}"),
            //                                            new FieldContent("paragraphValue", $": {value ?? string.Empty}")));
            //    }
            //    else
            //    {
            //        if (value == "\r\n") continue;
            //        var parentName = dtmDocData.FirstOrDefault(dd => dd.TemplateElementId == d.ParentId)?.Name;
            //        var item = dtmList.Items.FirstOrDefault(i => i.Fields.First(f => f.Name.Equals("paragraph")).Value.Equals(parentName));
            //        item?.AddNestedItem(new ListItemContent(new FieldContent("label", d.Name ?? string.Empty),
            //                                                new FieldContent("value", value ?? string.Empty)));
            //    }
            //}

            if (dtmList.Items != null)
            {
                valuesToFill.Lists.Add(dtmList);
            }

            using (var destinationTemplateStream = new FileStream(destinationTemplatePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var outputDocument = new TemplateProcessor(destinationTemplateStream))
                {
                    outputDocument.SetRemoveContentControls(true);
                    outputDocument.FillContent(valuesToFill);
                    outputDocument.SaveChanges();   //document save in .docx in destinationTemplatePath, so just take it

                    var resultStream = new MemoryStream();
                    if (extension.Equals("docx"))
                    {
                        destinationTemplateStream.Seek(0, SeekOrigin.Begin);
                        await destinationTemplateStream.CopyToAsync(resultStream);
                    }
                    else
                    {
                        var wordDocument = new Aspose.Words.Document(destinationTemplateStream);
                        wordDocument.Save(resultStream, SaveFormat.Pdf);
                    }
                    resultStream.Seek(0, SeekOrigin.Begin);
                    return resultStream;
                }
            }
        }


        private IEnumerable<string> GetDefinedTemplateProperties(string templatePath)
        {
            using (var wordDocument = WordprocessingDocument.Open(templatePath, true))
            {
                var mainPart = wordDocument.MainDocumentPart;
                var props = mainPart.Document.Body.Descendants<SdtProperties>().Select(p => p.GetFirstChild<Tag>()?.Val.ToString()).Where(w => w != null);
                return props;
            }
        }

    }
}

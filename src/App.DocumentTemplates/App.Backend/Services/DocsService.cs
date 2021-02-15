using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DocumentTemplates.Dto;
using App.DocumentTemplates.Models;
using App.DocumentTemplates.ViewModels;
using Core.Base.Data;
using Core.Services;
using Core.Services.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace App.DocumentTemplates.Services
{
    public class DocsService
    {
        //private readonly DeskService _service;
        private readonly ICommonDataService _dataService;
        private readonly IConfiguration _configuration;
        //private readonly IHostingEnvironment _hostingEnvironment;
        private readonly DtmService _dtmService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserInfoService _userService;
        //private readonly ExtRequestService _extrequestService;
        //private readonly IMailService _mailService;
        //private readonly IMvpCoreRepository _repository;
        //private readonly ActiveUserStorageWithHttpContext _activeUserStorageWithHttpContext;
        //private readonly IFilesService _filesService;

        public DocsService(IUserInfoService userService,
            ICommonDataService dataService,
            //DeskService service,
            IConfiguration configuration,
            //IHostingEnvironment hostingEnvironment,
            DtmService dtmService,
            IHttpContextAccessor accessor
            //ExtRequestService extrequestService,
            //IMvpCoreRepository repository,
            //IMailService mailService,
            //ActiveUserStorageWithHttpContext activeUserStorageWithHttpContext,
            //IFilesService filesService
            )
        {
            //_service = service;
            _dataService = dataService;
            _configuration = configuration;
            //_hostingEnvironment = hostingEnvironment;
            _dtmService = dtmService;
            _accessor = accessor;
            _userService = userService;
            //_extrequestService = extrequestService;
            //_repository = repository;
            //_mailService = mailService;
            //_activeUserStorageWithHttpContext = activeUserStorageWithHttpContext;
            //_filesService = filesService;
        }

        //public async Task SyncMedDocs(long entityId, string entityCode)
        //{
        //    var docs = await _service.MedDocsListByParentAsync(null, null, null, null, entityCode, null, entityId);
        //    var attaches = await _service.GetMedDocAttaches(docs.Select(d => d.Id.Value).ToArray());
        //    Guid guid;
        //    foreach (var attach in attaches)
        //    {
        //        if (Guid.TryParse(attach.FileGId.Split('.').First(), out guid))
        //            await _extrequestService.AddFileToSyncAsync(guid, entityId);
        //    }
        //}

        //public async Task SendMail(long entityId, string entityCode)
        //{
        //    var docs = await _service.MedDocsListByParentAsync(null, null, null, null, entityCode, null, entityId);
        //    var attaches = await _service.GetMedDocAttaches(docs.Select(d => d.Id.Value).ToArray());
        //    var mailAddress = await _extrequestService.ExtRequestrResponseEmailAsync(entityId);
        //    string[] guids = attaches.Select(a => a.FileGId).ToArray();
        //    FileDbModel[] files = await _extrequestService.FileList(guids);
        //    _mailService.Send(mailAddress, files);
        //    Log.Information($"Mail to[{mailAddress}] was sent with attaches guids[{string.Join(",", guids)}]");
        //}

        //public async Task<MemoryStream> GenerateMedDocFile(Guid entityId, string fileType = "pdf")
        //{
        //    //TODO: Check  if exists stored file
        //    var ownerId = _accessor.HttpContext.Session.GetString("subjectId");
        //    var file = await generatePdf_test(entityId, ownerId, fileType);
        //    return file;
        //}

        //public async Task<string> GetMedDocEntityCodeById(long id)
        //{
        //    return await _repository.All<MedDoc>().AsNoTracking().Where(m => m.Id.Equals(id))
        //                            .Join(_repository.All<DocType>().AsNoTracking(),
        //                                  medDoc => medDoc.ParentDocTypeId,
        //                                  docType => docType.Id,
        //                                  (medDoc, docType) => docType.Code)
        //                            .FirstOrDefaultAsync();
        //}

        //public async Task AttachMedDocFile(long medDocId)
        //{
        //    MedDocsVm doc = await _service.MedDocsCardAsync(medDocId);

        //    var docTypes = await _repository.All<DocType>().AsNoTracking().FromCacheAsync();
        //    var originDbentityCode = docTypes.FirstOrDefault(dt => dt.Id.Equals(doc.ParentDocTypeId))?.Code;
        //    //var currentDocType = docTypes.FirstOrDefault(dt => dt.Code.Equals(originDbentityCode));
        //    //if (currentDocType == null || currentDocType.Id != doc.ParentDocTypeId || !doc.ParentDocId.HasValue)

        //    if (originDbentityCode == null || !doc.ParentDocId.HasValue)
        //        return;


        //    if (originDbentityCode.Equals("Appointments"))
        //    {
        //        var extReqId = await _repository.All<Appointment>().AsNoTracking()
        //                                        .Where(a => a.Id.Equals(doc.ParentDocId.Value))
        //                                        .Select(a => a.ExternalRequestId)
        //                                        .FirstOrDefaultAsync();
        //        if (extReqId == null) return;
        //    }

        //    FileInfo info;
        //    string relativePath = GetRelativePath();
        //    string storeRootPath = _configuration["StorageSettings:Path"];
        //    string dirPath = Path.Combine(storeRootPath, relativePath);
        //    Directory.CreateDirectory(dirPath);
        //    var medDoc = await _dtmService.GetDocumentCard(doc.Id.Value, true, "MedDocs");
        //    var fullPath = Path.Combine(dirPath, medDoc.Name + ".pdf");
        //    //var pdf = await generatePdf(doc.Id.Value);
        //    var ownerId = _accessor.HttpContext.Session.GetString("subjectId");
        //    var pdf = await generatePdf_test(doc.Id.Value, ownerId);
        //    long length;
        //    using (var stream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        pdf.Seek(0, SeekOrigin.Begin);
        //        await pdf.CopyToAsync(stream);
        //        length = stream.Length;
        //        pdf.Dispose();
        //    }
        //    info = new FileInfo(fullPath);


        //    var attach = await _service.GetMedDocAttach(doc.Id);
        //    if (attach != null && !String.IsNullOrEmpty(attach.FileGId))
        //    {
        //        var path = Path.Combine(info.DirectoryName, attach.FileGId);

        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }
        //        File.Move(info.FullName, path);
        //        return;
        //    }

        //    long storageIdx = 0;
        //    var guidName = await _service.UserFilesInsertAsync(info, relativePath, length, DateTime.Now, "Attachment", storageIdx, originDbentityCode, doc.ParentDocId.Value, string.Empty, 6);
        //    attach = new MedDocAttach
        //    {
        //        Id = 0,
        //        FileGId = guidName,
        //        MedDocId = doc.Id
        //    };
        //    await _service.InsertMedDocAttach(attach);
        //}

        //private string GetRelativePath()
        //{
        //    var userId = _accessor.HttpContext?.Items["Id"];
        //    var sessionId = _accessor.HttpContext?.Items["SessionId"];
        //    var ownerId = Convert.ToInt64(_accessor.HttpContext?.Session.GetString("subjectId"));

        //    if (userId == null || sessionId == null) throw new ArgumentNullException($"No user or session id found");


        //    string userHashCode = ((uint)userId.ToString().GetHashCode()).ToString();
        //    string ownerHashCode = ((uint)ownerId.ToString().GetHashCode()).ToString();

        //    DateTime today = DateTime.Today;
        //    const int monthNumberDirNameLength = 2;

        //    return Path.Combine(ownerHashCode, userHashCode, today.Year.ToString(), today.Month.ToString().PadLeft(monthNumberDirNameLength, '0'));
        //}

        public async Task SaveMedDataAsPdf<TEntity>(Guid medDataId) where TEntity: class, IGenericEntity<Guid>
        {
            //var user = _activeUserStorageWithHttpContext.GetCurrentUser();
            //var user = _userService.GetCurrentUserInfo();
            //var doc = (await _dataService.GetEntityAsync<TemplateDocument>(d => d.Id == medDataId)).SingleOrDefault();
            //var appointment = (await _dataService.GetEntityAsync<TEntity>(a => a.Id == doc.EntityId)).Single();

            //var consultationSubTypeId = (await _repository.All<AppointmentSubType>().FromCacheAsync())
            //    .Where(a => a.Code.Equals("Consultation")).Select(a => a.Id).First();

            //var oldFile = await _repository.SingleAsync<FileDbModel>(f => f.EntityRecordId == appointment.Id && f.Name == "Медичні_Дані.pdf");

            //if (oldFile != null)
            //{
            //    var fileName = $"{oldFile.GId}{oldFile.Extension}";
            //    using (var file = await GetMedDataFile(fileName, (long)doc.Id, appointment))
            //    {
            //        var defaultPath = _configuration["StorageSettings:Path"];
            //        var relativePath = _filesService.GetRelativePath(user.Id, (long)user.OwnerId);
            //        var fullPath = Path.Combine(defaultPath, relativePath, fileName);

            //        if (!Directory.Exists(Path.Combine(defaultPath, relativePath)))
            //            Directory.CreateDirectory(Path.Combine(defaultPath, relativePath));

            //        oldFile.Size = file.Length;
            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }

            //        await _repository.SaveAsync();
            //        return;
            //    }
            //}

            //if (appointment.SubTypeId == consultationSubTypeId)
            //{
            //    var guid = Guid.NewGuid();
            //    var fileName = $"{guid.ToString()}.pdf";

            //    using (var file = await GetMedDataFile(fileName, (long)doc.Id, appointment))
            //    {
            //        var defaultPath = _configuration["StorageSettings:Path"];
            //        var relativePath = _filesService.GetRelativePath(user.Id, (long)user.OwnerId);
            //        var fullPath = Path.Combine(defaultPath, relativePath, fileName);

            //        if (!Directory.Exists(Path.Combine(defaultPath, relativePath)))
            //            Directory.CreateDirectory(Path.Combine(defaultPath, relativePath));

            //        var size = file.Length;
            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }

            //        var fileEntity = new FileDbModel
            //        {
            //            GId = guid,
            //            Name = "Медичні_Дані.pdf",
            //            Extension = ".pdf",
            //            TypeId = 1,
            //            Size = size,
            //            LocalPath = Path.Combine(relativePath, fileName),
            //            CreateDateUtc = DateTime.UtcNow,
            //            ModifiedDateUtc = DateTime.UtcNow,
            //            OwnerId = (long)user.OwnerId,
            //            EntityId = 9,
            //            EntityRecordId = appointment.Id,
            //            SubTypeId = 1,
            //            StorageIdx = 0
            //        };
            //        _repository.Create(fileEntity);
            //        await _repository.SaveAsync();

            //        _repository.Create(new UserFile
            //        {
            //            UserId = appointment.PersonId.ToString(),
            //            FileId = fileEntity.Id
            //        });
            //        await _repository.SaveAsync();
            //    }
            //}
            await Task.FromException(new NotImplementedException());
        }

        //private async Task<MemoryStream> GetMedDataFile(string fileName, long docDataId, Appointment appointment)
        //{
        //    var templateDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates");

        //    var sourceTemplatePath = Path.Combine(templateDirectoryPath, "MedData.docx");


        //    var card = await (from per in _repository.All<Person>().Where(x => x.Id == appointment.PersonId)

        //                      select new
        //                      {
        //                          FullName = per.FullName,
        //                          PatientDOB = per.DOB
        //                      }).FirstOrDefaultAsync();

        //    //TemplatePropList(sourceTemplatePath, out var templProps);
        //    //templProps = templProps.ToArray();

        //    if (!Directory.Exists(_configuration["DtmSettings:tmpTemplatePath"]))
        //        Directory.CreateDirectory(_configuration["DtmSettings:tmpTemplatePath"]);
        //    var destinationTemplatePath = Path.Combine(_configuration["DtmSettings:tmpTemplatePath"], fileName);

        //    if (File.Exists(destinationTemplatePath))
        //        File.Delete(destinationTemplatePath);

        //    File.Copy(sourceTemplatePath, destinationTemplatePath);

        //    var valuesToFill = new Content();
        //    var dtmList = new ListContent("DtmList");
        //    valuesToFill.Fields.Add(new FieldContent("PatientName", card.FullName.ToString()));
        //    valuesToFill.Fields.Add(new FieldContent("PatientDOB", card.PatientDOB.HasValue ? card.PatientDOB.Value.ToShortDateString() : ""));
        //    valuesToFill.Fields.Add(new FieldContent("DocumentDate", appointment.CreateDateUtc.ToString("dd.MM.yyyy")));
        //    var dtmDocData = await (
        //        from d in _repository.All<DocDataDbModel>()
        //        join dss in _repository.All<DocumentDbModel>() on d.DocumentId equals dss.Id
        //        join dt in _repository.All<DocTemplateElementsDbModel>() on d.TemplateElementId equals dt.Id
        //        where dss.Id == docDataId && dss.EntityTypeCode == "Appointments"
        //        select new DocDataVm
        //        {
        //            Id = d.Id, // DocData
        //            TemplateElementId = d.TemplateElementId,
        //            Value = d.Value,
        //            OriginDbId = d.OriginDbId,
        //            OriginDbRecordId = d.OriginDbRecordId,
        //            Order = dt.OrderNumber.ToString(),
        //            ParentId = dt.ParentId, // DocTempElements fields
        //            TemplateId = dt.TemplateId,
        //            GlobalElementId = dt.GlobalElementId,
        //            OrderNumber = dt.OrderNumber,
        //            ElementTypeCode = dt.ElementTypeCode,
        //            ControlTypeCode = dt.ControlTypeCode,
        //            Code = dt.Code,
        //            Name = dt.Name,
        //            Note = dt.Note,
        //            RecordStateCode = dt.RecordStateCode
        //        }).OrderBy(dd => dd.OrderNumber).ToListAsync();

        //    foreach (var d in dtmDocData)
        //    {
        //        var checkInnerEl = dtmDocData.Where(x => x.ParentId == d.TemplateElementId).Select(x => x.Value)
        //            .Except(new[] { "{\"ops\":[{\"insert\":\"\\n\"}]}", "", null }).ToList();

        //        if (d.ControlTypeCode == "SECTOR" && checkInnerEl.Count > 0)
        //        {
        //            dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name}"),
        //                new FieldContent("paragraphValue", $": ")));
        //        }

        //        if (string.IsNullOrWhiteSpace(d.Value)) continue;
        //        var value = _dtmDocManager.NormilizeDtmString(d.Value, d.ControlTypeCode);

        //        if (d.ParentId == null && dtmDocData.Where(x => x.Id == d.Id).Select(x => x.Value).Any(x => x != "{\"ops\":[{\"insert\":\"\\n\"}]}"))
        //        {
        //            dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name}"),
        //                new FieldContent("paragraphValue", $": {value}")));
        //        }
        //        else
        //        {
        //            if (value == "\r\n") continue;
        //            var parentName = dtmDocData.FirstOrDefault(dd => dd.TemplateElementId == d.ParentId)?.Name;
        //            var item = dtmList.Items.FirstOrDefault(i => i.Fields.First(f => f.Name.Equals("paragraph")).Value.Equals(parentName));
        //            item?.AddNestedItem(new ListItemContent(new FieldContent("label", d.Name ?? string.Empty),
        //                new FieldContent("value", value ?? string.Empty)));
        //        }
        //    }

        //    if (dtmList.Items != null)
        //    {
        //        valuesToFill.Lists.Add(dtmList);
        //    }

        //    using (var destinationTemplateStream = new FileStream(destinationTemplatePath, FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        using (var outputDocument = new TemplateProcessor(destinationTemplateStream))
        //        {
        //            outputDocument.SetRemoveContentControls(true);
        //            outputDocument.FillContent(valuesToFill);
        //            outputDocument.SaveChanges();   //document save in .docx in destinationTemplatePath, so just take it

        //            var resultStream = new MemoryStream();
        //            var wordDocument = new Aspose.Words.Document(destinationTemplateStream);
        //            wordDocument.Save(resultStream, SaveFormat.Pdf);
        //            resultStream.Seek(0, SeekOrigin.Begin);
        //            return resultStream;
        //        }
        //    }
        //}

        //public async Task<MemoryStream> generatePdf_test(long id, string ownerId, string fileType = "pdf")
        //{
        //    _repository.ChangeTracker(QueryTrackingBehavior.NoTracking);
        //    var card = await (from md in _repository.All<MedDoc>().Where(md => md.Id == id)
        //                      join docTypes in _repository.All<DocType>() on md.ParentDocTypeId equals docTypes.Id into docTypesGroup
        //                      from docTypes in docTypesGroup.DefaultIfEmpty()
        //                      join appointments in _repository.All<Appointment>() on md.ParentDocId equals appointments.Id into appGroup
        //                      from appointments in appGroup.DefaultIfEmpty()
        //                      join s in _repository.All<ScheduleSlot>() on appointments.Id equals s.AppointmentId into sGroup
        //                      from s in sGroup.DefaultIfEmpty()
        //                      join extRequests in _repository.All<ExtRequest>() on md.ParentDocId equals extRequests.Id into extRequestGroup
        //                      from extRequests in extRequestGroup.DefaultIfEmpty()
        //                      join patientPersons in _repository.All<Person>() on md.PatientId equals patientPersons.Id into patGroup
        //                      from patientPersons in patGroup.DefaultIfEmpty()
        //                      join employeeCards in _repository.All<EmployeeCard>() on md.EmployeeCardId equals employeeCards.Id into employeeCardsGroup
        //                      from employeeCards in employeeCardsGroup.DefaultIfEmpty()
        //                      join employeePersons in _repository.All<Person>() on employeeCards.PersonId equals employeePersons.Id into emplGroup
        //                      from employeePersons in emplGroup.DefaultIfEmpty()
        //                      join managerPersons in _repository.All<Person>() on md.ManagerEmployeeId equals managerPersons.Id into managGroup
        //                      from managerPersons in managGroup.DefaultIfEmpty()
        //                      join departments in _repository.All<Department>() on employeeCards.DepartmentId equals departments.Id into depGroup
        //                      from departments in depGroup.DefaultIfEmpty()
        //                      join departmentsLinks in _repository.All<DepartmentLink>() on departments.Id equals departmentsLinks.DepartmentId into depLinkGroup
        //                      from departmentsLinks in depLinkGroup.DefaultIfEmpty()
        //                      join highDepartments in _repository.All<Department>() on departmentsLinks.ParentDepartmentId equals highDepartments.Id into highDepGroup
        //                      from highDepartments in highDepGroup.DefaultIfEmpty()
        //                      select new
        //                      {
        //                          ParentDocTypeCode = docTypes != null ? docTypes.Code : null,
        //                          ParentDocId = md != null ? md.ParentDocId : null,
        //                          ActualStartDateTimeUtc = s != null ? s.TimeFrom : (DateTime?)null,
        //                          OwnerId = md != null ? md.OwnerId : (long?)null,
        //                          DocTypeId = md != null ? md.DocTypeId : (long?)null,
        //                          RegDateUtc = md != null ? md.RegDateUtc : null,
        //                          DtmTemplateCode = md != null ? md.DtmTemplateCode : null,
        //                          DtmTemplateName = md != null ? md.DtmTemplateName : null,
        //                          PatientFullName = patientPersons != null ? patientPersons.FullName : null,
        //                          PatientDOB = patientPersons != null ? patientPersons.DOB : null,
        //                          EmployeeId = employeePersons != null ? (long?)employeePersons.Id : null,
        //                          EmployeeFullName = employeePersons != null ? employeePersons.FullName : null,
        //                          EmployeeShortName = employeePersons != null ? employeePersons.ShortName : null,
        //                          DepartmentId = employeeCards != null ? employeeCards.DepartmentId : null,
        //                          EmployeeDepartmentFullName = departments != null ? departments.FullName : null,
        //                          EmployeeHighDepartmentFullName = highDepartments != null ? highDepartments.FullName : null,
        //                          ManagerEmployeeId = md != null ? md.ManagerEmployeeId : null,
        //                          ManagerEmployeeName = managerPersons != null ? managerPersons.ShortName : null
        //                      }).FirstOrDefaultAsync();

        //    //var templateDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates");
        //    var templateDirectoryPath = _configuration.GetValue<string>("DtmSettings:TemplatesPath");


        //    var sourceTemplatePath = Path.Combine(templateDirectoryPath, $"{card.DtmTemplateCode}.docx");

        //    TemplatePropList(sourceTemplatePath, out var templProps);
        //    templProps = templProps.ToArray();

        //    if (!Directory.Exists(_configuration["DtmSettings:tmpTemplatePath"]))
        //        Directory.CreateDirectory(_configuration["DtmSettings:tmpTemplatePath"]);
        //    var destinationTemplatePath = Path.Combine(_configuration["DtmSettings:tmpTemplatePath"],
        //                                               $"{card.ParentDocTypeCode}{id}.docx");

        //    File.Delete(destinationTemplatePath);
        //    File.Copy(sourceTemplatePath, destinationTemplatePath);

        //    var medDocData = await _dtmService.GetDocumentCard((long)card.ParentDocId, true, card.ParentDocTypeCode);

        //    IDictionary<string, string> paramsList = new Dictionary<string, string>
        //    {
        //        { "OriginDbRecordId", id.ToString() },
        //        { "EntityTypeCode", "MedDocs" }
        //    };

        //    List<DocDataVm> dtmDocData = await _dtmService.GetDocDataList(paramsList);

        //    //var ownerId = _accessor.HttpContext.Session.GetString("subjectId");
        //    var orgInfo = await _repository.All<DataLayer.Models.DbModelsOrgs.Organization>().SingleAsync(o => o.Id == Convert.ToInt64(ownerId));

        //    var valuesToFill = new Content();
        //    //if (!string.IsNullOrWhiteSpace(base64File))
        //    //    valuesToFill.Images.Add(new ImageContent("Logo", Convert.FromBase64String(base64File)));
        //    valuesToFill.Fields.Add(new FieldContent("ClinicName", orgInfo?.OfficialName ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("DepartmentName", card?.EmployeeHighDepartmentFullName?.ToUpper() ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("CompartmentName", card?.EmployeeDepartmentFullName?.ToUpper() ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("Adress", $"{orgInfo?.TaxCode ?? string.Empty}, {orgInfo?.ResAddress ?? string.Empty}"));
        //    valuesToFill.Fields.Add(new FieldContent("Email", orgInfo?.Email ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("Phone", orgInfo?.Phone ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("PatientName", card?.PatientFullName ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("PatientDOB", card?.PatientDOB?.ToShortDateString() ?? string.Empty));

        //    void DtmDataFunc(IEnumerable<string> temProps, string docDataCode)
        //    {
        //        if (temProps.Any(tp => tp == docDataCode))
        //        {
        //            var element = medDocData.DocDataList.FirstOrDefault(d => d.Code.Equals(docDataCode));
        //            if (element != null)
        //            {
        //                var value = _dtmDocManager.NormilizeDtmString(element.Value, element.ControlTypeCode);
        //                valuesToFill.Fields.Add(new FieldContent(docDataCode, value ?? string.Empty));
        //                return;
        //            }

        //            valuesToFill.Fields.Add(new FieldContent(docDataCode, string.Empty));
        //        }
        //    }

        //    if (medDocData != null)
        //    {
        //        DtmDataFunc(templProps, "ReferringDiagnosis");
        //        DtmDataFunc(templProps, "ResearchArea");
        //        DtmDataFunc(templProps, "ContrastEnhancement");
        //        DtmDataFunc(templProps, "EED");
        //    }
        //    else
        //    {
        //        if (templProps.Any(tp => tp == "ReferringDiagnosis")) valuesToFill.Fields.Add(new FieldContent("ReferringDiagnosis", string.Empty));
        //        if (templProps.Any(tp => tp == "ResearchArea")) valuesToFill.Fields.Add(new FieldContent("ResearchArea", string.Empty));
        //        if (templProps.Any(tp => tp == "ContrastEnhancement")) valuesToFill.Fields.Add(new FieldContent("ContrastEnhancement", string.Empty));
        //        if (templProps.Any(tp => tp == "EED")) valuesToFill.Fields.Add(new FieldContent("EED", string.Empty));
        //    }

        //    valuesToFill.Fields.Add(new FieldContent("ResearchDate", card?.ActualStartDateTimeUtc?.ToShortDateString() ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("DoctorName", card?.EmployeeShortName ?? string.Empty));
        //    valuesToFill.Fields.Add(new FieldContent("DocumentDate", card?.RegDateUtc?.ToString("dd.MM.yyyy") ?? string.Empty));

        //    if (card.ManagerEmployeeId.HasValue && card.ManagerEmployeeId.Value != card.EmployeeId)
        //    {
        //        valuesToFill.Fields.Add(new FieldContent("ManagerDoctor", "Керівник:"));
        //        valuesToFill.Fields.Add(new FieldContent("ManagerDoctorName", card?.ManagerEmployeeName ?? string.Empty));
        //    }

        //    var dtmList = new ListContent("DtmList");
        //    foreach (var d in dtmDocData)
        //    {
        //        var checkInnerEl = dtmDocData.Where(x => x.ParentId == d.TemplateElementId).Select(x => x.Value)
        //            .Except(new[] { "{\"ops\":[{\"insert\":\"\\n\"}]}", "", null }).ToList();

        //        if (d.ControlTypeCode == "SECTOR" && checkInnerEl.Count > 0)
        //        {
        //            dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name}"),
        //                                                new FieldContent("paragraphValue", $": ")));
        //        }

        //        if (string.IsNullOrWhiteSpace(d.Value)) continue;
        //        var value = _dtmDocManager.NormilizeDtmString(d.Value, d.ControlTypeCode);

        //        if (d.ParentId == null && dtmDocData.Where(x => x.Id == d.Id).Select(x => x.Value).Any(x => x != "{\"ops\":[{\"insert\":\"\\n\"}]}"))
        //        {
        //            dtmList.AddItem(new ListItemContent(new FieldContent("paragraph", $"{d.Name ?? string.Empty}"),
        //                                                new FieldContent("paragraphValue", $": {value ?? string.Empty}")));
        //        }
        //        else
        //        {
        //            if (value == "\r\n") continue;
        //            var parentName = dtmDocData.FirstOrDefault(dd => dd.TemplateElementId == d.ParentId)?.Name;
        //            var item = dtmList.Items.FirstOrDefault(i => i.Fields.First(f => f.Name.Equals("paragraph")).Value.Equals(parentName));
        //            item?.AddNestedItem(new ListItemContent(new FieldContent("label", d.Name ?? string.Empty),
        //                                                    new FieldContent("value", value ?? string.Empty)));
        //        }
        //    }

        //    if (dtmList.Items != null)
        //    {
        //        valuesToFill.Lists.Add(dtmList);
        //    }

        //    using (var destinationTemplateStream = new FileStream(destinationTemplatePath, FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        using (var outputDocument = new TemplateProcessor(destinationTemplateStream))
        //        {
        //            outputDocument.SetRemoveContentControls(true);
        //            outputDocument.FillContent(valuesToFill);
        //            outputDocument.SaveChanges();   //document save in .docx in destinationTemplatePath, so just take it

        //            var resultStream = new MemoryStream();
        //            if (fileType.Equals("docx"))
        //            {
        //                destinationTemplateStream.Seek(0, SeekOrigin.Begin);
        //                await destinationTemplateStream.CopyToAsync(resultStream);
        //            }
        //            else
        //            {
        //                var wordDocument = new Aspose.Words.Document(destinationTemplateStream);
        //                wordDocument.Save(resultStream, SaveFormat.Pdf);
        //            }
        //            resultStream.Seek(0, SeekOrigin.Begin);
        //            return resultStream;
        //        }
        //    }
        //}

        //private async Task<MemoryStream> generatePdf(long id)
        //{
        //    IEnumerable<string> templProps;

        //    var templateDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, "Templates");
        //    var template = Path.Combine(templateDirectory, "MedDocTemplateGeneric.docx");

        //    TemplatePropList(template, out templProps);
        //    var valuesToFill = new Content();
        //    MedDocsVm card = await _service.MedDocsCardAsync(id);
        //    Type docType = card.GetType();
        //    PropertyInfo[] cardFields = docType.GetProperties();
        //    foreach (PropertyInfo prop in cardFields)
        //    {
        //        if (!templProps.Any(p => p == prop.Name))
        //            continue;
        //        if (prop.PropertyType == typeof(DateTime?))
        //        {
        //            DateTime? value = (DateTime?)prop.GetValue(card);
        //            String str = value.HasValue ? value.Value.ToString("dd.MM.yyyy") : String.Empty;
        //            valuesToFill.Fields.Add(new FieldContent(prop.Name, str));
        //        }
        //        if (prop.PropertyType == typeof(string))
        //        {
        //            string str = (string)prop.GetValue(card);
        //            valuesToFill.Fields.Add(new FieldContent(prop.Name, str ?? String.Empty));
        //        }
        //        if (IsNumericType(prop.PropertyType))
        //        {
        //            string str = (string)prop.GetValue(card).ToString();
        //            valuesToFill.Fields.Add(new FieldContent(prop.Name, str ?? String.Empty));
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(card.PatientAge) && templProps.Any(p => p == "PatientAge"))
        //    {
        //        valuesToFill.Fields.Add(new FieldContent("PatientAge", card.PatientAge));
        //    }

        //    IDictionary<string, string> paramsList = new Dictionary<string, string>
        //    {
        //        { "OriginDbRecordId", id.ToString() },
        //        { "EntityTypeCode", "MedDocs" }
        //    };

        //    List<DocDataVm> dtmDocData = await _dtmService.GetDocDataList(paramsList);

        //    var dtmList = new ListContent("DtmList");
        //    foreach (var d in dtmDocData)
        //    {
        //        var value = d.Value;
        //        if ((d.ControlTypeCode == "TEXT" || d.ControlTypeCode == "LEXTREE") && !String.IsNullOrEmpty(value))
        //        {
        //            value = parseQuillDelta(value);
        //        }
        //        if (templProps.Any(p => p == d.Name))
        //        {
        //            valuesToFill.Fields.Add(new FieldContent(d.Name, value ?? String.Empty));
        //        }

        //        if (d.ControlTypeCode != "SECTOR")
        //        {
        //            var parentName = dtmDocData.First(dd => dd.TemplateElementId == d.ParentId).Name;
        //            var item = dtmList.Items.First(i => (i.Fields.First(f => f.Name.Equals("paragraph")).Value).Equals(parentName));

        //            item.AddNestedItem(new ListItemContent(new FieldContent("label", d.Name ?? String.Empty),
        //                                                   new FieldContent("value", value ?? String.Empty)));
        //        }
        //        else
        //            dtmList.AddItem(new ListItemContent("paragraph", d.Name));
        //    }

        //    if (dtmList.Items != null)
        //    {
        //        valuesToFill.Lists.Add(dtmList);
        //    }

        //    var sourcePath = @"C:\Users\ukrge\Desktop\InputTemplate.docx";
        //    var pdfPath = @"C:\Users\ukrge\Desktop\pdf.pdf";
        //    using (var s = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        //    {
        //        var sr = new StreamReader(s);
        //        var x = sr.CurrentEncoding;
        //        using (var f = new FileStream(pdfPath, FileMode.Create))
        //        {
        //            var wordDocument = new Aspose.Words.Document(s);
        //            wordDocument.Save(f, SaveFormat.Pdf);
        //        }
        //    }

        //    using (var templateStream = new FileStream(template, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var outputStream = new MemoryStream())
        //        {

        //            templateStream.CopyTo(outputStream);
        //            using (var outputDocument = new TemplateProcessor(outputStream))
        //            {
        //                outputDocument.SetRemoveContentControls(true);
        //                outputDocument.FillContent(valuesToFill);
        //                outputDocument.SaveChanges();

        //                var resultStream = new MemoryStream();
        //                var wordDocument = new Aspose.Words.Document(outputStream);
        //                wordDocument.Save(resultStream, SaveFormat.Pdf);
        //                return resultStream;
        //            }
        //        }
        //    }

        //}

        //private string parseQuillDelta(string value)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    JObject json = JObject.Parse(value);
        //    foreach (var q in json["ops"].AsJEnumerable())
        //    {
        //        if (q.SelectToken("insert") != null)
        //        {

        //            if (q.SelectToken("insert").ToString() == "\n")
        //            {
        //                sb.Append(System.Environment.NewLine);
        //                continue;
        //            }
        //            var str = q.SelectToken("insert").ToString().Replace("\n", Environment.NewLine);
        //            sb.Append(str);
        //        }
        //    }
        //    return sb.ToString();
        //}

        private void TemplatePropList(string templatePath, out IEnumerable<string> props)
        {
            using (var wordDocument = WordprocessingDocument.Open(templatePath, true))
            {
                var mainPart = wordDocument.MainDocumentPart;
                props = mainPart.Document.Body.Descendants<SdtProperties>().Select(p => p.GetFirstChild<Tag>()?.Val.ToString()).Where(w => w != null).ToList();
            }
        }


        private static bool IsNumericType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }

        public async Task<DocumentVm> DocumentUpdateAsync(TemplateDocumentDto dtoModel)
        {
            //var userInfo = _activeUserStorageWithHttpContext.GetCurrentUser();
            //var userInfo = _userService.GetCurrentUserInfo();
            var updateAllowed = true;
            //var isManager = false;
            //long? appointmentResponsibleEmployeeId = null;
            //if (dtoModel.EntityTypeCode.Equals("MedDocs"))
            //{
            //    var result = await (from m in _repository.All<MedDoc>().Where(m => m.Id.Equals(dtoModel.OriginDbRecordId))
            //                        join a in _repository.All<Appointment>() on m.AppointmentId equals a.Id
            //                        select new
            //                        {
            //                            m.EmployeeCardId,
            //                            a.Id,
            //                            a.EmployeeId,
            //                            a.AppointmentStateId,
            //                            a.CompletionDateUtc
            //                        }).FirstOrDefaultAsync();

            //    if (result == null)
            //        throw new ArgumentNullException($"MedDoc with id[{dtoModel.OriginDbRecordId}] not found.");

            //    appointmentResponsibleEmployeeId = result.EmployeeId;
            //    if (result.AppointmentStateId == StateCodesHelper.AppointmentStates.Cancelled.Id)
            //        updateAllowed = false;

            //    if (result.AppointmentStateId == StateCodesHelper.AppointmentStates.Completed.Id)
            //    {
            //        if (result.EmployeeId.Equals(userInfo.PersonId))
            //        {
            //            if (!result.CompletionDateUtc.HasValue)
            //                updateAllowed = false;
            //            else if (result.CompletionDateUtc.Value.AddDays(Convert.ToInt64(_configuration["AppointmentDataChangePeriod"])) < DateTime.UtcNow)
            //                updateAllowed = false;
            //        }
            //        else
            //        {
            //            updateAllowed = false;
            //        }

            //        isManager = userInfo.Session.MisUserRoles.Contains("Керівник_Охматд");
            //        if (isManager)
            //            updateAllowed = true;
            //    }
            //}

            if (updateAllowed)
            {
                var document = await _dtmService.DocumentUpdate(dtoModel);

                //if (isManager && !appointmentResponsibleEmployeeId.Equals(userInfo.PersonId))
                //{
                //    var updateMedDoc = await _repository.All<MedDoc>()
                //        .Where(m => m.Id.Equals(dtoModel.OriginDbRecordId))
                //        .UpdateFromQueryAsync(m => new MedDoc
                //        {
                //            ManagerEmployeeId = userInfo.PersonId
                //        });
                //}

                return document;
            }

            throw new Exception("Update not allowed.");
        }
    }
}

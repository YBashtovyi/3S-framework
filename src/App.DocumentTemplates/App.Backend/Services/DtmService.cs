using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using App.DocumentTemplates.Dto;
using App.DocumentTemplates.Extensions;
using App.DocumentTemplates.Models;
using App.DocumentTemplates.ReportModels;
using App.DocumentTemplates.ViewModels;
using Core.Base.Data;
using Core.Common.Extensions;
using Core.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App.DocumentTemplates.Services
{
    public class DtmService //: IDtmService
    {
        //private readonly IObjectMapper _mapper;
        private readonly MappingService _mapper;
        private readonly DtmDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly OwnerService _ownerService;
        public ICommonDataService DataService { get; }

        public DtmService(//IObjectMapper mapper,
                          MappingService mapper,
                          DtmDbContext dbContext,
                          IMemoryCache cache,
                          OwnerService ownerService,
                          ICommonDataService dataService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _memoryCache = cache;
            _ownerService = ownerService;
            DataService = dataService;
        }

        public async Task<DocTemplateVm[]> GetDocTemplatesListByCodes(string[] codesList)
        {
            //var recordList = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Where(x => x.RecordStateCode == "N" && codesList.Contains(x.Code)).ToArrayAsync();
            var recordList = await _dbContext.DocumentTemplates.Where(x => codesList.Contains(x.Code)).ToArrayAsync();
            return _mapper.Map<DocTemplateVm[]>(recordList);
        }

        public async Task<DocTemplateVm> GetDocTemplatesListByParent(string parentName)
        {
            //var recordList = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Include(x => x.DocTemplates)
            //    .Where(x => x.Parent.Name == parentName).ToArrayAsync();
            var recordList = await _dbContext.DocumentTemplates.Include(x => x.Templates)
                .Where(x => x.Parent.Caption == parentName).ToArrayAsync();
            return _mapper.Map<DocTemplateVm>(recordList);
        }

        public async Task<DocTemplateVm> GetDocTemplatesCard(Guid id)
        {
            //var record = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).FirstOrDefaultAsync(x => x.Id == id);
            var record = await _dbContext.DocumentTemplates.FirstOrDefaultAsync(x => x.Id == id);
            var entry = _dbContext.Entry(record);
            entry.Collection(e => e.TemplateElements).Query().OrderBy(x => x.OrderNumber).Load();
            return _mapper.Map<DocTemplateVm>(record);

        }

        public async Task<DocTemplateVm[]> GetDocTemplatesList()
        {
            //var recordList = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Where(x => x.RecordStateCode == "N" && x.ClassShortCode == "T").ToArrayAsync();
            var recordList = await _dbContext.DocumentTemplates.Where(x => x.ClassShortCode == "T").ToArrayAsync();
            return _mapper.Map<DocTemplateVm[]>(recordList);
        }

        public async Task<DocTemplateVm[]> GetDocTemplatesListDicom()
        {
            //var recordList = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Where(x => x.RecordStateCode == "N" && x.ClassShortCode == "T").ToArrayAsync();
            var recordList = await _dbContext.DocumentTemplates.Where(x => x.ClassShortCode == "T").ToArrayAsync();
            return _mapper.Map<DocTemplateVm[]>(recordList);
        }

        public async Task<DocTemplateVm[]> GetDocTemplatesListHeart()
        {
            //var recordList = await _dbContext.DocumentTemplates.Where(x => x.RecordStateCode == "N" && x.ClassShortCode == "T").ToArrayAsync();
            var recordList = await _dbContext.DocumentTemplates.Where(x => x.ClassShortCode == "T").ToArrayAsync();
            return _mapper.Map<DocTemplateVm[]>(recordList);
        }

        public async Task<DocTemplateVm[]> GetDocTemplates(IDictionary<string, string> paramsList)
        {
            Expression<Func<DocumentTemplate, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            foreach (var param in paramsList)
            {
                switch (param.Key)
                {
                    case "RootName":
                        predicate = predicate.And(p => p.Parent.Caption == param.Value);
                        break;
                    case "SearchText":
                        predicate = predicate.And(p => p.Code.Contains(param.Value));
                        break;
                    case "ParentId":
                        var parentId = param.Value.GetValueOrNull<Guid>();
                        predicate = predicate.And(p => p.ParentId == parentId);
                        break;
                    case "ClassShortCode":
                        var shortCode = param.Value;
                        predicate = predicate.And(p => p.ClassShortCode == shortCode);
                        break;
                }
            }

            //var result = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Where(predicate).ToArrayAsync();
            var result = await _dbContext.DocumentTemplates.Where(predicate).ToArrayAsync();
            return _mapper.Map<DocTemplateVm[]>(result);
        }

        public async Task<DocTemplateVm[]> GetDocTemplatesTree()
        {
            //var templates = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId)
            //    .Where(p => p.ClassShortCode == "F" && (p.RecordStateCode.Equals("N") || p.RecordStateCode.Equals("P"))).ToArrayAsync();
            var templates = await _dbContext.DocumentTemplates.Where(p => p.ClassShortCode == "F").ToArrayAsync();
            DocTemplateVm[] result = _mapper.Map<DocTemplateVm[]>(templates);
            return result.Where(r => r.ParentId == null).ToArray();
        }

        public async Task<DocTemplateVm[]> GetDocTemplateParents(Guid? id, string rootName = null)
        {
            //var result = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).GetParentHierarchyAsync(x => x.Id == id, rootName);
            var result = await _dbContext.DocumentTemplates.GetParentHierarchyAsync(x => x.Id == id, rootName);
            return _mapper.Map<DocTemplateVm[]>(result);
        }

        public async Task<DocumentVm[]> GetDocParents(Guid? id)
        {
            //var result = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId).GetParentHierarchyAsync(x => x.Id == id);
            var result = await _dbContext.TemplateDocuments.GetParentHierarchyAsync(x => x.Id == id);
            return _mapper.Map<DocumentVm[]>(result);
        }

        public async Task<DocControlTypeVm[]> GetDocControlTypesList()
        {
            var recordList = await _dbContext.DocumentControlTypes.ToArrayAsync();
            return _mapper.Map<DocControlTypeVm[]>(recordList);
        }

        public async Task<DocTemplateElementVm[]> GetDocTemplateElementsList(IDictionary<string, string> paramsList)
        {
            Expression<Func<DocumentTemplateElement, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            //predicate = paramsList.All(p => p.Key != "RecordStateCode") ? predicate.And(p => p.RecordStateCode.Equals("N")) : predicate.And(p => p.RecordStateCode != null);
            foreach (var param in paramsList.Where(kv => !string.IsNullOrWhiteSpace(kv.Value))) //TODO : convert to LINQ, investigate code
            {
                switch (param.Key)
                {
                    case "ServiceOwnerId":
                        var serviceOwnerId = Convert.ToInt64(param.Value);
                        predicate = predicate.And(p => p.OwnerId.Equals(serviceOwnerId));
                        break;
                    case "ParentId":
                        var parentId = Convert.ToInt64(param.Value);
                        predicate = predicate.And(p => p.ParentId.Equals(parentId));
                        break;
                    case "TemplateId":
                        var tId = Convert.ToInt64(param.Value);
                        predicate = predicate.And(p => p.TemplateId.Equals(tId));
                        break;
                    //case "TemplateCode":
                    //    var templ = _dbContext.DocTemplates.FirstOrDefault(t => t.Code.Equals(param.Value));
                    //    predicate = predicate.And(p => p.TemplateId.Equals(templ.Id));
                    //    break;
                    case "TemplateCode":
                        predicate = (param.Value == "medData") ? predicate.And(p => p.Template.Code.Equals("Other"))
                            : predicate.And(p => p.Template.Code.Equals(param.Value));
                        break;
                    case "Name":
                        predicate = predicate.And(p => p.Caption.Contains(param.Value));
                        break;
                    case "Code":
                        predicate = predicate.And(p => p.Code.Contains(param.Value));
                        break;
                    case "OrderNumber":
                        var oNumber = Convert.ToInt64(param.Value);
                        predicate = predicate.And(p => p.OrderNumber.Equals(oNumber));
                        break;
                }
            }

            var res = await (
                //from item in _dbContext.DocumentTemplateElements.Include(d => d.Template).FilterOwner(_ownerService.OwnerId).Where(predicate)
                from item in _dbContext.DocumentTemplateElements.Include(d => d.Template).Where(predicate)
                select new DocTemplateElementVm
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    TemplateId = item.TemplateId,
                    GlobalElementId = item.GlobalElementId,
                    OrderNumber = item.OrderNumber,
                    ElementTypeCode = item.ElementTypeCode,
                    ControlTypeCode = item.ControlTypeCode,
                    Code = item.Code,
                    Caption = item.Caption,
                    Description = item.Description,
                    Config = item.Config,
                    RecordState = item.RecordState,
                    ValuesTreeId = item.ValuesTreeId
                }).ToArrayAsync();

            return res;
        }

        public async Task<DocumentVm> DocumentInsert(TemplateDocumentDto dtoModel)
        {
            try
            {
                var dbModel = _mapper.Map<TemplateDocument>(dtoModel);
                var clone = dbModel;
                var elements = dbModel.DocumentDataList;
                clone.DocumentDataList = null;
                clone.SetOwner(_ownerService.OwnerId);

                if (string.IsNullOrWhiteSpace(clone.Name))
                {
                    clone.Name = _dbContext.DocumentTemplates.FirstOrDefault(dt => dt.Id.Equals(clone.TemplateId) &&
                                                                            dt.OwnerId.Equals(_ownerService.OwnerId))?.Caption;
                }

                _dbContext.TemplateDocuments.Add(clone);
                await _dbContext.SaveChangesAsync();
                ProcDocElements(elements, clone.Id);
                clone.DocumentDataList = elements;

                _dbContext.Entry(clone).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                var vModel = _mapper.Map<DocumentVm>(clone);

                return vModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentVm DocumentPreviewInsert(TemplateDocumentDto dtoModel)
        {
            var dbModel = _mapper.Map<TemplateDocument>(dtoModel);
            _memoryCache.Set(dbModel.Id, dbModel);
            return _mapper.Map<DocumentVm>(dbModel);
        }

        public DocumentVm GetDocumentPreview(Guid id)
        {
            try
            {
                return _mapper.Map<DocumentVm>(_memoryCache.Get<TemplateDocument>(id));
            }
            catch (Exception)
            {
                return new DocumentVm();
            }
        }

        public async Task<DocumentVm> DocumentUpdate(TemplateDocumentDto dtoModel)
        {
            var dbModel = _mapper.Map<TemplateDocument>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.Entry(dbModel).State = EntityState.Modified;

            foreach (var dd in dbModel.DocumentDataList)
            {
                dd.SetOwner(_ownerService.OwnerId);
                _dbContext.Entry(dd).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();

            var vModel = _mapper.Map<DocumentVm>(dbModel);

            return vModel;
        }

        public async Task<Report4110[]> GetReport4110(DateTime? dateFrom, DateTime? dateTo, long? ownerId, string appointments)
        {
            // there was a sp call,
            // TODO: remake to reading sql file or to LINQ, see Report4110.sql
            //var p0 = dateFrom != null ? new SqlParameter("@p0", dateFrom) : new SqlParameter("@p0", DBNull.Value);
            //var p1 = dateTo != null ? new SqlParameter("@p1", dateTo) : new SqlParameter("@p1", DBNull.Value);
            //var p2 = ownerId != null ? new SqlParameter("@p2", ownerId) : new SqlParameter("@p2", DBNull.Value);
            //var p3 = appointments != null ? new SqlParameter("@p3", appointments) : new SqlParameter("@p3", DBNull.Value);
            //var res = await _dbContext.Set<Report4110>().AsNoTracking().FromSql("exec spReport4110 @DateFrom = @p0, @DateTo = @p1, @OwnerId = @p2, @Json = @p3", p0, p1, p2, p3).ToArrayAsync();

            //return res;
            return await Task.FromException<Report4110[]>(new NotImplementedException("GetReport4110 method is not implemented"));
        }

        //public async Task<ReportCabinetKT[]> GetReportCabinetKT(DateTime? dateFrom, DateTime? dateTo, Int32? ownerId, string appointments)
        //{

        //    var p0 = dateFrom != null ? new SqlParameter("@p0", dateFrom) : new SqlParameter("@p0", DBNull.Value);
        //    var p1 = dateTo != null ? new SqlParameter("@p1", dateTo) : new SqlParameter("@p1", DBNull.Value);

        //    var res = await _dbContext.Set<ReportCabinetKT>().AsNoTracking().FromSql("exec  spReportCabinetKT  @DateFrom = @p0, @DateTo = @p1").ToArrayAsync();
        //    return res;

        //}


        public async Task<Result> DocumentRemove(Guid id, bool isOrig = false)
        {
            //var docQuery = _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId);
            var docQuery = _dbContext.TemplateDocuments;
            TemplateDocument doc;
            if (isOrig)
            {
                doc = await docQuery.FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                doc = await docQuery.FirstOrDefaultAsync(x => x.Id == id);
            }

            if (doc == null)
            {
                return new Result { Success = false, Text = $"There is not  entity with id:{id}" };
            }

            try
            {
                _dbContext.TemplateDocuments.Remove(doc);
                await _dbContext.SaveChangesAsync();
                return new Result() { Success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The DELETE statement conflicted with the SAME TABLE REFERENCE constraint"))
                {
                    return new Result { Success = false, Text = "Видалення  неможливе, існують  зв'язки" };
                }
                return new Result { Success = false, Text = ex.Message };
            }
        }

        private void ProcDocElements(ICollection<DocumentData> elements, Guid docId)
        {
            foreach (var elm in elements)
            {
                elm.DocumentId = docId;
                elm.SetOwner(_ownerService.OwnerId);
            }
        }
        public async Task<DocumentVm[]> GetDocumentsTreeAsync()
        {
            //var docs = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId)
            // .Where(p => p.ClassShortCode == "F" && (p.RecordStateCode.Equals("N") || p.RecordStateCode.Equals("P"))).ToArrayAsync();
            var docs = await _dbContext.TemplateDocuments
             .Where(p => p.ClassShortCode == "F").ToArrayAsync();

            DocumentVm[] result = _mapper.Map<DocumentVm[]>(docs);
            return result.Where(r => r.ParentId == null).ToArray();
        }

        public async Task<DocumentVm[]> GetDocumentListAsync(IDictionary<string, string> paramsList)
        {
            Expression<Func<TemplateDocument, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            //predicate = predicate.And(p => (p.RecordStateCode.Equals("N") || p.RecordStateCode.Equals("P")));
            foreach (var param in paramsList)
            {
                //if (param.Value != null && string.IsNullOrEmpty(param.Value.Trim())) continue;
                switch (param.Key)
                {
                    case "ParentId":
                        var parentId = param.Value.GetValueOrNull<Guid>();
                        predicate = predicate.And(p => p.ParentId == parentId);
                        break;
                    case "SearchText":
                        predicate = predicate.And(p => p.Name.Contains(param.Value));
                        break;
                    case "ClassShortCode":
                        var shortCode = param.Value;
                        predicate = predicate.And(p => p.ClassShortCode == shortCode);
                        break;
                }
            }
            //var result = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId).Where(predicate).ToArrayAsync();
            var result = await _dbContext.TemplateDocuments.Where(predicate).ToArrayAsync();
            return _mapper.Map<DocumentVm[]>(result);
        }

        public async Task<DocumentVm> GetDocumentCard(Guid id, bool isOrigin = false, string entityTypeCode = null)
        {
            TemplateDocument record;
            if (isOrigin == false)
            {
                //record = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId).Include(t => t.Template).FirstOrDefaultAsync(x => x.Id == id);
                record = await _dbContext.TemplateDocuments.Include(t => t.Template).FirstOrDefaultAsync(x => x.Id == id);
            }

            else
            {
                //record = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId).Include(t => t.Template).FirstOrDefaultAsync(x => x.OriginDbRecordId == id && x.EntityTypeCode == entityTypeCode);
                record = await _dbContext.TemplateDocuments.Include(t => t.Template).FirstOrDefaultAsync(x => x.Id == id && x.EntityTypeCode == entityTypeCode);
            }

            if (record == null)
            {
                return null;
            }

            var entry = _dbContext.Entry(record);
            entry.Collection(e => e.DocumentDataList).Query().Include(x => x.TemplateElement).Load();
            return _mapper.Map<DocumentVm>(record);
        }

        public async Task<DocumentVm> TryGetAppointmentDocumentFromRequest(dynamic data)
        {
            if (data != null)
            {
                var id = data.GetType().GetProperty("Id").GetValue(data, null);
                var requestId = data.GetType().GetProperty("RequestId").GetValue(data, null);
                var externalRequestId = data.GetType().GetProperty("ExternalRequestId").GetValue(data, null);

                if (requestId != null)
                {
                    await CloneDocument((Guid)requestId, (Guid)id, "Requests", "Appointments");
                }
                else if (externalRequestId != null)
                {
                    await CloneDocument((Guid)externalRequestId, (Guid)id, "ExtRequests", "Appointments");
                }

                return await GetDocumentCard(id, true, "Appointments");
            }

            return null;
        }

        public async Task CloneDocument(Guid? originId, Guid? newOriginId, string entityTypeCode = null, string newEntityTypeCode = null)
        {
            //var doc = await _dbContext.TemplateDocuments.FilterOwner(_ownerService.OwnerId).Include(t => t.Template).FirstOrDefaultAsync(x => x.OriginDbRecordId == originId && x.EntityTypeCode == entityTypeCode);
            var doc = await _dbContext.TemplateDocuments.Include(t => t.Template).FirstOrDefaultAsync(x => x.Id == originId && x.EntityTypeCode == entityTypeCode);
            if (doc == null)
            {
                return;
            }

            var entry = _dbContext.Entry(doc);
            entry.Collection(e => e.DocumentDataList).Query().Include(x => x.TemplateElement).Load();
            var docDto = _mapper.Map<TemplateDocumentDto>(doc);
            docDto.Id = Guid.Empty;
            //docDto.OriginDbRecordId = newOriginId;
            docDto.EntityTypeCode = newEntityTypeCode;
            docDto.DocumentData = GetClonedElements(docDto.DocumentData, newOriginId);
            await DocumentInsert(docDto);
        }

        private IEnumerable<DocDataDto> GetClonedElements(IEnumerable<DocDataDto> docDataVms, Guid? originId)
        {
            foreach (var dd in docDataVms)
            {
                dd.Id = Guid.Empty;
                //dd.OriginDbRecordId = originId;
            }
            return docDataVms;
        }

        public async Task<DocTemplateElementVm> DocTemplateElementsInsert(DocTemplateElementDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplateElement>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplateElements.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplateElementVm>(dbModel);
            return vModel;
        }

        public async Task<DocTemplateElementValueTreeVm[]> ValuesTreeListAsync()
        {
            //var result = await _dbContext.DocumentTemplateElementValueTrees.AsNoTracking().FilterOwner(_ownerService.OwnerId).ToArrayAsync();
            var result = await _dbContext.DocumentTemplateElementValueTrees.AsNoTracking().ToArrayAsync();
            return _mapper.Map<DocTemplateElementValueTreeVm[]>(result);
        }

        public async Task<DocTemplateElementValueTreeVm> ValuesTreeInsertAsync(DocTemplateElementValueTreeDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplateElementValueTree>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplateElementValueTrees.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplateElementValueTreeVm>(dbModel);
            return vModel;
        }

        public async Task<DocTemplateElementVm> DocTemplateElementCopy(DocTemplateElementDto dtoModel, bool withValues = false)
        {
            var dbModel = await PrepareElementToCopy(dtoModel, withValues);
            _dbContext.DocumentTemplateElements.Add(dbModel);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DocTemplateElementVm>(dbModel);
        }

        public async Task<Result> DocTemplateElementCopyToTemplate(DocTemplateElementDto dtoModel, Guid templateId, bool withValues)
        {
            try
            {
                var dbModel = await PrepareElementToCopy(dtoModel, withValues, templateId);
                _dbContext.DocumentTemplateElements.Add(dbModel);
                await _dbContext.SaveChangesAsync();
                return new Result
                {
                    Success = true,
                    Text = "Ok"
                };
            }
            catch (Exception e)
            {
                return new Result
                {
                    Success = false,
                    Text = e.Message
                };
            }
        }

        private async Task<DocumentTemplateElement> PrepareElementToCopy(DocTemplateElementDto dtoModel, bool withValues = false, Guid? templateId = null)
        {
            var dbModel = _mapper.Map<DocumentTemplateElement>(dtoModel);
            dbModel.Id = Guid.Empty;
            dbModel.Caption += "_Copy";
            dbModel.Code += "_Copy";
            if (templateId.HasValue)
            {
                dbModel.TemplateId = templateId.Value;
                dbModel.Template = null;
            }
            dbModel.SetOwner(_ownerService.OwnerId);
            if (withValues == true)
            {
                var copyTree = new DocumentTemplateElementValueTree();
                if (dbModel.ValuesTreeId != null)
                {
                    DocumentTemplateElementValueTree localValuesTree = null;
                    try
                    {
                        //localValuesTree = await _dbContext.DocumentTemplateElementValueTrees.FilterOwner(_ownerService.OwnerId).Where(t => t.Id == dbModel.ValuesTreeId)
                        localValuesTree = await _dbContext.DocumentTemplateElementValueTrees.Where(t => t.Id == dbModel.ValuesTreeId)
                            .Select(x => new DocumentTemplateElementValueTree
                            {
                                Id = x.Id,
                                Code = x.Code,
                                CreatedBy = x.CreatedBy,
                                CreatedOn = x.CreatedOn,
                                TemplateElements = x.TemplateElements,
                                TemplateElementValues = FlatToHierarchy(_dbContext.DocumentTemplateElementValues.Where(_ => _.ValuesTreeId == x.Id).ToArray(), null),
                                ModifiedBy = x.ModifiedBy,
                                ModifiedOn = x.ModifiedOn,
                                Name = x.Name,
                                //OriginDbId = x.OriginDbId,
                                //OriginDbRecordId = x.OriginDbRecordId,
                                OwnerId = x.OwnerId,
                                RecordState = x.RecordState
                            }).FirstOrDefaultAsync();

                        // .Include(x => x.TemplateElementValues.Where(_ => _.ParentId == null));
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }

                    if (localValuesTree != null)
                    {
                        copyTree.Id = Guid.Empty;
                        copyTree.Code = localValuesTree.Code + "_Copy";
                        copyTree.Name = localValuesTree.Name + "_Copy";
                        copyTree.OwnerId = localValuesTree.OwnerId;
                        copyTree.RecordState = localValuesTree.RecordState;
                        copyTree.SetOwner(_ownerService.OwnerId);
                        copyTree.TemplateElementValues = PrepareValuesInternal(localValuesTree.TemplateElementValues, copyTree);
                    }
                    dbModel.ValuesTreeId = null;
                    dbModel.ValuesTree = copyTree;
                }
            }

            if (dbModel.TemplateElements.Count > 0)
            {
                dbModel.TemplateElements = await PrepareElements(dbModel.TemplateElements, withValues, dbModel);
            }

            return dbModel;
        }

        private async Task<ICollection<DocumentTemplateElement>> PrepareElements(ICollection<DocumentTemplateElement> elements, bool withValues = false, DocumentTemplateElement parent = null)
        {
            var list = new List<DocumentTemplateElement>();
            foreach (var element in elements)
            {
                var newElm = new DocumentTemplateElement
                {
                    Id = Guid.Empty,
                    Caption = element.Caption + "_Copy",
                    Code = element.Code + "_Copy",
                    ControlTypeCode = element.ControlTypeCode,
                    Description = element.Description,
                    ElementTypeCode = element.ElementTypeCode,
                    OwnerId = element.OwnerId,
                    Parent = parent,
                    ParentId = null,
                    RecordState = element.RecordState,
                    Template = null,
                    TemplateId = parent != null ? parent.TemplateId : element.TemplateId,
                    OrderNumber = element.OrderNumber
                };

                newElm.SetOwner(_ownerService.OwnerId);
                if (element.TemplateElements.Count > 0)
                {
                    newElm.TemplateElements = await PrepareElements(element.TemplateElements, withValues, newElm);
                }

                if (withValues == true)
                {
                    var copyTree = new DocumentTemplateElementValueTree();
                    if (element.ValuesTreeId != null)
                    {
                        var localValuesTree = await _dbContext.DocumentTemplateElementValueTrees.Where(t => t.Id == element.ValuesTreeId).Include(x => x.TemplateElementValues).FirstOrDefaultAsync();
                        if (localValuesTree != null)
                        {
                            copyTree.Id = Guid.Empty;
                            copyTree.Code = localValuesTree.Code + "_Copy";
                            copyTree.Name = localValuesTree.Name + "_Copy";
                            copyTree.OwnerId = localValuesTree.OwnerId;
                            copyTree.RecordState = localValuesTree.RecordState;
                            copyTree.TemplateElementValues = PrepareValuesInternal(localValuesTree.TemplateElementValues, copyTree);
                        }
                        newElm.ValuesTreeId = null;
                        newElm.ValuesTree = copyTree;
                    }
                }
                list.Add(newElm);
            }
            return list;
        }

        public async Task<DocTemplateElementValueTreeVm> ValuesTreeCopyAsync(DocTemplateElementValueTreeDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplateElementValueTree>(dtoModel);
            var local = _dbContext.DocumentTemplateElementValueTrees.Include(x => x.TemplateElementValues).FirstOrDefault(f => f.Id == dbModel.Id);
            dbModel.Id = Guid.Empty;
            if (local.TemplateElementValues.Count > 0)
            {
                dbModel.TemplateElementValues = PrepareValuesInternal(local.TemplateElementValues, dbModel);
            }
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplateElementValueTrees.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplateElementValueTreeVm>(dbModel);
            return vModel;
        }

        private ICollection<DocumentTemplateElementValue> FlatToHierarchy(ICollection<DocumentTemplateElementValue> list, Guid? parentId = null)
        {
            return (from x in list
                    where x.ParentId == parentId
                    select new DocumentTemplateElementValue
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Name = x.Name,
                        RecordState = x.RecordState,
                        ValueTypeCode = x.ValueTypeCode,
                        ContentValue = x.ContentValue,
                        TemplateElementValues = FlatToHierarchy(list, x.Id)
                    }).ToList();
        }

        private ICollection<DocumentTemplateElementValue> PrepareValuesInternal(ICollection<DocumentTemplateElementValue> values, DocumentTemplateElementValueTree model, DocumentTemplateElementValue parent = null)
        {
            var list = new List<DocumentTemplateElementValue>();
            foreach (var x in values)
            {
                if (x.Parent == null || parent != null)
                {
                    var copy = new DocumentTemplateElementValue
                    {
                        Id = Guid.Empty,
                        ParentId = null,
                        Parent = parent,
                        ValuesTree = model,
                        ValuesTreeId = null,
                        Name = x.Name,
                        RecordState = x.RecordState,
                        ValueTypeCode = x.ValueTypeCode,
                        ContentValue = x.ContentValue,
                        TemplateElementValues = (x.TemplateElementValues != null && x.TemplateElementValues.Count > 0) ? PrepareValuesInternal(x.TemplateElementValues, model, x) : x.TemplateElementValues
                    };
                    copy.SetOwner(_ownerService.OwnerId);
                    list.Add(copy);
                }
                //TODO:Implement deep copy helper

            }
            return list;
        }

        public async Task<DocTemplateElementValueVm[]> GetDocTemplateElementValuesList(IDictionary<string, string> paramsList)
        {
            Expression<Func<DocumentTemplateElementValue, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            //predicate = paramsList.ToList().All(p => p.Key != "RecordStateCode") ? predicate.And(p => p.RecordStateCode.Equals("N")) : predicate.And(p => p.RecordStateCode != null);

            foreach (var param in paramsList.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
            {
                switch (param.Key)
                {
                    case "ParentId":
                        if (param.Value != null)
                        {
                            var parentId = Guid.Parse(param.Value);
                            predicate = predicate.And(p => p.ParentId.Value == parentId);
                        }
                        else
                        {
                            predicate = predicate.And(p => !p.ParentId.HasValue);
                        }
                        break;
                    case "ServiceOwnerId":
                        var serviceOwnerId = Guid.Parse(param.Value);
                        predicate = predicate.And(p => p.OwnerId == serviceOwnerId);
                        break;
                    case "ContentValue":
                        predicate = predicate.And(p => p.ContentValue.Contains(param.Value));
                        break;
                    case "ValuesTreeId":
                        var valueTreeId = Guid.Parse(param.Value);
                        predicate = predicate.And(p => p.ValuesTreeId == valueTreeId);
                        break;
                    case "Name":
                        predicate = predicate.And(p => p.Name.Contains(param.Value));
                        break;
                    default: break;
                }
            }

            //var values = _dbContext.DocumentTemplateElementValues.FilterOwner(_ownerService.OwnerId);
            var values = _dbContext.DocumentTemplateElementValues;
            DocTemplateElementValueVm[] result;
            try
            {
                result = await (

                    from item in values.Where(predicate)
                    let hc = (from c in values
                              where (c.ParentId == item.Id)
                              select c).Count()
                    select new DocTemplateElementValueVm
                    {
                        Id = item.Id,
                        TemplateElementId = item.TemplateElementId,
                        ValuesTreeId = item.ValuesTreeId,
                        ParentId = item.ParentId,
                        Caption = item.Name,
                        ContentValue = item.ContentValue,
                        RecordState = item.RecordState,
                        OrderNumber = item.OrderNumber,
                        ValueTypeCode = item.ValueTypeCode,
                        ChildsCount = hc
                    }).OrderBy(x => x.OrderNumber).ToArrayAsync();
            }
            catch (Exception)
            {
                return new List<DocTemplateElementValueVm>().ToArray();
            }
            return result;

        }

        public async Task<DocTemplateElementValueVm> DocTemplateElementValuesInsert(DocTemplateElementValueDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplateElementValue>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplateElementValues.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplateElementValueVm>(dbModel);
            return vModel;

        }

        public async Task<DocTemplateElementValueVm> DocTemplateElementValuesUpdate(DocTemplateElementValueDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplateElementValue>(dtoModel);
            try
            {
                dbModel.SetOwner(_ownerService.OwnerId);
                _dbContext.DocumentTemplateElementValues.Attach(dbModel);
                _dbContext.Entry(dbModel).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var vModel = _mapper.Map<DocTemplateElementValueVm>(dbModel);
            return vModel;
        }

        public async Task<Result> DocTemplateElementValueDelete(Guid id)
        {
            var res = new Result { Success = true };
            try
            {
                //var elementValue = await _dbContext.DocumentTemplateElementValues.FilterOwner(_ownerService.OwnerId).SingleOrDefaultAsync(x => x.Id == Id);
                var elementValue = await _dbContext.DocumentTemplateElementValues.SingleOrDefaultAsync(x => x.Id == id);
                if (elementValue != null)
                {
                    _dbContext.DocumentTemplateElementValues.Remove(elementValue);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    res.Success = false;
                    res.Text = $"DocTempElementValues has not entity with Id: {id}.";
                }
            }
            catch (Exception e)
            {
                res.Success = false;
                res.Text = e.Message;
            }
            return res;
        }

        public async Task<DocDataVm> DocDataInsert(DocDataDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentData>(dtoModel);
            _dbContext.DocumentData.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocDataVm>(dbModel);
            return vModel;

        }

        public async Task<DocDataVm> DocDataUpdate(DocDataDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentData>(dtoModel);
            try
            {
                _dbContext.DocumentData.Attach(dbModel);
                _dbContext.Entry(dbModel).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var vModel = _mapper.Map<DocDataVm>(dbModel);
            return vModel;

        }

        public async Task<Result> DocDataInsertBatch(DocDataDto[] dtoBatch)
        {
            var res = new Result { Success = true };

            try
            {
                foreach (var dtoModel in dtoBatch)
                {
                    //var vomModel = DocDataVom.CreateFromDtoModel(dtoModel);
                    //var dbModel = _mappingService.Map<DocDataDbModel>(vomModel);
                    var dbModel = _mapper.Map<DocumentData>(dtoModel);
                    _dbContext.DocumentData.Add(dbModel);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                res.Success = false;
                res.Text = e.Message;
            }
            return res;

        }

        public async Task<Result> DocDataUpdateBatch(DocDataDto[] dtoBatch)
        {
            var res = new Result { Success = true };

            try
            {
                foreach (var dtoModel in dtoBatch)
                {
                    var dbModel = _mapper.Map<DocumentData>(dtoModel);
                    var local = _dbContext.Set<DocumentData>().Local.FirstOrDefault(f => f.Id == dbModel.Id);
                    if (local != null)
                    {
                        _dbContext.Entry(local).State = EntityState.Detached;

                        _dbContext.Entry(local).CurrentValues.SetValues(dbModel);
                    }
                    _dbContext.Entry(dbModel).State = EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                res.Success = false;
                res.Text = e.Message;
            }
            return res;

        }

        public async Task<List<DocDataVm>> GetDocDataList(IDictionary<string, string> paramsList)
        {

            Expression<Func<DocumentData, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            //predicate = paramsList.ToList().All(p => p.Key != "RecordStateCode") ? predicate.And(p => p.RecordStateCode.Equals("N")) : predicate.And(p => p.RecordStateCode != null);
            foreach (var param in paramsList.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
            {
                switch (param.Key)
                {
                    case "Id":
                        var id = Guid.Parse(param.Value);
                        predicate = predicate.And(p => p.Id == id);
                        break;
                    case "TemplateElementId":
                        var templateElementId = Guid.Parse(param.Value);
                        predicate = predicate.And(p => p.TemplateElementId == templateElementId);
                        break;
                    case "EntityTypeCode":
                        predicate = predicate.And(p => p.Document.EntityTypeCode == param.Value);
                        break;
                    case "Value":
                        predicate = predicate.And(p => p.Value.Contains(param.Value));
                        break;
                    case "OriginDbRecordId":
                        //var originDbRecordId = Convert.ToInt64(param.Value);
                        //predicate = predicate.And(p => p.OriginDbRecordId == originDbRecordId);
                        break;
                    default: break;
                }
            }

            var res = await (
                from d in _dbContext.DocumentData.Where(predicate)
                join dt in _dbContext.DocumentTemplateElements on d.TemplateElementId equals dt.Id
                //from dt in gdt.DefaultIfEmpty()
                select new DocDataVm
                {
                    Id = d.Id, // DocData
                    TemplateElementId = d.TemplateElementId,
                    Value = d.Value,
                    //ServiceDocId = d.ServiceDocId,
                    //ServiceOwnerId = d.ServiceOwnerId,
                    //OriginDbId = d.OriginDbId,
                    //OriginDbRecordId = d.OriginDbRecordId,
                    Order = dt.OrderNumber.ToString(),
                    ParentId = dt.ParentId, // DocTempElements fields
                    TemplateId = dt.TemplateId,
                    GlobalElementId = dt.GlobalElementId,
                    OrderNumber = dt.OrderNumber,
                    ElementTypeCode = dt.ElementTypeCode,
                    ControlTypeCode = dt.ControlTypeCode,
                    Code = dt.Code,
                    Caption = dt.Caption,
                    Description = dt.Description,
                    RecordState = dt.RecordState
                }).OrderBy(dd => dd.OrderNumber).ToListAsync();

            return res;
        }

        public async Task<DocDataVm> GetDocDataCard(Guid id)
        {
            //var record = await dbContext.DocData.FindAsync(id);
            var res = await (
                from d in _dbContext.DocumentData.Where(x => x.Id == id)
                join dt in _dbContext.DocumentTemplateElements on d.TemplateElementId equals dt.Id
                select new DocDataVm
                {
                    Id = d.Id, // DocData
                    TemplateElementId = d.TemplateElementId,
                    Value = d.Value,
                    //ServiceDocId = d.ServiceDocId,
                    //ServiceOwnerId = d.ServiceOwnerId,
                    //OriginDbId = d.OriginDbId,
                    //OriginDbRecordId = d.OriginDbRecordId,
                    ParentId = dt.ParentId, // DocTempElements fields
                    TemplateId = dt.TemplateId,
                    GlobalElementId = dt.GlobalElementId,
                    OrderNumber = dt.OrderNumber,
                    ElementTypeCode = dt.ElementTypeCode,
                    ControlTypeCode = dt.ControlTypeCode,
                    Code = dt.Code,
                    Caption = dt.Caption,
                    Description = dt.Description,
                    RecordState = dt.RecordState
                }).FirstOrDefaultAsync();

            return res;
        }

        public async Task<DocTemplatePresetVm[]> GetDocTemplatePresetList(IDictionary<string, string> paramsList)
        {
            Expression<Func<DocumentTemplatePreset, bool>> predicate = p => p.RecordState != RecordState.Deleted;
            foreach (var param in paramsList.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
            {
                switch (param.Key)
                {
                    case "TemplateId":
                        var templateId = Guid.Parse(param.Value);
                        predicate = predicate.And(p => p.TemplateId == templateId);
                        break;
                    default: break;
                }
            }

            var res =
                from p in _dbContext.DocumentTemplatePresets.Include(p => p.PresetValues).Where(predicate)
                select new DocTemplatePresetVm
                {
                    Id = p.Id,
                    TemplateId = p.TemplateId,
                    Caption = p.Caption,
                    PresetValues = p.PresetValues.Select(v => new DocTemplatePresetValueVm
                    {
                        Id = v.Id,
                        Value = v.Value,
                        PresetId = v.PresetId,
                        TemplateElementId = v.TemplateElementId
                    }),
                    OrderNumber = p.OrderNumber,
                    //OriginDbId = p.OriginDbId,
                    //OriginDbRecordId = p.OriginDbRecordId,
                    //OwnerId = p.OwnerId,
                    RecordState = p.RecordState
                };

            return await res.ToArrayAsync();
        }

        public async Task<DocTemplatePresetVm> DocTemplatePresetInsert(DocTemplatePresetDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplatePreset>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplatePresets.Add(dbModel);
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplatePresetVm>(dbModel);
            return vModel;
        }

        public async Task<DocTemplatePresetVm> DocTemplatePresetUpdate(DocTemplatePresetDto dtoModel)
        {
            var dbModel = _mapper.Map<DocumentTemplatePreset>(dtoModel);
            dbModel.SetOwner(_ownerService.OwnerId);
            _dbContext.Entry(dbModel).State = EntityState.Modified;
            foreach (var val in dbModel.PresetValues)
            {
                _dbContext.Entry(val).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
            var vModel = _mapper.Map<DocTemplatePresetVm>(dbModel);
            return vModel;
        }

        public async Task<DocTemplateVm> DocTemplateInsert(DocTemplateDto dtoModel)
        {
            var notUnique = false;
            if (dtoModel.ClassShortCode.ToUpper().Equals("T"))
            {
                notUnique = _dbContext.DocumentTemplates.Any(d => d.Code.Equals(dtoModel.Code)
                                                            && d.OwnerId.GetValueOrDefault().Equals(_ownerService.OwnerId));
            }
            else
            {
                notUnique = _dbContext.DocumentTemplates.Any(p => p.OwnerId.GetValueOrDefault().Equals(_ownerService.OwnerId)
                                                            && p.Caption.Equals(dtoModel.Caption) && p.ParentId.Equals(dtoModel.ParentId));
            }

            if (notUnique)
            {
                throw new ValidationException($"Code: {dtoModel.Code} not unique");
            }

            var dbModel = _mapper.Map<DocumentTemplate>(dtoModel);
            var clone = dbModel;
            var elements = dbModel.TemplateElements;
            clone.TemplateElements = null;
            clone.SetOwner(_ownerService.OwnerId);
            _dbContext.DocumentTemplates.Add(clone);
            await _dbContext.SaveChangesAsync();
            ProcNestedElements(elements, clone.Id);
            clone.TemplateElements = elements;

            _dbContext.Entry(clone).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            var vModel = _mapper.Map<DocTemplateVm>(clone);

            return vModel;
        }

        private void ProcNestedElements(ICollection<DocumentTemplateElement> elements, Guid templId)
        {
            if (elements == null)
            {
                return;
            }

            foreach (var elm in elements)
            {
                elm.TemplateId = templId;
                elm.SetOwner(_ownerService.OwnerId);
                if (elm.ValuesTreeId == Guid.Empty)
                {
                    var tree = new DocumentTemplateElementValueTree
                    {
                        Code = elm.Code + "_ValuesTree",
                        Name = elm.Caption + "_ValuesTree",
                        OwnerId = elm.OwnerId
                    };
                    tree.SetOwner(_ownerService.OwnerId);
                    elm.ValuesTree = tree;
                    //_dbContext.DocTempElementValuesTree.Add()
                }
                if (elm.TemplateElements != null && elm.TemplateElements.Count > 0)
                {
                    ProcNestedElements(elm.TemplateElements, templId);
                }
            }
        }

        public async Task<Result> DocTemplateRemove(Guid id)
        {
            //var tmpl = await _dbContext.DocumentTemplates.FilterOwner(_ownerService.OwnerId).Where(x => x.Id == id).FirstOrDefaultAsync();
            var tmpl = await _dbContext.DocumentTemplates.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (tmpl == null)
            {
                return new Result { Success = false, Text = $"There is not  entity with id:{id}" };
            }

            try
            {
                _dbContext.DocumentTemplates.Remove(tmpl);
                await _dbContext.SaveChangesAsync();
                return new Result() { Success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("FK_DocTemplates_DocTemplates_ParentId") ||
                    ex.InnerException.Message.Contains("FK_dss_Documents_DocTemplates_TemplateId"))
                {
                    return new Result { Success = false, Text = "Видалення  неможливе, існують  зв'язки" };
                }
                return new Result { Success = false, Text = ex.Message };
            }
        }

        public async Task<DocTemplateVm> DocTemplateUpdate(DocTemplateDto dtoModel)
        {
            try
            {
                var dbModel = _mapper.Map<DocumentTemplate>(dtoModel);
                var local = _dbContext.DocumentTemplates.Include(x => x.TemplateElements).FirstOrDefault(f => f.Id == dbModel.Id);

                if (local != null)
                {
                    if (local.Code != dbModel.Code)
                    {
                        var notUnique = _dbContext.DocumentTemplates.Any(d => d.Code.Equals(dtoModel.Code) &&
                                        d.OwnerId.GetValueOrDefault().Equals(_ownerService.OwnerId));
                        if (notUnique)
                        {
                            throw new ValidationException($"Code: {dtoModel.Code} not unique");
                        }
                    }

                    foreach (var localElm in local.TemplateElements)
                    {
                        if (!dbModel.TemplateElements.Flatten(d => d.TemplateElements).Any(c => c.Id == localElm.Id))
                        {
                            _dbContext.DocumentTemplateElements.Remove(localElm);
                        }
                    }

                    ProcElements(dbModel.TemplateElements, dbModel.Id, local);
                    dbModel.SetOwner(_ownerService.OwnerId);
                    dbModel.TemplateElements = dbModel.TemplateElements.Flatten(d => d.TemplateElements).ToList();
                    _dbContext.Entry(local).CurrentValues.SetValues(dbModel);

                    await _dbContext.SaveChangesAsync();

                    var vModel = _mapper.Map<DocTemplateVm>(local);
                    return vModel;
                }
                else
                {
                    throw new Exception($"Template with Id:{dbModel.Id} does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Result> DocTemplateMove(Guid templateId, Guid? parentId = null)
        {
            var template = await _dbContext.DocumentTemplates.FirstOrDefaultAsync(x => x.Id == templateId);
            template.Parent = null;
            template.ParentId = parentId;
            try
            {
                _dbContext.Entry(template).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new Result { Success = true };

            }
            catch (Exception ex)
            {
                return new Result { Success = false, Text = ex.Message };
            }

        }

        private void ProcElements(ICollection<DocumentTemplateElement> elements, Guid templId, DocumentTemplate localTemplate = null, DocumentTemplateElement localElement = null)
        {
            foreach (var element in elements)
            {
                element.SetOwner(_ownerService.OwnerId);
                if (element.ValuesTreeId == Guid.Empty)
                {
                    var tree = new DocumentTemplateElementValueTree
                    {
                        Id = Guid.Empty,
                        Code = element.Code + "_ValuesTre",
                        Name = element.Caption + "_ValuesTre",
                        OwnerId = element.OwnerId
                    };
                    tree.SetOwner(_ownerService.OwnerId);
                    element.ValuesTree = tree;
                    element.ValuesTreeId = null;
                    _dbContext.DocumentTemplateElementValueTrees.Add(tree);

                    //_dbContext.Entry(element).State = EntityState.Modified;
                    //element.ValuesTreeId = tree.Id;
                }

                DocumentTemplateElement localElm = null;
                if (localElement != null && localElement.TemplateElements != null && localElement.TemplateElements.Count > 0)
                {
                    localElm = localElement.TemplateElements
                   .Where(c => c.Id != Guid.Empty && c.Id == element.Id)
                   .SingleOrDefault();
                }

                if (localElm == null && localTemplate != null && localTemplate.TemplateElements != null && localTemplate.TemplateElements.Count > 0)
                {
                    localElm = localTemplate.TemplateElements
                   .Where(c => c.Id != Guid.Empty && c.Id == element.Id)
                   .SingleOrDefault();

                    element.Parent = localElement;
                    element.ParentId = null;


                }

                if (localElm != null)
                {
                    _dbContext.Entry(localElm).CurrentValues.SetValues(element);
                    if (localElement != null)
                    {
                        localElement.TemplateElements.Add(localElm);
                    }
                }
                else
                {
                    element.TemplateId = templId;
                    if (localElement != null)
                    {
                        if (localElement.TemplateElements == null)
                        {
                            localElement.TemplateElements = new List<DocumentTemplateElement>();
                        }

                        var tmp = JsonConvert.SerializeObject(element, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        localElm = JsonConvert.DeserializeObject<DocumentTemplateElement>(tmp);
                        localElm.TemplateElements = new List<DocumentTemplateElement>();
                        localElm.Parent = localElement;
                        localElement.SetOwner(_ownerService.OwnerId);
                        localElement.TemplateElements.Add(localElm);
                    }
                    else
                    {
                        if (localTemplate.TemplateElements == null)
                        {
                            localTemplate.TemplateElements = new List<DocumentTemplateElement>();
                        }

                        var tmp = JsonConvert.SerializeObject(element, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        localElm = JsonConvert.DeserializeObject<DocumentTemplateElement>(tmp);
                        localElm.TemplateElements = new List<DocumentTemplateElement>();
                        localElm.Parent = null;
                        localElm.SetOwner(_ownerService.OwnerId);
                        localTemplate.TemplateElements.Add(localElm);
                    }
                }
                if (element.TemplateElements != null && element.TemplateElements.Count > 0)
                {
                    //var nextLocalElement = localElement.TemplateElements.Where(e => e.Id == element.Id).FirstOrDefault();
                    ProcElements(element.TemplateElements, templId, localTemplate, localElm);
                }

            }
        }
        public async Task<DocTemplateElementVm> DocTemplateElementInsert(DocTemplateElementDto dtoModel)
        {
            try
            {
                var dbModel = _mapper.Map<DocumentTemplateElement>(dtoModel);
                _dbContext.DocumentTemplateElements.Add(dbModel);
                await _dbContext.SaveChangesAsync();
                var vModel = _mapper.Map<DocTemplateElementVm>(dbModel);
                return vModel;

            }
            catch (Exception)
            {
                return null;
            }
        }

        //public Task<Result> DocTemplateElementDelete(DocTemplateElementsDto dtoModel)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<DocDataVm[]> EEDDataAsync(string originIds)
        //{
        //    try
        //    {
        //        var origins = originIds.Split(',').Select(Int64.Parse).ToArray();
        //        var result = await _dbContext.DocumentData.Where(x => origins.Any(o => o == x.OriginDbRecordId) && x.Code == "EED").ToArrayAsync();
        //        return _mapper.Map<DocDataVm[]>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #region Mappings



        #endregion Mappings

        public string NormilizeDtmString(string dtmString, string controlTypeCode)
        {
            if (string.IsNullOrWhiteSpace(dtmString))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(controlTypeCode))
            {
                throw new ArgumentNullException($"Parameter [{nameof(controlTypeCode)}] is null.");
            }

            switch (controlTypeCode)
            {
                case "TEXT":
                case "TEXTBLOCK":
                case "SECTOR":
                case "LEXTREE":
                case "MULTYDICT":
                    dtmString = ParseJsonString(dtmString);
                    break;
                case "BIT":
                    dtmString = dtmString == "true" ? "Так" : "Ні";
                    break;
                case "DATE":
                    dtmString = Convert.ToDateTime(dtmString).ToString("dd.MM.yyyy");
                    break;
                case "CHECKLIST":
                case "NUMBER":
                case "ICD":
                case "SPREADSSHEET":
                    break;
            }

            return dtmString;
        }

        private string ParseJsonString(string value)
        {
            var sb = new StringBuilder();
            var json = JObject.Parse(value);
            foreach (var q in json["ops"].AsJEnumerable())
            {
                if (q.SelectToken("insert") != null)
                {

                    if (q.SelectToken("insert").ToString() == "\n")
                    {
                        sb.Append(System.Environment.NewLine);
                        continue;
                    }
                    var str = q.SelectToken("insert").ToString().Replace("\n", Environment.NewLine);
                    sb.Append(str);
                }
            }
            return sb.ToString();
        }
    }
}

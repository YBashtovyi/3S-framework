using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using App.DocumentTemplates.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace App.DocumentTemplates.Services
{
    public interface IPrintService
    {
        MemoryStream GenerateDocx(DocumentVm model);
        MemoryStream CreateFromTemplate(DocumentVm model);
    }

    public class PrintService : IPrintService
    {
        //private readonly MappingService _mappingService;
        //private readonly OwnerService _ownerService;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _log;

        //public PrintService(MappingService mappingService, OwnerService ownerService, IHostingEnvironment env, ILogger<PrintService> log)
        public PrintService(IHostingEnvironment env, ILogger<PrintService> log)
        {
            //_mappingService = mappingService;
            //_ownerService = ownerService;
            _env = env;
            _log = log;
        }

        public MemoryStream GenerateDocx(DocumentVm model)
        {
            var docx = new MemoryStream();
            _log.LogInformation($"templatePath: {_env.ContentRootPath}\\AppData\\DTM.dotx");
            using (var file = File.Open($"{_env.ContentRootPath}\\AppData\\DTM.dotx", FileMode.Open))
            {
                try
                {
                    _log.LogInformation($"template read");
                    docx.SetLength(file.Length);
                    file.Read(docx.GetBuffer(), 0, (int)file.Length);
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"read template in docx error: {ex.Message}");
                }
            }

            try
            {
                var docElements = PrepareData(model.DocumentDataList);

                using (var wordDocument = WordprocessingDocument.Open(docx, true))
                {

                    wordDocument.ChangeDocumentType(WordprocessingDocumentType.Document);

                    var mainPart = wordDocument.MainDocumentPart;
                    var body = mainPart.Document.Body;

                    if (string.IsNullOrEmpty(model.Caption))
                    {
                        AppendTitle(body, model.Caption);
                    }

                    FillBody(body, docElements);
                    mainPart.Document.Save();
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"create docx error: {ex.Message}");
            }

            return docx;
        }

        private void AppendTitle(Body body, string title)
        {
            var p = new Paragraph();
            var pp = new ParagraphProperties
            {
                ParagraphStyleId = new ParagraphStyleId { Val = "DocTitle" }
            };
            p.Append(pp);
            p.Append(new Run(new Text(title)));
            body.Append(p);
        }

        private void FillBody(Body body, IEnumerable<DocElementVm> dataList, int level = 0)
        {
            foreach (var item in dataList)
            {
                if (item.Panel.ControlTypeCode == "SECTOR" && item.IsEmpty == false)
                {
                    var spp = new ParagraphProperties();
                    if (level > 0)
                    {
                        spp.Indentation = new Indentation() { Left = $"{level.ToString()}00" };
                    }

                    spp.ParagraphStyleId = new ParagraphStyleId { Val = "SectorTitle" };
                    var run = new Run();
                    run.Append(new Text(item.Panel.Caption));
                    var sector = new Paragraph();
                    sector.Append(spp);
                    sector.Append(run);
                    body.Append(sector);
                    FillBody(body, item.Nested, level + 1);
                    continue;
                }

                if (item.Panel.ControlTypeCode == "SECTOR" && item.IsEmpty == true)
                {
                    continue;
                }

                var pp = new ParagraphProperties();
                if (level > 0)
                {
                    pp.Indentation = new Indentation() { Left = $"{level.ToString()}00" };
                }
                //pp.ParagraphStyleId = new ParagraphStyleId { Val = "SectorContent" };

                var label = new Run();
                label.Append(new Text($"{item.Panel.Caption}:"));
                var paragraph = new Paragraph();
                paragraph.Append(pp);
                paragraph.Append(label);

                AppendElementValue(paragraph, item);

                body.Append(paragraph);
            }
        }

        private IEnumerable<DocElementVm> PrepareData(IEnumerable<DocDataVm> dataList, Guid? parentId = null)
        {
            var result = new List<DocElementVm>();
            foreach (var item in dataList)
            {
                if ((item.ParentId == null && parentId == null) || item.ParentId == parentId)
                {
                    var element = new DocElementVm
                    {
                        Panel = item,
                        IsEmpty = string.IsNullOrEmpty(item.Value)
                    };
                    if (item.ControlTypeCode == "SECTOR")
                    {
                        element.Nested = PrepareData(dataList, item.TemplateElementId);
                        element.IsEmpty = element.Nested.All(x => x.IsEmpty == true);
                    }
                    result.Add(element);
                }
            }
            return result.OrderBy(x => x.Panel.OrderNumber);
        }

        private void AppendElementValue(OpenXmlElement xmlElm, DocElementVm docElm)
        {
            switch (docElm.Panel.ControlTypeCode)
            {
                case "DATE":
                    var dateValue = new Run();
                    var date = DateTime.Parse(docElm.Panel.Value.Split('+')[0]);
                    dateValue.Append(new Text(date.ToString("dd.MM.yyyy")));
                    xmlElm.Append(dateValue);
                    break;
                case "TEXT":
                    RenderQuill(xmlElm, docElm.Panel.Value);
                    break;
                case "LEXTREE":
                    RenderQuill(xmlElm, docElm.Panel.Value);
                    break;
                default:
                    var value = new Run();
                    value.Append(new Text(docElm.Panel.Value));
                    xmlElm.Append(value);
                    break;
            }
        }
        private void RenderQuill(OpenXmlElement p, string quill)
        {
            if (!string.IsNullOrEmpty(quill))
            {
                var json = JObject.Parse(quill);
                foreach (var op in json["ops"].AsJEnumerable())
                {
                    if (op.SelectToken("insert") != null)
                    {
                        var insert = op.SelectToken("insert").ToString();
                        if (!string.IsNullOrEmpty(insert))
                        {
                            var run = new Run();
                            var props = new RunProperties();
                            var attr = op.SelectToken("attributes");
                            if (attr != null && attr.SelectToken("bold") != null)
                            {
                                props.Append(new Bold());
                            }
                            if (attr != null && attr.SelectToken("italic") != null)
                            {
                                props.Append(new Italic());
                            }
                            if (attr != null && attr.SelectToken("underline") != null)
                            {
                                props.Append(new Underline());
                            }
                            run.Append(props);
                            var lines = insert.Split('\n');
                            for (var i = 0; i < lines.Count(); i++)
                            {
                                if (i > 0 && !string.IsNullOrEmpty(lines[i]))
                                {
                                    run.Append(new Break() { Clear = BreakTextRestartLocationValues.None, Type = BreakValues.TextWrapping });
                                }

                                run.Append(new Text(lines[i])
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                });

                            }

                            p.Append(run);
                        }
                    }
                }
            }
        }

        public MemoryStream CreateFromTemplate(DocumentVm model)
        {
            var docx = new MemoryStream();
            var code = model.Template.Code;
            using (var file = File.Open($"{_env.ContentRootPath}/AppData/{code}.docx", FileMode.Open))
            {
                docx.SetLength(file.Length);
                file.Read(docx.GetBuffer(), 0, (int)file.Length);
            }

            using (var wordDocument = WordprocessingDocument.Open(docx, true))
            {

                wordDocument.ChangeDocumentType(WordprocessingDocumentType.Document);

                var mainPart = wordDocument.MainDocumentPart;
                var body = mainPart.Document.Body;

                var blocks = body.OfType<SdtBlock>().ToList();


                mainPart.Document.Save();
            }

            return docx;
        }
    }
}

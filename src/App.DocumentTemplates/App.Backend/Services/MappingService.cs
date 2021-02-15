using System.Collections.Generic;
using System.Linq;
using App.DocumentTemplates.Dto;
using AutoMapper;
using App.DocumentTemplates.ViewModels;
using App.DocumentTemplates.Models;

namespace App.DocumentTemplates.Services
{
    public class MappingService
	{
		private readonly IMapper _mapper;
        public MappingService()
		{
            var comparer = new DtElComparer<DocumentTemplateElement>();
			var config = new MapperConfiguration(cfg =>
			{
                cfg.CreateMap<DocumentTemplate, DocTemplateVm>()
                .ForMember(x => x.TemplateElements, x => x.MapFrom(m => m.TemplateElements.Where(c => c.ParentId == null).OrderBy(c => c, comparer)))
                .ForMember(p => p.Templates, p => p.MapFrom(m => m.Templates.Where(c => c.ParentId == m.Id)));
				cfg.CreateMap<DocumentControlType, DocControlTypeVm>();
				cfg.CreateMap<DocumentTemplateElement, DocTemplateElementVm>();
				cfg.CreateMap<DocumentTemplatePreset, DocTemplatePresetVm>();
				cfg.CreateMap<DocumentTemplateElementValue, DocTemplateElementValueVm>();
				cfg.CreateMap<DocTemplateElementDto, DocumentTemplateElement>();
				cfg.CreateMap<DocTemplateElementValueDto, DocumentTemplateElementValue>();
                cfg.CreateMap<DocTemplateDto, DocumentTemplate>();
			    cfg.CreateMap<DocDataDto, DocumentData>();
			    cfg.CreateMap<DocumentData, DocDataVm>().ForMember(x=>x.Config, x=>x.MapFrom(m=>m.TemplateElement.Config));
			    cfg.CreateMap<DocumentTemplatePreset, DocTemplatePresetVm>();
			    cfg.CreateMap<DocTemplatePresetDto, DocumentTemplatePreset>();
                cfg.CreateMap<DocumentTemplatePresetValue, DocTemplatePresetValueVm >();
			    cfg.CreateMap<DocTemplatePresetValueDto, DocumentTemplatePresetValue>();
                cfg.CreateMap<DocTemplateElementDto, DocumentTemplateElement>();
                cfg.CreateMap<TemplateDocument, DocumentVm>()
                .ForMember(p => p.Documents, p => p.MapFrom(m => m.Documents.Where(c => c.ParentId == m.Id))); ;
                cfg.CreateMap<TemplateDocumentDto, TemplateDocument>();

                cfg.CreateMap<DocTemplateElementValueTreeDto, DocumentTemplateElementValueTree>();
                cfg.CreateMap<DocumentTemplateElementValueTree, DocTemplateElementValueTreeVm>();
            });
			_mapper = config.CreateMapper();
		}

		public T Map<T>(object source)
		{
			return _mapper.Map<T>(source);
		}

        private class DtElComparer<T>: IComparer<T>
            where T : DocumentTemplateElement
        {
            public int Compare(T x, T y)
            {
                if (x.OrderNumber > y.OrderNumber)
                {
                    return 1;
                }
                else if (x.OrderNumber < y.OrderNumber)
                {
                    return -1;
                }
                    
                return 0;
            }
        }
    }
}

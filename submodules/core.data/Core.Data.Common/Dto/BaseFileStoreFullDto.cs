using System.ComponentModel.DataAnnotations;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Common.Dto
{
    public abstract class BaseFileStoreFullDto: BaseFileStoreDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        [Display(Name="Тип локацїї")]
        public virtual FileStoreDestinationType FileStoreDestinationType { get; set; }

        [CaseFilter(CaseFilterOperation.Equals)]
        [Display(Name = "Шлях до файлу")]
        public virtual string FilePath { get; set; }

        [Display(Name = "Тип контенту")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string ContentType { get; set; }

        [Display(Name = "Розмір файлу")]
        [CaseFilter(CaseFilterOperation.InputRange)]
        public virtual int FileSize { get; set; }

        [Display(Name = "Группа файлу")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string FileGroupCaption { get; set; }

        [Display(Name = "Тип документа")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string DocumentTypeCaption { get; set; }
    }
}

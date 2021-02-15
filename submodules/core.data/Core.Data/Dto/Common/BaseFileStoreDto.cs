using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Common
{
    public abstract class BaseFileStoreDto: CoreDto
    {
        [Display(Name = "Сутність")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual string EntityName { get; set; }

        [Display(Name = "Ідентифікатор сутності")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public virtual Guid EntityId { get; set; }

        [Display(Name = "Ім'я файлу")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string FileName { get; set; }

        [Display(Name = "Опис")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string Description { get; set; }

        [Display(Name = "Тип файлу")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string FileType { get; set; }

        //HACK: remove DocumentTypeId and FileGroupId and clear it in code
        [NotMapped]
        [Display(Name = "Тип документа")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? DocumentTypeId { get; set; }

        [NotMapped]
        [Display(Name = "Группа")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? FileGroupId { get; set; }
    }
}

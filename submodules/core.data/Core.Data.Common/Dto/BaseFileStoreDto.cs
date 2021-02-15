using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Common.Dto
{
    public abstract class BaseFileStoreDto: BaseDto
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

        [Display(Name = "Тип документа")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? DocumentTypeId { get; set; }

        [Display(Name = "Группа")]
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid? FileGroupId { get; set; }
    }
}

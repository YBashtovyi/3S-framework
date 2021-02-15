using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Cdn
{
    /// <summary>
    /// Додаткові характеристики об’єктів
    /// </summary>
    public class ConstructionObjectExPropertyDictionaryDto: CoreDto
    {
        /// <summary>
        /// Code of type of construction object (dictionary)
        /// </summary>
        [CaseFilter]
        public string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        /// <summary>
        /// Group DataFormat <see cref="EnumRecord"/>
        /// </summary>
        [CaseFilter]
        public string DataFormat { get; set; }


        [Display(Name = "Підпорядкування")]
        [CaseFilter]
        public Guid? ParentId { get; set; }
    }

    [MainEntity(nameof(ConstructionObjectExPropertyDictionary))]
    public class ConstructionObjectExPropertyDictionaryListDto: ConstructionObjectExPropertyDictionaryDto, IPagingCounted
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string DataFormatName { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ConstructionObjectExPropertyDictionary))]
    public class ConstructionObjectExPropertyDictionaryDetailsDto: ConstructionObjectExPropertyDictionaryDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParentName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string DataFormatName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string FullName { get; set; }
    }

    public class ConstructionObjectExPropertyDictionaryEditDto: ConstructionObjectExPropertyDictionaryDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string FullName { get; set; }
    }

    /// <summary>
    /// List TypeOfObject
    /// </summary>
    public class ConstructionObjectTypeOfObjectListDto: CoreDto
    {
        /// <summary>
        /// Code of type of construction object (dictionary)
        /// </summary>
        [CaseFilter]
        public string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter]
        public Guid? ParentId { get; set; }
    }
}

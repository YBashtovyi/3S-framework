using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using App.Data.Dto.Common.NotMapped;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(ConstructionObject))]
    public class ConstructionObjectListDto : CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        [CaseFilter]
        public string ObjectStatus { get; set; }
        public string ObjectStatusName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Code { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter]
        public string TypeOfConstructionObject { get; set; }
        public string TypeOfConstructionObjectName { get; set; }

        [CaseFilter]
        public string ClassOfConsequence { get; set; }
        public string ClassOfConsequenceName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string AtuCoordinates { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string ExtendedProperty { get; set; }

        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(ConstructionObject))]
    public class ConstructionObjectDetailsDto : CoreDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string Code { get; set; }

        [CaseFilter]
        public string ObjectStatus { get; set; }
        public string ObjectStatusName { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string FullName { get; set; }

        public Guid TypeOfConstructionObjectId { get; set; }
        [CaseFilter]
        public string TypeOfConstructionObject { get; set; }
        public string TypeOfConstructionObjectName { get; set; }

        [CaseFilter]
        public string ClassOfConsequence { get; set; }
        public string ClassOfConsequenceName { get; set; }

        public string AtuCoordinates { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }

        [NotMapped]
        public MapCoordinate AtuCoordinateList { get; set; } = new MapCoordinate();
    }

    [MainEntity(nameof(ConstructionObject))]
    public class ConstructionObjectEditDto : CoreDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public string Code { get; set; }

        [CaseFilter]
        public string ObjectStatus { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Name { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string FullName { get; set; }

        [CaseFilter]
        public string TypeOfConstructionObject { get; set; }

        [CaseFilter]
        public string ClassOfConsequence { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }
    }
}

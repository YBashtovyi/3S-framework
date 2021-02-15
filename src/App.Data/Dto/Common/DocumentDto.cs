using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(Document))]
    public class DocumentListDto: CoreDto, IPagingCounted
    {
        public string DerivedEntity { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public string DocType { get; set; }
        public string DocTypeName { get; set; }

        public string DocState { get; set; }
        public string DocStateName { get; set; }

        public string Description { get; set; }

        [CaseFilter]
        public Guid? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }
}

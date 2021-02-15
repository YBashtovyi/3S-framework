using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Dtm.Common.Interface;

namespace App.DocumentTemplates.Models
{
    /// <summary>
    /// Types of DocTemplate elements
    /// </summary>
    [Table("DtmControlType")]
    public class DocumentControlType: BaseEntity, IOwnedEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
    }
}




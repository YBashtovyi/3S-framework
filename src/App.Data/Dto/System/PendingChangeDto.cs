using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Models;
using Core.Security;

namespace App.Data.Dto.System
{
    [RightsCheckList(nameof(PendingChange))]
    public class PendingChangeDto: CoreDto
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string Owner { get; set; }
        public string Action { get; set; }
        public bool Processed { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    [RightsCheckList(nameof(PendingChange))]
    public class PendingChangeDetailDto: CoreDto
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string Owner { get; set; }
        public string Action { get; set; }
        public bool Processed { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}


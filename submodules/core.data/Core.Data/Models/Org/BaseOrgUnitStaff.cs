using System;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;

namespace Core.Data.Models.Org
{
    [Display(Name = "Штатна одиниця орг. структурури (встановлена штатним розписом)")]
    public class BaseOrgUnitStaff: CoreEntity
    {
        [RequiredNonDefault]
        public virtual Guid OrgUnitPositionId { get; set; }

        public virtual bool IsMainPosition { get; set; }

        [RequiredNonDefault]
        public virtual Guid EmployeeId { get; set; }

        public virtual DateTime StartDate { get; set; }

        public DateTime EndDate
        {
            get
            {
                _endDate ??= DateTime.MaxValue;
                return _endDate ?? DateTime.MaxValue;
            }
            set
            {
                _endDate = value;
            }
        }

        private DateTime? _endDate = null;
    }
}

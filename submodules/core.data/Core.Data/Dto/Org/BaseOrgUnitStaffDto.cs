using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Dto.Org
{
    public class BaseOrgUnitStaffDto: CoreDto
    {
        public Guid OrgUnitPositionId { get; set; }

        public Guid EmployeeId { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime StartDate { get; set; }

        [CaseFilter(CaseFilterOperation.InputRange)]
        public DateTime? EndDate
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

        /// <summary>
        /// Used to display at the front-end
        /// </summary>
        public DateTime? EndDateFront
        {
            get
            {
                return _endDate.Value.Date.Equals(DateTime.MaxValue.Date) ? null : _endDate;
            }
        }
    }
}

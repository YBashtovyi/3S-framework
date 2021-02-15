using System;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    public interface IEvent: ICoreEntity
    {
        DateTime? StartDate { get; set; }

        DateTime? EndDate { get; set; }

        Guid? ParentId { get; set; }

        string Description { get; set; }

        string RegNumber { get; set; }

        DateTime RegDate { get; set; }

        Guid PatientCardId { get; set; }

        Guid EmployeeId { get; set; }
    }
}

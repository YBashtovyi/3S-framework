using System;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Org
{
    [MainEntity(nameof(Employee))]
    public class OrgEmployeeSimpleDto: CoreDto
    {
        public RecordState RecordState { get; set; }

        /// <summary>
        /// Id of the <see cref="User"/>
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id of the <see cref="Person"/>
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Id of the <see cref="OrgUnit"/>
        /// </summary>
        public Guid OrgUnitId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace Core.Data.Dto.Org
{
    public class BaseOrgEmployeeDto: CoreDto
    {
        public Guid PersonId { get; set; }

        public string PersonFullName { get; set; }
    }
}

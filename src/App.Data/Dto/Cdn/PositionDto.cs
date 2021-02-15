using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Data.Dto.Cdn;

namespace App.Data.Dto.Cdn
{
    public class PositionListDto: BasePositionDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }
        public int TotalRecordCount { get; set; }
    }

    public class PositionDto: BasePositionDto
    {
    }
}

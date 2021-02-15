using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace Core.Mvc.Data
{
    public struct GetListResult<T> where T: CoreDto
    {
        public IEnumerable<T> Data { get; set; }
        public Dictionary<string, object> ViewData { get; set; }
    }
}

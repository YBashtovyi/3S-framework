using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace Core.Mvc.Data
{
    public struct GetEditResult<T> where T: CoreDto
    {
        public T Model { get; set; }
        public Dictionary<string, object> ViewData { get; set; }
    }
}

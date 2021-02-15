using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mvc.Data
{
    public struct PostDeleteResult
    {
        public bool Success { get; set; }
        public Dictionary<string, object> ViewData { get; set;}
        public string RedirectToOnSuccess { get; set; }
        public object RedirectToValues { get; set; }
    }
}

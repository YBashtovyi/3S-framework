using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Services.Data
{
    public class OfficeDocumentFieldConfig
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public Func<DateTime, string> DateFormatter { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}

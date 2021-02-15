using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MainEntityAttribute: Attribute
    {
        public string EntityName { get; }

        public MainEntityAttribute(string entityName)
        {
            EntityName = entityName;
        }
    }
}

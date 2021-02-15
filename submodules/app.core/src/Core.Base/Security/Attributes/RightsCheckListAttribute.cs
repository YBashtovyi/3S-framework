using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Security
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class RightsCheckListAttribute: Attribute
    {
        public List<string> CheckedEntities { get; set; } = new List<string>();
        public RightsCheckListAttribute()
        {
           
        }

        public RightsCheckListAttribute(params string[] entities): base()
        {
            CheckedEntities.AddRange(entities);
        }
    }
}

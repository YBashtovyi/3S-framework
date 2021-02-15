using System;
using Core.Common.Enums;

namespace Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CaseFilterAttribute : Attribute
    {
        public CaseFilterOperation Operation { get; set; }
        public string Condition { get; set; }
        public string Group { get; set; }

        public CaseFilterAttribute(CaseFilterOperation operation = CaseFilterOperation.Equals, string condition = null)
        {
            Operation = operation;
            Condition = condition;
        }
    }
}


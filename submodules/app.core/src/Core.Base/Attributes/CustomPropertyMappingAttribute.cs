using System;

namespace Core.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomPropertyMappingAttribute : Attribute
    {
        public string NewPropertyName { get; set; }

        public CustomPropertyMappingAttribute(string newPopertyName)
        {
            NewPropertyName = newPopertyName;
        }
    }
}

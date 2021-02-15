using System;

namespace Core.Security
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OperationRightAttribute: Attribute
    {
        public string OperationName { get; }
        public OperationRightAttribute(string operationName) : base()
        {
            if (string.IsNullOrEmpty(operationName))
            {
                throw new ArgumentException("Operation name cannot be null or empty");
            }
            OperationName = operationName;
        }
    }
}

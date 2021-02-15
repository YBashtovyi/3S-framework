using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SocServ.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NotRequiredIfChecked : ValidationAttribute
    {
        public string Condition { get; set; }

        public NotRequiredIfChecked(string condition)
        {
            this.Condition = condition;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(Condition);
            object propertyValue = property.GetValue(instance);

            if ((propertyValue != null && propertyValue.ToString() == "True") ||
                !String.IsNullOrEmpty(value?.ToString()))
            {
                return null;
            }
            else
            {
                return new ValidationResult("Заповніть поле");
            }
        }
    }
}

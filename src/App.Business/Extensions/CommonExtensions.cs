using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace App.Business.Extensions
{
    public static class CommonExtensions
    {
        public static string DisplayName(this object obj, string fieldName)
        {
            var fi = obj.GetType().GetField(fieldName);
            var displayAttribute = fi?.GetCustomAttribute<DisplayAttribute>(false);
            return displayAttribute == null ? fieldName : displayAttribute.Name;
        }
    }
}

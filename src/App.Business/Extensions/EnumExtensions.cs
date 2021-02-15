using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace App.Business.Extensions
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var displayAttribute = fi?.GetCustomAttribute<DisplayAttribute>(false);
            return displayAttribute == null ? value.ToString() : displayAttribute.Name;
        }
    }
}

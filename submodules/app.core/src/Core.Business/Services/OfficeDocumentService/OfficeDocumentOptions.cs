using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Data;

namespace Core.Services.Data
{
    public class OfficeDocumentOptions<TData>: IOfficeDocumentOptions<TData> where TData: class
    {
        public Func<DateTime, string> DefaultDateFormatter { get; set; }
        public List<OfficeDocumentFieldConfig> Fields { get; protected set; }

        public void ConfigureFields(IDictionary<string, string> fields)
        {
            var type = typeof(TData);
            var props = type.GetProperties();
            var newFields = new List<OfficeDocumentFieldConfig>();
            if (fields != null)
            {
                // storing origin column order
                foreach (var field in fields)
                {
                    var prop = props.FirstOrDefault(x => x.Name.ToLower() == field.Key.ToLower());
                    if (prop != null)
                    {
                        var caption = string.IsNullOrEmpty(field.Value) ? GetDisplayName(prop) : field.Value;
                        var fieldConfig = new OfficeDocumentFieldConfig
                        {
                            Caption = caption,
                            Name = prop.Name,
                            PropertyInfo = prop
                        };
                        newFields.Add(fieldConfig);
                    }
                }
            }
            else
            {
                foreach (var prop in props)
                {
                    var fieldConfig = new OfficeDocumentFieldConfig
                    {
                        Caption = GetDisplayName(prop),
                        Name = prop.Name,
                        PropertyInfo = prop
                    };
                    newFields.Add(fieldConfig);
                }
            }

            if (Fields == null)
            {
                Fields = newFields;
            }
            // protection from wrong use when this method is called after other methods (this method should be called first)
            // take 
            else
            {
                RestoreFieldConfig(newFields);
            }
        }

        public void ConfigureDateFormatting(IDictionary<string, Func<DateTime, string>> dateFormatters)
        {
            if (Fields == null)
            {
                ConfigureFields(null);
            }
            foreach (var formatter in dateFormatters)
            {
                var fieldConfig = Fields.FirstOrDefault(x => x.Name == formatter.Key);
                if (fieldConfig != null)
                {
                    fieldConfig.DateFormatter = formatter.Value;
                }
            }
        }

        private void RestoreFieldConfig(List<OfficeDocumentFieldConfig> newFields)
        {
            var oldFields = Fields;
            foreach (var newField in newFields)
            {
                var oldField = Fields.FirstOrDefault(x => x.Name == newField.Name);
                if (oldField != null)
                {
                    newField.DateFormatter = oldField.DateFormatter;
                }
            }
            Fields = newFields;
        }

        private string GetDisplayName(PropertyInfo propInfo)
        {
            var displayNameAttribute = propInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayNameAttribute == null)
            {
                return propInfo.Name;
            }

            return displayNameAttribute.Name;
        }
    }
}

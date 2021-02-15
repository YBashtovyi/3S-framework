using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Data
{
    public interface IOfficeDocumentOptions<in TData> where TData: class
    {
        //public virtual List<OfficeDocumentFieldConfig> Fields { get; protected set; }
        /// <summary>
        /// Default date formatting function that is used when there is no dedicated formatting function for concrete field
        /// </summary>
        Func<DateTime, string> DefaultDateFormatter { get; set; }
        /// <summary>
        /// Configures fields for service
        /// </summary>
        /// <param name="fields">Key - field name, value - it's display name</param>
        void ConfigureFields(IDictionary<string, string> fields);
        /// <summary>
        /// Configures date formatters for fields
        /// </summary>
        /// <param name="dateFormatters">Key - field name, value - function that formats date to string</param>
        void ConfigureDateFormatting(IDictionary<string, Func<DateTime, string>> dateFormatters);
    }
}

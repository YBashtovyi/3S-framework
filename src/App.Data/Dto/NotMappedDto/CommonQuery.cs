using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Represents query parameters to select data from database
    /// </summary>
    public class CommonQuery
    {
        /// <summary>
        /// Filters collection where key - field name
        /// </summary>
        public IDictionary<string, string> Filters { get; set; }

        /// <summary>
        /// Order by fields. Can be list separated by comma
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Records count to skip
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Records count to select 
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Caching duration if needed
        /// </summary>
        /// <remarks>
        /// Caching takes into account current filter and pagination
        /// </remarks>
        public int CacheDuration { get; set; }
    }
}

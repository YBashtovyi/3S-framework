using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Data
{
    /// <summary>
    /// Defines entites, that have caption
    /// </summary>
    public interface ICaption
    {
        /// <summary>
        /// Field contains human-readable representation, that also can be calculated before saving to database
        /// </summary>
        string Caption { get; set; }
    }
}

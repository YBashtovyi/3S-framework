using System;
using System.Linq;

namespace Core.Data
{
    /// <summary>  
    /// User's culture information.  
    /// </summary>  
    public class BaseUserCultureInfo
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="UserCultureInfo"/> class.  
        /// </summary>  
        public BaseUserCultureInfo()
        {
            DateTimeFormat = "dd.MM.yyyy HH:m:ss"; // Default format.
        }
        /// <summary>  
        /// Gets or sets the date time format.  
        /// </summary>  
        /// <value>  
        /// The date time format.  
        /// </value>  
        public string DateTimeFormat { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Administration
{
    [Flags]
    public enum CrudOperation
    {
        /// <summary>
        /// Nothing is selected. Means not initialized.
        /// </summary>
        None = 0,
        /// <summary>
        /// Create
        /// </summary>
        C = 1,
        /// <summary>
        /// Read
        /// </summary>
        R = 2,
        /// <summary>
        /// Update
        /// </summary>
        U = 4,
        /// <summary>
        /// Delete
        /// </summary>
        D = 8,
        /// <summary>
        /// All
        /// </summary>
        A = 16,
        /// <summary>
        /// Ban
        /// </summary>
        B = 32
    }
}

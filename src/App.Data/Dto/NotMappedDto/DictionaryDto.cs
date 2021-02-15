using Core.Base.Data;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Contains main info from any dictionary
    /// </summary>
    /// <remarks>
    /// General purpose - to transfer information from backend to frontend
    /// Key usage: get data from database, then map to this type
    /// </remarks>
    public class DictionaryDto: BaseDto
    {
        /// <summary>
        /// Dictionary code. Usually is unique
        /// </summary>
        public string Code { get; set; }
    }
}

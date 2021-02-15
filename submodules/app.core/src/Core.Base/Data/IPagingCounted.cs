namespace Core.Base.Data
{
    /// <summary>
    /// Used when count of records from database should be evaluated
    /// </summary>
    /// <remarks>
    /// When some records fetched from database with pagination, the total record count
    /// is different from the actual count fetched. To be able to use pagination with page numbers
    /// we should know a real count of records
    /// </remarks>
    public interface IPagingCounted
    {
        /// <summary>
        /// Records count.
        /// </summary>
        int TotalRecordCount { get; set; }
    }
}

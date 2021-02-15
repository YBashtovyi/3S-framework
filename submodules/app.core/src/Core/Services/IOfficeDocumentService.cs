using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Core.Base.Data;
using Core.Data;

namespace Core.Services.Data
{
    public interface IOfficeDocumentService
    {
        /// <summary>
        /// Generate stream that can be saved as office document file
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataCollection"></param>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        byte[] GenerateStream<TData>(IEnumerable<TData> dataCollection, Action<IOfficeDocumentOptions<TData>> optionsBuilder) where TData : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream">Stream of supported file format</param>
        /// <param name="sheetName">Name of the sheet. If not set, the first sheet will be read</param>
        /// <param name="columns">Dictionary, where key - current column name from the stream, value - resulting column name
        /// If not set, columns will be named by default
        /// </param>
        /// <returns>DataTable with columns and rows from the given set</returns>
        DataTable ReadStream(Stream stream, string sheetName = null, Dictionary<string, string> columns = null);
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business.Helpers;
using App.Data.Dto.NotMappedDto;
using Core.Common.Helpers;
using Core.Services.Data;
using Microsoft.Extensions.Logging;

namespace App.Business.Services.ApplicationServices
{
    public class CommonDtoService
    {
        public ICommonDataService DataService { get; }
        private readonly ILogger<CommonDtoService> _logger;

        public CommonDtoService(ICommonDataService dataService, ILogger<CommonDtoService> logger)
        {
            DataService = dataService;
            _logger = logger;
        }

        /// <summary>
        /// Get dtos collections of different types from database
        /// </summary>
        /// <param name="queries">Collection of parameters for every dto to select from database, where key - dto type name</param>
        /// <returns>Dto collections, where key - dto type name and value - dto collection fetched from the database</returns>
        public async Task<Dictionary<string, object>> GetDtoCollectionsAsync(IDictionary<string, CommonQuery> queries) 
        {
            var result = new Dictionary<string, object>();
            foreach (var query in queries)
            {
                var dtoType = ReflectionHelper.GetTypeByName(query.Key);
                var data = await Task.Factory.StartNew(() =>
                    ReflectionHelper.InvokeGenericMethod(this, dtoType, nameof(GetDto), new object[] { query.Value }));
                result.Add(query.Key, data);
            }

            return result;
        }

        /// <summary>
        /// Gets application enums with its values and display names
        /// </summary>
        /// <param name="names">Enum names</param>
        /// <returns>Collection, where key - enum name and value - list of its values <see cref="EnumDto"/></returns>
        public IEnumerable<KeyValuePair<string, List<EnumDto>>> GetEnumValues(string[] names)
        {
            var result = EnumHelper.Enums.Where(x => names.Contains(x.Key));
            return result;
        }

        /// <summary>
        /// Gets application enum with its values and display names
        /// </summary>
        /// <param name="name">Enum name</param>
        /// <returns>List of enum values <see cref="EnumDto"/></returns>
        public List<EnumDto> GetEnumValues(string name)
        {
            var result = EnumHelper.Enums.Where(x => x.Key == name).FirstOrDefault();
            return result.Value;
        }

        private object GetDto<TDto>(CommonQuery query) where TDto: class
        {
            var orderBy = query.OrderBy ?? string.Empty;
            var data = DataService.GetDto<TDto>(orderBy, null, query.Filters, query.Skip, query.Take, query.CacheDuration, null, null);
            return data;
        }
    }
}

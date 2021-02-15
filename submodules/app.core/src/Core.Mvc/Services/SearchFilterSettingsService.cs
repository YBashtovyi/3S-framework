using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Core.Data;
using Core.Mvc.Attributes;
using Core.Services.Data;
using Core.Services;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Mvc.Services
{
    public class SearchFilterSettingsService: ISearchFilterSettingsService
    {
        private readonly IMemoryCache _cache;
        private readonly BaseUserInfo _userInfo;
        private readonly ICommonDataService _dataService;
        private readonly ILogger<SearchFilterSettingsService> _logger;

        private Dictionary<string, SelectList> DictionaryOfSelectLists;
        private delegate object GeneratedAttributeNode(string type, string labelName, string memberName);


        public SearchFilterSettingsService(IMemoryCache cache, ICommonDataService dataService,
            IUserInfoService userInfoService, ILogger<SearchFilterSettingsService> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _userInfo = userInfoService.GetCurrentUserInfo();
            _dataService = dataService;
            _logger = logger;
        }

        public string GenerateInputConfig(Type dtoType)
        {
            return GenerateInputConfig(dtoType, null);
        }

        public string GenerateInputConfig(Type dtoType, Dictionary<string, SelectList> dictionaryOfSelectLists)
        {
            DictionaryOfSelectLists = dictionaryOfSelectLists;

            var cacheName = dtoType.FullName + "Filter";
            if (!_cache.TryGetValue(cacheName, out string result))
            {
                var listNodes = new List<object>();
                try
                {
                    var dtoMembersWithSfAttribute = dtoType.GetMembers()
                        .Where(x => x.GetCustomAttributes(typeof(SearchFilterAttribute), true).Any())
                        .Select(m => new
                        {
                            Attribute = m.GetCustomAttributes(typeof(SearchFilterAttribute), true).FirstOrDefault(),
                            m.Name
                        })
                        .OrderBy(a => a.Attribute.GetPropValue("Order")).ToList();

                    foreach (var member in dtoMembersWithSfAttribute)
                    {
                        listNodes.Add(GenerateNode(member.Attribute, member.Name));
                    }
                    result = JsonConvert.SerializeObject(listNodes);
                    _cache.Set(cacheName, result);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Search filter service error");
                }
            }
            return result;
        }

        private object GenerateNode(object attribute, string memberName)
        {
            object node;
            var type = attribute.GetPropValue("FieldType").ToString();
            var labelName = attribute.GetPropValue("LabelName").ToString();
            var isDataGroup = (bool)attribute.GetPropValue("DataGroup");

            if (isDataGroup)
            {
                var nodeFrom = GenerateNode(type, "з:", memberName + "_From");
                var nodeTo = GenerateNode(type, "по:", memberName + "_To");
                node = GenerateDataGroupNode(nodeFrom, nodeTo, labelName);
            }
            else
            {
                node = GenerateNode(type, labelName, memberName);
            }
            return node;
        }

        private object GenerateDataGroupNode(object nodeFrom, object nodeTo, string labelName)
        {
            var children = new List<object>() { nodeFrom, nodeTo };
            return new
            {
                labelName,
                type = "dataGroup",
                children
            };
        }

        //Types:
        //text, hidden,
        //select,
        //datepicker, datepickerBefore, datepickerAfter,datepickerBeforeAfter,
        //checkbox
        private object GenerateNode(string type, string labelName, string memberName)
        {
            GeneratedAttributeNode nodeGenerator = null;
            switch (type)
            {
                case "text":
                case "hidden":
                    nodeGenerator = GenerateTextNode;
                    break;
                case "select":
                    nodeGenerator = GenerateSelectlistNode;
                    break;
                case "datepicker":
                case "datepickerBefore":
                case "datepickerAfter":
                case "datepickerBeforeAfter":
                    nodeGenerator = GenerateDatepickerNode;
                    break;
                case "checkbox":
                    nodeGenerator = GenerateCheckboxNode;
                    break;
            }
            return nodeGenerator(type, labelName, memberName);
        }


        private object GenerateTextNode(string type, string labelName, string memberName)
        {
            return new
            {
                labelName,
                name = memberName,
                type
            };
        }

        private object GenerateSelectlistNode(string type, string labelName, string memberName)
        {
            if (!DictionaryOfSelectLists.TryGetValue(memberName, out var selectOptions))
            {
                selectOptions = null;
            }
            return new
            {
                labelName,
                name = memberName,
                type,
                selectOptions
            };
        }

        private object GenerateDatepickerNode(string type, string labelName, string memberName)
        {
            return new
            {
                labelName,
                name = memberName,
                type,
                validate = "['date']"
            };
        }

        private object GenerateCheckboxNode(string type, string labelName, string memberName)
        {
            return new
            {
                labelName,
                name = memberName,
                type,
                value = false
            };
        }

        public async Task<string> GetUserPresettingsAsync(string journalName)
        {
            var user = _userInfo.Id.ToString();
            return (await _dataService.GetEntityAsync<UserPresettings>(x => x.User == user && x.JournalName == journalName, true))
                .SingleOrDefault()?.PresettingsJson;
        }

        public async Task SetUserPresettings(string journalName, string presettingsJson)
        {
            var user = _userInfo.Id.ToString();
            var item = (await _dataService.GetEntityAsync<UserPresettings>(x => x.User == user && x.JournalName == journalName, false))
                .SingleOrDefault();

            var itemExists = (item != null);
            if (itemExists)
            {
                item.PresettingsJson = presettingsJson;
            }
            else
            {
                item = new UserPresettings
                { JournalName = journalName, User = user, PresettingsJson = presettingsJson };
            }

            _dataService.Add(item, itemExists);
            await _dataService.SaveChangesAsync();
        }
    }
}

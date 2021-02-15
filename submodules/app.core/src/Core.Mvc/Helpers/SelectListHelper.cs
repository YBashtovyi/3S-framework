using Core.Base.Data;
using Core.Common.Extensions;
using Core.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers
{
    public class SelectListHelper
    {
        private readonly ICommonDataService _dataService;

        public SelectListHelper(ICommonDataService dataService)
        {
            _dataService = dataService;
        }

        private void InitSelectList(ref SelectList list, string initialSelectedValue)
        {
            if ((initialSelectedValue != null) && (list.Count() > 0))
            {
                if (initialSelectedValue == "")
                {
                    var oldList = list.ToList();
                    oldList.Insert(0, new SelectListItem("", "", true));
                    list = new SelectList(oldList, "Value", "Text", list.SelectedValue);
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (item.Value == initialSelectedValue)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        public async Task<SelectList> ListAsync<T>(
            string idPropertyName = "Id",
            string textPropertyName = "Caption",
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string initialSelectedValue = null,
            int skip = 0,
            int take = 0,
            int expirationTimeSeconds = 0) where T : CoreDto
        {
            var data = await _dataService.GetDtoAsync<T>(predicate, orderBy, null, skip, take, expirationTimeSeconds, null, null);

            var selList = new SelectList(data, idPropertyName, textPropertyName);
            
            InitSelectList(ref selList, initialSelectedValue);
            return selList;
        }

        public async Task<SelectList> EnumAsync<TEnumRecord>(string enumType,
            Expression<Func<TEnumRecord, bool>> predicate = null,
            Func<IQueryable<TEnumRecord>, IOrderedQueryable<TEnumRecord>> orderBy = null,
            string initialSelectedValue = null,
            int take = 0,
            int expirationTimeSeconds = 600) where TEnumRecord: BaseEnumRecord
        {
            Expression<Func<TEnumRecord, bool>> filter;
            if (predicate == null)
            {
                filter = x => x.Group.ToLower() == enumType.ToLower();
            } else
            {
                filter = predicate.And(x => x.Group.ToLower() == enumType.ToLower());
            }

            var data = await _dataService.GetEntityAsync(filter, orderBy, 0, take, true, expirationTimeSeconds, null, null);

            var selList = new SelectList(data, "Code", "Caption");

            InitSelectList(ref selList, initialSelectedValue);
            return selList;
        }

        
        public async Task<MultiSelectList> MultiSelectAsync<T>(
            IEnumerable selectedItems = null,
            string idPropertyName = "Id",
            string textPropertyName = "Caption",
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int skip = 0,
            int take = 0,
            int expirationTimeSeconds = 0) where T : CoreDto
        {
            var data = await _dataService.GetDtoAsync<T>(predicate, orderBy, null, skip, take, expirationTimeSeconds, null, null);

            return new MultiSelectList(data, idPropertyName, textPropertyName, selectedItems);
        }
    }
}

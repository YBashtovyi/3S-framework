using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace ReflectionIT.Mvc.Paging
{

    public class PagingList<T>: List<T>, IPagingList<T> where T : class
    {

        public int PageIndex { get; }
        public int PageCount { get; }
        public int TotalRecordCount { get; }
        public string Action { get; set; }
        public string PageParameterName { get; set; }
        public string SortExpressionParameterName { get; set; }
        public string SortExpression { get; }

        public string DefaultSortExpression { get; }

        //[Obsolete("Use PagingList.CreateAsync<T>() instead")]
        //public static Task<PagingList<T>> CreateAsync(IOrderedQueryable<T> qry, int pageSize, int pageIndex)
        //{
        //    return PagingList.CreateAsync(qry, pageSize, pageIndex);
        //}

        //[Obsolete("Use PagingList.CreateAsync<T>() instead")]
        //public static Task<PagingList<T>> CreateAsync(IQueryable<T> qry, int pageSize, int pageIndex, string sortExpression, string defaultSortExpression)
        //{
        //    return PagingList.CreateAsync(qry, pageSize, pageIndex, sortExpression, defaultSortExpression);
        //}

        internal PagingList(IEnumerable<T> list, int pageIndex, int pageCount, int totalRecordCount)
            : base(list)
        {
            TotalRecordCount = totalRecordCount;
            PageIndex = pageIndex;
            PageCount = pageCount;
            Action = "Index";
            PageParameterName = "pg_Page";
            SortExpressionParameterName = "pg_SortExpression";
        }

        internal PagingList(IEnumerable<T> list, int pageIndex, int pageCount, string sortExpression, string defaultSortExpression, int totalRecordCount)
            : this(list, pageIndex, pageCount, totalRecordCount)
        {

            SortExpression = sortExpression;
            DefaultSortExpression = defaultSortExpression;
        }

        public RouteValueDictionary RouteValue { get; set; }

        public RouteValueDictionary GetRouteValueForPage(int pageIndex)
        {

            var dict = RouteValue == null ? new RouteValueDictionary() :
                                                 new RouteValueDictionary(RouteValue);

            dict[PageParameterName] = pageIndex;

            if (SortExpression != DefaultSortExpression)
            {
                dict[SortExpressionParameterName] = SortExpression;
            }

            return dict;
        }

        public RouteValueDictionary GetRouteValueForSort(string sortExpression)
        {

            var dict = RouteValue == null ? new RouteValueDictionary() :
                                                 new RouteValueDictionary(RouteValue);

            if (sortExpression == SortExpression)
            {
                sortExpression = "-" + sortExpression;
            }

            dict[SortExpressionParameterName] = sortExpression;

            return dict;
        }

        public int NumberOfPagesToShow { get; set; } = PagingOptions.Current.DefaultNumberOfPagesToShow;

        public int StartPageIndex
        {
            get
            {
                var half = (int)((NumberOfPagesToShow - 0.5) / 2);
                var start = Math.Max(1, PageIndex - half);
                if (start + NumberOfPagesToShow - 1 > PageCount)
                {
                    start = PageCount - NumberOfPagesToShow + 1;
                }
                return Math.Max(1, start);
            }
        }

        public int StopPageIndex => Math.Min(PageCount, StartPageIndex + NumberOfPagesToShow - 1);

    }
}

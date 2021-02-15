using Core.Base.Data;

namespace Core.Mvc.Data
{
    public class ActionListOption<TDto> where TDto: IPagingCounted
    {
        public string pg_SortExpression { get; set; }
        public int pg_Page { get; set; } = 1;
        public string pg_PartialViewName { get; set; } = "List";
        //public IQueryable<TDTO> pg_QueryList { get; set; }
    }
}

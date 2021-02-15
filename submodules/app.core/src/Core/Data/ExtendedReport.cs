using System.Collections.Generic;
using Core.Base.Data;
using Core.Services.Data;

namespace Core.Data.Reports
{
    public class ExtendedReport<TPredicateDto, TResultDto>: BaseReport<TResultDto>
        where TPredicateDto : CoreDto
        where TResultDto : CoreDto
    {
        private readonly IQueryTextService _queryTextHelper;

        public ExtendedReport(IQueryTextService queryTextHelper, ICommonDataService dataService) : base(dataService)
        {
            _queryTextHelper = queryTextHelper;
        }

        public override void Generate(IDictionary<string, string> paramList)
        {
            var nestedSelect = _queryTextHelper.GetParameterizedQueryString(typeof(TPredicateDto), paramList);
            ReportData = DataService.GetDto<TResultDto>("", null, null, 0, 0, 0, null, new object[] { nestedSelect });
        }
    }
}

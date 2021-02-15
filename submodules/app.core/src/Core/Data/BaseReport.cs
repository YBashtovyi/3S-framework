using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Base.Data;
using Core.Services.Data;

namespace Core.Data.Reports
{
    public class BaseReport<TReportDto> where TReportDto : CoreDto
    {
        public IEnumerable<TReportDto> Data => ReportData;
        protected IEnumerable<TReportDto> ReportData;
        protected ICommonDataService DataService { get; }

        public BaseReport(ICommonDataService dataService)
        {
            DataService = dataService;
        }

        public virtual void Generate()
        {
            GenerateReport();
        }

        public virtual void Generate(Expression<Func<TReportDto, bool>> predicate)
        {
            GenerateReport(predicate: predicate);
        }

        public virtual void Generate(IDictionary<string, string> paramList)
        {
            GenerateReport(parameters: paramList);
        }

        public virtual void Generate(IDictionary<string, string> paramList, Expression<Func<TReportDto, bool>> predicate)
        {
            GenerateReport(paramList, predicate);
        }

        protected virtual void ProcessData()
        {

        }

        private void GenerateReport(IDictionary<string, string> parameters = null, Expression<Func<TReportDto, bool>> predicate = null)
        {
            ReportData = DataService.GetDto(predicate, null, parameters, 0, 0, 0, null, new object[0]);

            ProcessData();
        }
    }
}



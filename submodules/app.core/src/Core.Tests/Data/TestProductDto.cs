using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Tests
{
    internal class TestProductDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public decimal Price { get; set; }
    }

    internal class TestProductDetailDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public decimal Price { get; set; }
        [CaseFilter(CaseFilterOperation.Contains)]
        public string Manufacturer { get; set; }
    }

    internal class TestProductListDto: BaseDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public decimal Price { get; set; }
    }
}

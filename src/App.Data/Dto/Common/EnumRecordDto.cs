using System;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(EnumRecord))]
    [RightsCheckList(nameof(EnumRecord))]
    [NotMapped]
    public class EnumRecordDto: BaseEnumRecordDto
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public override string Group { get => base.Group; set => base.Group = value; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public override string GroupName { get => base.GroupName; set => base.GroupName = value; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public override string Code { get => base.Code; set => base.Code = value; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Value { get => base.Value; set => base.Value = value; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public int ItemNumber { get; set; }
    }

    [MainEntity(nameof(EnumRecord))]
    [RightsCheckList(nameof(EnumRecord))]
    public class EnumRecordListDto: BaseEnumRecordDto, IPagingCounted
    {
        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public override string Group { get => base.Group; set => base.Group = value; }
        [CaseFilter(CaseFilterOperation.Equals)]
        public override string Code { get => base.Code; set => base.Code = value; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public override string Value { get => base.Value; set => base.Value = value; }

        [CaseFilter(CaseFilterOperation.Equals)]
        public int ItemNumber { get; set; }
        public int TotalRecordCount { get; set; }
    }
}

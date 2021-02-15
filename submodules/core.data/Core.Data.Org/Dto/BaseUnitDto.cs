using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace Core.Data.Org.Dto
{
    public interface IOrgUnitDetailDto
    {
        Guid Id { get; set; }
        string FullName { get; set; }
        Guid? ParentId { get; set; }
        string Parent { get; set; }
        string Description { get; set; }
        //string PropertiesJson { get; set; }
        //string AtuAddressGuids { get; set; }
    }

    public interface IOrgUnitListDto
    {
        Guid Id { get; set; }
        string FullName { get; set; }
    }

    //[RightsCheckList(nameof(OrgUnit))]
    public abstract class BaseUnitDetailDto : BaseDto, IOrgUnitDetailDto
    {
        [Display(Name = "Назва")]
        public virtual string FullName { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual string Parent { get; set; }
        public virtual string Description { get; set; }
        //public string PropertiesJson { get; set; }
        public virtual string AtuAddressGuids { get; set; }
    }

    //[RightsCheckList(nameof(OrgUnit))]
    public abstract class BaseUnitListDto : BaseDto, IOrgUnitListDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }

        [Display(Name = "Повна назва")]
        [CaseFilter(CaseFilterOperation.Contains)]
        public virtual string FullName { get; set; }


        //public string PropertiesJson { get; set; }
    }

    //[RightsCheckList(nameof(OrgUnit))]
    public abstract class BaseUnitMinDto : BaseDto
    {
        public virtual string Name { get; set; }
    }
}

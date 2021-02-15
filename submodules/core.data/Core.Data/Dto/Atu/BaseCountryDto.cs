using Core.Base.Data;

namespace Core.Data.Dto.Atu
{
    public abstract class BaseCountryListDto: CoreDto, IPagingCounted
    {
        public virtual int TotalRecordCount { get; set; }
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Code { get; set; }
        public virtual string Caption { get; set; }
        public virtual string Comment { get; set; }
    }

    public abstract class BaseCountryEditDto : CoreDto
    {
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Code { get; set; }
        public virtual string Caption { get; set; }
        public virtual string Comment { get; set; }
    }

    public abstract class BaseCountryDetailsDto : CoreDto
    {
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Code { get; set; }
        public virtual string Caption { get; set; }
        public virtual string Comment { get; set; }
    }
}

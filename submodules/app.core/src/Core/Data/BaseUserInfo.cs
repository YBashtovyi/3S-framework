using System;
using System.Collections.Generic;
using Core.Administration;
using Core.Base.Administration;

namespace Core.Data
{
    public abstract class BaseUserInfo: IUserApplicationRights
    {
        public virtual Guid Id { get; set; }
        public virtual Dictionary<string, string> LoginData { get; set; }
        public virtual string AccountId { get; set; }
        public virtual BaseUserCultureInfo CultureInfo { get; set; }

        public abstract void AssertCanReadTypeData(Type dataType);
        public abstract void AssertCanWriteTypeData(Type dataType);
        public abstract void AssertRlsAllowsObject(object obj);   
        public abstract void AssertWriteRights(string entityName, string fieldName);
        public abstract void AssertCanExecuteOperation(string operationName);
        public abstract CrudOperation GetFieldRight(string entityName, string fieldName);
        public abstract List<RowLevelRightData> GetTypeFieldsRlsRights(Type dataType);
        public abstract bool RlsAllowsAccessToObject(object obj);
        public abstract IList<RowLevelSecurityItem> GetOrgUnitRls();
        public abstract IList<RowLevelSecurityItem> GetUserRls();
        public abstract IList<RowLevelSecurityItem> GetRegionRls();
    }
}

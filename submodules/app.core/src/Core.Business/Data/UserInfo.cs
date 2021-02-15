using System;
using System.Collections.Generic;
using Core.Administration;
using Core.Base.Administration;

namespace Core.Data
{
    public class UserInfo: BaseUserInfo, IRights
    {
        public override BaseUserCultureInfo CultureInfo { get; set; }
        public UserApplicationRights Rights { get; set; }

        public override void AssertCanReadTypeData(Type dataType)
        {
            Rights.AssertCanReadTypeData(dataType);
        }

        public override void AssertRlsAllowsObject(object obj)
        {
            Rights.AssertRlsAllowsObject(obj);
        }

        public override void AssertCanWriteTypeData(Type dataType)
        {
            Rights.AssertCanWriteTypeData(dataType);
        }

        public override void AssertWriteRights(string entityName, string fieldName)
        {
            Rights.AssertWriteRights(entityName, fieldName);
        }

        public override void AssertCanExecuteOperation(string operationName)
        {
            Rights.AssertCanExecuteOperation(operationName);
        }

        public override CrudOperation GetFieldRight(string entityName, string fieldName)
        {
            return Rights == null ? CrudOperation.None : Rights.GetFieldRight(entityName, fieldName);
        }

        public override List<RowLevelRightData> GetTypeFieldsRlsRights(Type dataType)
        {
            return Rights == null ? new List<RowLevelRightData>(0) : Rights.GetTypeFieldsRlsRights(dataType);
        }

        public override bool RlsAllowsAccessToObject(object obj)
        {
            return Rights == null ? false : Rights.RlsAllowsAccessToObject(obj);
        }

        public override IList<RowLevelSecurityItem> GetOrgUnitRls()
        {
            return Rights == null ? new List<RowLevelSecurityItem>(0) : Rights.RowLevelRights.OrgUnit;
        }

        public override IList<RowLevelSecurityItem> GetUserRls()
        {
            return Rights == null ? new List<RowLevelSecurityItem>(0) : Rights.RowLevelRights.User;
        }

        public override IList<RowLevelSecurityItem> GetRegionRls()
        {
            return Rights == null ? new List<RowLevelSecurityItem>(0) : Rights.RowLevelRights.AtuObject;
        }
    }
}

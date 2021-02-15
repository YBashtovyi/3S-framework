using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.XPath;
using Core.Base.Administration;
using Core.Security;
using RowLevelRightData = Core.Base.Administration.RowLevelRightData;

namespace Core.Administration
{
    public class UserApplicationRights : IUserApplicationRights
    {
        private readonly bool _isFullRight;

        /// <summary>
        /// Entity rights dictionary, where key - is the name of entity
        /// </summary>
        public Dictionary<string, CrudOperation> EntityRights { get; set; } = new Dictionary<string, CrudOperation>();

        public List<string> InterfaceRights { get; set; } = new List<string>();

        /// <summary>
        /// Row level rights list
        /// </summary>
        public RowLevelSecurityData RowLevelRights { get; set; } = new RowLevelSecurityData();

        /// <summary>
        /// Roles that the user has, where key - guid, name - name of role
        /// </summary>
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Dictionary, containing rigths data for given type
        /// Key - entity type, for which rigths data belongs to
        /// </summary>
        private static readonly ConcurrentDictionary<Type, TypeRightsData> _typeRights = new ConcurrentDictionary<Type, TypeRightsData>();

        public UserApplicationRights() { }

        public UserApplicationRights(bool isFullRight)
        {
            _isFullRight = isFullRight;
        }

        public void AssertCanReadTypeData(Type dataType)
        {
            if (_isFullRight)
            {
                return;
            }

            return;
        }

        public void AssertCanWriteTypeData(Type dataType)
        {
            if (_isFullRight)
            {
                return;
            }

            if (dataType == null)
            {
                throw new ArgumentNullException("dataType argument cannot be null");
            }


            return;
        }

        public void AssertRlsAllowsObject(object obj)
        {
            if (_isFullRight)
            {
                return;
            }
            return;
        }

        public void AssertWriteRights(string entityName, string fieldName)
        {
            if (_isFullRight)
            {
                return;
            }
            return;
        }

        public void AssertCanExecuteOperation(string operationName)
        {
            if (_isFullRight)
            {
                return;
            }

        }

        public CrudOperation GetFieldRight(string entityName, string fieldName)
        {
            return CrudOperation.A;
        }

        public List<RowLevelRightData> GetTypeFieldsRlsRights(Type type)
        {
            var rlsList = new List<RowLevelRightData>();
            var typeRights = GetTypeRightsData(type);

            // iterating rls properties for givent type
            // rls properties defined by one or many RlsRight attributes
            foreach (var propRight in typeRights.RlsProperties)
            {
                // RowLevelRights contains all active rules
                // so if we do not found row level right, then rule does not exist or inactive and access is granted
                if (IsFullRight())
                {
                    rlsList.Add(new RowLevelRightData(propRight.PropertyName, CrudOperation.A, new List<string>()));
                    continue;
                }

                if (propRight.EntityName == "OrgUnit")
                {
                    var orgUnitIds = RowLevelRights.OrgUnit.Select(x => new { x.Id, x.AccessLevel }).ToList();
                    if (!orgUnitIds.Any())
                    {
                        rlsList.Add(new RowLevelRightData(propRight.PropertyName, CrudOperation.None, new List<string>()));
                        continue;
                    }

                    rlsList.AddRange(orgUnitIds.Select(orgUnitId => new RowLevelRightData(propRight.PropertyName, orgUnitId.AccessLevel, new List<string> { orgUnitId.Id.ToString() })));
                }
            }

            return rlsList;
        }

        public bool RlsAllowsAccessToObject(object obj)
        {
            return true;
        }

        private TypeRightsData GetTypeRightsData(Type type)
        {
            if (_typeRights.TryGetValue(type, out var rightsData))
            {
                return rightsData;
            }

            // checked entities
            var rightsCheckListAttribute = type.GetCustomAttribute<RightsCheckListAttribute>(true);
            rightsData = new TypeRightsData();
            if (rightsCheckListAttribute != null)
            {
                rightsData.CheckedEntites = rightsCheckListAttribute.CheckedEntities;
            }

            // add entity itself, if present in rights
            if (EntityRights.ContainsKey(type.Name) && !rightsData.CheckedEntites.Contains(type.Name))
            {
                rightsData.CheckedEntites.Add(type.Name);
            }

            // if MainEntityAttribute is set
            var mainEntityAttr = type.GetCustomAttribute<MainEntityAttribute>(true);
            if (mainEntityAttr != null)
            {
                rightsData.MainEntityName = mainEntityAttr.EntityName;
                if (!rightsData.CheckedEntites.Contains(rightsData.MainEntityName))
                {
                    rightsData.CheckedEntites.Add(rightsData.MainEntityName);
                }
            }

            // adding informations aboout type rls
            foreach (RlsRightAttribute rightsAttr in type.GetCustomAttributes(typeof(RlsRightAttribute), true))
            {
                if (string.IsNullOrEmpty(rightsAttr.TypeName) ||
                    string.IsNullOrEmpty(rightsAttr.PropertyName)) continue;

                var propertyInfo = type.GetProperty(rightsAttr.PropertyName);
                if (propertyInfo == null) continue;

                var propData = new RlsPropertyData(rightsAttr.TypeName, rightsAttr.PropertyName, propertyInfo);
                rightsData.RlsProperties.Add(propData);
            }

            // adding data to cache, if is not present yet
            _typeRights.TryAdd(type, rightsData);

            return rightsData;
        }

        private bool IsFullRight()
        {
            return Roles.ContainsValue("Global_Admin");
        }

        /// <summary>
        /// Represents data of RlsRightAttribute
        /// </summary> 
        private class RlsPropertyData
        {
            /// <summary>
            /// EntityName - is  name of the class, that represents RLS entity
            /// For example, if Employee has attribute RlsRight(nameof(Organization), nameof(OrganizationId)),
            /// then EntityName = "Organization"
            /// </summary>
            public string EntityName { get; set; }
            /// <summary>
            /// PropertyName - is  property name that references to entity with name EntityName
            /// For example, if Employee has attribute RlsRight(nameof(Organization), nameof(OrganizationId)),
            /// then PropertyName = "OrganizationId"
            /// </summary>
            public string PropertyName { get; set; }
            /// <summary>
            /// PropertyInfo for property with name PropertyName
            /// </summary>
            public PropertyInfo PropertyInfo { get; set; }

            public RlsPropertyData(string entityName, string propertyName, PropertyInfo propertyInfo)
            {
                EntityName = entityName;
                PropertyName = propertyName;
                PropertyInfo = propertyInfo;
            }
        }

        private class TypeRightsData
        {
            /// <summary>
            /// MainEntityAttribute value for EntityType
            /// </summary>
            public string MainEntityName { get; set; } = string.Empty;
            /// <summary>
            /// These entities will be included into rights check for given EntityType (RightsCheckListAttribute values)
            /// </summary>
            public List<string> CheckedEntites { get; set; } = new List<string>();
            /// <summary>
            /// Represents model properties, that should be checked by rls system.
            /// These properties are described by RlsRightAttribute
            /// </summary>
            public List<RlsPropertyData> RlsProperties { get; set; } = new List<RlsPropertyData>();
        }
    }
}

using System;
using System.Collections.Generic;
using Core.Base.Administration;

namespace Core.Administration
{
    /// <summary>
    /// Represents rights operations with entities
    /// </summary>
    public interface IUserApplicationRights
    {
        /// <summary>
        /// Asserts that user has permission to read current type data
        /// </summary>
        /// <param name="dataType"></param>
        void AssertCanReadTypeData(Type dataType);

        /// <summary>
        /// Asserts that user has permission to create, update or delete current type data
        /// </summary>
        /// <param name="dataType"></param>
        void AssertCanWriteTypeData(Type dataType);

        /// <summary>
        /// Asserts that user has permission to access an object within given rls
        /// </summary>
        /// <param name="obj">Object to check</param>
        void AssertRlsAllowsObject(object obj);

        /// <summary>
        /// Asserts that user has permission to access a given field of an entity
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        void AssertWriteRights(string entityName, string fieldName);

        /// <summary>
        /// Asserts that user has permission to perform an operation
        /// </summary>
        /// <param name="operationName"></param>
        void AssertCanExecuteOperation(string operationName);

        /// <summary>
        /// Gets user access level to given entity field
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        CrudOperation GetFieldRight(string entityName, string fieldName);

        /// <summary>
        /// Gets user rls data for given type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        List<RowLevelRightData> GetTypeFieldsRlsRights(Type dataType);

        /// <summary>
        /// Checks that user has permission to access an object within given rls
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if user has permission, otherwise false</returns>
        bool RlsAllowsAccessToObject(object obj);
       
    }
}

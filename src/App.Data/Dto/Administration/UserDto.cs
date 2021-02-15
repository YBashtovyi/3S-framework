using System;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Administration;
using Core.Administration.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Administration
{
    [MainEntity(nameof(User))]
    public class UserListDto: CoreDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }

        public Guid EmployeeId { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonMiddleName { get; set; }

        public string PersonLastName { get; set; }

        public string OrgUnitName { get; set; }

        public string OrgUnitPositionName { get; set; }

        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(User))]
    public class UserDetailsDto : CoreDto
    {
        public Guid EmployeeId { get; set; }

        public string PersonFirstName { get; set; }

        public string PersonMiddleName { get; set; }

        public string PersonLastName { get; set; }

        public string OrgUnitName { get; set; }

        public string OrgUnitPositionName { get; set; }

        public string Rls { get; set; }

        public string Roles { get; set; }
    }

    [MainEntity(nameof(User))]
    public class UserLoginDto: CoreDto
    {
        /// <summary>
        /// Id of <see cref="Employee"/>
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }
    }

    /// <summary>
    /// This class stores user profile data
    /// </summary>
    /// <remarks>
    /// The main goal of this dto is to get required data for logged in
    /// user and pass it to the frontend.
    /// </remarks>
    [MainEntity(nameof(Employee))]
    [RlsRight(nameof(Organization), nameof(OrganizationId))]
    public class UserFullDto
    {
        /// <summary>
        /// Id of <see cref="Employee"/>
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Id of AdmUser
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Caption of <see cref="Person"/>
        /// </summary>
        public string PersonFullName { get; set; }

        /// <summary>
        /// If of <see cref="Organization"/>
        /// </summary>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Caption of <see cref="Organization"/>
        /// </summary>
        public string OrganizationCaption { get; set; }

        /// <summary>
        /// <see cref="Person"/> name
        /// </summary>
        public string PersonFirstName { get; set; }

        /// <summary>
        /// <see cref="Person"/> middle name
        /// </summary>
        public string PersonMiddleName { get; set; }

        /// <summary>
        /// <see cref="Person"/> last name
        /// </summary>
        public string PersonLastName { get; set; }

        /// <summary>
        /// <see cref="Position"/>
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Rights object that contains all application rights of the user:
        ///   rights on entities, rights on records (RLS), rights on operations
        /// </summary>
        /// <remarks>
        /// This object is filled from different tables depending on user profile
        /// with help of complex algorithm nad should be stored in cache while user is logged in
        /// </remarks>
        [NotMapped]
        public UserApplicationRights Rights { get; set; }
    }
}

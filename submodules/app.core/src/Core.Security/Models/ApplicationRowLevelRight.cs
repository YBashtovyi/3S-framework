using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Security.Models
{
    /// <summary>
    /// Represents existing row level rights in the application.
    /// </summary>
    [Table("Sys" + nameof(ApplicationRowLevelRight))]
    [Display(Name = "Існуючі доступи на рівні записів")]
    public class ApplicationRowLevelRight: CoreEntity, ICaption
    {
        public string Caption { get; set; } = string.Empty;
        /// <summary>
        /// Name of the entity that separates access to the table
        /// For example, for EntityName = "Organization" access to application tables will be separated by organizations
        /// In such case an allowed organizations list should be defined for every user 
        /// </summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Set value to false if you want to disable using this application row level right
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Means that rights are also checked for child records 
        /// </summary>
        public bool IsHierarchical { get; set; }

        /// <summary>
        /// The column that connects a record with a parent record. For example, ParentId
        /// </summary>
        public string HierarchyFieldName { get; set; } = string.Empty;
    }
}

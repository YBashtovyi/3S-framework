using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Represents message with entity data connected and error description
    /// </summary>
    public class IntegrationServiceErrorDto : CoreDto
    {
        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public string EntityName { get; set; }

        /// <summary>
        /// Entity name in another system
        /// </summary>
        public string MappedEntityName { get; set; }

        public string EntityId { get; set; }

        /// <summary>
        /// Entity id in another system
        /// </summary>
        public string MappedEntityId { get; set; }

        /// <summary>
        /// Message is processed by receiver
        /// </summary>
        public bool Processed { get; set; }

        public DataState DataState { get; set; }

        /// <summary>
        /// Date of operation on entity (create, update, delete) in sender's system
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Entity fields/values
        /// </summary>
        public Dictionary<string, string> EntityData { get; set; }
    }
}

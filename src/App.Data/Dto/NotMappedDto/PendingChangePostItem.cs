using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// DTO which used to Post Data to integration service
    /// </summary>
    public class PendingChangePostItem
    {
        /// <summary>
        /// name of the entity
        /// must be the same as in config in the integration service
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Id of the entity
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// Shows if pending change is processed
        /// </summary>
        public bool Processed { get; set; }

        /// <summary>
        /// Data state of the message:
        /// <see cref="DataState"/>.
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// Shows when the operation was performed
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Inner data of the entity
        /// </summary>
        public Dictionary<string,string> EntityData { get; set; }
    }

    /// <summary>
    /// DTO which used to get response from integration service 
    /// after sending changes to it  
    /// </summary>
    public class PostResponseMessageDto
    {
        /// <summary>
        /// name of the entity
        /// must be the same as in config in the integration service
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Id of the entity
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Error while processing this message
        /// </summary>
        public string Error { get; set; }
    }

    /// <summary>
    /// DTO which used to receive data from integration service
    /// </summary>
    public class GetMessageDto: CoreDto
    {
        /// <summary>
        /// name of the entity
        /// must be the same as in config in the integration service
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Id of the entity
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Data state of the message:
        /// <see cref="DataState"/>.
        /// </summary>
        public DataState DataState { get; set; }

        /// <summary>
        /// Shows when the operation was performed
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Inner data of the entity
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> EntityData { get; set; }

        /// <summary>
        /// Dictionary which uses integration service 
        /// to send links to the unmapped messages
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> SystemValues { get; set; }
    }

    /// <summary>
    /// DTO which used to send response to the integration service
    /// after received data was processed
    /// </summary>
    public class ProcessMessageDto: CoreDto
    {
        /// <summary>
        /// name of the entity
        /// must be the same as in config in the integration service
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Id of the entity
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Shows when the operation was performed
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Error while processing this message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Data state of the message:
        /// <see cref="NotMappedDto.DataState"/>.
        /// </summary>
        public DataState DataState { get; set; }
    }


    /// <summary>
    /// Enum which represents the data state of the message
    /// </summary>
    public enum DataState
    {
        Added,
        Modified,
        Deleted
    }
}

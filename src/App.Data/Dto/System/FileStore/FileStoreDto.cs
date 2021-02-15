using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Common.Attributes;
using Core.Common.Enums;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(FileStore))]
    [RightsCheckList(nameof(FileStore))]
    //[NotMapped]
    public class FileStoreDto: BaseFileStoreDto
    {
        public DateTime CreatedOn { get; set; }

        public string TypeOfAttachedFileName { get; set; }
    }
}

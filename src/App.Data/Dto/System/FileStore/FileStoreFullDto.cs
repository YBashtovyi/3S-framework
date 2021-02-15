using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Models;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.System
{
    [MainEntity(nameof(FileStore))]
    [RightsCheckList(nameof(FileStore))]
    [NotMapped]
    public class FileStoreFullDto: BaseFileStoreFullDto
    {
    }
}

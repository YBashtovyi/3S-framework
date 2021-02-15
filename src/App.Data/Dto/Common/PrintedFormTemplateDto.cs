using App.Data.Models;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    /// <inheritdoc/>
    [MainEntity(nameof(PrintedFormTemplate))]
    public class PrintedFormTemplateDto : BasePrintedFormTemplateDto
    {
    }
}

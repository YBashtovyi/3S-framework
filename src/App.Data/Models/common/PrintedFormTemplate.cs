using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;


namespace App.Data.Models
{
    /// <inheritdoc/>
    [MainEntity(nameof(PrintedFormTemplate))]
    [Table("CmnPrintedFormTemplate")]
    public class PrintedFormTemplate : BasePrintedFormTemplate
    {
    }
}

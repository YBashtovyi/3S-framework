using System;
using Core.Base.Data;

namespace Core.Data.Mis.Dto
{
    public class BaseMedicalExaminationDto: BaseDocumentDto
    {
        public virtual string Comment { get; set; }

        /// <summary>
        /// EnumRecord EnumType = "MedicalDocumentType"
        /// </summary>
        public virtual Guid DocumentTypeId { get; set; }
    }
}

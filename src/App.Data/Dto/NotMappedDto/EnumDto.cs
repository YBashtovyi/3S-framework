using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.NotMappedDto
{
    /// <summary>
    /// Represents enum value in application
    /// </summary>
    public class EnumDto
    {
        /// <summary>
        /// Enum value
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Enum display name
        /// </summary>
        public string Caption { get; set; }

        public EnumDto()
        {

        }

        public EnumDto(int id, string caption)
        {
            Id = id;
            Caption = caption;
        }
    }
}

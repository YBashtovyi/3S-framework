using System;
using App.Data.Models;
using Newtonsoft.Json;

namespace App.Business.ViewModels
{
    public class ConstructionObjectExtendedPropertyViewModel
    {
        /// <summary>
        /// Id of <see cref="ConstructionObject"/>
        /// </summary>
        public Guid ConstructionObjectId { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>
        /// </summary>
        public Guid DictionaryId { get; set; }

        public Guid ConstructionObjectExPropertyId { get; set; }

        /// <summary>
        /// Id of <see cref="ConstructionObjectExPropertyDictionary"/>.
        /// This value from Enum (if dataFormat(<see cref="ConstructionObjectExPropertyId"/>) is EnumRecord)
        /// </summary>
        public Guid ConstructionObjectSubExPropertyId { get; set; }

        /// <summary>
        /// If EnumRecord - value contains Code of the dictionary
        /// If Text - value contains text
        /// etc...
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Use when DataFormat = EnumRecord. Contains Name of the dictionary
        /// </summary>
        public string ValueName { get; set; }

        public string Description { get; set; }
    }
}

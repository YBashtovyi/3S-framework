using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using App.Data.Dto.NotMappedDto;
using App.Data.Enums;
using Core.Common.Helpers;

namespace App.Business.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Application enums with key - name of the enum
        /// </summary>
        /// <remarks>
        /// Only some of the enums supported. Fill free to add any needed enum here
        /// </remarks>
        public static IReadOnlyDictionary<string, List<EnumDto>> Enums
        {
            get
            {
                return new Dictionary<string, List<EnumDto>>(_enums);
            }
        }

        /// <summary>
        /// Backing field for Enums property
        /// </summary>
        private static readonly Dictionary<string, List<EnumDto>> _enums = new Dictionary<string, List<EnumDto>>();

        /// <summary>
        /// Supported enums. Only this enums can be fetched by names
        /// </summary>
        private static readonly string[] _supportedEnums = new string[]
        {
            //nameof(EpisodeTransferState),
            //nameof(EncounterTransferState)
        };

        static EnumHelper()
        {
            try
            {
                InitializeEnums();
            }
            catch
            {
                // cannot do nothing; enums list will be empty
            }
        }

        private static void InitializeEnums()
        {
            var enumTypes = ReflectionHelper.EnumList.Where(x => _supportedEnums.Contains(x.Name)).ToArray();
            
            // iterating enum types and get names and values for every type
            foreach (var type in enumTypes)
            {
                var typeValues = new List<EnumDto>();
                
                try
                {
                    // this is an example from Microsoft, how iterate enum fields
                    var fields = type.GetFields();
                    foreach (var fi in fields)
                    {
                        if (fi.Name.Equals("value__"))
                        {
                            continue;
                        }

                        var displayAttribute = fi?.GetCustomAttribute<DisplayAttribute>(false);
                        var displayName = displayAttribute == null ? fi.Name : displayAttribute.Name;
                        var value = (int)fi.GetRawConstantValue();

                        typeValues.Add(new EnumDto(value, displayName));
                    }
                }
                catch
                {
                    // just skip this type
                }
                
                _enums.Add(type.Name, typeValues);
            }
        }
    }
}

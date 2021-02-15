using System;
using App.Business.Tests.TestData;
using App.Data.Models;

namespace App.Business.Tests
{
    public static class InstanceCreator
    {
        /// <summary>
        /// Creates instance of the given type
        /// If method does not find initializer for preferred type it tries find initializer for mapFromType type
        /// </summary>
        /// <typeparam name="T">Entity type that should be created</typeparam>
        /// <param name="secondChanceType">Another type which instahce will be mapped to result entity.
        /// Pass here model type if you want to get dto from the model</param>
        /// <returns>An instance of needed type</returns>
        public static T Create<T>(Type secondChanceType = null) where T : class
        {
            var preferredType = typeof(T);
            var instance = CreateInstance(preferredType, secondChanceType);

            // instance should always be created
            if (instance == null)
            {
                var errorMesage = "There is no initializer for";
                if (secondChanceType == null)
                {
                    errorMesage += " type " + preferredType.Name;
                }
                else
                {
                    errorMesage += " types: " + preferredType.Name + ", " + secondChanceType.Name;
                }
                throw new Exception(errorMesage);
            }

            // if there is only one type to map
            if (secondChanceType == null)
            {
                return (T)instance;
            }
            else
            {
                // if instance is already of needed type, then we do not need mapping
                if (instance.GetType() == typeof(T))
                {
                    return (T)instance;
                }
                var mapper = IntegrationBase.GetMapper();
                return mapper.Map<T>(instance);
            }
        }

        private static object CreateInstance(Type preferredType, Type secondChanceType)
        {
            object instance;

            // example of using check for dto
            // add dto check at first place 
            if (preferredType == typeof(EnumRecord) || secondChanceType == typeof(EnumRecord))
            {
                instance = EnumRecordHelper.CreateGender(); // doesn't matter what type
            }
            // just for more convinient adding new tests
            // but a dedicated instance creator for such objects is preferred
            else
            {
                instance = Activator.CreateInstance(preferredType);
            }

            return instance;
        }
    }
}

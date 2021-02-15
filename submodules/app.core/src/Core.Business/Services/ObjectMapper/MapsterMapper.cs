using System;
using Mapster;

namespace Core.Services.Data
{
    public class MapsterMapper: IObjectMapper
    {
        public TDestination Map<TDestination>(object source, TDestination destination = null)
            where TDestination : class
        {
            if (destination != null)
            {
                return source.Adapt(destination);
            }

            return source.Adapt<TDestination>();
        }

        public object Map(object source, Type sourceType, Type destinationType, object destination = null)
        {
            if (destination != null)
            {
                return source.Adapt(destination, sourceType, destinationType);
            }

            return source.Adapt(sourceType, destinationType);
        }
    }
}

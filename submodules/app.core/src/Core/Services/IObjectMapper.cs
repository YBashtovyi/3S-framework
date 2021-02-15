using System;

namespace Core.Services.Data
{
    public interface IObjectMapper
    {
        TDestination Map<TDestination>(object source, TDestination destination = null) where TDestination : class;

        object Map(object source, Type sourceType, Type destinationType, object destination = null);
    }
}

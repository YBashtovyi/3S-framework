using System;
using System.Collections.Generic;
using Core.Business.Extensions;
using AutoMapper;
using System.Collections.Concurrent;

namespace Core.Services.Data
{
    public class ObjectMapper: IObjectMapper
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, IMapper>> _mappers =
            new ConcurrentDictionary<Type, ConcurrentDictionary<Type, IMapper>>();

        public TDestination Map<TDestination>(object source, TDestination destination = null)
            where TDestination : class
        {
            var mapper = GetMapperInternal(source.GetType(), typeof(TDestination));
            if (destination != null)
            {

                return mapper.Map(source, destination);
            }

            return mapper.Map<TDestination>(source);

        }

        public object Map(object source, Type sourceType, Type destinationType, object destination = null)
        {
            var mapper = GetMapperInternal(sourceType, destinationType);
            if (destination != null)
            {

                return mapper.Map(source, destination, sourceType, destinationType);
            }

            return mapper.Map(source, sourceType, destinationType);
        }

        private IMapper GetMapperInternal(Type sourceType, Type destinationType)
        {
            IMapper mapper = null;
            if (_mappers.TryGetValue(sourceType, out var typeMappers))
            {
                if (typeMappers.TryGetValue(destinationType, out mapper))
                {
                    return mapper;
                }
            }
            else
            {
                typeMappers = new ConcurrentDictionary<Type, IMapper>();
                _mappers.TryAdd(sourceType, typeMappers);
            }

            if (mapper == null)
            {
                mapper = new MapperConfiguration(cfg =>
                        cfg.CreateMap(sourceType, destinationType)
                            .MapOnlyIfChanged(destinationType.Name))
                            .CreateMapper();
            }

            typeMappers.TryAdd(destinationType, mapper);
            return mapper;
        }

    }
}

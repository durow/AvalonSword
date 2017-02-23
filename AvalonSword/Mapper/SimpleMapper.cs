/*
 * Author:durow
 * map one object to another.
 * Date:2017.02.23
 */

using System;
using System.Collections.Generic;

namespace Ayx.AvalonSword.Mapper
{
    public class SimpleMapper:IMapper
    {
        public readonly List<MappingInfo> Cache = new List<MappingInfo>();

        public MappingInfo CreateMap<TSrc,TDest>(Dictionary<string,string> nameMapping = null)
        {
            var srcType = typeof(TSrc);
            var destType = typeof(TDest);

            var info = FindCache(srcType, destType);
            if (info != null) return info;

            info = new MappingInfo(srcType, destType, nameMapping);
            Cache.Add(info);

            return info;
        }

        public T Map<T>(object src)
        {
            return Map<T>(src, nameMapping:null);
        }

        public T Map<T>(object src, Dictionary<string,string> nameMapping)
        {
            var srcType = src.GetType();
            var destType = typeof(T);

            var info = FindCache(srcType, destType);
            if (info == null)
            {
                info = new MappingInfo(srcType, destType, nameMapping);
                Cache.Add(info);
            }
            return Map<T>(src, info);
        }

        public T Map<T>(object src, MappingInfo cache)
        {
            var dest = Activator.CreateInstance<T>();
            foreach (var property in cache.Properties)
            {
                var v = property.Key.GetValue(src, null);
                property.Value.SetValue(dest, v, null);
            }

            return dest;
        }

        private MappingInfo FindCache(Type src, Type dest)
        {
            foreach (var item in Cache)
            {
                if (item.Src == src && item.Dest == dest)
                    return item;
            }
            return null;
        }
    }
}

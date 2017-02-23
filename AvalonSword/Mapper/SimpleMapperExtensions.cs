using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Mapper
{
    public static class SimpleMapperExtensions
    {
        public static SimpleMapper UseSimpleMapper(this Locator locator, Action<SimpleMapper> mappingAction)
        {
            var mapper = new SimpleMapper();
            if (mappingAction != null)
                mappingAction.Invoke(mapper);

            locator.ServiceContainer.AddSingleton<IMapper>(mapper);
            return mapper;
        }
    }
}

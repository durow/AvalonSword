using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Mapper
{
    public static class AvalonMapperExtensions
    {
        public static AvalonMapper UseSimpleMapper(this Locator locator, Action<AvalonMapper> mappingAction)
        {
            var mapper = new AvalonMapper();
            if (mappingAction != null)
                mappingAction.Invoke(mapper);

            locator.ServiceContainer.AddSingleton<IMapper>(mapper);
            return mapper;
        }
    }
}

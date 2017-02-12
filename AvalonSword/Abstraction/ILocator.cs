using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword
{
    public interface ILocator
    {
        IConfig Config { get; set; }
        IServiceContainer ServiceContainer { get; set; }
        IViewContainer VmContainer { get; set; }

        T GetService<T>();
        object GetViewModel<TView>();
        TView GetView<TView>();
    }
}

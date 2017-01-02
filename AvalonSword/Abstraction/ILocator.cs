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
        IViewModelContainer VmContainer { get; set; }

        T GetService<T>();
        object GetViewModel<TView>();
        TView GetView<TView>();
    }
}

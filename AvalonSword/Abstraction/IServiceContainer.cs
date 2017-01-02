using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ayx.AvalonSword
{
    public interface IServiceContainer
    {
        void Add<TInterface, TService>();
        void AddSingleton<TInterface, TService>(TService service);
        TInterface GetService<TInterface>();
    }
}

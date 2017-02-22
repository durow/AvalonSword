using Ayx.AvalonSword.Abstraction;
using System;

namespace Ayx.AvalonSword.IoC
{
    public class DefaultContainer : IServiceContainer
    {
        private AyxContainer container = new AyxContainer();

        public void Add<TInterface, TService>() where TService : TInterface
        {
            container.Wire<TInterface, TService>();
        }

        public void AddSingleton<TInterface>(TInterface item)
        {
            container.WireSingleton<TInterface>(item);
        }

        public void AddSingleton<TInterface, TService>() where TService : TInterface
        {
            container.WireSingleton<TInterface, TService>();
        }

        public bool Contains<T>()
        {
            return true;
        }

        public object GetService(Type type)
        {
            return container.Get(type);
        }

        public TInterface GetService<TInterface>()
        {
            return container.Get<TInterface>();
        }
    }
}

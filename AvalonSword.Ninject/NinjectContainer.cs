using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ayx.AvalonSword.Abstraction;
using Ninject;

namespace Ayx.AvalonSword.Ninject
{
    public class NinjectContainer : IServiceContainer
    {
        StandardKernel container;

        public NinjectContainer()
        {
            container = new StandardKernel();
        }

        public NinjectContainer(StandardKernel kernel)
        {
            if (kernel == null)
                throw new NullReferenceException($"{nameof(kernel)} can't be null!");
            container = kernel;
        }
        
        public void Add<TInterface, TService>() where TService : TInterface
        {
            container.Bind<TInterface>().To<TService>();
        }

        public void AddSingleton<TInterface>(TInterface item)
        {
            container.Bind<TInterface>().ToConstant(item).InSingletonScope();
        }

        public void AddSingleton<TInterface, TService>() where TService : TInterface
        {
            container.Bind<TInterface>().To<TService>().InSingletonScope();
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

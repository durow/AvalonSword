using Ninject;
using System;

namespace Ayx.AvalonSword.Ninject
{
    public static class NinjectExtensions
    {
        public static StandardKernel UseNinject(this Locator locator,  Action<StandardKernel> injectAction = null)
        {
            var result = new StandardKernel();
            injectAction?.Invoke(result);
            locator.ServiceContainer = new NinjectContainer(result);
            return result;
        }
    }
}

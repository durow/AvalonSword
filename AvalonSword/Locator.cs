/*
 * Author:durow
 * manager of application.
 * Date:2017.02.12
 */

using Ayx.AvalonSword.Abstraction;
using System;

namespace Ayx.AvalonSword
{
    public class Locator
    {
        public IServiceContainer ServiceContainer { get; private set; }
        public IViewManager ViewContainer { get; set; }

        public Locator(IServiceContainer serviceContainer)
        {
            if (serviceContainer == null)
                throw new NullReferenceException($"ServiceContainer can't be null!");

            ServiceContainer = serviceContainer;
            ViewContainer = new ViewManager(serviceContainer);
            ServiceContainer.AddSingleton(ViewContainer);
            ServiceContainer.AddSingleton(ServiceContainer);
        }
    }
}

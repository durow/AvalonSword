/*
 * Author:durow
 * manager of application.
 * Date:2017.02.12
 */

using Ayx.AvalonSword.Abstraction;
using System;

namespace Ayx.AvalonSword
{
    public class AppManager
    {
        public IServiceContainer ServiceContainer { get; private set; }
        public IViewContainer ViewContainer { get; set; }

        public AppManager(IServiceContainer serviceContainer)
        {
            if (serviceContainer == null)
                throw new NullReferenceException($"ServiceContainer can't be null!");

            ServiceContainer = serviceContainer;
            ViewContainer = new ViewContainer(serviceContainer);
            ServiceContainer.AddSingleton(ViewContainer);
            ServiceContainer.AddSingleton(ServiceContainer);
        }
    }
}

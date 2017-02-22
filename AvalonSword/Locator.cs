/*
 * Author:durow
 * manager of application.
 * Date:2017.02.12
 */

using Ayx.AvalonSword.Abstraction;
using Ayx.AvalonSword.IoC;
using System;
using System.Windows;

namespace Ayx.AvalonSword
{
    public class Locator
    {
        private IServiceContainer _serviceContainer;
        public IServiceContainer ServiceContainer
        {
            get { return _serviceContainer; }
            set
            {
                if(_serviceContainer != value)
                {
                    _serviceContainer = value;
                    InitServices();
                }
            }
        }

        public IViewManager ViewManager { get; set; }

        public Window MainWin { get; set; }

        public Locator() : this(null) { }
        public Locator(IServiceContainer serviceContainer)
        {
            if (serviceContainer == null)
                serviceContainer = new DefaultContainer();

            ServiceContainer = serviceContainer;
        }

        public void BindViews(Action<IViewManager> bindAction)
        {
            if (ViewManager == null)
            {
                ViewManager = new ViewManager();
                ViewManager.ServiceContainer = ServiceContainer;
            }

            bindAction?.Invoke(ViewManager);
        }

        public void ShowMainWindow<TMainWin>() where TMainWin : Window
        {
            var win = ViewManager.CreateWindow<TMainWin>();
            if (win == null)
                throw new Exception($"{typeof(TMainWin).Name} is not a window!");
            MainWin = win;
            win.Show();
        }

        private void InitServices()
        {
            if (ViewManager == null)
                ViewManager = new ViewManager();
            ViewManager.ServiceContainer = ServiceContainer;

            ServiceContainer.AddSingleton(ViewManager);
            ServiceContainer.AddSingleton(ServiceContainer);
            ServiceContainer.AddSingleton(this);
            
        }
    }
}

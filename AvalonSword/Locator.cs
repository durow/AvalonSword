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
                    Init();
                }
            }
        }
        public IViewManager ViewContainer { get; set; }

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
            bindAction?.Invoke(ViewContainer);
        }

        public void ShowMainWindow<TMainWin>() where TMainWin : Window
        {
            var win = ViewContainer.CreateWindow<TMainWin>();
            if (win == null)
                throw new Exception($"{typeof(TMainWin).Name} is not a window!");
            MainWin = win;
            win.Show();
        }

        private void Init()
        {
            ViewContainer = new ViewManager(ServiceContainer);
            ServiceContainer.AddSingleton(ViewContainer);
            ServiceContainer.AddSingleton(ServiceContainer);
            ServiceContainer.AddSingleton(this);
        }
    }
}

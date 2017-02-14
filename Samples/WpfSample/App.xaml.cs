using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using Ayx.AvalonSword;

namespace WpfSample
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static AppManager Manager;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Manager = GetAppManager();
            Manager.ViewContainer.CreateView<MainWindow>()?.Show();
        }
    }
}

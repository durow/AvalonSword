using System.Windows;
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

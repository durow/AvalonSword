using System.Windows;
using Ayx.AvalonSword;

namespace WpfSample
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Locator Locator;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Locator = GetAppManager();
            Locator.ShowMainWindow<MainWindow>();
        }
    }
}

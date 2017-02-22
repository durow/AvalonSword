using System.Windows;
using Ayx.AvalonSword;
using Ayx.AvalonSword.Ninject;
using Ninject;
using Ayx.AvalonSword.Abstraction;
using WpfSample.ViewModels;
using WpfSample.Views;

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
            Locator = new Locator();
            Locator.UseNinject(BindServices);
            Locator.BindViews(BindViews);
            Locator.ShowMainWindow<MainWindow>();
        }

        void BindServices(StandardKernel kernel)
        {
            kernel.Bind<IBindedTabManager>().To<BindedTabManager>();
        }

        void BindViews(IViewManager container)
        {
            container.BindView<MainWindow, MainWindowViewModel>();
            container.BindView<TestView, TestViewModel>();
            container.BindView<EventTestView, EventTestViewModel>();
            container.BindView<RemarkEditView, RemarkEditViewModel>();
        }
    }
}

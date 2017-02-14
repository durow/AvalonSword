using Ayx.AvalonSword.Ninject;
using Ayx.AvalonSword;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.ViewModels;
using WpfSample.Views;

namespace WpfSample
{
    partial class App
    {
        public AppManager GetAppManager()
        {
            var kernel = new StandardKernel();
            var result = new AppManager(new NinjectContainer(kernel));

            BindServices(kernel);
            BindViews(result.ViewContainer);

            return result;
        }

        static void BindServices(StandardKernel kernel)
        {

        }

        static void BindViews(IViewContainer container)
        {
            container.BindView<MainWindow, MainWindowViewModel>();
            container.BindView<TestView, TestViewModel>();
        }
    }
}

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
using Ayx.AvalonSword.Abstraction;
using System.Windows.Controls;

namespace WpfSample
{
    partial class App
    {
        public Locator GetAppManager()
        {
            var kernel = new StandardKernel();
            var result = new Locator(new NinjectContainer(kernel));

            BindServices(kernel);
            BindViews(result.ViewContainer);

            return result;
        }

        static void BindServices(StandardKernel kernel)
        {
            kernel.Bind<IBindedTabManager>().To<BindedTabManager>();
        }

        static void BindViews(IViewManager container)
        {
            container.BindView<MainWindow, MainWindowViewModel>();
            container.BindView<TestView, TestViewModel>();
            container.BindView<EventTestView, EventTestViewModel>();
            container.BindView<RemarkEditView, RemarkEditViewModel>();
        }
    }
}

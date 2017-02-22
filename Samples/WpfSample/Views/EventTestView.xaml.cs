using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSample.Views
{
    /// <summary>
    /// TestControl.xaml 的交互逻辑
    /// </summary>
    public partial class EventTestView : UserControl
    {
        public EventTestView()
        {
            InitializeComponent();
        }

        private EventDispatcher _eventDispatcher;
        public EventDispatcher EventDispatcher
        {
            get
            {
                if (_eventDispatcher == null)
                    _eventDispatcher = (DataContext as ViewModelBase)?.EventDispatcher;
                return _eventDispatcher;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            EventDispatcher.RouteEvent("Loaded", e);  
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            EventDispatcher.RouteEvent("MouseEnter", e);
        }

        private void DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            EventDispatcher.RouteEvent("MouseLeave", e);
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            EventDispatcher.RouteEvent("MouseMove", e);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventDispatcher.RouteEvent("MouseDoubleClick", e);
        }
    }
}

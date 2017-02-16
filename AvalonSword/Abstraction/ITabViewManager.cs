using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Ayx.AvalonSword.Abstraction
{
    public interface ITabViewManager
    {
        ObservableCollection<object> TabItems { get; set; }
        object SelectedItem { get; set; }

        void AddTab<TView>(string title) where TView : FrameworkElement;
        TViewModel AddTabFromModel<TViewModel>(string title) where TViewModel : class;
        void CloseTab(string title);
        void CloseAll();
        int GetSelectedIndex();
        object ShowTab(string title);
        object ShowNext();
        object ShowPreview();
        FrameworkElement GetTabContent(string title);
        TViewModel GetTabContentVM<TViewModel>(string title)
            where TViewModel : class;
        object GetTabItem(string title);
    }
}

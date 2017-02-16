/*
 * Author:durow
 * interface of BindedTabManager
 * Date:2017.02.12
 */

using System.Collections.ObjectModel;
using System.Windows;

namespace Ayx.AvalonSword.Abstraction
{
    public interface IBindedTabManager
    {
        ObservableCollection<object> TabItems { get; set; }
        object SelectedItem { get; set; }

        void AddTab<TView>(string title) where TView : FrameworkElement;
        TViewModel AddTabFromModel<TViewModel>(string title) where TViewModel : class;
        void AddTabFromModel<TViewModel>(string title,TViewModel viewmodel) where TViewModel : class;
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

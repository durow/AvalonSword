/*
 * Author:durow
 * interface of TabManager
 * Date:2017.02.16
 */

using System.Windows;

namespace Ayx.AvalonSword
{
    public interface ITabManager
    {
        void AddToOpen(object item);
        void AddOrShow(object item);
        void AddOrShow<TView>(string title) where TView : FrameworkElement;
        void AddOrShowFromModel<TViewModel>(string title) where TViewModel : class;
        void AddOrShowFromModel<TViewModel>(string title, TViewModel viewmodel)
            where TViewModel : class;
        void CloseTab(string title);
        void CloseAll();
        object ShowTab(string title);
        object ShowNext();
        object ShowPreview();
        object GetTabItem(string title);
    }
}

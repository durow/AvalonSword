using Ayx.AvalonSword.Abstraction;
using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using Ayx.AvalonSword.MVVM;

namespace Ayx.AvalonSword
{
    public class BindedTabManager : NotificationObject, IBindedTabManager
    {
        private IViewManager viewManager;
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public ObservableCollection<object> TabItems { get; set; }

        public BindedTabManager(IViewManager viewManager)
        {
            this.viewManager = viewManager;
            TabItems = new ObservableCollection<object>();
        }

        public void AddTab<TView>(string title) where TView : FrameworkElement
        {
            var item = CheckExists(title);
            if (item != null) return;

            var view = viewManager.CreateView<TView>();
            ShowNewTab(title, view);
        }

        public void AddTab(string title, object contentView)
        {
            var item = CheckExists(title);
            if (item != null) return;

            ShowNewTab(title, contentView);
        }

        public TViewModel AddTabFromModel<TViewModel>(string title) where TViewModel : class
        {
            var item = CheckExists(title);
            if (item != null) return null;

            var view = viewManager.CreateViewFromModel<TViewModel>();
            ShowNewTab(title, view);
            return view.DataContext as TViewModel;
        }

        public void AddTabFromModel<TViewModel>(string title, TViewModel viewmodel)
            where TViewModel : class
        {
            var item = CheckExists(title);
            if (item != null) return;

            var view = viewManager.CreateViewFromModel(viewmodel);
            ShowNewTab(title, view);
        }

        public void CloseAll()
        {
            TabItems.Clear();
        }

        public void CloseTab(string title)
        {
            var item = GetTabItem(title);
            TabItems.Remove(item);
        }
        public void CloseTab(object content)
        {
            TabItems.Remove(content);
        }

        public FrameworkElement GetTabContent(string title)
        {
            throw new NotImplementedException();
        }

        public TViewModel GetTabContentVM<TViewModel>(string title)
            where TViewModel:class
        {
            var item = GetTabItem(title) as TabItem;
            if (item == null) return null;

            var view = item.Content as FrameworkElement;
            if (view == null) return null;
            return view.DataContext as TViewModel;
        }

        public object GetTabItem(string title)
        {
            foreach (TabItem item in TabItems)
            {
                if (item.Header.ToString() == title)
                    return item;
            }
            return null;
        }

        public int GetSelectedIndex()
        {
            return TabItems.IndexOf(SelectedItem);
        }

        public object ShowNext()
        {
            if (TabItems.Count == 0) return null;

            var index = TabItems.IndexOf(SelectedItem);
            index = ++index % TabItems.Count;
            SelectedItem = TabItems[index];
            return SelectedItem;
        }

        public object ShowPreview()
        {
            if(TabItems.Count == 0) return null;

            var index = TabItems.IndexOf(SelectedItem);
            index--;
            if (index < 0)
                index = TabItems.Count - 1;
            SelectedItem = TabItems[index];
            return SelectedItem;
        }

        public object ShowTab(string title)
        {
            var tab = GetTabItem(title);
            if (tab != null)
                SelectedItem = tab;
            return tab;
        }

        object CheckExists(string title)
        {
            foreach (TabItem item in TabItems)
            {
                if (item.Header.ToString() == title)
                {
                    SelectedItem = item;
                    return item;
                }
            }
            return null;
        }

        private void ShowNewTab(string title, object view)
        {
            var tab = new TabItem();
            tab.Header = title;
            tab.Content = view;
            tab.ToolTip = "double click to close tab!";
            tab.MouseDoubleClick += Header_MouseDoubleClick;
            TabItems.Add(tab);
            SelectedItem = tab;
        }

        private void Header_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var tab = sender as TabItem;
            TabItems.Remove(tab);
        }

        private void CreateHeaderTemplate()
        {
            var result = new DataTemplate();
        }
    }
}

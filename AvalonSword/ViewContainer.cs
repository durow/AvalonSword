/*
 * Author:durow
 * default implementation of IVIewContainer
 * Date:2017.02.12
 */

using Ayx.AvalonSword.Abstraction;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ayx.AvalonSword
{
    public class ViewContainer : IViewContainer
    {
        private Dictionary<Type, Type> VMList = new Dictionary<Type, Type>();
        private IServiceContainer serviceContainer;

        public int Count
        {
            get
            {
                return VMList.Count;
            }
        }

        public ViewContainer(IServiceContainer serviceContainer)
        {
            this.serviceContainer = serviceContainer;
        }

        #region CreateView

        public TView CreateView<TView>()
            where TView : FrameworkElement
        {
            var view = serviceContainer.GetService<TView>();
            var vmType = GetViewModelFromView<TView>();
            if (vmType == null) return view;

            var vm = serviceContainer.GetService(vmType);
            view.DataContext = vm;
            return view;
        }

        public FrameworkElement CreateViewFromModel<TViewModel>()
            where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (!VMList.ContainsKey(vmType))
                throw new Exception($"can't find view from viewmodel {vmType}");

            var vm = serviceContainer.GetService<TViewModel>();
            var view = serviceContainer.GetService(VMList[vmType]) as FrameworkElement;
            view.DataContext = vm;
            return view;
        }

        public TView CreateView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var vm = serviceContainer.GetService<TViewModel>();
            return CreateView<TView, TViewModel>(vm);
        }

        public TView CreateView<TView, TViewModel>(TViewModel viewmodel)
            where TView : FrameworkElement
            where TViewModel : class
        {
            var view = serviceContainer.GetService<TView>();
            view.DataContext = viewmodel;
            return view;
        }

        #endregion

        #region CreateWindow

        public TView CreateWindow<TView>()
            where TView : Window
        {
            var win = CreateView<TView>();
            return win;
        }

        public Window CreateWindowFromModel<TViewModel>() where TViewModel : class
        {
            var view = CreateViewFromModel<TViewModel>();
            return ConvertToWindow(view);
        }

        public TView CreateWindow<TView, TViewModel>()
            where TView : Window
            where TViewModel : class
        {
            return CreateView<TView, TViewModel>();
        }

        public TView CreateWindow<TView, TViewModel>(TViewModel viewmodel)
            where TView : Window
            where TViewModel : class
        {
            return CreateView<TView, TViewModel>(viewmodel);
        }

        #endregion

        #region ShowWindow

        public TView ShowWindow<TView>(Window owner = null) 
            where TView : Window
        {
            var win = CreateWindow<TView>();
            win.Owner = owner;
            win.Show();
            return win;
        }

        public Window ShowWindowFromModel<TViewModel>(Window owner = null) 
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>();
            win.Owner = owner;
            win.Show();
            return win;
        }

        public TView ShowWindow<TView, TViewModel>(Window owner = null)
           where TView : Window
           where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>();
            win.Owner = owner;
            win.Show();
            return win;
        }

        public TView ShowWindow<TView, TViewModel>(TViewModel viewmodel, Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>(viewmodel);
            win.Owner = owner;
            win.Show();
            return win;
        }
        #endregion

        #region ShowDialog
        public TView ShowDialog<TView>(Window owner = null) 
            where TView : Window
        {
            var win = CreateWindow<TView>();
            win.Owner = owner;
            win.ShowDialog();
            return win;
        }

        public Window ShowDialogFromModel<TViewModel>(Window owner = null) 
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>();
            win.Owner = owner;
            win.ShowDialog();
            return win;
        }

        public TView ShowDialog<TView, TViewModel>(Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>();
            win.Owner = owner;
            win.ShowDialog();
            return win;
        }

        public TView ShowDialog<TView, TViewModel>(TViewModel viewmodel, Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>(viewmodel);
            win.Owner = owner;
            win.ShowDialog();
            return win;
        }

        #endregion

        #region OtherMethos
        public void BindView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (VMList.ContainsKey(vmType))
                throw new Exception($"{nameof(vmType)} have binded!");
            VMList.Add(vmType, typeof(TView));
        }

        public void RemoveView<TView>()
            where TView : FrameworkElement
        {
            var vmType = GetViewModelFromView<TView>();
            if (vmType != null)
            {
                VMList.Remove(vmType);
            }
        }

        public void RemoveViewModel<TViewModel>()
            where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (VMList.ContainsKey(vmType))
                VMList.Remove(vmType);
        }

        public bool ContainsView<TView>()
            where TView : FrameworkElement
        {
            return GetViewModelFromView<TView>() != null;
        }

        public bool ContainsViewModel<TViewModel>()
            where TViewModel : class
        {
            return VMList.ContainsKey(typeof(TViewModel));
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PrivateMethods

        private Type GetViewModelFromView<TView>()
        {
            foreach (var vm in VMList)
            {
                if (vm.Value == typeof(TView))
                    return vm.Key;
            }

            return null;
        }

        private Window ConvertToWindow(FrameworkElement view)
        {
            var win = view as Window;
            if (win == null)
                throw new Exception($"{view.GetType()} is not a Window!");

            return win;
        }

        #endregion

    }
}

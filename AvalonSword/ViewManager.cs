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
    public class ViewManager : IViewManager
    {
        private Dictionary<Type, Type> VMList = new Dictionary<Type, Type>();
        public IServiceContainer ServiceContainer { get; set; }

        public int Count
        {
            get
            {
                return VMList.Count;
            }
        }

        public ViewManager(IServiceContainer serviceContainer = null)
        {
            if(serviceContainer  != null)
                this.ServiceContainer = serviceContainer;
        }

        #region CreateView

        public TView CreateView<TView>()
            where TView : FrameworkElement
        {
            var viewType = typeof(TView);
            var vmType = GetViewModelFromView(viewType);
            if (vmType == null)
                return ServiceContainer.GetService<TView>();

            return CreateView(viewType, vmType) as TView;
        }

        public FrameworkElement CreateViewFromModel<TViewModel>()
            where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (!VMList.ContainsKey(vmType))
                throw new Exception($"can't find view from viewmodel {vmType}");

            return CreateView(VMList[vmType], vmType);
        }

        public FrameworkElement CreateViewFromModel<TViewModel>(TViewModel vm)
            where TViewModel : class
        {
            var vmType = vm.GetType();
            if (!VMList.ContainsKey(vmType))
                throw new Exception($"can't find view from viewmodel {vmType}");

            return CreateView(VMList[vmType], vmType, vm);
        }

        public TView CreateView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var vm = ServiceContainer.GetService<TViewModel>();
            return CreateView<TView, TViewModel>(vm);
        }

        public TView CreateView<TView, TViewModel>(TViewModel viewmodel)
            where TView : FrameworkElement
            where TViewModel : class
        {
            return CreateView(typeof(TView), typeof(TViewModel)) as TView;
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

        public Window CreateWindowFromModel<TViewModel>(TViewModel viewmodel) where TViewModel : class
        {
            var view = CreateViewFromModel<TViewModel>(viewmodel);
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
            if(owner != null)
                win.Owner = owner;

            win.Show();
            return win;
        }

        public Window ShowWindowFromModel<TViewModel>(Window owner = null) 
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>();
            if (owner != null)
                win.Owner = owner;

            win.Show();
            return win;
        }

        public Window ShowWindowFromModel<TViewModel>(TViewModel viewmodel, Window owner = null)
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>(viewmodel);
            if (owner != null)
                win.Owner = owner;

            win.Show();
            return win;
        }

        public TView ShowWindow<TView, TViewModel>(Window owner = null)
           where TView : Window
           where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>();
            if (owner != null)
                win.Owner = owner;

            win.Show();
            return win;
        }

        public TView ShowWindow<TView, TViewModel>(TViewModel viewmodel, Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>(viewmodel);
            if (owner != null)
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
            if (owner != null)
                win.Owner = owner;

            win.ShowDialog();
            return win;
        }

        public Window ShowDialogFromModel<TViewModel>(Window owner = null) 
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>();
            if (owner != null)
                win.Owner = owner;

            win.ShowDialog();
            return win;
        }

        public Window ShowDialogFromModel<TViewModel>(TViewModel viewmodel, Window owner = null)
            where TViewModel : class
        {
            var win = CreateWindowFromModel<TViewModel>(viewmodel);
            if (owner != null)
                win.Owner = owner;

            win.ShowDialog();
            return win;
        }

        public TView ShowDialog<TView, TViewModel>(Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>();
            if (owner != null)
                win.Owner = owner;

            win.ShowDialog();
            return win;
        }

        public TView ShowDialog<TView, TViewModel>(TViewModel viewmodel, Window owner = null)
            where TView : Window
            where TViewModel : class
        {
            var win = CreateWindow<TView, TViewModel>(viewmodel);
            if (owner != null)
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
            var vmType = GetViewModelFromView(typeof(TView));
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
            return GetViewModelFromView(typeof(TView)) != null;
        }

        public bool ContainsViewModel<TViewModel>()
            where TViewModel : class
        {
            return VMList.ContainsKey(typeof(TViewModel));
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : class
        {
            return GetViewModel(typeof(TViewModel)) as TViewModel;
        }

        public object GetViewModel(Type vmType)
        {
            var vm = ServiceContainer.GetService(vmType);
            return SetCommandAop(vm);
        }

        #endregion

        #region PrivateMethods

        private Type GetViewModelFromView(Type viewType)
        {
            foreach (var vm in VMList)
            {
                if (vm.Value == viewType)
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

        private void AddViewToVM(FrameworkElement view, object vm, Type vmType)
        {
            var property = vmType.GetProperty("View");
            if (property == null) return;

            property.SetValue(vm, view, null);
        }

        private FrameworkElement CreateView(Type viewType, Type vmType, object vm = null)
        {
            var view = ServiceContainer.GetService(viewType) as FrameworkElement;
            if (vm == null)
                vm = GetViewModel(vmType);

            AddViewToVM(view, vm, vmType);
            view.DataContext = vm;

            return view;
        }

        private object SetCommandAop(object vm)
        {
            return vm;
        }

        #endregion

    }
}

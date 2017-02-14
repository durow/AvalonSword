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

        public void BindView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (VMList.ContainsKey(vmType))
                throw new Exception($"{nameof(vmType)} have binded!");
            VMList.Add(vmType, typeof(TView));
        }

        public TView CreateView<TView>() where TView : FrameworkElement
        {
            var view = serviceContainer.GetService<TView>();
            var vmType = GetViewModelFromView<TView>();
            if (vmType == null) return view;
            
            var vm = serviceContainer.GetService(vmType);
            view.DataContext = vm;
            return view;
        }

        public FrameworkElement CreateViewFromModel<TViewModel>() where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (!VMList.ContainsKey(vmType))
                throw new Exception($"can't find view from viewmodel {vmType}");

            var vm = serviceContainer.GetService<TViewModel>();
            var view = serviceContainer.GetService(VMList[vmType]) as FrameworkElement;
            view.DataContext = vm;
            return view;
        }

        public void RemoveView<TView>() where TView : FrameworkElement
        {
            var vmType = GetViewModelFromView<TView>();
            if(vmType != null)
            {
                VMList.Remove(vmType);
            }
        }

        public void RemoveViewModel<TViewModel>() where TViewModel : class
        {
            var vmType = typeof(TViewModel);
            if (VMList.ContainsKey(vmType))
                VMList.Remove(vmType);
        }

        public bool ContainsView<TView>() where TView : FrameworkElement
        {
            return GetViewModelFromView<TView>() != null;
        }

        public bool ContainsViewModel<TViewModel>() where TViewModel : class
        {
            return VMList.ContainsKey(typeof(TViewModel));
        }

        private Type GetViewModelFromView<TView>()
        {
            foreach (var vm in VMList)
            {
                if (vm.Value == typeof(TView))
                    return vm.Key;
            }

            return null;
        }
    }
}

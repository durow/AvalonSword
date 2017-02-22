/*
 * Author:durow
 * interface of ViewContainer
 * Date:2017.02.12
 */

using Ayx.AvalonSword.Abstraction;
using System.Windows;

namespace Ayx.AvalonSword
{
    public interface IViewManager
    {
        int Count { get; }
        IServiceContainer ServiceContainer {get;set;}

        #region CreateView

        FrameworkElement CreateViewFromModel<TViewModel>()
            where TViewModel : class;

        FrameworkElement CreateViewFromModel<TViewModel>(TViewModel viewmodel)
            where TViewModel : class;

        TView CreateView<TView>() 
            where TView : FrameworkElement;

        TView CreateView<TView, TViewModel>() 
            where TView : FrameworkElement 
            where TViewModel : class;

        TView CreateView<TView, TViewModel>(TViewModel viewmodel) 
            where TView : FrameworkElement 
            where TViewModel : class;

        #endregion

        #region CreateWindow

        TView CreateWindow<TView>()
            where TView : Window;

        Window CreateWindowFromModel<TViewModel>()
            where TViewModel : class;

        Window CreateWindowFromModel<TViewModel>(TViewModel viewmodel)
            where TViewModel : class;

        TView CreateWindow<TView, TViewModel>()
            where TView : Window
            where TViewModel : class;

        TView CreateWindow<TView, TViewModel>(TViewModel viewmodel)
            where TView : Window
            where TViewModel : class;

        #endregion

        #region ShowWindow

        TView ShowWindow<TView>(Window owner = null) 
            where TView : Window;

        Window ShowWindowFromModel<TViewModel>(Window owner = null)
            where TViewModel : class;

        Window ShowWindowFromModel<TViewModel>(TViewModel viewmodel, Window owner = null)
            where TViewModel : class;

        TView ShowWindow<TView, TViewModel>(Window owner = null) 
            where TView : Window 
            where TViewModel : class;

        TView ShowWindow<TView, TViewModel>(TViewModel viewmodel, Window owner = null) 
            where TView : Window 
            where TViewModel : class;

        #endregion ShowWindow

        #region ShowDialog

        TView ShowDialog<TView>(Window owner = null)
            where TView : Window;

        Window ShowDialogFromModel<TViewModel>(Window owner = null)
            where TViewModel : class;

        Window ShowDialogFromModel<TViewModel>(TViewModel viewmodel, Window owner = null)
            where TViewModel : class;

        TView ShowDialog<TView, TViewModel>(Window owner = null)
            where TView : Window
            where TViewModel : class;

        TView ShowDialog<TView, TViewModel>(TViewModel viewmodel, Window owner = null)
            where TView : Window
            where TViewModel : class;

        #endregion

        #region OthreMethods

        void BindView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class;

        TViewModel GetViewModel<TViewModel>()
            where TViewModel : class;

        void RemoveViewModel<TViewModel>()
            where TViewModel : class;

        void RemoveView<TView>()
            where TView : FrameworkElement;

        bool ContainsView<TView>()
            where TView : FrameworkElement;

        bool ContainsViewModel<TViewModel>()
            where TViewModel : class;

        #endregion
    }
}

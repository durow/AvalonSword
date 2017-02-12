/*
 * Author:durow
 * interface of ViewContainer
 * Date:2017.02.12
 */

using System.Windows;

namespace Ayx.AvalonSword
{
    public interface IViewContainer
    {
        int Count { get; }

        void BindView<TView, TViewModel>()
            where TView : FrameworkElement where TViewModel : class;

        FrameworkElement CreateViewFromModel<TViewModel>()
            where TViewModel : class;

        TView CreateView<TView>() where TView : FrameworkElement;

        void RemoveViewModel<TViewModel>()
            where TViewModel : class;

        void RemoveView<TView>()
            where TView : FrameworkElement;

        bool ContainsView<TView>() where TView : FrameworkElement;

        bool ContainsViewModel<TViewModel>() where TViewModel : class;
    }
}

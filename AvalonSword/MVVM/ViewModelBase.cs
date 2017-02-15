﻿/*
 * Author:durow
 * base class of ViewModel
 * Date:2017.02.14
 */

using System;
using System.Windows;

namespace Ayx.AvalonSword.MVVM
{
    public abstract class ViewModelBase : NotificationObject
    {
        protected CommandGenerator CmdGenerator = new CommandGenerator();
        public CommandRouter CommandRouter { get; set; }
        public FrameworkElement View { get; set; }

        private AyxCommand _router;
        public AyxCommand Router
        {
            get
            {
                if (CommandRouter == null)
                    CommandRouter = new CommandRouter(this);
                if (_router == null)
                    _router = new AyxCommand(o =>
                    {
                        CommandRouter.ExecuteCommand(o.ToString());
                    },o=> {
                        return CommandRouter.CheckCommand(o.ToString());
                    });
                return _router;
            }
        }

        protected TViewModel GetViewModel<TViewModel>(FrameworkElement view)
            where TViewModel : class
        {
            if (view.DataContext == null)
                return null;

            return (TViewModel)view.DataContext;
        }
    }
}

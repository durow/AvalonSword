using Ayx.AvalonSword;
using Ayx.AvalonSword.Abstraction;
using Ayx.AvalonSword.MVVM;
using WpfSample.Infrastructure;
using System.Windows;
using System;

namespace WpfSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _LogText = $"[{DateTime.Now.ToStandard()}] Application Started !\n";
        public string LogText
        {
            get { return _LogText; }
            set{ SetAndNotify("LogText", ref _LogText, value); }
        }


        private bool _canRouterTest;
        public bool CanRouterTest
        {
            get { return _canRouterTest; }
            set { _canRouterTest = value; RaisePropertyChanged("CanRouterTest"); }
        }


        public IBindedTabManager TabViewManager { get; set; }

        private IViewManager viewManager;

        public MainWindowViewModel(IViewManager viewManager, IBindedTabManager tabManager)
        {
            this.viewManager = viewManager;
            this.TabViewManager = tabManager;
        }


        public AyxCommand CmdEventTest
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    TabViewManager.AddTabFromModel<EventTestViewModel>("EventTest");
                });
            }
        }

        public AyxCommand CmdAddText
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    TabViewManager.AddTabFromModel<EventTestViewModel>("AddText");
                });
            }
        }

        public void ShowWindow()
        {
            var win = viewManager.CreateWindowFromModel<TestViewModel>();
            win.Owner = View as Window;
            win.Closed += (s,e)=>{
                AddLog(win.GetType().ToString() + " closed.");
            };
            win.Show();
            AddLog(win.GetType().ToString() + " opened.");
        }

        public bool ShowWindowCheck()
        {
            return CanRouterTest;
        }

        public void ShowDialog()
        {
            AddLog("try to show dialog.");
            var dlg = viewManager.ShowDialogFromModel<TestViewModel>(View as Window);
            AddLog(dlg.GetType().ToString() + " closed.");
        }

        public bool ShowDialogCheck()
        {
            return CanRouterTest;
        }

        public void ShowPreview()
        {
            TabViewManager.ShowPreview();
        }

        public void ShowNext()
        {
            TabViewManager.ShowNext();
        }

        private void AddLog(string log)
        {
            LogText += $"[{DateTime.Now.ToStandard()}] {log}\n";
        }
    }
}

using Ayx.AvalonSword;
using Ayx.AvalonSword.Abstraction;
using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfSample.ViewModels
{
    public class MainWindowViewModel:ViewModelBase
    {
        private string _resultText;
        public string ResultText
        {
            get { return _resultText; }
            set
            {
                if(value != _resultText)
                {
                    _resultText = value;
                    NotifyPropertyChanged("ResultText");
                }
            }
        }

        private bool _canRouterTest;
        public bool CanRouterTest
        {
            get { return _canRouterTest; }
            set
            {
                if(value != _canRouterTest)
                {
                    _canRouterTest = value;
                    NotifyPropertyChanged("CanRouterTest");
                }
            }
        }

        public IBindedTabManager TabViewManager { get; set; }

        private IViewManager viewManager;

        public MainWindowViewModel(IViewManager viewManager, IBindedTabManager tabManager)
        {
            this.viewManager = viewManager;
            this.TabViewManager = tabManager;
        }


        public AyxCommand CmdOpenView
        {
            get
            {
                return CmdGenerator.GetCmd(o=>{
                    TabViewManager.AddTabFromModel<TestControlViewModel>("OpenView");
                });
            }
        }

        public AyxCommand CmdAddText
        {
            get
            {
                return CmdGenerator.GetCmd(o=>
                {
                    TabViewManager.AddTabFromModel<TestControlViewModel>("AddText");
                });
            }
        }

        public void RouterTest()
        {
            TabViewManager.AddTabFromModel<TestControlViewModel>("RouterTest");
        }

        public bool RouterTestCheck()
        {
            return CanRouterTest;
        }

        public void RouterTest2()
        {
            TabViewManager.AddTabFromModel<TestControlViewModel>("RouterTest2");
        }

        public void ShowPreview()
        {
            TabViewManager.ShowPreview();
        }

        public void ShowNext()
        {
            TabViewManager.ShowNext();
        }
    }
}

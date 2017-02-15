using Ayx.AvalonSword;
using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private IViewContainer viewContainer;

        public MainWindowViewModel(IViewContainer container)
        {
            viewContainer = container;
        }


        public AyxCommand CmdOpenView
        {
            get
            {
                return CmdGenerator.GetCmd(o=>{
                    viewContainer.ShowWindowFromModel<TestViewModel>(View as Window);
                });
            }
        }

        public AyxCommand CmdAddText
        {
            get
            {
                return CmdGenerator.GetCmd(o=>
                {
                    ResultText += $"[{DateTime.Now}] from CommandGenerator!\n";
                });
            }
        }

        public void RouterTest()
        {
            ResultText += $"[{DateTime.Now}] from Router RouterTest!\n";
        }

        public bool RouterTestCheck()
        {
            return CanRouterTest;
        }

        public void RouterTest2()
        {
            ResultText += $"[{DateTime.Now}] from Router RouterTest2!\n";
        }
    }
}

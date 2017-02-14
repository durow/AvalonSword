using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.ViewModels
{
    public class MainWindowViewModel:NotificationObject
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

        public MainWindowViewModel()
        {
            ResultText = $"[{DateTime.Now}] this is a test!";
        }

    }
}

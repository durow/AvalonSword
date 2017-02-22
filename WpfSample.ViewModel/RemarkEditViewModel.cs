using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfSample.ViewModels
{
    public class RemarkEditViewModel : ViewModelBase
    {
        private string _RemarkText;
        public string RemarkText
        {
            get { return _RemarkText; }
            set
            {
                if (_RemarkText != value)
                {
                    _RemarkText = value;
                    RaisePropertyChanged("RemarkText");
                }
            }
        }

        public bool Result { get; set; }

        public AyxCommand CmdOk
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    Result = true;
                    CloseViewAsWindow();
                });
            }
        }

        public AyxCommand CmdCancel
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    Result = false;
                    CloseViewAsWindow();
                });
            }
        }

    }
}

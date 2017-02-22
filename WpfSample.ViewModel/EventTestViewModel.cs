using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfSample.Infrastructure;
using WpfSample.Domain.Models;
using Ayx.AvalonSword;
using System.Windows;

namespace WpfSample.ViewModels
{
    public class EventTestViewModel:ViewModelBase
    {
        private IViewManager viewManager;
        public ObservableCollection<EventLog> LogList { get; set; }
        private EventLog _SelectedLog;

        public EventLog SelectedLog
        {
            get { return _SelectedLog; }
            set
            {
                if (_SelectedLog != value)
                {
                    _SelectedLog = value;
                    RaisePropertyChanged("SelectedLog");
                }
            }
        }


        private string _PositionText;

        public string PositionText
        {
            get { return _PositionText; }
            set
            {
                SetAndNotify("PositionText", ref _PositionText, value);
            }
        }

        public EventTestViewModel(IViewManager viewManager)
        {
            this.viewManager = viewManager;
            LogList = new ObservableCollection<EventLog>();
        }

        public void Loaded(object param)
        {
            LogList.Add(new EventLog($"Window Loaded Event!"));
        }

        public AyxCommand CmdClearLog
        {
            get
            {
                return CmdGenerator.GetCmd(o =>
                {
                    LogList.Clear();
                });
            }
        }

        public void MouseEnter(object param)
        {
            var p = param as MouseEventArgs;
            if (p == null) return;

            LogList.Add(new EventLog($"Mouse Enter event, mouse entered at : {p.GetPosition(View)}!"));
        }

        public void MouseLeave(object param)
        {
            var p = param as MouseEventArgs;
            if (p == null) return;

            LogList.Add(new EventLog($"Mouse Leave event, mouse leaved at : {p.GetPosition(View)}!"));
        }

        public void MouseMove(object param)
        {
            var p = param as MouseEventArgs;
            if (p == null) return;

            PositionText = $"Mouse Position : {p.GetPosition(View)}";
        }

        public void MouseDoubleClick(object param)
        {
            if (SelectedLog == null)
                return;

            var vmEdit = viewManager.GetViewModel<RemarkEditViewModel>();
            vmEdit.RemarkText = SelectedLog.Remark;
            var win = viewManager.ShowDialogFromModel(vmEdit);

            if (vmEdit.Result)
                SelectedLog.Remark = vmEdit.RemarkText;
        }
    }
}

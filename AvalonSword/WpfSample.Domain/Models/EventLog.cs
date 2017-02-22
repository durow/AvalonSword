using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfSample.Infrastructure;

namespace WpfSample.Domain.Models
{
    public class EventLog:NotificationObject
    {
        public string LogDateTime { get; set; }
        public string LogContent { get; set; }

        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    RaisePropertyChanged("Remark");
                }
            }
        }

        public EventLog(string logContent)
        {
            LogDateTime = DateTime.Now.ToStandard();
            LogContent = logContent;
        }
    }
}

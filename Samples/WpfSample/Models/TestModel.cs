using Ayx.AvalonSword.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSample.Models
{
    public class TestModel : NotificationObject
    {
        private int intValue;
        public int IntValue
        {
            get { return intValue; }
            set
            {
                intValue = value;
                RaisePropertyChanged(()=>IntValue);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword
{
    public interface IEventBus
    {
        Action<object> this[object key] { get;set; }
        void On(object eventName, Action<object> action);
        void Emit(object eventName, object parameter);
    }
}

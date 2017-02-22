using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ayx.AvalonSword.MVVM
{
    public class EventDispatcher
    {
        private object viewmodel;
        private Type vmType;
        private Dictionary<string, Action<object>> eventList = new Dictionary<string, Action<object>>();

        public EventDispatcher(object vm)
        {
            viewmodel = vm;
            vmType = vm.GetType();
        }

        public void RouteEvent(string eventName, object param)
        {
            if (!eventList.ContainsKey(eventName))
            {
                var action = FindEventAction(eventName);
                if(action == null)
                    throw new Exception($"{eventName} does not exists!");

                eventList[eventName] = action;
            }

            eventList[eventName].Invoke(param);
        }

        public void RegisterEvent(string eventName, Action<object> action)
        {
            if (eventList.ContainsKey(eventName))
                throw new Exception($"{eventName} has registered!");

            eventList[eventName] = action;
        }

        public Action<object> FindEventAction(string eventName)
        {
            var method = vmType.GetMethod(eventName, new Type[] { typeof(object) });
            if (method == null) return null;

            return new Action<object>(o =>
            {
                method.Invoke(viewmodel, new object[] { o });
            });
        }

        public bool ExistsEvent(string eventName)
        {
            return eventList.ContainsKey(eventName);
        }
    }
}

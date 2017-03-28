/*
 * Author:durow
 * event bus.
 * Date:2017.03.28
 */

using System;
using System.Collections.Generic;

namespace Ayx.AvalonSword
{
    public class EventBus : IEventBus
    {
        private Dictionary<string, Action<object>> eventList = new Dictionary<string, Action<object>>();

        public int Count
        {
            get { return eventList.Count; }
        }

        public Action<object> this[object key]
        {
            get
            {
                return eventList[key.ToString()];
            }
            set
            {
                eventList[key.ToString()] = value;
            }
        }

        public void Emit(object eventName, object parameter)
        {
            if (!ContainsEvent(eventName)) return;
            eventList[eventName.ToString()](parameter);
        }

        public void On(object eventName, Action<object> action)
        {
            if (ContainsEvent(eventName))
                throw new Exception($"event {eventName} has been registed!");

            eventList[eventName.ToString()] = action;
        }

        public bool ContainsEvent(object eventName)
        {
            return eventList.ContainsKey(eventName.ToString());
        }

        public bool RemoveEvent(object eventName)
        {
            if (!ContainsEvent(eventName))
                return false;

            eventList.Remove(eventName.ToString());
            return true;
        }
    }
}

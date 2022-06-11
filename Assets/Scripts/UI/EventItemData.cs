using System;

namespace UI
{
    public class EventItemData
    {
        public EventItemData(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; private set; }
    }
}
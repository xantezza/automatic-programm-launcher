using System;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class EventItemData
    {
        public EventItemData(string eventName)
        {
            EventName = eventName;
            EventId = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        [field:SerializeField] public string EventName { get; private set; }
        [field:SerializeField] public long EventId { get; private set; }
    }
}
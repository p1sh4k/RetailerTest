using System;
using System.Collections.Generic;
using System.Text;

namespace EmitEvent
{
    public interface IEventLog
    {
        void EventPublish(string eventName, string message);
    }
}

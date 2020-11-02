using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MessageBroker
{
    public interface ISubscriber
    {
        void StartConsuming();
    }
}

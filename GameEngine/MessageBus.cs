using Redbus;
using Redbus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class MessageBus
    {
        public readonly static IEventBus Instantce = new EventBus();
    }
}

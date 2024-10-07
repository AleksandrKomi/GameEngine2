using GameEngine.Characters;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Messages
{
    public class OpponentDiedMessage : EventBase
    {
        // Nothing here
        public OpponentDiedMessage(Opponent opponent)
        {
           Opponent = opponent;
        }

        public Opponent Opponent { get; }
    }
}

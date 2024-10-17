using GameEngine.Characters;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Messages
{
    public class OpponentShootsMessage : EventBase
    {
        // Nothing here
        public OpponentShootsMessage(Opponent opponent)
        {
            Opponent = opponent;
            
        }
        public Opponent Opponent { get; }
    }
}

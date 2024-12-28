using GameEngine.Characters;
using GameEngine.Interfeces;
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
        public OpponentShootsMessage(IPositionable opponent )
        {
            Opponent = opponent;
        }
        public IPositionable Opponent { get; }
    }
}

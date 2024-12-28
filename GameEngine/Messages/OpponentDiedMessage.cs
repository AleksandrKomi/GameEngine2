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
    public class OpponentDiedMessage : EventBase
    {
        // Nothing here
        public OpponentDiedMessage(ISprite opponent)
        {
           Opponent = opponent;
        }

        public ISprite Opponent { get; }
    }
}

using GameEngine.Characters;
using Redbus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Messages
{
    public class PlayerDiedMessage : EventBase
    {
        // Nothing here
        public PlayerDiedMessage(Player player)
        {
            Player = player;
        }

        public  Player  Player { get; }

    }
}

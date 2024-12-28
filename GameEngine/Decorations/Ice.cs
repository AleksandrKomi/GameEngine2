using GameEngine.Characters;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Ice : BaseFire
    {

        public Ice(IPositionable shooter) : base(shooter, 
           texturePath: shooter.Direction == Direction.Right ? "Resources/Ice.png" : "Resources/IceLeft.png") 
        {
            // Nothing here              
        }

        protected override int FireSize => 30;

        public override void ProcessPhysics()
        {
           base.ProcessPhysics();
        }
    }

}

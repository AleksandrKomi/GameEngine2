using GameEngine.Characters;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Bomb : BaseFire
    {
        private readonly IPositionable _shooter;
        private readonly IPositionable _target;
        private readonly Direction _direction;

        protected override int FireSize => 40;

        public Bomb(IPositionable shooter, IPositionable target) : base(shooter,
           texturePath: target.X < shooter.X ? "Resources/Bomb.png" : "Resources/BombRight.png")
        {
           _shooter = shooter;
            _target = target;
            _direction = target.X < shooter.X
               ? Direction.Left
               : Direction.Right;
        }              
       
        public override void ProcessPhysics()
        {
            PhysicsBomb();
        }

        public void PhysicsBomb()
        {

            if (_direction == Direction.Left)  
            {
                X -= 4;
            }
            else
            {
                X += 4;
            }

            if (_shooter.Y < _target.Y)
            {
                Y += 1;
            }
            else if (_shooter.Y > _target.Y)
            {
                Y -= 1;
            }

        }
                
    }
}

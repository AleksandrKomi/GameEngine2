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
    public class Bullet : BaseFire
    {
            
        private readonly IPositionable _shooter;
        private readonly IPositionable _target;
        private readonly Direction _direction;

        protected override int FireSize => 30;

        public Bullet(IPositionable shooter, IPositionable target) : base(shooter,
           texturePath: target.X < shooter.X ? "Resources/bulletLeft.png" : "Resources/bullet.png")
        {
           _shooter = shooter;
            _target = target;
            _direction = target.X < shooter.X 
                ? Direction.Left 
                : Direction.Right;
        }                 
       
        public override void ProcessPhysics()
        {
            PhysicsBullet();
        }

        public void PhysicsBullet()
        {

            if (_direction == Direction.Left)  
            {
                X -= 4;
            }
            else
            {
                X += 4;             
            }

            if (Y < _target.Y)
            {
                Y += 1;
            }
            else if (Y > _target.Y)
            {
                Y -= 1;
            }

        }
                
    }
}

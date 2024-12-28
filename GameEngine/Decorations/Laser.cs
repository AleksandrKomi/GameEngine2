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
    public class Laser : BaseFire
    {
        private readonly IPositionable _shooter;
        private readonly IPositionable _target;
        private readonly Direction _direction;

        public Laser(IPositionable shooter, IPositionable target) : base(shooter, texturePath: "Resources/laser.png")
        {
            _shooter = shooter;
            _target = target;
            _direction = target.X < shooter.X
                ? Direction.Left
                : Direction.Right;
        }

        protected override int FireSize => 40;

        public override void ProcessPhysics()
        {
            PhysicsLaser();
        }

        public void PhysicsLaser()
        {

            if (_direction == Direction.Left)
            {
                X -= 6;
            }
            else
            {
                X += 6;
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

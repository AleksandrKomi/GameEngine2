using GameEngine.Decorations;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public class RobotOpponent : BaseOpponent
    {
        public RobotOpponent(int xp) : base(xp, "Resources/robot.png", "Resources/robotLeft.png")
        {
            // Nothing here...
        }

        public override int OpponentSize => 120;
        private DateTime lastShoot = DateTime.MinValue;


        protected override void Jump()
        {

            if (CurrentDirection == Direction.Left)
            {
                Y += 0;
                X -= 0;
            }
            else if (CurrentDirection == Direction.Right)
            {
                Y += 0;
                X += 0;
            }
        }

        public virtual void ProcessPhysics()
        {
            // Вызывается 100 раз в сек.

            if (Y > Land.BlockSize)
            {
                Y -= 5;
            }

            if (Y < Land.BlockSize + 45 && _xp >= 0)
            {
                Travel();

                if (X < 300)
                {
                    CurrentDirection = Direction.Right;
                }
                else if (X > 750)
                {
                    CurrentDirection = Direction.Left;
                }
                               
                if (DateTime.Now - lastShoot > TimeSpan.FromSeconds(1))
                {
                    Shoot();
                    lastShoot = DateTime.Now;
                }

            }
        }


    }
}

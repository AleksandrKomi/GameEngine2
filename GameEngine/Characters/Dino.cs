using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public class Dino : BaseOpponent, ICollider
    {
        public Dino(int xp) : base(xp, "Resources/Dino.png", "Resources/DinoLeft.png")
        {
            // Nothing here...
        }

        public override int OpponentSize => 100;

        public bool IsDisappearsOnCollision => false;

        public override void ProcessPhysics()
        {
            // Вызывается 100 раз в сек.

            if (Y > Land.BlockSize)
            {
                Y -= 5;
            }

            if (Y < Land.BlockSize + 45 && _xp >= 0)
            {
                Travel();

                if (X < 30)
                {
                    CurrentDirection = Direction.Right;
                }
                else if (X > 750)
                {
                    CurrentDirection = Direction.Left;
                }           
            }
        }

        protected override void Travel()
        {

            if (CurrentDirection == Direction.Left)
            {
                _currentDirectionOpponentImage = OpponentLeftImage;

                X -= 2;
            }
            else if (CurrentDirection == Direction.Right)
            {
                X += 2;
                _currentDirectionOpponentImage = OpponentImage;
            }
        }
        public void OnCollision(ICollider collider)
        {
            if (collider is Fire)
            {
                _xp -= 25;
            }
            else if (collider is Ice)
            {
                _xp -= 13;
            }
           
            if (_xp <= 0)
            {
                MessageBus.Instantce.Publish(new OpponentDiedMessage(this));
            }
        }

    }
}

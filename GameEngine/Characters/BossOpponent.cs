using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
     public class BossOpponent : IPhysicalSprite, ICollidable
     {
        public const int BossOpponentSize = 90;
        private int _xp;

        public int X { get; private set; } = 500;
        public int Y { get; private set; } = 600;

        public int Height => BossOpponentSize;
        public int Width => BossOpponentSize;

        private Direction _currentDirection = Direction.Left;
        private Image _BossOpponent;
        private Image _BossOpponentLeft;
        private Image _BossOpponentRight;
        private DateTime lastJump = DateTime.MinValue;
        private DateTime lastShoot = DateTime.MinValue;
        private string text;
        private Counter _counter;
        private Player _player;
        private readonly Bullet _bullet;

        public BossOpponent Bossopponent;

         public BossOpponent(int xp, Counter count, Player player)
         {
             // Load resources
            _xp = 100;
            _counter = count;
            _player = player;
            _player._xp = 50;
            _BossOpponent = Image.FromFile("Resources/Boss.png");
            _BossOpponentLeft = Image.FromFile("Resources/BossLeft.png");
            _BossOpponentRight = _BossOpponent;

         }

        public bool CanCollide(ICollider collider)
        {
             return collider is Fire || collider is Ice;
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            throw new NotImplementedException();
        }

        public void OnCollision(ICollider collider)
        {
            if (collider is Fire)
            {
                _xp -= 12;
            }
            else if (collider is Ice)
            {
                _xp -= 7;
            }

             if (_xp > 0)
             {
               Shoot();
             }

            if (_xp <= 0)
            {
               // MessageBus.Instantce.Publish(new OpponentDiedMessage(this));
            }
        }

        public void ProcessPhysics()
        {
          throw new NotImplementedException();
        }

        private void Shoot()
        {
           // MessageBus.Instantce.Publish(new OpponentShootsMessage(this));

        }
     }
}
